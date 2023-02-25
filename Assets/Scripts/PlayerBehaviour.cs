using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { Newspaper, Wood, Cement }

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement Attributes")]
    public float movementSpeed = 50f;
    public float maxSpeedLimit = 4f; 
    private bool canMove = true;
    private float horizontalAxis;
    private float verticalAxis;
    private Rigidbody rigidBody;

    [Header("Wind Area Moveement")]
    public bool inWindArea = false;
    public GameObject WindArea;

    [Header("State Attributes")]
    public State playerState;
    public Material woodMaterial;
    public Material cementMaterial;
    public Material paperMaterial;

    [Header("Push Attribute")]
    private bool canPush=true;

    [Header("GroundStateAttributes")]
    [SerializeField] private bool isGrounded;
    public float maxRayDistance;
    public LayerMask layerMask;

    [Header("Game Camera")]
    public Camera gameCamera;
    private CameraBehaviour cameraBehaviour;

    [Header("Water Movement")]
    public float underWaterDrag = 3;
    public float underWaterAngularDrag = 1;
    public float overWaterDrag = 0;
    public float overWaterAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterHeight;
    bool isUnderWater;
    [SerializeField]bool inWater;

    [Header("SoundEffects")]
    public AudioClip woodSound;
    public AudioClip cementSound;
    public AudioClip paperSound;
    public AudioClip thudSound;
    private AudioClip rollSound;
    private AudioSource playerAudio;
    private float pitchMinLimit;
    private float volumeMinLimit;
    private float pitchMaxLimit;
    private float volumeMaxLimit;
    private float pitchClampLimit;
    private float volumeClampLimit;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        cameraBehaviour = gameCamera.GetComponent<CameraBehaviour>();
        playerAudio = GetComponent<AudioSource>();
        ChangeState(playerState);
    }

    // Update is called once per frame
    private void Update()
    {
        RayCasting();
        //Clamp if the speed exceeds the maxSpeedLimit
        if (rigidBody.velocity.magnitude > maxSpeedLimit && (isGrounded))
        {
            rigidBody.velocity=Vector3.ClampMagnitude(rigidBody.velocity, maxSpeedLimit);
        }
        if(rigidBody.velocity.magnitude>0 && isGrounded && !playerAudio.isPlaying)
        {
            playerAudio.clip = rollSound;
            volumeClampLimit = volumeMaxLimit;
            pitchClampLimit = pitchMaxLimit;
            volumeMinLimit = 0f;
            pitchMinLimit = 0f;
            playerAudio.Play();
        }
        else if (rigidBody.velocity.magnitude <= 0)
        {
            playerAudio.Stop();
        }
        playerAudio.pitch = rigidBody.velocity.magnitude / 3;
        playerAudio.volume = rigidBody.velocity.magnitude;
        playerAudio.pitch = Mathf.Clamp(playerAudio.pitch, pitchMinLimit, pitchClampLimit);
        playerAudio.volume = Mathf.Clamp(playerAudio.volume, volumeMinLimit, volumeClampLimit);
    }
    private void FixedUpdate()
    {
        if (GameManager.gm.GetGameState() == GameState.isPlaying)
        {
            //Player Movement Behaviour
            Movement();
            //In Wind Zone
            WindMovement();
            //In Water
            WaterMovement();
        }
        else if((GameManager.gm.GetGameState() == GameState.isPaused || 
            GameManager.gm.GetGameState() == GameState.isGameOver ||
            GameManager.gm.GetGameState()==GameState.isCompleted)
            && isGrounded)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
        
    }

    void Movement()
    {
        //if the player can move then add force based on Input Direction
        if (canMove)
        {
            if (cameraBehaviour.GetCameraState() == CameraState.Front)
            {
                horizontalAxis = Input.GetAxis("Horizontal");
                verticalAxis = Input.GetAxis("Vertical");
            }
            else if (cameraBehaviour.GetCameraState() == CameraState.Back)
            {
                horizontalAxis = -Input.GetAxis("Horizontal");
                verticalAxis = -Input.GetAxis("Vertical");
            }
            else if (cameraBehaviour.GetCameraState() == CameraState.Right)
            {
                horizontalAxis = -Input.GetAxis("Vertical");
                verticalAxis = Input.GetAxis("Horizontal");
            }
            else if (cameraBehaviour.GetCameraState() == CameraState.Left)
            {
                horizontalAxis = Input.GetAxis("Vertical");
                verticalAxis = -Input.GetAxis("Horizontal");
            }
            Vector3 movementForce = new Vector3(horizontalAxis * movementSpeed * Time.fixedDeltaTime, 0f, verticalAxis * movementSpeed * Time.fixedDeltaTime);
            rigidBody.AddForce(movementForce);
        }
    }

    void WindMovement()
    {
        //if the player enters the wind Area
        if (inWindArea)
        {
            if(playerState == State.Newspaper)
            {
                WindArea windArea = WindArea.GetComponent<WindArea>();
                rigidBody.AddForce(windArea.direction * windArea.strength);
            }
        }
    }

    void WaterMovement()
    {
        if (inWater)
        {
            float difference = transform.position.y - waterHeight;
            if (difference < 0)
            {
                rigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
                if (!isUnderWater)
                {
                    isUnderWater = true;
                    SwitchStateDrag(true);
                }
            }
            else if (isUnderWater)
            {
                isUnderWater = false;
                SwitchStateDrag(false);
            }
        }
        
    }

    void SwitchStateDrag(bool isUnderWater)
    {
        if (!isUnderWater)
        {
            rigidBody.drag = underWaterDrag;
            rigidBody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rigidBody.drag = overWaterDrag;
            rigidBody.angularDrag = overWaterAngularDrag;
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
                canPush = false;
                rollSound = paperSound;
                pitchMaxLimit = 0.4f;
                volumeMaxLimit = 0.25f;
                break;
            case State.Wood:
                this.GetComponent<Renderer>().material = woodMaterial;
                rigidBody.mass = 4;
                canPush = true;
                rollSound = woodSound;
                pitchMaxLimit = 0.8f;
                volumeMaxLimit = 0.3f;
                break;
            case State.Cement:
                this.GetComponent<Renderer>().material = cementMaterial;
                rigidBody.mass = 8f;
                canPush = true;
                rollSound = cementSound;
                pitchMaxLimit = 0.6f;
                volumeMaxLimit = 0.43f;
                break;
        }
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public bool Push()
    {
        return canPush;
    }

    public IEnumerator InTransitionPlate(float stopTime, State newState)
    {
        canMove = false;
        ChangeState(newState);
        yield return new WaitForSeconds(stopTime);
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player enters the wind Area
        if (other.gameObject.CompareTag("WindArea"))
        {
            WindArea = other.gameObject;
            inWindArea = true;
        }
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = true;
            waterHeight = other.gameObject.transform.position.y+1f;
        }
           
    }

    private void OnTriggerExit(Collider other)
    {
        //if the player enters the wind Area
        if (other.gameObject.CompareTag("WindArea"))
        {
            WindArea = null;
            inWindArea = false;
        }
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = false;
            waterHeight = 0f;
            rigidBody.drag = overWaterDrag;
            rigidBody.angularDrag = overWaterAngularDrag;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerAudio.PlayOneShot(thudSound);
        }
    }

    private void RayCasting()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, maxRayDistance,layerMask))
        {
            Debug.DrawRay(transform.position, Vector3.down*maxRayDistance, Color.green);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down*maxRayDistance, Color.red);
            isGrounded = false;
        }
    }

    public bool GetGroundedStatus()
    {
        return isGrounded;
    }

    public void SetGroundedStatus(bool value)
    {
        isGrounded = value;
    }

    public void SetMass(float value)
    {
        rigidBody.mass = value;
    }

    public bool GetWaterStatus()
    {
        return inWater;
    }
}
