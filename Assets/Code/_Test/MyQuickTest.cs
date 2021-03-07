using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuickTest : MonoBehaviour
{
    void Start()
    {
        int[] array1 = new int[] { 1, 2, 3 };
        int[] array2 = array1;
        ChangeInt(array2);

        Debug.Log("array 1");
        for (int i = 0; i < array1.Length; i++)
        {
            Debug.Log(array1[i].ToString());
        }

        Debug.Log("array 2");
        for (int i = 0; i < array2.Length; i++)
        {
            Debug.Log(array2[i].ToString());
        }
    }

    void ChangeInt (int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] *= 2;
        //Debug.Log("modified: " + array[i]);
        }
    }
}
