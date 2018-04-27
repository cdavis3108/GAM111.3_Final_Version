using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEasy : MonoBehaviour {

    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemyMovementSpeed;
    public float damping;
    public Transform fpsTarget;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        fpsTargetDistance = Vector3.Distance(fpsTarget.position,transform.position);
        if (fpsTargetDistance < enemyLookDistance)
        {
            //play wake up animation
            lookAtPlayer();
            print("Look at the Player Please!");
        }

        if(fpsTargetDistance < attackDistance)
        {
            attackPlease();
            print("Attack!");
        }
	}

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(fpsTarget.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    void attackPlease()
    {
        rb.AddForce(transform.forward * enemyMovementSpeed);
    }
}
