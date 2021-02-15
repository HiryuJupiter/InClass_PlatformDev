using UnityEngine;

namespace BreadAndButter.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        private static MobileInput instance = null;

        [SerializeField]
        private JoystickInput joystickInput;

        private static bool Initialized => instance != null;

        public static void InstantiateFromResource ()
        {
            if (Initialized)
                throw new System.InvalidOperationException("Mobile Input already initialized");

            MobileInput prefabInstance = Resources.Load<MobileInput>("MobileInputPrefab");
            instance = Instantiate(prefabInstance);
            instance.gameObject.name = "MobileInputPrefab";
            DontDestroyOnLoad(instance.gameObject);
        }
  
        public static float GetJoystickAxis(JoystickAxis axis)
        {
            if (!Initialized)
                throw new System.InvalidOperationException("Mobile Input not initialized");

            if (instance.joystickInput == null)
                throw new System.NullReferenceException("Joystick input reference not set");

            switch (axis)
            {
                case JoystickAxis.Horizontal:   return instance.joystickInput.DragDir.x;
                case JoystickAxis.Vertical:     return instance.joystickInput.DragDir.y;
                default:                        return 0f;
            }
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