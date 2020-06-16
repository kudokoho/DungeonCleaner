using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    protected enum MoveState
    {
        Stop, Back, Front, TargetPlayer
    }

    [SerializeField]protected Vector3 start_position_;
    protected CharacterBase character_ = null;
    protected GameObject player_ = null;
    protected Dictionary<string, bool> flg_list_ = new Dictionary<string, bool>();
    protected MoveState move_state = MoveState.Stop;
     


    private void Start()
    {
        character_ = GetComponent<CharacterBase>();
        player_ = GameObject.FindGameObjectWithTag("Player");
        transform.position = start_position_;
        StartChild();
    }
    virtual protected void StartChild() { }

    private void Update()
    {
        if (character_.HP <= 0) return;

        if (move_state == MoveState.Front)
        {
            character_.MoveFront();
        } else if (move_state == MoveState.Back)
        {
            character_.MoveBack();
        } else if(move_state == MoveState.TargetPlayer)
        {
            if (PlayerPositionDiff().z > 0)
            {
                character_.MoveBack();
            } else
            {
                character_.MoveFront();
                
            }
        }
        UpdateChild();
    }
    virtual protected void UpdateChild() { }

    protected void MoveBack()
    {
        move_state = MoveState.Back;
    }

    protected void MoveFront()
    {
        move_state = MoveState.Front;
    }

    protected void MoveStop()
    {
        move_state = MoveState.Stop;
    }

    protected void Attack()
    {
        character_.Attack();
    }

    protected float PlayerPositionDistance()
    {
        Vector3 Apos = transform.position;
        Vector3 Bpos = player_.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);
        return dis;
    }

    protected Vector3 PlayerPositionDiff()
    {
        Vector3 Apos = transform.position;
        Vector3 Bpos = player_.transform.position;
        return Apos-Bpos;
    }
}
