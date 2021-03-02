using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class ParabolicMoveTest2 : MonoBehaviour
    {
        const float Height = 0.5f;


        public Transform target1;
        public Transform target2;
        public Transform p1;
        public Transform p2;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine (DoDirectionalParabolicMove(p1.position, p2.position));
            }
        }

        IEnumerator DoDirectionalParabolicMove(Vector3 startingPos, Vector3 endPos)
        {
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                //Lerp their positions
                target1.transform.position = p1.position + ParabolicMove.Move(startingPos, endPos, t);
                target2.transform.position = p2.position + ParabolicMove.Move(endPos, startingPos, t);
                yield return null;
            }
        }
    }
}