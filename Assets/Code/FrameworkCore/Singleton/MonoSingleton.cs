using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NullReferenceException = System.NullReferenceException;

namespace BreadAndButter
{

    //T stands for type
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour    //Anything passed into monosingleton extends from monoSingleton or is a monoSingleton
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        throw new NullReferenceException(string.Format("No objct of type: {0} was found", typeof(T).Name));
                    }
                }
                return instance;
            }
        }

        public static void DontDestroyOnLoad() => DontDestroyOnLoad(Instance.gameObject);

        public static bool IsSingletonValid() => instance != null;

        protected T CreateInstance () //This is so the class can make the instance before instance is referenced the first time
        {
            return Instance;
        }

    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NullReferenceException = System.NullReferenceException;

namespace BreadAndButter
{

    //T stands for type
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>    //Anything passed into monosingleton extends from monoSingleton or is a monoSingleton
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        throw new NullReferenceException(string.Format("No objct of type: {0} was found", typeof(T).Name));
                    }
                }
                return instance;
            }
        }

        private static T instance = null;

        public static bool IsSingletonValid() => instance != null;

        protected T CreateInstance () //This is so the class can make the instance before instance is referenced the first time
        {
            return Instance;
        }

        public static void FlagAsPersistant() => DontDestroyOnLoad(Instance.gameObject);
    }
}

 */