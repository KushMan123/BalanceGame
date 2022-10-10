using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    [Header("Wind Area Attributes")]
    public float strength;
    public Vector3 direction;
    public Transform windHeight;
    [Header("Rotation Attributes")]
    public GameObject partToRotate;
    public float rotationSpeed;

    private void Update()
    {
        partToRotate.transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }
}
