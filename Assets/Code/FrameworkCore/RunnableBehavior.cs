using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InvalidOperationException = System.InvalidOperationException;

namespace BreadAndButter
{
    public abstract class RunnableBehavior : MonoBehaviour, IRunnables
    {
        public bool Enabled { get; set; }
        private bool isSetup = false;

        public void Run(params object[] _param)
        {
            if (Enabled)
            {
                OnRun(_param);
            }
        }

        public void Setup(params object[] _param)
        {
            if (isSetup)
                throw new InvalidOperationException("Runnable already setup");

            OnSetup(_param);
            isSetup = true;
        }

        protected abstract void OnSetup(params object[] _params);
        protected abstract void OnRun(params object[] _params);
    }
}
