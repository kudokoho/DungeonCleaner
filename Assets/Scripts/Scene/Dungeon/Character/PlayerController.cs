using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] CharacterBase player_ = null;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			player_.Jump();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			player_.MoveBack();
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			player_.MoveFront();
		}
	}
}
