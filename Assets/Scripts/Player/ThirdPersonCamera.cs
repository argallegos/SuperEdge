using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public bool cameraMove;
    [SerializeField] float damping; 
    public Vector2 downYZ, upYZ;
    public float maxRotY, minRotY;
    public GameObject camLookTarget, camFollowTarget, camPivot, player;
    Transform cameraLookTarget, cameraFollowTarget;
    PlayerScript playerScript;

    Vector2 startYZ;

    float camOffsetY, camOffsetZ;
    float followPointY, followPointZ;
    float rotY;

    private void Start()
    {
        cameraLookTarget = camLookTarget.transform;
        cameraFollowTarget = camFollowTarget.transform;
        playerScript = player.GetComponent<PlayerScript>();
        Cursor.lockState = CursorLockMode.Locked;
        startYZ.Set(cameraFollowTarget.localPosition.y, cameraFollowTarget.localPosition.z);

    }

    private void FixedUpdate()
    {
        camPivot.transform.Rotate(Vector3.right * -playerScript.camY);
        rotY = camPivot.transform.localEulerAngles.x;
        if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) > maxRotY) {
            rotY = maxRotY;
            print("exceeds max");
        }
        else if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) < minRotY) {
            rotY = minRotY;
            print("exceeds MIN");
        }


        if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) < 0)
        {
            followPointZ = upYZ.y + (1 - (Mathf.Abs(Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) / maxRotY))) * (startYZ.y - upYZ.y);
            followPointY = upYZ.x + (1 - (Mathf.Abs(Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) / maxRotY))) * (startYZ.x - upYZ.x);
            cameraFollowTarget.localPosition = new Vector3(cameraFollowTarget.localPosition.x, followPointY, followPointZ);

        }
        if (Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) > 0)
        {
            followPointZ = downYZ.y + (1 - (Mathf.Abs(Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) / minRotY))) * (startYZ.y - downYZ.y);
            followPointY = downYZ.x + (1 - (Mathf.Abs(Mathf.DeltaAngle(0, camPivot.transform.localEulerAngles.x) / maxRotY))) * (startYZ.x - downYZ.x);
            cameraFollowTarget.localPosition = new Vector3(cameraFollowTarget.localPosition.x, followPointY, followPointZ);

        }
        camPivot.transform.localEulerAngles = new Vector3(rotY, 0f, 0f);


    }

    void LateUpdate()
    {


        Vector3 targetPosition = cameraFollowTarget.position;
        
        transform.LookAt(cameraLookTarget.transform);

        if (cameraMove) transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
    }

    public void CamReset()
    {
        transform.position = cameraFollowTarget.position;

    }
}
