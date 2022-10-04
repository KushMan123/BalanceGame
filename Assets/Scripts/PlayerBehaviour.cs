using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Newspaper, Wood, Cement }
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Attributes")]
    public float movementSpeed = 50f;
    private bool canMove = true;
    private float horizontalAxis;
    private float verticalAxis;
    private Rigidbody rigidBody;

    [Header("State Attributes")]
    public State playerState = State.Wood;
    public Material woodMaterial;
    public Material cementMaterial;
    public Material paperMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Player Movement Behaviour
        Movement();
    }

    void Movement()
    {
        //if the player can move then add force based on Input Direction
        if (canMove)
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            rigidBody.AddForce(new Vector3(horizontalAxis * movementSpeed, transform.position.y, verticalAxis * movementSpeed));
        }
    }

    void ChangeState(State newState)
    {
        // Change the State of the Player based on the Tranistion Plate
        playerState = newState;
        switch (newState)
        {
            case State.Newspaper:
                this.GetComponent<Renderer>().material = paperMaterial;
                rigidBody.mass = 2;
                break;
            case State.Wood:
                this.GetComponent<Renderer>().material = woodMaterial;
                rigidBody.mass = 5;
                break;
            case State.Cement:
                this.GetComponent<Renderer>().material = cementMaterial;
                rigidBody.mass = 10;
                break;
        }
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public IEnumerator InTransitionPlate(float stopTime, State newState)
    {
        canMove = false;
        ChangeState(newState);
        yield return new WaitForSeconds(stopTime);
        canMove = true;
    }
}
