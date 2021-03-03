using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuickTest : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.white);

        if (Input.GetMouseButtonDown(0))
        {
            //Physics.RaycastAll
        }
    }
}
