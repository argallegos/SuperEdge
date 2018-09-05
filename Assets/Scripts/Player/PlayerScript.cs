using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool inAir, falling, jumping = false, sprinting = false, hanging = false;
    [HideInInspector]
    public float speed, pInputVertical, pInputHorizontal, camY;

    public Rigidbody playerRB;
    public Animator anim;

    public Camera mainCam;

    public CameraPivot cameraPivot;

    [SerializeField] MouseInput MouseControl;
    Vector2 mouseInput;

    public InputController playerInput;
    public WallClimb wallClimb;

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
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);

        inputs = Vector3.zero;
        inputs.x = playerInput.Horizontal;
        inputs.y = 0f;
        inputs.z = playerInput.Vertical;

        if (playerInput.jump && OnGround() && !jumping) Jump();

        if (falling)
        {
            if (OnGround())
            {
                anim.SetBool("isFalling", false);
                if (inAir) inAir = false;
                falling = false;
            }
            else if (!OnGround() && !hanging) //FALLING
            {
                AnimState("falling");
            }
        }

        if ((playerInput.shift && !sprinting) || (!playerInput.shift && sprinting)) Sprint();

        if (playerRB.velocity.y < -0.7f) {
            jumping = false;
            if (!jumping) falling = true; 
        }
        else if (playerRB.velocity.y >= 0)
        {
            falling = false;
            if (jumping && OnGround()) jumping = false;
        }

        if (!falling && !sprinting && !inAir && !hanging)
            if (inputs == Vector3.zero) //IS IDLE
            {
                AnimState("idle");
            }

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
        if (!wallClimb.climbing) //NOT CLIMBING
        {
            transform.position += transform.forward * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;
            if (!sprinting) AnimState("run");
            else AnimState("sprint");

        }
        else // IS CLIMBING
        {
            transform.position += transform.up * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;
            AnimState("climb");
        }

    }

    public void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce);
        inAir = true;
        jumping = true;
        AnimState("jump");
    }

    void Sprint()
    {
        if (!sprinting)
        {
            sprinting = true;
            speed = sprintSpeed;
        }
        else
        {
            sprinting = false;
            speed = moveSpeed;

            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isIdle", true);
        }
    }

    public void Launch()
    {
        playerRB.AddForce(launchDirection * launchForce);
    }

    private bool OnGround()
    {
        return Physics.Raycast(feet.transform.position, Vector3.down, .6f);
    }


    #region Animation States
    public void AnimState(string state)
    {
        if (state == "idle")
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);
        }
        else if (state == "run")
        {
            anim.SetBool("isClimbing", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);
        }
        else if (state == "sprint")
        {
            anim.SetBool("isDoubleRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
        }
        else if (state == "jump")
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", true);
            anim.SetBool("isHanging", false);
        }
        else if (state == "falling")
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isClimbing", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isHanging", false);
        }
        else if (state == "hang")
        {
            anim.SetBool("isHanging", true);
            anim.SetBool("isClimbing", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        else if (state == "grab")
        {

        }
        else if (state == "climb")
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isDoubleRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isHanging", false);
            anim.SetBool("isClimbing", true);
        }
    }
    #endregion

}
