using System.Collections;
using UnityEngine;

public class PlayerBullet_Basic : PlayerBulletBase
{
    const float LifeDuration = 10f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed = 10f;
    [SerializeField] Light light;

    private Rigidbody rb;
    private PoolManager_BeachDefence poolM;
    private float lifeTimer;

    #region MonoBehavior
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        lifeTimer = LifeDuration;
    }

    private void Start()
    {
        poolM = PoolManager_BeachDefence.Instance;
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity * Time.deltaTime, Color.red, 1f);
        if (Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit, rb.velocity.magnitude * Time.deltaTime, groundLayer))
        {
            HitsGround(hit.collider);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGroundLayer(collision.collider))
        {
            HitsGround(collision.collider);
        }
    }
    #endregion

    private void HitsGround(Collider collider)
    {
        Deactivate();
    }

    #region Refresh auto destroy timer
    #endregion

    private void Deactivate()
    {
        poolM.SpawnFadeOutLight(transform.position - transform.forward * 0.1f, light);
        GetComponent<Collider>().enabled = false;
        //Destroy(gameObject);
    }

    private bool IsGroundLayer(Collider col) => groundLayer == (groundLayer | 1 << col.gameObject.layer);
    private void RefreshDestroyTimer () => lifeTimer = LifeDuration;
}