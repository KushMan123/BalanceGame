using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoxBehaviour : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
    }
}
