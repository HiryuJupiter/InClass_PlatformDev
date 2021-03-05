using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [Header("Pfx")]
    [SerializeField] GameObject pf_basicBullet;
    public Pool basicBullet;

    [Header("Environment")]
    [SerializeField] GameObject pf_Asteroid;
    public Pool asteroids;

    void Awake ()
    {
        instance = this;

        basicBullet = new Pool(pf_basicBullet, transform);
        asteroids = new Pool(pf_Asteroid, transform);
    }
}