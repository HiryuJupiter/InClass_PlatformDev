using System.Collections;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    public static GameLayers Instance;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask PlayerLayer;

    private void Awake()
    {
        Instance = this;
    }

    public static bool IsGroundLayer(GameObject go) => Instance.groundLayer == (Instance.groundLayer | 1 << go.layer);
    public static bool IsPlayerLayer(GameObject go) => Instance.PlayerLayer == (Instance.PlayerLayer | 1 << go.layer);
}