using UnityEngine;
using System.Collections;
//Alex Gallegos third person camera script
public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField] public Vector3 cameraOffset;
    [SerializeField] float damping;
    public GameObject camTarget, player;
    Transform cameraLookTarget;
    PlayerScript localPlayer;
    public float maxAngle, minAngle;

    private Quaternion maxRotation;

    public float inX;
    public float rotationX;


    public Vector3 offsetRef;

    private void Start()
    {
        cameraLookTarget = camTarget.transform;
        localPlayer = player.GetComponent<PlayerScript>();
        offsetRef = cameraOffset;

    }
    private void FixedUpdate()
    {

        if (cameraLookTarget.transform.eulerAngles.x > maxAngle) rotationX = maxAngle;
        
        else if (cameraLookTarget.transform.eulerAngles.x < minAngle) rotationX = minAngle;
        
        else rotationX = cameraLookTarget.transform.eulerAngles.x;

        maxRotation.Set(rotationX, transform.rotation.y, transform.rotation.z, 1);

    }
    void LateUpdate()
    {


        Vector3 targetPosition = cameraLookTarget.position
            + localPlayer.transform.forward * cameraOffset.z
            + localPlayer.transform.up * cameraOffset.y
            + localPlayer.transform.right * cameraOffset.x;

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookTarget.rotation, damping * Time.deltaTime);
    }
}
