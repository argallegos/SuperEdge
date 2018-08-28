using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Alex Gallegos main player script

public class PlayerScript : MonoBehaviour {

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    public float speed, moveSpeed, sprintSpeed, jumpForce;
    public GameObject playerMesh, feet;
    public Vector2 direction;
    public Vector3 aimOffset;

    [HideInInspector]
    public Vector3 facingDirection;
    public Vector3 inputs = Vector3.zero;
    [HideInInspector]
    public bool inAir, falling, sprinting = false;
    [HideInInspector]
    public float pInputVertical, pInputHorizontal, camY;

    public Rigidbody playerRB;

    public Camera mainCam;

    public CameraPivot cameraPivot;

    [SerializeField] MouseInput MouseControl;

    public InputController playerInput;
    public WallClimb wallClimb;
    //public MeshSwitchy meshSwitch;
    public GameObject mesh;
    Vector2 mouseInput;

    public bool win = false;
    public bool paused = false;


    void Awake () {
        playerInput = GetComponent<InputController>();
        wallClimb = GetComponent<WallClimb>();
        //meshSwitch = mesh.GetComponent<MeshSwitchy>();

        playerRB = GetComponent<Rigidbody>();
        playerRB.isKinematic = false;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        facingDirection = mainCam.transform.forward;
        playerRB.freezeRotation = true;
        falling = false;
        Sprint();
    }
	
	void Update () {


        pInputHorizontal = playerInput.Horizontal;
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);

        inputs = Vector3.zero;
        inputs.x = playerInput.Horizontal;
        inputs.z = playerInput.Vertical;

        if (playerInput.jump && OnGround()) Jump();

        if (!OnGround())
        {
            Flying();
            inAir = true;
        }
        if (inAir && OnGround())
        {
            inAir = false;
            falling = false;
           // meshSwitch.SwitchMesh(meshSwitch.run);
        }

        if ((playerInput.shift && !sprinting) || (!playerInput.shift && sprinting)) Sprint();
        if (!paused) Look();

    }
    private void FixedUpdate()
    {
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        if (inputs != Vector3.zero && !wallClimb.climbing) Move(direction);

    }

    void Look()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
        camY = mouseInput.y * MouseControl.Sensitivity.y;

    }

    public void Move(Vector2 direction)
    {
        if (!wallClimb.climbing)
        {
            transform.position += transform.forward * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;
        }
        else transform.position += transform.up * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;
        //meshSwitch.SwitchMesh(meshSwitch.run);

    }

    public void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce);
        inAir = true;
       // meshSwitch.SwitchMesh(meshSwitch.runJump);
    }

    void Sprint()
    {
        if (!sprinting)
        {
            sprinting = true;
            speed = sprintSpeed;
           // meshSwitch.SwitchMesh(meshSwitch.sprint);

        }
        else
        {
            sprinting = false;
            speed = moveSpeed;
           // meshSwitch.SwitchMesh(meshSwitch.run);

        }
    }
    void Flying()
    {
        if (!falling && playerRB.velocity.y < 0f)
        {
            falling = true;
           // meshSwitch.SwitchMesh(meshSwitch.fall);

        }
        //if (falling &&)
        else
        {
            

        }
    }

    private bool OnGround()
    {
        return Physics.Raycast(feet.transform.position, Vector3.down, .6f);
    }


}
