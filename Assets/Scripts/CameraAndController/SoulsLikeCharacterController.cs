﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsLikeCharacterController : MonoBehaviour {
    /* I'll get around to working this out to what is needed when there is actually a reason too, 
     * I just copied it from an old project. Ignore it for now, I'll fix it myself */
     /* add slow down when rotating later, just steal it off the third person script */
     [SerializeField]
    float moveSpeed;
   
    [SerializeField]
    Animator anim;
    [SerializeField]
    CharacterController player;
    Vector3 moveDirection;
    public bool lockedOnTarget = false; //if locked on target, we no longer rotate when the camera turns. 
    public Transform lockOnTarget;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = Vector3.zero;
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 targetDirection = h * right + v * forward;

       // float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
        //moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, 0.4f);
        // Debug.Log(targetSpeed);
        if (targetDirection != Vector3.zero)
        {
            moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 535 * Mathf.Deg2Rad * Time.deltaTime, 1000);

            moveDirection = moveDirection.normalized;

        }

        Vector3 movement = moveDirection * moveSpeed;
        if (movement != Vector3.zero)
        {
            if (!lockedOnTarget)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
            else {
                //if locked, just face the target.
                transform.LookAt(lockOnTarget);
            }
            // Do the rotation here
        }


        movement *= Time.deltaTime;
        if (movement != Vector3.zero)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        player.Move(movement * moveSpeed);
  
    }
}
