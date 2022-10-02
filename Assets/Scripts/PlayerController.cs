using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable Initialization
    public float movementSpeed = 50f;
    public Rigidbody rigidBody;

    private bool canMove = true;
    private float horizontalAxis;
    private float verticalAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (canMove)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            rigidBody.AddForce(new Vector3(horizontalAxis, 0f, verticalAxis) * movementSpeed);
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
