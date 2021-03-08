using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    void Update()
    {
        transform.position = followTarget.position;
        transform.rotation = followTarget.rotation;
    }
}