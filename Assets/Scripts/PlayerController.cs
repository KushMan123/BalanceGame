using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable Initialization
    public float movementSpeed = 50f;

    public Rigidbody rigidBody;
    private bool canMove = true;
    private bool inWindZone = false;
    private float horizontalAxis;
    private float verticalAxis;
    private WindArea windZone;

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        WindMovement();
    }

    void Movement()
    {
        //Movement of the Player
        if (canMove)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            rigidBody.AddForce(new Vector3(horizontalAxis, 0f, verticalAxis) * movementSpeed);
        }
    }

    void WindMovement()
    {
        //Movement of the Ball in the WindZone
        if (inWindZone && GetComponent<PlayerState>().getState()==State.Newspaper)
        {
            rigidBody.AddForce(windZone.direction * windZone.strength);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WindZone"))
        {
            windZone = other.gameObject.GetComponent<WindArea>();
            inWindZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WindZone"))
        {
            windZone = null;
            inWindZone = false;
        }
    }

    public void SetCanMove( bool value)
    {
        canMove = value;
    }

    public IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(2);
        SetCanMove(true);
    }
}
