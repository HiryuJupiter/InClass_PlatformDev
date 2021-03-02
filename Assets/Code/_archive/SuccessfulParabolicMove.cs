using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class SuccessfulParabolicMove : MonoBehaviour
    {
        const float Height = 1f;

        public Transform target;
        public Transform p1;
        public Transform p2;

        Vector3 customDir = new Vector3(1, 1, 0);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DoDirectionalParabolicMove(p1.position, p2.position));
            }
        }

        IEnumerator DoDirectionalParabolicMove(Vector3 startingPos, Vector3 endPos)
        {
            Vector3 dir = endPos - startingPos;
            float magnitude = dir.magnitude;

            Debug.DrawRay(startingPos, dir, Color.red, 1f);

            for (float x = 0; x < 1f; x += Time.deltaTime)
            {
                float y = -4 * Height * x * x + 4 * Height * x;
                Vector3 normalizedParabolicPos = new Vector3(x* magnitude, y * magnitude);

                target.position = GetRelativeVector2D(normalizedParabolicPos, dir);
                //target.position = headingRotation * Quaternion.Euler(normalizedPos);

                yield return null;
            }
        }

        // Convert a world-space vector into a vector relative to the modifier
        Vector3 GetRelativeVector2D(Vector3 dir, Vector3 modifier) //make dir relative to the referenceDir
        {
            Vector3 modifierUp = Quaternion.Euler(0f, 0f, 90f) * modifier; //Rotate a vector 90 degrees to the left
            return Quaternion.LookRotation(Vector3.forward, modifierUp) * dir;
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