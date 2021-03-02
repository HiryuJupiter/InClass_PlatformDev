using System.Collections;
using UnityEngine;

public class ParabolicMove : MonoBehaviour
{
    const float Height = 0.5f;
    public static Vector3 Move(Vector3 startPos, Vector3 endPos, float t)
    {
        //Vector3 facing = endPos - startPos;
        //Vector3 relativeUpDir = Quaternion.Euler(0f, 0f, 90f) * facing; //Rotate a vector 90 degrees to the left

        //Debug.DrawRay(startPos, facing, Color.red, 1f);
        //Debug.DrawRay(startPos, relativeUpDir, Color.green, 1f);

        //float magnitude = facing.magnitude;

        //float x = Mathf.Lerp(0f, magnitude, t);

        //Vector3 scaledLinearPos = new Vector3(x, 0f);
        //return Quaternion.LookRotation(Vector3.forward, relativeUpDir) * scaledLinearPos;

        //
        Vector3 facing = endPos - startPos;
        float magnitude = facing.magnitude;

        float x = t;
        float y = -4 * Height * x * x + 4 * Height * x;

        Vector3 scaledParabolicPos = new Vector3(x * magnitude, y * magnitude);
        Vector3 relativeUpDir = Quaternion.Euler(0f, 0f, 90f) * facing.normalized; //Rotate a vector 90 degrees to the left
        return Quaternion.LookRotation(Vector3.forward, relativeUpDir) * scaledParabolicPos;
    }
}


//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

/*
The y, height formula, is this when simplified: -4*x*x+4*x 
First remember that x is always 0~1, as it is the t variable in Lerp.
at x = 0.5, x*x will result in half of x, since 0.5x0.5 = 0.25
at x = 1, x*x == x. 
 */
