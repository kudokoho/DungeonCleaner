using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus
{
    float power_ = 10;
    float hp_ = 100;
    float speed_ = 0.8f;

    public float Power
    {
        get { return power_; }
        set { power_ = value; }
    }

    public float HP
    {
        get { return hp_; }
        set { hp_ = value; }
    }

    public float Speed
    {
        get { return speed_; }
        set { speed_ = value; }
    }
}
