using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 The purpose of this is so we can have a Start/Awake/Setup function with custom parameters, 
and so we can have custom Update function (defined by Run())
 */

namespace TafeDiplomaFramework
{
    public abstract class RunnableBehavior : MonoBehaviour
    {
        public bool Enabled { get; set; }
        private bool isSetup = false;

        public void Setup(params object[] _param)
        {
            if (isSetup)
                throw new System.InvalidOperationException("Runnable already setup");

            OnSetup(_param);
            isSetup = true;
        }

        public void Run(params object[] _param)
        {
            if (Enabled)
            {
                OnRun(_param);
            }
        }

        protected abstract void OnSetup(params object[] _params);
        protected abstract void OnRun(params object[] _params);


    }
}
