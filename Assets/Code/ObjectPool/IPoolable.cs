using UnityEngine;
using System.Collections;

public interface IPoolable
{
    void InitialActivation(Pool pool);
    void Reactivation();
    void Reactivation(Vector3 pos);
    void Reactivation(Vector3 pos, Quaternion rot);
    void Despawn();
}


//A simple abstract class that specifies the variables and methods this object must contain
//public abstract class PoolObject : MonoBehaviour
//{
//    protected Pool pool;

//    public void SetPool(Pool pool)
//    {
//        this.pool = pool;
//    }

//    protected void ReturnToPool()
//    {
//        pool.Despawn(gameObject);
//    }
//}