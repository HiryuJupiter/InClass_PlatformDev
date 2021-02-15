using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //What holds the dragging capabilities

namespace BreadAndButter.Mobile
{

    //Must be attached to the RectTransform of the background
    public class JoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float DeadZone = .25f;

        [Header("UI elements")]
        [SerializeField] private RectTransform handle;//Joystick handle 
        [SerializeField] private RectTransform backGround; //Joystick background, used to calculate handle's relative position.

        private Vector3 resetPosition;
        private float xPixelSpace;
        private float yPixelSpace;

        public Vector2 DragDir { get; private set; } = Vector2.zero;

        #region MonoBehavior
        private void Start()
        {
            resetPosition = handle.transform.position;

            //Calculate space between the handle-image and the edge of the background-image.
            xPixelSpace = (backGround.rect.size.x - handle.rect.size.x) * 0.5f;
            yPixelSpace = (backGround.rect.size.y - handle.rect.size.y) * 0.5f;
        }
        #endregion

        #region Public
        public void OnDrag(PointerEventData eventData)
        {
            CalculateDragDirection(eventData);
            MoveJoystickIcon();
        }

        public void OnEndDrag(PointerEventData _eventData)
        {
            ResetAxis();
            ResetHandle();
        }

        public void OnDragRelease(PointerEventData _eventData) { }
        public void OnPointerDown(PointerEventData _eventData) => OnDrag(_eventData); //When you clicked but havne't started dragging
        public void OnPointerUp(PointerEventData _eventData) => OnEndDrag(_eventData);

        #endregion

        #region Private
        private void CalculateDragDirection(PointerEventData eventData)
        {
            DragDir = new Vector2(
                (eventData.position.x - backGround.position.x) / xPixelSpace,
                (eventData.position.y - backGround.position.y) / yPixelSpace);

            DragDir = (DragDir.magnitude > 1f) ? DragDir.normalized : DragDir; //Normalize if went above 1f.
            DragDir = (DragDir.magnitude < DeadZone) ? Vector2.zero : DragDir;  //Reset if below dead zone
        }

        private void MoveJoystickIcon()
        {
            handle.transform.position = new Vector3(
                   DragDir.x * xPixelSpace + backGround.position.x,
                   DragDir.y * yPixelSpace + backGround.position.y,
                   0f);
        }

        private void ResetAxis() => DragDir = Vector2.zero;
        private void ResetHandle() => handle.transform.position = resetPosition;
        #endregion
    }
}


/*
 * //Pointer event data holds the data on the last frame
 * 
 //Axis is normalized number. This is to make the inner handle stay in range of the background.
            //In game, we're not "dragging" the sprite handle, we're dragging the screen, an empty cursor, so we calculate the graphics
            //to make the graphics reflect that
            //We only want to move the handle amount of pixel based on the background to handle empty space.
 
 */