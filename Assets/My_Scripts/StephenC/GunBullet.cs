using UnityEngine;
using System.Collections;

public class GunBullet : MonoBehaviour {

	public float damage = 10f;
    public float range = 100f;
    public float FireRate = 15f;

    public Camera fpsCam;

    public Rigidbody Bullet;
    public Transform Barrelend;

    private float nextTimetoShoot = 0f;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") )
        {
            //nextTimetoShoot = Time.time + 1f / FireRate;
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

            //WRITE CLEAN UP SCRIPT HERE!
        }
    }

    // https://www.youtube.com/watch?v=blO039OzUZc
    // https://www.youtube.com/watch?v=THnivyG0Mvo
    // https://www.youtube.com/watch?v=4rZAAHevq1s
}
