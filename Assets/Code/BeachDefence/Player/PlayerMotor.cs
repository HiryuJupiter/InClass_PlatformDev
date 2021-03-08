using System.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1000f;

    Rigidbody rb;

    private void Awake()
    {
        
    }

    void Start()
    {

    }

    //void Update()
    //{
    //    if (CanRotate)
    //        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, 0f, 0f));
    //    //transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime * PlayerInput.MouseY, rotationSpeed * Time.deltaTime * PlayerInput.MouseX, 0f));
    //}

    //bool CanRotate =>
    //    (transform.rotation.x > -80f && rotationSpeed > 0f) &&
    //    (transform.rotation.x > 80f && rotationSpeed < 0f);
}