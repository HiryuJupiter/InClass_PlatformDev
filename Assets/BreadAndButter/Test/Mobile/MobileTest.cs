using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreadAndButter.Mobile;

public class MobileTest : MonoBehaviour
{
    void Start()
    {
        MobileInput.Initialize();
    }

    void Update()
    {
        transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
        transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
    }
}
