using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuickTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("mod 1: " + Mod1(1, 4));
        Debug.Log("mod 2: " + Mod2(1, 4));
        Debug.Log("mod 1: " + Mod1(-1, 5));
        Debug.Log("mod 2: " + Mod2(-1, 5));
        Debug.Log("Mathf.Floor(-1/5)" + (Mathf.Floor(-1f / 5f)));
    }



    float Mod1(float a, float b)
    {
        float r = a % b;
        return a < 0f ? a + b : a;
    }

    float Mod2(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }
}
