using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBehaviour : MonoBehaviour
{
    public Transform target;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public SphereCollider sphereCollider;
    public float triggerRadius = 5f;
    public float bulletSpeed = 50f;
    private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 2f;
     private bool isActive = false;
    [SerializeField] private bool isDisabled = false;


    private void Start()
    {
        sphereCollider.radius = triggerRadius;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive && !isDisabled)
        {
            transform.LookAt(target.position);
            StartCoroutine(ShootCharging());
        }
    }

    void Shoot()
    {
        if(countdownBetweenFire <= 0)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            countdownBetweenFire = 1f / fireRate;
        }
        countdownBetweenFire -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
        }
    }

    private IEnumerator ShootCharging()
    {
        yield return new WaitForSeconds(3);
        Shoot();
    }
}
