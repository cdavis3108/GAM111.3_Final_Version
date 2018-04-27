
using UnityEngine;

public class newGun : MonoBehaviour {

    public float damage = 10.0f;
    public float range = 100.0f;

    public GameObject gunbarrel;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect; 
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            GetComponent<AudioSource>().Play();
        }
	}

    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(gunbarrel.transform.position, gunbarrel.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();


            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.1f);
        }
    }
}
