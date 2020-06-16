using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{

    protected override void StartChild()
    {
        walk_animator_paramater_ = "";
    }

    protected override void OnCollisionEnter(Collision collision)
    {

         // 着地
        if (collision.gameObject.tag == "Wepon" || collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<CharacterBase>().State == CharacterBase.StateType.Attack)
            {
                // ダメージ処理
                Debug.Log("プレーヤーの攻撃を受けた");
                animator_.SetBool("TakeDamage", true);
                Damage(collision.gameObject.GetComponent<CharacterBase>().Power);
                KnockBack();
            }
            
        }

        base.OnCollisionEnter(collision);
    }

}

