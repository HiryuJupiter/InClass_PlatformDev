using UnityEngine;

namespace TafeDiplomaFramework.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        private static MobileInput instance = null;

        [SerializeField] 
        private JoystickInput joystickInput;
        [SerializeField]
        private SwipeInputInClass swipeInput;

        public static void InstantiateFromResource ()
        {
            if ( instance == null)
            {
                MobileInput prefabInstance = Resources.Load<MobileInput>("MobileInputPrefab");
                instance = Instantiate(prefabInstance);
                instance.gameObject.name = "MobileInputPrefab";
            }
        }
  
        public static float GetJoystickAxis(JoystickAxis axis)
        {
            switch (axis)
            {
                case JoystickAxis.Horizontal:   return instance.joystickInput.DragDir.x;
                case JoystickAxis.Vertical:     return instance.joystickInput.DragDir.y;
                default:                        return 0f;
            }
        }

        public SwipeInputInClass.Swipe GetSwipe(int _index)
        {
            //!initialized

            //isntance.swipeInput == null

            return instance.swipeInput.GetSwipe(_index);
        }

        public static void GetFlickData (out float _flickPower, out Vector2 _flickDirection)
        {
            //Set the out parameter to their corresponding values in the swipe input class
            _flickPower = instance.swipeInput.FlickPower;
            _flickDirection = instance.swipeInput.FlickDirection;
        }
    }
}


/*
 using UnityEngine;

namespace BreadAndButter.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        private static MobileInput instance = null;

        [SerializeField]
        private JoystickInput joystickInput;

        private static bool Initialized => instance != null;

        public static void Initialize ()
        {
            if (Initialized)
            {
                throw new System.InvalidOperationException("Mobile Input already initialized");
            }

            MobileInput prefabInstance = Resources.Load<MobileInput>("MobileInputPrefab");
            instance = Instantiate(prefabInstance);

            //Remove the suffix "_clone" that is automatically generated when instantiating a prefab
            instance.gameObject.name = "MobileInputPrefab";
            DontDestroyOnLoad(instance.gameObject);
        }
  
        public static float GetJoystickAxis(JoystickAxis axis)
        {
            if (!Initialized)
            {
                throw new System.InvalidOperationException("Mobile Input not initialized");
            }

            if (instance.joystickInput == null)
            {
                throw new System.NullReferenceException("Joystick input reference not set");
            }


            switch (axis)
            {
                case JoystickAxis.Horizontal:   return instance.joystickInput.DragDir.x;
                case JoystickAxis.Vertical:     return instance.joystickInput.DragDir.y;
                default:                        return 0f;
            }
        }
    }
}
 */