using System.Collections;
using UnityEngine;

namespace TafeDiplomaFramework
{
    
    public static class RunnableUtil
    {
        //Validates if IRunnable is attached to the gameobject
        public static bool TryGetRunnableFromGameObject<T>(ref T runnable, GameObject from) where T : RunnableBehavior
        {
            if ((runnable != null) ||
                ((runnable = from.GetComponent<T>()) != null) ||
                ((runnable = from.GetComponentInChildren<T>()) != null)) 
                return true;

            return false;
        }

        public static bool Setup<T>(ref T runnable, GameObject from, params object[] _params) where T : RunnableBehavior
        {
            if (TryGetRunnableFromGameObject(ref runnable, from))
            {
                runnable.Setup(_params);
                return true;
            }
            return false;
        }

        public static void Run<T>(ref T _runnable, GameObject _from, params object[] _params) where T : RunnableBehavior
        {
            if (TryGetRunnableFromGameObject(ref _runnable, _from))
            {
                _runnable.Run(_params);
            }
        }
    }
}


/* ORIGINAL
 * 
 * Things I changed 
 * 1. In validate, since we're checking for a RunnableBehavior or an IRunnable interface, in both cases, it's a 
 * class, which is a reference type, so we don't need the ref keyword. WRONG!!! You DO NEED REF keyword, because
 * the pointer will be modified when pointer = GetComponent<T>. The original pointer will be pointing to a different
 * location.
 * 
 * 2. The params object[] _params is too unreliable. In the example usage given by the lecturer:
 * RunnableUtil.Setup(runnableTest, gameObject, "Sally", new Vector3(1, 1, 1));
 * This is simply too unreliable as a way to setup a gameObject, you can't expect the programmer to know the order of 
 * parameters. I think you either make it completely fool proof with clear definition of parameters, or you make it 
 * very flexible and unrobust for faster development speed. This is kind of a half-way approach of, implementing so many 
 * precautious as seen Validate, yet at another junctions, we allow for things to easily go wrong .
 * 
 * 
 * 
 * 
 using System.Collections;
using UnityEngine;

namespace BreadAndButter
{
    
    public static class RunnableUtil
    {
        //Validates if IRunnable is attached to the gameobject
        public static bool Validate<T>(ref T _runnable, GameObject _from) where T : RunnableBehavior
        {
            if (_runnable != null) 
                return true;

            if (_runnable == null)
            {
                _runnable = _from.GetComponent<T>();
                if (_runnable != null)
                    return true;
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

        public static bool Setup<T>(ref T _runnable, GameObject _from, params object[] _params) where T : RunnableBehavior
        {
            if (Validate(ref _runnable, _from))
            {
                _runnable.Setup(_params);
                return true;
            }
            return false;
        }

        public static void Run<T>(ref T _runnable, GameObject _from, params object[] _params) where T: RunnableBehavior
        {
            if (Validate(ref _runnable, _from))
            {
                _runnable.Run(_params);
            }
        }
    }


}


 */
