using System.Collections;
using UnityEngine;
using System.Collections.Generic;

//What is flick? Flick is taking the first touch, in update frame, monitor it moving until it ends. Then you check the amount of time for this to occur. If it's more than a second, it becomes a swipe.
//https://github.com/jjhesk/unity-interview/blob/master/Assets/Plugins/EasyTouch/EasyTouch.cs

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

public class MobileInputPrev : MonoBehaviour
{
    private static Dictionary<int, Swipe> Swipes = new Dictionary<int, Swipe>(); // FingerID to swipe
    private float flickDuration = 0; 
    private Vector2 flickOrigin = Vector2.positiveInfinity;
    private int initialFinger = -1; 

    public static Vector3 FlickDirection { get; private set; }  = Vector3.positiveInfinity; //Positive infinithy means no flick occured.
    public static float FlickPower { get; private set; }        = float.PositiveInfinity;
    public static int SwipeCount => Swipes.Count; 

    public static Swipe GetSwipe(int _index)
    {
        Swipe temp;
        Swipes.TryGetValue(_index, out temp);
        return temp;
    }

    void Update()
    {
        if (TouchDetected)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && flickOriginResetted)
                {
                    flickOrigin = touch.position;
                    initialFinger = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && touch.fingerId == initialFinger && flickDuration < 1f)
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
            flickDuration = Time.deltaTime;
        }
        else
        {
            ResetSwipe();
        }
    }

    private void CalculateFlick(Vector2 _endTouchPos)
    {
        Vector2 heading = flickOrigin - _endTouchPos;
        FlickPower = heading.magnitude;
        FlickDirection = heading.normalized;
        flickOrigin = Vector2.positiveInfinity;
    }

    private void ResetSwipe()
    {
        FlickDirection = Vector2.positiveInfinity;
        FlickPower = float.PositiveInfinity;
        flickOrigin = Vector2.positiveInfinity;
        flickDuration = 0; ;
    }

    bool TouchDetected => Input.touchCount > 0;
    bool flickOriginResetted => flickOrigin == Vector2.positiveInfinity;
}