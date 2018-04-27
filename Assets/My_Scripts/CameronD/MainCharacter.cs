using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpSpeed = 100.0f;
    public Rigidbody rb;
    public bool jumping;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        // On right mouse click...
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            // Draw ray from camera to mouse position
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,100f))
            {
                //set the NavMeshAgents destination.
                agent.destination = hit.point;
            }
        }

        if(Input.GetKeyDown("escape"))
            SceneManager.LoadScene(0);
    }
}