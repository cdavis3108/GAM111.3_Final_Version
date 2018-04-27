using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    Animator characterAnimation;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {

        characterAnimation = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rb.velocity.x > 0 || rb.velocity.y > 0 || rb.velocity.z > 0)
        {
            characterAnimation.SetTrigger("Walk");
        }
        else
        {
            characterAnimation.SetTrigger("Idle");
        }

        /*
        if (Input.GetKeyDown(KeyCode.W))    
        {
            characterAnimation.SetTrigger("Walk");
        }
		
        if (Input.GetKeyUp(KeyCode.W))
        {
            characterAnimation.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            characterAnimation.SetTrigger("Walk");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            characterAnimation.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            characterAnimation.SetTrigger("Walk");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            characterAnimation.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            characterAnimation.SetTrigger("Walk");
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            characterAnimation.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterAnimation.SetTrigger("Jump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            characterAnimation.SetTrigger("Idle");
        }
        */
	}
}
