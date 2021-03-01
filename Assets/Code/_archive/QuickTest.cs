using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class QuickTest : MonoBehaviour
    {
        public Transform target;
        public Transform p1;
        public Transform p2;

        bool leftToRight = true;

        IEnumerator DoMove()
        {
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                target.position = Vector3.Lerp(leftToRight ? p1.position : p2.position, 
                    leftToRight ? p2.position : p1.position, t);

                yield return null;
            }
            leftToRight = !leftToRight;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DoMove());
            }
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