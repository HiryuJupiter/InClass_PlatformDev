using System.Collections;
using UnityEngine;

public class PoolManager_BeachDefence : MonoBehaviour
{
    public static PoolManager_BeachDefence Instance;

    [SerializeField] private GameObject PlayerBulletBasic;
    [SerializeField] private GameObject BulletExplosion;
    [SerializeField] private GameObject FadeOutLight;

    private Pool pool_BulletExplosion;
    private Pool pool_PlayerBulletBasic;
    private Pool pool_FadeOutLight;

    public void SpawnBullet (Vector3 pos, Quaternion rot)
    {
        pool_PlayerBulletBasic.Spawn(pos, rot);
    }

    public void SpawnFadeOutLight (Vector3 pos, Light light)
    {
        pool_FadeOutLight.Spawn(pos).GetComponent<BulletLight>().SetStartIntensity(light);
    }

    private void Awake()
    {
        //pool_BulletExplosion  = new Pool(BulletExplosion, transform);
        pool_PlayerBulletBasic  = new Pool(PlayerBulletBasic, transform);
        pool_FadeOutLight        = new Pool(FadeOutLight, transform);

        Instance = this;
    }
}