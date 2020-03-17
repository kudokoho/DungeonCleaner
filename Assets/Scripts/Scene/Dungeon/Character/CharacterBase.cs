using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected float speed_ = 0.05f;
    protected Rigidbody rigidbody_ = null;
    protected Animator animator_ = null;
    protected Vector3 jump_vec_ = new Vector3(0, 5, 0);

    List<System.Action> action_list_ = new List<System.Action>();

    // Start is called before the first frame update
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
        rigidbody_.constraints = RigidbodyConstraints.FreezeRotation;

        animator_ = GetComponentInChildren<Animator>();
        StartChild();
    }

    virtual protected void StartChild()
    {

    }

    int counter = 0;

    // Update is called once per frame
    void Update()
    {
        UpdateChild();

        if (action_list_.Count > 0)
        {
            action_list_[0]();
            action_list_.RemoveAt(0);
        } else
        {
            animator_.SetBool("walk", false);
        }

    }

    virtual protected void UpdateChild()
    {

    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        action_list_.Add(JumpAction);
    }

    void JumpAction()
    {
        //animator_.SetBool("jump", true);
        if (rigidbody_ == null) return;
        rigidbody_.velocity = jump_vec_;
    }

    /// <summary>
    /// 移動(前進)
    /// </summary>
    public void MoveFront()
    {

        action_list_.Add(MoveFrontAction);
    }

    void MoveFrontAction()
    {
        animator_.SetBool("walk", true);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Vector3 pos = transform.position;
        pos.z += speed_;
        transform.position = pos;
    }

    /// <summary>
    /// 移動(後退)
    /// </summary>
    public void MoveBack()
    {
        action_list_.Add(MoveBackAction);
    }


    void MoveBackAction()
    {
        animator_.SetBool("walk", true);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        Vector3 pos = transform.position;
        pos.z -= speed_;
        transform.position = pos;
    }
}
