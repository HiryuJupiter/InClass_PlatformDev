using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    const float MoveSpeed = 5f;
    const float RecoilAmount = 1f;

    [SerializeField] Transform shootPoint;

    PoolManager_BeachDefence poolM;

    Vector3 startPos;

    void Start()
    {
        poolM = PoolManager_BeachDefence.Instance;
        startPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, MoveSpeed * Time.deltaTime);
    }

    public void Shoot ()
    {
        poolM.SpawnBullet(shootPoint.position, shootPoint.rotation);
        transform.position = transform.position - transform.forward * RecoilAmount;
    }
}