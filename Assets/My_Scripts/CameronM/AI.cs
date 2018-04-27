using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    [SerializeField]
    public Animator MyAnimator { get; private set; }

    [SerializeField]
    public Rigidbody rb;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float walkSpeed;

    [SerializeField]
    protected float turnSpeed;

    public virtual void Start()
    {
        MyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
}
