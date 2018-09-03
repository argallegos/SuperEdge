using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Alex Gallegos main player script

public class PlayerScript : MonoBehaviour
{

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    public float moveSpeed, sprintSpeed, jumpForce, launchForce;

    public GameObject playerMesh, feet;
    public GameObject animator;

    public Vector2 direction;
    public Vector3 aimOffset;

    [HideInInspector]
    public Vector3 facingDirection, inputs = Vector3.zero, launchDirection;
    [HideInInspector]
    public bool inAir, falling, jumping = false, sprinting = false;
    [HideInInspector]
    public float speed, pInputVertical, pInputHorizontal, camY;

    public Rigidbody playerRB;
    public Animator anim;

    public Camera mainCam;

    public CameraPivot cameraPivot;

    [SerializeField] MouseInput MouseControl;

    public InputController playerInput;
    public WallClimb wallClimb;
    public GameObject mesh;
    Vector2 mouseInput;

    public bool win = false;
    public bool paused = false;


    void Awake()
    {
        playerInput = GetComponent<InputController>();
        wallClimb = GetComponent<WallClimb>();

        anim = animator.GetComponent<Animator>();

        playerRB = GetComponent<Rigidbody>();
        playerRB.isKinematic = false;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        facingDirection = mainCam.transform.forward;
        playerRB.freezeRotation = true;
        falling = false;
        Sprint();
    }

    void Update()
    {

        pInputHorizontal = playerInput.Horizontal;
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);

        inputs = Vector3.zero;
        inputs.x = playerInput.Horizontal;
        inputs.y = 0f;
        inputs.z = playerInput.Vertical;

        if (playerInput.jump && OnGround()) Jump();
        /*
        if (!OnGround())
        {
            Flying();
            inAir = true;
        }*/
        if (falling && OnGround())
        {
            if (inAir) inAir = false;
            falling = false;
            //print("on ground!");
        }

        if ((playerInput.shift && !sprinting) || (!playerInput.shift && sprinting)) Sprint();
        if (!paused) Look();

        if (playerRB.velocity.y < -0.1f) falling = true;

        if (!falling && !sprinting && !inAir)
            if (inputs == Vector3.zero)
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isDoubleRunning", false);
                anim.SetBool("isJumping", false);
            }
        //print("falling = " + falling + ", OnGround = " + OnGround());
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
            anim.SetBool("isClimbing", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);
        }
        else transform.position += transform.up * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;
        anim.SetBool("isRunning", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isJumping", false);


    }

    public void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce);
        inAir = true;
        jumping = true;
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDoubleRunning", false);
        anim.SetBool("isJumping", true);
    }

    void Sprint()
    {
        if (!sprinting)
        {
            sprinting = true;
            speed = sprintSpeed;

            anim.SetBool("isDoubleRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);

        }
        else
        {
            sprinting = false;
            speed = moveSpeed;

            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isIdle", true);
            //anim.SetInteger("AnimState", 1);

        }
    }
    public void Launch()
    {
        playerRB.AddForce(launchDirection * launchForce);
    }


    void Flying()
    {
        if (!falling && playerRB.velocity.y < 0f)
        {
            falling = true;


        }
        //if (falling &&)
        else
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isClimbing", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);

        }
    }

    private bool OnGround()
    {
        return Physics.Raycast(feet.transform.position, Vector3.down, .6f);
    }


}
