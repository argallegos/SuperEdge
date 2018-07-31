using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Alex Gallegos main player script
[RequireComponent(typeof(MoveController))]

public class PlayerScript : MonoBehaviour {

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    public float speed, moveForce, jumpForce;
    public GameObject playerMesh;
    public Vector2 direction;
    public Vector3 aimOffset;

    [HideInInspector]
    public Vector3 facingDirection;
    public Vector3 inputs = Vector3.zero;
    [HideInInspector]
    public bool inAir;
    [HideInInspector]
    public float pInputVertical, pInputHorizontal;

    Rigidbody playerRB;

    public Camera mainCam;

    public CameraPivot cameraPivot;

    [SerializeField] MouseInput MouseControl;

    InputController playerInput;
    ThirdPersonCamera camScript;
    Vector2 mouseInput;

/*
    private Crosshair m_Crosshair;
    private Crosshair Crosshair
    {
        get
        {
            if (m_Crosshair == null)
                m_Crosshair = GetComponentInChildren<Crosshair>();
            return m_Crosshair;
        }
    }

*/

    void Awake () {
        playerInput = GetComponent<InputController>();
        camScript = mainCam.GetComponent<ThirdPersonCamera>();
        playerRB = GetComponent<Rigidbody>();
        playerRB.isKinematic = false;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        facingDirection = mainCam.transform.forward;
    }
	
	void Update () {

        //crosshairPos = Crosshair.crosshairPos;

        pInputHorizontal = playerInput.Horizontal;
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);

        inputs = Vector3.zero;
        inputs.x = playerInput.Horizontal;
        inputs.z = playerInput.Vertical;

       // if (inputs != Vector3.zero)
          //  transform.forward = inputs;


        if (playerInput.jump && OnGround()) Jump();

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        Look();

	}
    private void FixedUpdate()
    {
        facingDirection = mainCam.transform.forward;
        //Vector3 newPosition = playerRB.position + transform.TransformDirection(playerRB.transform.forward);
        //if (inputs != Vector3.zero)
        //    playerRB.MovePosition(playerRB.transform.forward + inputs * speed * Time.fixedDeltaTime);
        playerRB.AddForce(transform.forward * speed * playerInput.Vertical);
    }

    void Look()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        //Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
        //cameraPivot.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void Move()
    {
        //ADDFORCE MOVE



        facingDirection.Set(facingDirection.x, 0f, facingDirection.z);
        playerRB.AddForce(facingDirection * speed * playerInput.Vertical);
    }

    void Jump()
    {
        playerRB.AddForce(Vector3.up * jumpForce);
    }

    private bool OnGround()
    {
        return Physics.Raycast(transform.position + new Vector3(0, .1f, 0), Vector3.down, .4f);
    }


}
