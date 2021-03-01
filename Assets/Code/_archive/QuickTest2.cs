using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class QuickTest2 : MonoBehaviour
    {
        const float Height = 1f;

        public Transform target;
        public Transform p1;
        public Transform p2;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DoMove(p1.position, p2.position));
            }
        }
        IEnumerator DoMove(Vector3 startingPos, Vector3 endPos)
        {
            Vector3 dir = endPos - startingPos;

            Transform t1 = new Transform();




            //Quaternion headingRotation = Quaternion.LookRotation(Vector3.forward, dir);
            float magnitude = dir.magnitude;

            Vector3 normalizedPos;
            float y;

            for (float x = 0; x < 1f; x += Time.deltaTime)
            {
                y = -4 * Height * x * x + 4 * Height * x;
                normalizedPos = new Vector3(x* magnitude, y * magnitude);
                target.position = headingRotation * Quaternion.Euler(normalizedPos);

                yield return null;
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