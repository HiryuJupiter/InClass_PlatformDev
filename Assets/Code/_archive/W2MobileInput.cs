using System.Collections;
using UnityEngine;
using System.Collections.Generic;


//Swipe: there is no native function for swipe
//What is flick? Flick is taking the first touch, in update frame, monitor it moving until it ends. Then you check the amount of time for this to occur. If it's more than a second, it becomes a swipe.

public class W2MobileInput : MonoBehaviour
{

    


    /// <summary>
    /// have swipe as a class so that we can calculate them sepertately. Have a
    /// </summary>
    /// 
    public class Swipe
    {
        /// <summary>
        /// List of points along the swipe, added each frame
        /// </summary>
        public List<Vector2> positions = new List<Vector2>();
        /// <summary>
        /// The position the swipe started from
        /// </summary>
        public Vector2 initialPosition = new Vector2();
        /// <summary>
        /// The finger id associated with this swipe
        /// </summary>
        public int fingerID = 0;

        public Swipe(Vector2 _initialPos, int _fingerID)
        {
            initialPosition = _initialPos;
            fingerID = _fingerID;
            positions.Add(_initialPos);
        }

    }


    /// <summary>
    /// The direciton o that the flick last occured, positive infinithy means no flick occured.
    /// </summary>
    public static Vector3 FlickDirection { get; private set; } = Vector3.positiveInfinity;

    /// <summary>
    /// How hard and far the player flicked. speed.
    /// </summary>
    public static float FlickPower { get; private set; } = float.PositiveInfinity;
    /// <summary>
    /// The count of how many swipes are in progress
    /// </summary>
    public static int SwipeCount => Swipes.Count;

    //Key: finger iD. Value: swipe class
    private static Dictionary<int, Swipe> Swipes = new Dictionary<int, Swipe>();

    //How long the swipe has been running for.
    private float flickTime = 0;
    //The origin point for the occuring swipe
    private Vector2 flickOrigin = Vector2.positiveInfinity;

    //The initialiting finger index, so that if the player accidentaly pinky touched it, the will aga
    private int initialFInger = -1;


    /// <summary>
    /// Attempt to retrieve the relevant swaipt information relating to the passed ID
    /// </summary>
    /// <param name="_index"> The finger ID we are attempting to get the swipe for </param>
    /// <returns> The corresponding swipe if it exists, otherwise null. </returns>
    public static Swipe GetSwipe (int _index)
    {
        Swipe temp;
        //Assigns swipe if it exists
        Swipes.TryGetValue(_index, out temp);
        return temp;
    }


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
                if (touch.phase == TouchPhase.Began && flickOrigin.Equals(Vector2.positiveInfinity))
                {
                    flickOrigin = touch.position;
                    initialFInger = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFInger && flickTime < 1f)
                {
                    CalculateFlick(touch.position);
                }

                // --- Begin swipe storage ---
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

                // --- End swipe storage ---
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
}