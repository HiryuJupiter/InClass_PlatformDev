using System;
using System.Collections;
using UnityEngine;

//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

/*
The y, height formula, is this when simplified: -4*x*x+4*x 
First remember that x is always 0~1, as it is the t variable in Lerp.
at x = 0.5, x*x will result in half of x, since 0.5x0.5 = 0.25
at x = 1, x*x == x. 
 */


public class MathParabola
{
    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(
            mid.x,
            f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}


/*
         public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(
                mid.x, 
                f(t) + Mathf.Lerp(start.y, end.y, t), 
                mid.z);
        }
 */