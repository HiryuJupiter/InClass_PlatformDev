using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //What holds the dragging capabilities

namespace BreadAndButter.Mobile
{
    public enum JoystickAxis { None, Horizontal, Vertical }

    public class JoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Vector2 Axis { get; private set; } = Vector2.zero;

        [SerializeField]
        private RectTransform handle;//Joystick handle 
        [SerializeField]
        private RectTransform backGround; //Joystick background, used to calculate handle's relative position.
        [SerializeField, Range(0, 1)]
        private float deadZone = .25f;

        private Vector3 initialPosition;

        float xDiff;
        float yDiff;


        void Start() => initialPosition = handle.transform.position;


        //Pointer event data holds the data on the last frame
        public void OnDrag (PointerEventData _eventData)
        {
            
            xDiff = (backGround.rect.size.x - handle.rect.size.x) * 0.5f;
            yDiff = (backGround.rect.size.y - handle.rect.size.y) * 0.5f;

            //Axis is normalized number. This is to make the inner handle stay in range of the background.
            //In game, we're not "dragging" the sprite handle, we're dragging the screen, an empty cursor, so we calculate the graphics
            //to make the graphics reflect that
            //We only want to move the handle amount of pixel based on the background to handle empty space.

            //Cursor moved amount divide by the pixel difference, both of which in world space.
            Axis = new Vector2(
                (_eventData.position.x - backGround.position.x) / xDiff,
                (_eventData.position.y - backGround.position.y) / yDiff);

            //Axis = new Vector2(
            //    (_eventData.position.x - backGround.position.x) / (backGround.rect.size.x  / 2f),
            //    (_eventData.position.y - backGround.position.y) / (backGround.rect.size.y  / 2f));

            //Only normalize if it goes above 1f.
            Axis = (Axis.magnitude > 1f) ? Axis.normalized : Axis;

            //Apply the axis world position to the handle
            handle.transform.position = new Vector3(
                Axis.x * xDiff + backGround.position.x,
                Axis.y * yDiff + backGround.position.y,
                0f);

            // Apply the dead zone after the handle has been placed into its correct position
            Axis = (Axis.magnitude < deadZone) ? Vector2.zero : Axis;
        }

        public void OnEndDrag (PointerEventData _eventData)
        {
            ResetAxis();
            ResetHandle();
        }

        public void OnDragRelease(PointerEventData _eventData)
        {

        }

        public void OnPointerDown(PointerEventData _eventData)    => OnDrag(_eventData); //When you clicked but havne't started dragging
        public void OnPointerUp(PointerEventData _eventData)      => OnEndDrag(_eventData); 

        private void ResetAxis () => Axis = Vector2.zero;
        private void ResetHandle () => handle.transform.position = initialPosition;

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 200, 20), "xDiff: " + xDiff);
            GUI.Label(new Rect(20, 40, 200, 20), "xDiff: " + xDiff);
            GUI.Label(new Rect(20, 60, 200, 20), "Axis: " + Axis);
        }
    }
}
