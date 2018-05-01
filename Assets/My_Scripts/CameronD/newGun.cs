
using UnityEngine;
using UnityEngine.UI;

public class newGun : MonoBehaviour {

    public float damage = 10.0f;
    public float range = 100.0f;

    public GameObject gunbarrel;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Image crossHairs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            GetComponent<AudioSource>().Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(gunbarrel.transform.position, gunbarrel.transform.forward, out hit, range))
        {
            crossHairs.color = new Color(1, 1, 1, 0.16f);
        }
        else
        {
            crossHairs.color = new Color(1, 0, 0, 0.16f);
        }
    }

    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(gunbarrel.transform.position, gunbarrel.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

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
