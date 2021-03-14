using System.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1000f;

    [SerializeField] float XSensitivity = 2f;
    [SerializeField] float YSensitivity = 2f;
    [SerializeField] float rotationSmoothTime = 5f;

    Rigidbody rb;
    private Quaternion characterTargetRot;
    private Quaternion cameraTargetRot;
    private Transform cam;

    private void Awake()
    {
        cameraTargetRot = transform.localRotation;

        cam = Camera.main.transform;
    }

    void Start()
    {

    }

    void Update()
    {
        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        LookRotation();

        //Rotate
        Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
        rb.velocity = velRotation * rb.velocity;

        //if (CanRotate)
        //    transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, 0f, 0f));
        //transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, rotationSpeed * Time.deltaTime * PlayerInput.MouseX, 0f));
    }

    void LookRotation ()
    {

    }

    bool CanRotateY =>
        (transform.eulerAngles.x > -80f && PlayerInput.MouseY > 0f) &&
        (transform.eulerAngles.x > 80f && PlayerInput.MouseY < 0f);

    void OldNotWorkingMovementRotationScheme ()
    {
        Quaternion r = transform.rotation;
        r = r * Quaternion.AngleAxis(PlayerInput.MouseX * Time.deltaTime * rotationSpeed, Vector3.up);
        r = r * Quaternion.AngleAxis(PlayerInput.MouseY * Time.deltaTime * rotationSpeed, transform.right);

        transform.rotation = r;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "Mouse X: " + PlayerInput.MouseX);
        GUI.Label(new Rect(20, 40, 200, 20), "Mouse Y: " + PlayerInput.MouseY);
        GUI.Label(new Rect(20, 60, 200, 20), "transform.eulerAngles.x: " + transform.eulerAngles.x);
        GUI.Label(new Rect(20, 80, 200, 20), "transform.eulerAngles.y: " + transform.eulerAngles.y);
    }
}