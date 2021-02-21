using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class QuickTest : MonoBehaviour
    {
        QuickTest A;
        QuickTest B;
        void Start()
        {
            Validate(A);
            Validate(ref B);

            Debug.Log("Is A null :" + (A == null));
            Debug.Log("Is B null :" + (B == null));
        }

        void Validate (QuickTest quickTest)
        {
            quickTest = GetComponent<QuickTest>();
        }

        void Validate(ref QuickTest quickTest)
        {
            quickTest = GetComponent<QuickTest>();
        }
    }

    //public class MyGeneric<T> : MonoBehaviour where T : MonoBehaviour
    //{
    //    private static T instance;
    //    public static T Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = 
    //            }

    //            return instance;
    //        }
    //    }
    //}
}