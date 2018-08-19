using UnityEngine;
using System.Collections;
//Alex Gallegos third person camera script
public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField] public Vector3 cameraOffset;
    public Vector2 downYZ, upYZ;
    private float camOffsetY, camOffsetZ;
    [SerializeField] float damping;
    public GameObject camTarget, camPivot, player;
    Transform cameraLookTarget;
    PlayerScript playerScript;
    public float maxRotY, minRotY;
    public bool cameraMove;

    private Quaternion maxRotation;

    public float rotY;

    public Vector3 offsetRef;

    private void Start()
    {
        cameraLookTarget = camTarget.transform;
        playerScript = player.GetComponent<PlayerScript>();
        offsetRef = cameraOffset;
        cameraMove = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void FixedUpdate()
    {
        camPivot.transform.Rotate(Vector3.right * -playerScript.camY);
        rotY = camPivot.transform.localEulerAngles.x;
        if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) > maxRotY) {
            rotY = maxRotY;
            print("exceeds max, angle = " + Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) + "max =" + maxRotY);
        }
        else if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) < minRotY) {
            rotY = minRotY;
            print("exceeds MIN");
        }
        if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) < 0)
        {
            //camOffsetY = ((cameraOffset.y - upYZ.y) / minRotY) * (minRotY - (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x))) + (camOffsetY);
           // print("camOffsetY = " + camOffsetY);
        }

        
        //camPivot.transform.localEulerAngles = new Vector3(rotY, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
    void LateUpdate()
    {


        Vector3 targetPosition = cameraLookTarget.position
            + playerScript.transform.forward * cameraOffset.z
            + playerScript.transform.up * cameraOffset.y
            + playerScript.transform.right * cameraOffset.x;

        //Vector3 targetPos = new Vector3(targetPosition.x, targetPosition.y + playerScript.camY, targetPosition.z);

        transform.LookAt(cameraLookTarget.transform);

        if (cameraMove) transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
    }

    public void CamReset()
    {
        transform.position = cameraLookTarget.position
            + playerScript.transform.forward * cameraOffset.z
            + playerScript.transform.up * cameraOffset.y
            + playerScript.transform.right * cameraOffset.x;
    }
}
