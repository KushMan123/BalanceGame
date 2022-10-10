using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 70f;

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
}

