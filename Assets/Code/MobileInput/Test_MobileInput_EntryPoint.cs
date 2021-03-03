using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TafeDiplomaFramework.Mobile;

namespace TafeDiplomaFramework
{
    public class Test_MobileInput_EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private bool joystick = false; //Joystick will work with mobile and pc regardless
        [SerializeField]
        private bool testSwipe = false;
        [SerializeField, TagAttributes]
        private string groundTag;
        [SerializeField, SceneField]
        private string otherLevel;

        void Awake()
        {
            MobileInput.InstantiateFromResource();
        }

        void Update()
        {
            if (joystick)
            {
                transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
                transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
            }
            if (testSwipe)
            {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
                //If in Android and iOS


#else
                 if (Input.GetMouseButtonDown(0)) //Touch start
                {

                }

                if (Input.GetMouseButton(0)) //Touch update
                {

                }

                if (Input.GetMouseButtonUp(0)) //Touch end
                {
                    
                }

                //Touch position emutliaotn
                Vector2 touchPos = Input.mousePosition;
#endif
            }
        }
    }
}
