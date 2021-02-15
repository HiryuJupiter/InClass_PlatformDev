using System.Collections;
using UnityEngine;

namespace BreadAndButter
{
    public static class RunnableUtil
    {
        public static bool Validate<T>(ref T _runnable, GameObject _from) where T : IRunnables
        {
            if (_runnable != null) //If runnable is already set
            {
                return true;
            }

            if (_runnable == null)
            {
                //If not set, try to reference it from the GameObject passed in.
                _runnable = _from.GetComponent<T>();
                if (_runnable != null)
                {
                    return true;
                }
            }

            if (_runnable == null)
            {
                _runnable = _from.GetComponentInChildren<T>();
                if (_runnable != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Setup<T>(ref T _runnable, GameObject _from, params object[] _params) where T : IRunnables
        {
            if (Validate(ref _runnable, _from))
            {
                _runnable.Setup(_params);
                return true;
            }
            return false;
        }

        public static void Run<T>(ref T _runnable, GameObject _from, params object[] _params) where T: IRunnables
        {
            if (Validate(ref _runnable, _from))
            {
                _runnable.Run(_params);
            }
        }
    }


}