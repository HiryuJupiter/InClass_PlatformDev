using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

//A flick is a swipe under 1 second and it starts and ends on the same fingerID without interrupted by new swipes.
//Swipe concerns with paths. Flick concerns with direction and power.

namespace TafeDiplomaFramework.Mobile
{
    public class SwipeInput : MonoBehaviour
    {
        private static Dictionary<int, Swipe> currentSwipes = new Dictionary<int, Swipe>(); 

        //Fields
        private float swipeDuration;
        private Vector2 swipeStartPoint;
        private int swipeFingerID = -1;

        //Properties
        public static Vector3 FlickDirection { get; private set; }
        public static  float FlickPower { get; private set; }

        public static int SwipeCount => currentSwipes.Count;
        public static Swipe GetSwipe(int fingerID)
        {
            Swipe temp;
            currentSwipes.TryGetValue(fingerID, out temp);
            return temp;
        }
        #region Mono
        void Awake()
        {
            ResetSwipe();
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                CalculateSwipes();
            }
            else
            {
                ResetSwipe();
            }
        }
        #endregion

        #region Click calculations
        private void CalculateSwipes ()
        {
            foreach (Touch touch in Input.touches)
            {
                //Calculate flick
                if (InBeganPhase(touch) && flickResetted)
                {
                    swipeStartPoint = touch.position;
                    swipeFingerID = touch.fingerId;
                }
                else if (InEndedPhase(touch) && touch.fingerId == swipeFingerID && swipeDuration < 1f)
                {
                    CalculateFlick(touch.position);
                }

                // SWIPE
                if (InBeganPhase(touch))
                {
                    AddSwipe(touch);
                }
                else if (InMovedPhase(touch) && currentSwipes.TryGetValue(touch.fingerId, out Swipe swipe))
                {
                    AddSwipePathPoint(swipe, touch);
                }
                else if (TouchEndedOrCanceled(touch) && HasSwipe(touch, out swipe))
                {
                    //Remove swipe if device lost track of finger or detecting finger lifted 
                    RemoveSwipe(swipe);
                }
            }
            swipeDuration += Time.deltaTime;
        }

        private void CalculateFlick(Vector2 endTouchPos)
        {
            Vector2 d = swipeStartPoint - endTouchPos;
            FlickPower = d.magnitude;
            FlickDirection = d.normalized;

            swipeStartPoint = Vector2.positiveInfinity;
        }

        private void ResetSwipe()
        {
            FlickDirection = Vector2.positiveInfinity;  //Positive infinithy means no flick occured.
            FlickPower = float.PositiveInfinity;

            swipeStartPoint = Vector2.positiveInfinity;
            swipeDuration = 0;
        }
        #endregion

        #region Helpers
        private bool flickResetted => swipeStartPoint == Vector2.positiveInfinity;

        private bool InBeganPhase(Touch touch) => touch.phase == TouchPhase.Began;
        private bool InCanceledPhase(Touch touch) => touch.phase == TouchPhase.Canceled;
        private bool InEndedPhase(Touch touch) => touch.phase == TouchPhase.Ended;
        private bool InMovedPhase(Touch touch) => touch.phase == TouchPhase.Moved;
        private bool InStationaryPhase(Touch touch) => touch.phase == TouchPhase.Stationary;
        private bool TouchEndedOrCanceled(Touch touch) => (InEndedPhase(touch) || InCanceledPhase(touch));

        private bool HasSwipe(Touch touch, out Swipe swipe) => currentSwipes.TryGetValue(touch.fingerId, out swipe);
        private void AddSwipe(Touch touch) => currentSwipes.Add(touch.fingerId, new Swipe(touch.position, touch.fingerId));
        private void AddSwipePathPoint(Swipe swipe, Touch touch) => swipe.pathPoints.Add(touch.position);
        private void RemoveSwipe(Swipe swipe) => currentSwipes.Remove(swipe.fingerID);
        #endregion

    }
}