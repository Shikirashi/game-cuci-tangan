using UnityEngine;
using System.Collections;

public class EnemyRaycast : MonoBehaviour {
    //public int damage = 10;
    public float range = 100f;
    public float damageLow;
    public float damageHigh;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float nonAccuracy = 5f;
    public static int ammoCountTxt;
    public static int maxAmmoCountTxt;
    //public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject flashEffect;
    public Transform muzzleFlasher;
    public AudioClip audioMan;

    //public Animator animator;
    //public Animation animations;

    public int maxAmmo = 10;
    public float reloadTime = 1f;

    public GameObject fpsCam;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;
    private bool isShooting = false;

    WeaponSwitching WeaponName;

    void Start() {
        currentAmmo = maxAmmo;
    }

    private void OnEnable() {
        isReloading = false;
        //animator.SetBool("reloading", false);
    }

    void Update() {
        ammoCountTxt = currentAmmo;
        maxAmmoCountTxt = maxAmmo;
        if (isReloading) {
            return;
        }
        if (isShooting) {
            return;
        }
        if (currentAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }
        RaycastHit hitPlayerInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitPlayerInfo, range) && Time.time >= nextTimeToFire) {
            if(hitPlayerInfo.collider.CompareTag("Player") && GetComponentInParent<EnemyMovement>().State == EnemyMovement.StateEnum.SHOOT){
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        //muzzleFlash.Play();
        GameObject muzzleFlash = Instantiate(flashEffect, muzzleFlasher.transform.position, Quaternion.LookRotation(muzzleFlasher.transform.forward));
        Destroy(muzzleFlash, 1f);
        currentAmmo--;
        AudioSource.PlayClipAtPoint(audioMan, muzzleFlasher.transform.position);
        Vector3 aimDeviation = new Vector3(Random.Range(-nonAccuracy, nonAccuracy), Random.Range(-nonAccuracy, nonAccuracy), Random.Range(-nonAccuracy, nonAccuracy));
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position + aimDeviation, fpsCam.transform.forward, out hitInfo, range)) {
            Debug.Log(hitInfo.transform.name);
            PlayerStats player = hitInfo.transform.GetComponent<PlayerStats>();
            if (player != null) {
                player.TakeDamage((int)Random.Range(damageLow, damageHigh));
            }

            if (hitInfo.rigidbody != null) {
                hitInfo.rigidbody.AddForce(fpsCam.transform.forward * impactForce);
            }

            GameObject impact = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impact, 1f);
        }
    }

    IEnumerator Reload() {
        isReloading = true;
        Debug.Log("EnemyReloading");

        //animator.SetBool("reloading", true);

        yield return new WaitForSeconds(reloadTime);

        //animator.SetBool("reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    /*IEnumerator Shooting(){
        isShooting = true;
        Debug.Log("SHOOTING");

        animator.SetBool("shooting", true);

        yield return new WaitForSeconds(1f/fireRate);

        animator.SetBool("shooting", false);

        isShooting = false;
    }*/
}
