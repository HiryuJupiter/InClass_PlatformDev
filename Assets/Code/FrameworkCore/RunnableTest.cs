using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreadAndButter;
using BreadAndButter.Mobile;

namespace BreadAndButter
{
    
    public class RunnableTest : RunnableBehavior
    {
        [SerializeField]
        private TextMesh nameplate;

        protected override void OnSetup(params object[] _params)
        {
            string username = (string)_params[0];
            Vector3 spawnPoint = (Vector3)_params[1];

            nameplate.text = username;
            transform.position = spawnPoint;
        }

        protected override void OnRun(params object[] _params)
        {
            transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
            transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
        }
    }
}
