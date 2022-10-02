using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBehaviour : MonoBehaviour
{
    public Transform target;
    public SphereCollider sphereCollider;
    public float triggerRadius = 5f;
    [SerializeField] private bool isActive = false;


    private void Start()
    {
        sphereCollider.radius = triggerRadius;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.LookAt(target.position);
        }
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
}
