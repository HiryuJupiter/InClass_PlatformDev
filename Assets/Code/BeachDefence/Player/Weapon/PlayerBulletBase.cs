using System.Collections;
using UnityEngine;

public abstract class PlayerBulletBase : MonoBehaviour, IPoolable
{
    public virtual void InitialActivation(Pool pool)
    {
    }

    public virtual void Despawn()
    {
    }
    public virtual void Reactivation() { }

    public void Reactivation(Vector3 pos)
    {
    }

    public void Reactivation(Vector3 pos, Quaternion rot)
    {
    }


}