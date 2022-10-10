using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareBall : MonoBehaviour
{
    public Material ballMaterial;

    void Start()
    {
        GetComponent<Renderer>().material = ballMaterial;
    }
}
