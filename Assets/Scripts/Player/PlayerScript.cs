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

    public float speed, jumpForce;
    public GameObject playerMesh, feet;
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
    Vector2 mouseInput;


    void Awake () {
        playerInput = GetComponent<InputController>();

        playerRB = GetComponent<Rigidbody>();
        playerRB.isKinematic = false;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        facingDirection = mainCam.transform.forward;
    }
	
	void Update () {


        pInputHorizontal = playerInput.Horizontal;
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);

        inputs = Vector3.zero;
        inputs.x = playerInput.Horizontal;
        inputs.z = playerInput.Vertical;

        if (playerInput.jump && OnGround()) Jump();

        Look();

    }
    private void FixedUpdate()
    {
        direction.Set(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        if (inputs != Vector3.zero) Move(direction);

    }

    void Look()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

    }

    public void Move(Vector2 direction)
    {
        transform.position += transform.forward * direction.x * Time.fixedDeltaTime + transform.right * direction.y * Time.fixedDeltaTime;

    }

    void Jump()
    {
        print("Jump!");
        playerRB.AddForce(Vector3.up * jumpForce);
    }

    private bool OnGround()
    {
        return Physics.Raycast(feet.transform.position, Vector3.down, .6f);
    }


}
