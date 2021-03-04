using System.Collections;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private static Settings Instance;

    [SerializeField] float tileMoveSpeed = 5f;
    [SerializeField] float tileFlySpeed = 15f;
    [SerializeField] float minDragDist = 50f;

    public static float TileMoveSpeed => Instance.tileMoveSpeed;
    public static float TileFlySpeed => Instance.tileFlySpeed;
    public static float MinDragDist => Instance.minDragDist;

    private void Awake()
    {
        Instance = this;
    }

}