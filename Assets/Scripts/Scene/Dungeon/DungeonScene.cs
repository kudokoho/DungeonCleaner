using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : MonoBehaviour
{
    [SerializeField] CharacterBase player_ = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (player_ == null) return;

       // player_.MoveFront();

        counter++;


        if(counter == 300)
        {
            //player_.Jump();
            counter = 0;
        }

    }
}
