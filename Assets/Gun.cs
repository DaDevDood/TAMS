using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using EZCameraShake;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Shooting stuff")]
    public float offset;
    public Transform shotPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 10f;
    private float nextFireTime = 0f;
    //public ParticleSystem ShootEffect;
    //public TextMeshProUGUI ClipSize;
    bool isReloading;

    //[SerializeField] private AudioSource gunShoot;
    //[SerializeField] private AudioSource gunReload;
    //[SerializeField] private AudioSource gunDryShoot;

    private GameObject player;
    [Header("Ammo stuff")]
    public int currentClip;//, currentAmmo, maxClipSize = 10;
    public GameObject ReloadPrompt;

    Vector3 gunOffset = new Vector3(1.73300004f, -0.0299999993f, 0);
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = player.transform.position + gunOffset;
        //ClipSize.text = "" + currentClip;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentClip > 0)
        {
            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
            //CameraShaker.Instance.ShakeOnce(.1f, 1f, .1f, .2f);
            //gunShoot.Play();
            //ShootEffect.Play();
            
        }
        else if(Input.GetMouseButton(0) && (Time.time>=nextFireTime||Time.time<=nextFireTime) && currentClip == 0)
        {
            StartCoroutine(Reload());
            //gunDryShoot.Play();
            
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, transform.rotation);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        currentClip--;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("reloading...)");
        yield return new WaitForSeconds(1);
        currentClip = 40;
        isReloading = false;
    }
}
