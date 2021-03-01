using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public interface IMyInterface
    {
        void PrintText();

        void PrintSomething(int number) => Debug.Log(number);
    }

    public class QuickTest : MonoBehaviour
    {
        enum Toys { Duck, Chicken}

        QuickTest A;
        QuickTest B;
        Toys toys;

        void Start()
        {
            switch (toys)
            {
                case Toys.Duck:
                    break;
                case Toys.Chicken:
                    break;
                default:
                    break;
            }

            //toys switch


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