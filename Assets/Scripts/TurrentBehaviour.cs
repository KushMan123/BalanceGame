using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBehaviour : MonoBehaviour
{
    [Header("Attributes")]
    public float shootingRange=1.5f;
    public float detectRange = 7f;
    public float turnSpeed = 10f;
    public float fireCountdown = 3f;

    [Header("Essentials")]
    public SphereCollider sphereCollider;
    public Transform mainBody;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;


    private void Start()
    {
        sphereCollider.radius = detectRange;
    }

    private void Update()
    {
        //If the target(player has not entered) then do nothing
        if (target == null) {
            return;        
        }

        //If the target player has entered range then rotate
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(mainBody.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        mainBody.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //If the player is in shooting range then shooting Bullet after every 3secs
        float distanceFromTarget = Vector3.Distance(transform.position, target.position); 

        if(distanceFromTarget <= shootingRange)
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 3f;
            }
            fireCountdown -= Time.deltaTime;
        }
        
    }

    private void Shoot()
    {
        GameObject bulletPb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float bulletSpeed = bulletPb.GetComponent<Bullet>().bulletSpeed;
        bulletPb.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
}
