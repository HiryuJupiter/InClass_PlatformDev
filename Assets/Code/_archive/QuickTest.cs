using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class QuickTest : MonoBehaviour
    {
        public Transform target;
        public Transform p1;
        public Transform p2;

        IEnumerator DoMove()
        {
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                target.position = ParabolicMove.Move(p1.position, p2.position, t);
                yield return null;
            }
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