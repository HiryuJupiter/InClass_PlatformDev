using UnityEngine;

//When I'm using this namespace, I will only have access to this subname space, to avoid pulling the entire thing.
using InvalidOperationException = System.InvalidOperationException;
using NullReferenceException    = System.NullReferenceException;

//This is to avoid name comflipt
using UnitySceneMananger = UnityEngine.SceneManagement;

namespace BreadAndButter.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        private static MobileInput instance = null;
        public static bool Initialized => instance != null;

        [SerializeField]
        private JoystickInput joystickInput;



        public static void Initialize ()
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Mobile Input already initialized");
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
                throw new InvalidOperationException("Mobile Input not initialized");
            }

            if (instance.joystickInput == null)
            {
                throw new NullReferenceException("Joystick input reference not set");
            }

            switch (axis)
            {
                case JoystickAxis.Horizontal:   return instance.joystickInput.Axis.x;
                case JoystickAxis.Vertical:     return instance.joystickInput.Axis.y;
                default:                        return 0f;
            }
        }
    }
}
