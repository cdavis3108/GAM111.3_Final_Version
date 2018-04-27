using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float FireRate = 5f;

    public Camera fpsCam;

    public Rigidbody Bullet;
    public Transform Barrelend;

    private float nextTimetoShoot = 0.5f;

   
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            nextTimetoShoot = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Rigidbody BulletInStage;
        BulletInStage = Instantiate(Bullet, Barrelend.position, Barrelend.rotation) as Rigidbody;
        BulletInStage.AddForce(Barrelend.forward * 5000);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

        }

    }
}
