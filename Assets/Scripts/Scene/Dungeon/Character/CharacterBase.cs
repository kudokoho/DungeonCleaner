using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    [SerializeField] protected float hp_ = 0;
    [SerializeField] protected float power_ = 0;
    [SerializeField] protected float speed_ = 0;

    protected Rigidbody rigidbody_ = null;
    protected Animator animator_ = null;
    protected Vector3 jump_vec_ = new Vector3(0, 5, 0);

    protected bool is_ground_ = false;
    protected bool is_foward_ = true;
    protected bool move_control_enable_ = true;

    protected float knockback_input_invalid_time_ = 1.0f;
    protected float knockback_after_input_invalid_time_ = 1.0f;
    protected float knockback_input_invalid_time_count_ = 0.0f;
    protected bool is_nockback = false;

    protected string walk_animator_paramater_ = "walk";
    protected string attack_animator_paramater_ = "attack";

    protected StateType state = StateType.Wait;

    protected float collision_after_time_ = 0.0f;


    public enum StateType
    {
        Wait,
        AttackReady,
        Attack,
        Dead,
        CollisionAfter
    }

    protected List<System.Action> action_list_ = new List<System.Action>();

    [SerializeField] protected HpGauge hp_gauge_ = null;

    /// <summary>
    /// 無敵かどうか
    /// </summary>
    public bool IsInvincible
    {
        get { return !move_control_enable_ || is_nockback; }
    }

    public StateType State
    {
        get { return state; }
    }

    public float HP {
        get { return hp_; }
    }

    public float Power
    {
        get { return power_; }
    }

    public float Speed {
        get { return speed_; }
    }



    // Start is called before the first frame update
    void Start()
    {

        rigidbody_ = GetComponent<Rigidbody>();
        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;

        animator_ = GetComponentInChildren<Animator>();
        if (hp_gauge_ != null)
        {
            hp_gauge_.FllowingTransform = transform;
            hp_gauge_.MaxHp = hp_;
        }



        StartChild();
    }

    virtual protected void StartChild()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ゲームプレイ中以外はキャラクターの動きを更新しない
        if (DungeonInfo.DungeonState != DungeonInfo.State.GamePlay) return;

        //　衝突後は動きを止める
        if (state == StateType.CollisionAfter)
        {
            if (collision_after_time_ <= 0)
            {
                state = StateType.Wait;
            }
            else
            {
                collision_after_time_ -= Time.deltaTime;
            }
            return;
        }

        UpdateChild();

        if (action_list_.Count > 0)
        {
            action_list_[0]();
            action_list_.RemoveAt(0);
        }


        if (is_nockback)
        {
            knockback_input_invalid_time_count_ += Time.deltaTime;
            if (knockback_input_invalid_time_count_ > knockback_input_invalid_time_)
            {
                move_control_enable_ = true;
            }
            if (knockback_input_invalid_time_count_ > knockback_input_invalid_time_ + knockback_after_input_invalid_time_)
            {
                is_nockback = false;
            }
        }

        if (state == StateType.AttackReady)
        {
            AnimatorStateInfo stateInfo = animator_.GetCurrentAnimatorStateInfo(0);
            // 攻撃アニメーションに入ったら状態をAttackに変更
            if (stateInfo.IsTag("Attack"))
            {
                state = StateType.Attack;
            }

        }

        if (state == StateType.Attack)
        {
            AnimatorStateInfo stateInfo = animator_.GetCurrentAnimatorStateInfo(0);
            // 攻撃アニメーションではなければwaitに戻す
            if (!stateInfo.IsTag("Attack"))
            {
                state = StateType.Wait;
            }

        }

        
    }

    virtual protected void UpdateChild()
    {

    }

    /// <summary>
    /// 衝突処理
    /// </summary>
    /// <param name="collision"></param>
    virtual protected void OnCollisionEnter(Collision collision)
    {

        // 着地
        if (collision.gameObject.tag == "Ground")
        {
            is_ground_ = true;
        }

        if(collision.gameObject.tag != "Ground" && gameObject.tag != "Player")
        {
            state = StateType.CollisionAfter;
            collision_after_time_ = 2.0f;
        }

    }

    /// <summary>
    /// ジャンプ
    /// </summary>s
    public void Jump()
    {
        if (!move_control_enable_) return;
        if (!is_ground_) return;
        is_ground_ = false;
        action_list_.Add(JumpAction);
        
    }

    void JumpAction()
    {
        if (!move_control_enable_) return;
        if (rigidbody_ == null) return;
        rigidbody_.velocity = jump_vec_;
    }

    /// <summary>
    /// 移動(前進)
    /// </summary>
    public void MoveFront()
    {
        if (!move_control_enable_) return;
        action_list_.Add(MoveFrontAction);
    }

    void MoveFrontAction()
    {
        if (!move_control_enable_) return;
        if (walk_animator_paramater_ != "") animator_.SetBool(walk_animator_paramater_, true);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Vector3 pos = transform.position;
        pos.z += speed_;
        transform.position = pos;
        is_foward_ = true;
    }

    /// <summary>
    /// 移動(後退)
    /// </summary>
    public void MoveBack()
    {
        if (!move_control_enable_) return;
        action_list_.Add(MoveBackAction);
    }


    void MoveBackAction()
    {
        if (!move_control_enable_) return;
        if (walk_animator_paramater_ != "") animator_.SetBool(walk_animator_paramater_, true);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        Vector3 pos = transform.position;
        pos.z -= speed_;
        transform.position = pos;
        is_foward_ = false;
    }

    public void Attack()
    {
        if (!move_control_enable_) return;
        action_list_.Add(AttackAction);
    }

    void AttackAction()
    {
        if (!move_control_enable_) return;
        animator_.SetBool("attack", true);
        state = StateType.AttackReady;
        //GetComponent<CapsuleCollider>().enabled = true;
    }

    virtual protected void Damage(float value)
    {
        hp_ = hp_ - value;
        hp_gauge_.UpdateHP(hp_);
        if (hp_gauge_.CurrentHp <= 0)
        {
            // 死亡処理
            Dead();
        }
    }

    void Dead()
    {
        state = StateType.Dead;
        animator_.SetBool("Dead", true);
        // 倒れるアニメーション再生
    }

    void Delete()
    {
        Destroy(gameObject);
        Destroy(hp_gauge_.gameObject);
    }

    /// <summary>
    /// ぶつかって後ずさる
    /// </summary>
    public void KnockBack()
    {
        if (is_nockback) return;
        is_nockback = true;
        knockback_input_invalid_time_count_ = 0.0f;
        move_control_enable_ = false;

        Vector3 force = new Vector3(0, 0, is_foward_ == true ? -2 : 2);
        rigidbody_.AddForce(force, ForceMode.Impulse);

        // なんかcolorがうまく取得できないからコメントアウト
        //StartCoroutine("ColorCoroutine");
    }
}
