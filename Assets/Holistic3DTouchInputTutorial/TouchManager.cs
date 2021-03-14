using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    public Text tCount;

    void Start()
    {
        
    }

    void Update()
    {
        tCount.text = Input.touchCount.ToString();    

    }
}
