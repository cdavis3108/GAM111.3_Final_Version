using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
    
    public Animator MyAnimator { get; private set; }
    public GameObject hoverLight;
    public Rigidbody rb;

    public float health = 50f;

    public void Start()
    {
        MyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log(this.tag + " damaged");
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.tag == "AI")
        {
            MyAnimator.SetBool("hasTarget", false);
            MyAnimator.SetBool("powerDown", true);
            rb.constraints = RigidbodyConstraints.None;
            hoverLight.SetActive(false);
            gameObject.GetComponent<AIMedium>().enabled = false;
            gameObject.GetComponent<FieldOfView>().enabled = false;
        }
    }
}
