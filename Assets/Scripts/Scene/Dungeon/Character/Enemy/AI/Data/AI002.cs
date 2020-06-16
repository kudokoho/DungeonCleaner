using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI002 : AIBase
{

    protected override void StartChild()
    {
        move_state = MoveState.Front;
    }

    override protected void UpdateChild()
    {
        if(move_state == MoveState.Front && transform.position.z > start_position_.z + 2)
        {
            move_state = MoveState.Back;
        }

        if (move_state == MoveState.Back && transform.position.z < start_position_.z - 2)
        {
            move_state = MoveState.Front;
        }

        if (PlayerPositionDistance() < 2)
        {
            move_state = MoveState.TargetPlayer;
        }

        if (move_state == MoveState.TargetPlayer && PlayerPositionDistance() > 4)
        {
            if (PlayerPositionDiff().z > 0)
            {
                move_state = MoveState.Front;
            } else
            {
                move_state = MoveState.Back;
            }
        }

        if (PlayerPositionDistance() < 0.5)
        {
            Attack();
        }

    }

}
