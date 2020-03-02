using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected float speed_ = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(bool back = false)
    {
        Vector3 pos = transform.position;
        if (back)
        {
            pos.x -= speed_;
        } else
        {
            pos.x += speed_;
        }
        
        transform.position = pos;
    }
}
