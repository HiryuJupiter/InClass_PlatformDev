using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TafeDiplomaFramework.Mobile
{
    public class Swipe
    {
        public Vector2 startPoint = new Vector2();
        public List<Vector2> pathPoints = new List<Vector2>();
        public int fingerID;

        public Swipe(Vector2 startPoint, int fingerID)
        {
            this.fingerID = fingerID;
            this.startPoint = startPoint;
            pathPoints.Add(startPoint);
        }
    }
}