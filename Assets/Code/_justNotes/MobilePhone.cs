using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePhone : MonoBehaviour
{
    void Start()
    {
        //can get touch out of bounds error
        if (Input.touchCount > 0)
        {
            //Because multi touch is supported, you can put this in for loop
            Touch touch = Input.GetTouch(0);


            //positer - never us it, hte position in world
            //Raw position - 
            //Delta position - change in pixel betwen frames
            //Delta time - time passed between the last touch
            //Tap count - how many time tapped
            //Touch phase - which point in the touch lifestyle. Begin the first frame, ended when touch is lifted from screen, cnaceld is if there is an error, doesn't happen often but does
            //Pressure - it's pointless, only iOS has this (it's called force touch)
            //Touch type: direct (standard), indirect (remote touch, simulated touch), Stylus (touching stylus)

            //both have to do with stylus:
            //Altitude angle: angle on the Y the stylus is touching. 
            //AzimuthAngle - x axis. 0 means stylus is parallel

            //radius: fingers, radius of the touch
            //radius variant -averaged touch radius. You can add this to radius to get the maximum touch size, or minus to the get the smallest touch size.


            //Gyroscope

            //gravity: acceleration vector of the frame of turning. How quickly 
            //rotation rate: same thing but more accurate,the hard value of totalspeed, similar to delta time
            //update interval: how often it is updating in seconds
            //acceleration: count number of rotation
            //

            Input.gyro.enabled = true; //you actually have to turn it on first, it's not on by default.
            


        }
    }

    void Update()
    {
        
    }
}
