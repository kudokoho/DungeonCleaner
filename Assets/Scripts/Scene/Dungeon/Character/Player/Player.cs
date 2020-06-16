using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{


    // Start is called before the first frame update
    override protected void StartChild()
    {
    }

    // Update is called once per frame
    override protected  void UpdateChild()
    {
        if (action_list_.Count <= 0)
        {
            animator_.SetBool("walk", false);
            animator_.SetBool("attack", false);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision.gameObject.tag = " + collision.gameObject.tag);
        // 着地
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy")
        {
            Debug.Log("test");

            if (IsInvincible) return;// 無敵の場合は処理しない
            if (state == StateType.Attack) return;
            // ダメージ処理
            Debug.Log("敵にぶつかった");

            Damage(collision.gameObject.GetComponent<CharacterBase>().Power);
            KnockBack();
        }

        base.OnCollisionEnter(collision);
    }



}
