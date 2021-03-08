using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public static float MoveX;
    public static float MoveY;
    public static bool Jump;
    public static float MouseX;
    public static float MouseY;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //Move
        if (Input.GetKey(KeyCode.A))
            MoveX = -1f;
        if (Input.GetKey(KeyCode.D))
            MoveX = 1f;
        else
            MoveX = 0f;

        if (Input.GetKey(KeyCode.W))
            MoveY = -1f;
        if (Input.GetKey(KeyCode.S))
            MoveY = 1f;
        else
            MoveY = 0f;

        //Mouse look
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "Mouse X: " + MouseX);
        GUI.Label(new Rect(20, 40, 200, 20), "Mouse Y: " + MouseY);
    }
}