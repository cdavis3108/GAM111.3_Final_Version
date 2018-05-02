using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMedium : AI {

    private IEnemyState currentState;
    public FieldOfView fieldOfView;

    public GameObject targetAcquired;

    public bool patrols;
    public bool ranged;
    public bool coward;

    public bool moving;

    public Transform weaponEnd;

    public float damage;
    public ParticleSystem muzzleFlash;
    public ParticleSystem meleeAttackFlash;
    public GameObject impactEffect;
    public AudioSource gunFire;
    public AudioSource meleeAttack;

    public float impactForce = 30f;
    public float attackDelay = 15f;
    public float attackTimer = 0f;
    public float meleeRange = 5f;

    public GameObject path;
    public Transform[] pathnodes;
    private int destPoint = 0;
    public NavMeshAgent agent;

    public bool idling;
    public DifficultySetting getdifficultysetting;
    public GameObject difficultySetter;
    public Light spotlight;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        fieldOfView = GetComponent<FieldOfView>();
        //spotlight = GetComponentInChildren<Light>();
        
        if (path != null)
        pathnodes = path.GetComponentsInChildren<Transform>();

        transform.rotation = Random.rotation;

        if (GameObject.FindGameObjectWithTag("DifficultySetting") != null)
            difficultySetter = GameObject.FindGameObjectWithTag("DifficultySetting");

        if (difficultySetter != null)
        {
            getdifficultysetting = difficultySetter.GetComponent<DifficultySetting>();

            damage = getdifficultysetting.damage;
        }
        else
        {
            damage = 10f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (fieldOfView.visibleTargets.Count <= 0)
        {
            targetAcquired = null;
        }
        else
        {
            targetAcquired = fieldOfView.visibleTargets[0];
            if (!ranged && !coward)
                agent.destination = targetAcquired.transform.position;
        }

        currentState.Execute();

        if(targetAcquired != null)
        {
            LookAtTarget();
        } else
        {
            rb.angularVelocity = new Vector3(0,0,0);
        }

        if (!ranged && !coward)
        {
            if (idling)
            {
                agent.destination = this.transform.position;
            }
        }
	}

    private void FixedUpdate()
    {
        if (!ranged && !coward)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (!idling)
                    GotoNextPoint();
            }
        } else
        {
            if (moving)
            {
                Move();
            }
            else
            {
                rb.velocity = new Vector3(0,0,0);
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (targetAcquired != null)
        {
            if (coward)
            {
                rb.velocity = Vector3.back * moveSpeed;
            }
        }
        else
        {
            //rb.AddRelativeForce(Vector3.forward * walkSpeed, ForceMode.Force);
        }
    }

    public Vector3 GetDirection()
    {
        //issue is here somewhere
        return (targetAcquired.transform.position - transform.position);
    }
    
    private void LookAtTarget ()
    {
        if (targetAcquired != null)
        {
            //Look at target
            Vector3 targetDir = targetAcquired.transform.position - transform.position;
            targetDir.y = 0.0f;
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public void RangedAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(weaponEnd.position, weaponEnd.forward,out hit, 50f))
        {
            if (hit.collider.tag == "AI")
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.1f);
                muzzleFlash.Play();
                gunFire.Play();
            }
            else if (hit.collider.tag == "Player")
            {

                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    //hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.1f);
                muzzleFlash.Play();
                gunFire.Play();
            } else
            {

            }
        }
    }

    public void MeleeAttack()
    {
        meleeAttackFlash.Play();
        meleeAttack.Play();
        RaycastHit hit;
        if (Physics.Raycast(weaponEnd.position, weaponEnd.forward, out hit, 1f))
        {
            if(hit.collider.tag == "AI")
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                //This can be deleted once there is a melee attack effect
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            } else if (hit.collider.tag == "Player")
            { 
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.1f);
            } else
            {

            }
        }
    }

    public bool InMeleeRange()
    {
        bool inMeleeRange;
        float distance = Vector3.Distance(transform.position, targetAcquired.transform.position);
        if (distance <= meleeRange)
        {
            inMeleeRange = true;
        } else
        {
            inMeleeRange = false;
        }
        return inMeleeRange;
    }

    void GotoNextPoint()
    {
        if (pathnodes.Length < 1)
            return;

        if (targetAcquired == null && path != null)
        {
            agent.destination = pathnodes[destPoint].position;

            if (destPoint >= pathnodes.Length)
            {
                destPoint = 0;
            }
            else
            {
                destPoint = (destPoint + 1) % pathnodes.Length;
            }
        }
    }
}
