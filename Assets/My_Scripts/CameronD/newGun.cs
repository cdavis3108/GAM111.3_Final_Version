
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class newGun : MonoBehaviour {

    public float damage = 10.0f;
    public float range = 100.0f;
    public float fireRate = 0.25f;

    public GameObject gunbarrel;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Image crossHairs;

    private Camera fpsCam;
    private WaitForSeconds shotduration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private float nextFire;

    private void Start()
    {
        gunAudio = GetComponent<AudioSource>();

        fpsCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Shoot();
        }

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
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
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.1f);
        }
    }

    private IEnumerator ShotEffect()
    {
        muzzleFlash.Play();

        gunAudio.Play();

        yield return shotduration;
    }
}
