using System.Collections;
using UnityEngine;


//Swipe: there is no native function for swipe
//What is flick? Flick is taking the first touch, in update frame, monitor it moving until it ends. Then you check the amount of time for this to occur. If it's more than a second, it becomes a swipe.

public class W1MobileInput : MonoBehaviour
{

    /// <summary>
    /// The direciton o that the flick last occured, positive infinithy means no flick occured.
    /// </summary>
    public static Vector3 Flick { get; private set; } = Vector3.positiveInfinity;


    /// <summary>
    /// How hard and far the player flicked. 
    /// </summary>
    public static float FlickPower { get; private set; } = float.PositiveInfinity;

    //How long the swipe has been running for.
    private float swipeTime = 0;
    //The origin point for the occuring swipe
    private Vector2 swipeOrigin = Vector2.positiveInfinity;

    //The initialiting finger index, so that if the player accidentaly pinky touched it, the will aga
    private int initialFInger = -1;



    void Start()
    {

    }

    //Make sure we
    void Update()
    {
        //Check if any touches, 
        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                //If this is the first touh in the swip 
                if (touch.phase == TouchPhase.Began && swipeOrigin.Equals(Vector2.positiveInfinity))
                {
                    swipeOrigin = touch.position;
                    initialFInger = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFInger && swipeTime < 1f)
                {
                    CalculateFlick(touch.position);
                }
            }
            //There isn't so reset swipe data

            //swipeTime = Ti


        }
        else
        {
            ResetSwipe();
        }
    }

    /// <summary>
    /// Calculate the flick
    /// </summary>
    /// <param name="_endTouchPos"><the position the touch was when we ended the system/param>

    //Subtract start to end gives us heading
    private void CalculateFlick(Vector2 _endTouchPos)
    {
        //Calculate flick and flick power
        Vector2 heading = swipeOrigin - _endTouchPos;
        //Direction = herading.normalized

        FlickPower = heading.magnitude;
        Flick = heading.normalized;

        //Reset swipe origin
        swipeOrigin = Vector2.positiveInfinity;

    }

    /// <summary>
    /// Reset flick data
    /// </summary>
    private void ResetSwipe()
    {
        Flick = Vector2.positiveInfinity;
        FlickPower = float.PositiveInfinity;
        swipeOrigin = Vector2.positiveInfinity;
        swipeTime = 0; ;

    }



}