using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //What holds the dragging capabilities


//This class must be attached to the RectTransform of the joystick-background image, as it will reference it's size.
namespace TafeDiplomaFramework.Mobile
{
    public class JoystickInput : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("UI elements")]
        [SerializeField] private RectTransform handle;//Joystick handle 
        [SerializeField] private RectTransform backGround; //Joystick background, used to calculate handle's relative position.

        private Vector3 startingPos;
        private float bgEmptySpace_X;
        private float bgEmptySpace_Y;

        public Vector2 DragDir { get; private set; }

        #region MonoBehavior
        private void Start()
        {
            startingPos = handle.transform.position;

            //Calculates the empty space between the handle-image and the edge of the background-image.
            bgEmptySpace_X = (backGround.rect.size.x - handle.rect.size.x) * 0.5f;
            bgEmptySpace_Y = (backGround.rect.size.y - handle.rect.size.y) * 0.5f;
        }
        #endregion

        #region Public
        public void OnDrag(PointerEventData eventData)
        {
            CalculateDragDirection(eventData);
            MoveJoystickIconPosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetDragDirection();
            ResetJoystickIconPosition();
        }

        public void OnDragRelease(PointerEventData eventData) { }
        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData); //When you clicked but havne't started dragging
        public void OnPointerUp(PointerEventData eventData) => OnEndDrag(eventData);
        #endregion

        #region Private
        private void CalculateDragDirection(PointerEventData eventData)
        {
            DragDir = new Vector2(
                (eventData.position.x - backGround.position.x) / bgEmptySpace_X,
                (eventData.position.y - backGround.position.y) / bgEmptySpace_Y);

            //"Clamp" the DragDir maginitude between dead-zone and 1f. 
            DragDir = (DragDir.magnitude > 1f) ? DragDir.normalized : DragDir;
            DragDir = (DragDir.magnitude < 0.2f) ? Vector2.zero : DragDir;
        }

        private void MoveJoystickIconPosition()
        {
            handle.transform.position = new Vector3(
                   DragDir.x * bgEmptySpace_X + startingPos.x,
                   DragDir.y * bgEmptySpace_Y + startingPos.y,
                   0f);
        }

        private void ResetDragDirection() => DragDir = Vector2.zero;
        private void ResetJoystickIconPosition() => handle.transform.position = startingPos;
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