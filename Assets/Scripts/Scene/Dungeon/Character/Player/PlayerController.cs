using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] CharacterBase player_ = null;

	[SerializeField] FloatingJoystick floatingJoystick_;

	float tap_seconds_ = 0.0f;
	float attack_judgs_econds_ = 0.2f;

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
		if (Input.GetMouseButton(0))
		{
			tap_seconds_ += Time.deltaTime;
		} else {
			// タップを離した時間が一定以下なら攻撃
			if(tap_seconds_> 0 && tap_seconds_ < attack_judgs_econds_)
			{
				player_.Attack();
			}
			tap_seconds_ = 0.0f;
		}

		if (tap_seconds_ > attack_judgs_econds_ && floatingJoystick_.Horizontal < 0)
		{
			player_.MoveBack();
		}
		if (tap_seconds_ > attack_judgs_econds_ && floatingJoystick_.Horizontal > 0)
		{
			player_.MoveFront();
		}
		if (tap_seconds_ > attack_judgs_econds_ && floatingJoystick_.Vertical > 0.5)
		{
			player_.Jump();
		}
	}

}
