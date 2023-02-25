using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool lerp = false;

    // Update is called once per frame
    void Update()
    {
        if (!lerp)
        {
            transform.position = target.position + offset;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 2f*Time.deltaTime);
        }
    }

    public void setLerp(bool value)
    {
        lerp = value;
    }
}
