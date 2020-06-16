using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    [SerializeField] Slider gauge_ = null;
    float current_hp_ = 0;
    float max_hp_ = 100;

    [SerializeField] Vector3 position_adjust_ = new Vector3(0, 1, 0);

    Transform fllowing_transform_ = null;
    public Transform FllowingTransform
    {
        get { return fllowing_transform_; }
        set { fllowing_transform_ = value; }
    }

    public float CurrentHp
    {
        get { return current_hp_; }
    }

    public float MaxHp
    {
        set { max_hp_ = value; current_hp_ = value; }
        get { return max_hp_; }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fllowing_transform_ == null) return;
        transform.position = fllowing_transform_.position + position_adjust_;
    }

    /// <summary>
    /// HPの更新
    /// </summary>
    /// <param name="value"></param>
    public void UpdateHP(float value)
    {
        current_hp_ = value;
        if (max_hp_ < current_hp_) current_hp_ = max_hp_;
        if (0 > current_hp_) current_hp_ = 0;

        GaugeUpdate();
    }

    // ゲージの表示の更新
    void GaugeUpdate()
    {
        gauge_.value = current_hp_ / max_hp_ * 100;
    }
}
