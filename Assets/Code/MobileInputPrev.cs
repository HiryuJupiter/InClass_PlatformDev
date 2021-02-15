using System.Collections;
using UnityEngine;
using System.Collections.Generic;

//What is flick? Flick is taking the first touch, in update frame, monitor it moving until it ends. Then you check the amount of time for this to occur. If it's more than a second, it becomes a swipe.
//https://github.com/jjhesk/unity-interview/blob/master/Assets/Plugins/EasyTouch/EasyTouch.cs


public class MobileInputPrev : MonoBehaviour
{
    public class Swipe
    {
        public Vector2 initialPosition = new Vector2();
        public List<Vector2> positions = new List<Vector2>();
        public int fingerID;

        public Swipe(Vector2 _initialPos, int _fingerID)
        {
            initialPosition = _initialPos;
            positions.Add(_initialPos);
            fingerID = _fingerID;
        }
    }

    public static Vector3 FlickDirection { get; private set; } = Vector3.positiveInfinity; // The direciton of the lats flick. Positive infinithy means no flick occured.
    public static float FlickPower { get; private set; } = float.PositiveInfinity; // How fast or hard the player flicked.
    public static int SwipeCount => Swipes.Count; // How many swipes are in progress.


    private static Dictionary<int, Swipe> Swipes = new Dictionary<int, Swipe>(); //Key - fingerID. Value - swipe.
    private float flickTime = 0; //How long the swipe has been running for.
    private Vector2 flickOrigin = Vector2.positiveInfinity; //The origin point for the occuring swipe
    private int initialFinger = -1; //The initialiting finger index


    /// <summary>
    /// Attempt to retrieve the relevant swaipt information relating to the passed ID
    /// </summary>
    /// <param name="_index"> The finger ID we are attempting to get the swipe for </param>
    /// <returns> The corresponding swipe if it exists, otherwise null. </returns>
    public static Swipe GetSwipe(int _index)
    {
        Swipe temp;
        Swipes.TryGetValue(_index, out temp);
        return temp;
    }

    void Update()
    {
        //
        if (HasTouch)
        {
            foreach (Touch touch in Input.touches)
            {
                //If this is the first touch in the swipe
                //Flick 
                if (touch.phase == TouchPhase.Began && flickOriginResetted)
                {
                    flickOrigin = touch.position;
                    initialFinger = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFinger && flickTime < 1f)
                {
                    CalculateFlick(touch.position);
                }

                // Swipe storage 
                if (touch.phase == TouchPhase.Began)
                {
                    Swipes.Add(touch.fingerId, new Swipe(touch.position, touch.fingerId));
                }
                else if (touch.phase == TouchPhase.Moved && Swipes.TryGetValue(touch.fingerId, out Swipe swipe))
                {
                    swipe.positions.Add(touch.position);
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && Swipes.TryGetValue(touch.fingerId, out swipe))
                {
                    //handle cancellation if device lost track of finger or detecting finger lifted 
                    //The swipe has ended so remove it
                    Swipes.Remove(swipe.fingerID);
                }
            }
            //There isn't so reset swipe data
            flickTime = Time.deltaTime;
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
        Vector2 heading = flickOrigin - _endTouchPos;
        //Direction = herading.normalized

        FlickPower = heading.magnitude;
        FlickDirection = heading.normalized;

        //Reset swipe origin
        flickOrigin = Vector2.positiveInfinity;
    }

    /// <summary>
    /// Reset flick data
    /// </summary>
    private void ResetSwipe()
    {
        FlickDirection = Vector2.positiveInfinity;
        FlickPower = float.PositiveInfinity;
        flickOrigin = Vector2.positiveInfinity;
        flickTime = 0; ;
    }

    bool HasTouch => Input.touchCount > 0;
    bool flickOriginResetted => flickOrigin == Vector2.positiveInfinity;
}