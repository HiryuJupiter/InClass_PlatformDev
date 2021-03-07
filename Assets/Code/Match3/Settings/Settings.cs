using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Settings : MonoBehaviour
{
    private static Settings Instance;

    [SerializeField] float tileMoveSpeed = 4f;
    [SerializeField] float tileFallAcceleration = 3f;
    [SerializeField] float minDragDist = 10f;

    public static float TileMoveSpeed => Instance.tileMoveSpeed;
    public static float TileFallAcceleration => Instance.tileFallAcceleration;
    public static float MinDragDist => Instance.minDragDist;

    private void Awake()
    {
        Instance = this;
    }
}