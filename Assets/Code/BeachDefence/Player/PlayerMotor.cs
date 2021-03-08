using System.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1000f;

    Rigidbody rb;
    Quaternion targetRotation;

    private void Awake()
    {
        targetRotation = transform.rotation;
    }

    void Start()
    {

    }

    void Update()
    {
        Quaternion r = transform.rotation;
        r = r * Quaternion.AngleAxis(PlayerInput.MouseX * Time.deltaTime * rotationSpeed, Vector3.up);
        r = r * Quaternion.AngleAxis(PlayerInput.MouseY * Time.deltaTime * rotationSpeed, transform.right);

        transform.rotation = r;

        //if (CanRotate)
        //    transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, 0f, 0f));
        //transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, rotationSpeed * Time.deltaTime * PlayerInput.MouseX, 0f));
    }

    bool CanRotateY =>
        (transform.eulerAngles.x > -80f && PlayerInput.MouseY > 0f) &&
        (transform.eulerAngles.x > 80f && PlayerInput.MouseY < 0f);


    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "Mouse X: " + PlayerInput.MouseX);
        GUI.Label(new Rect(20, 40, 200, 20), "Mouse Y: " + PlayerInput.MouseY);
        GUI.Label(new Rect(20, 60, 200, 20), "transform.eulerAngles.x: " + transform.eulerAngles.x);
        GUI.Label(new Rect(20, 80, 200, 20), "transform.eulerAngles.y: " + transform.eulerAngles.y);
    }
}