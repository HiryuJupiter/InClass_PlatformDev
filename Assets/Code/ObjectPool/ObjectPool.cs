using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool
{
    Vector3 offscreen = new Vector3(-100, -100, -100);

    List<GameObject> active = new List<GameObject>();
    List<GameObject> inactive = new List<GameObject>();
    GameObject prefab;
    Transform parent;

    //Constructor
    public Pool(GameObject prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        if (prefab.GetComponent<IPoolable>() == null)
        {
            Debug.LogWarning("Object is not a valid IPoolable type");
        }
    }

    public void InitializeInactives (int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }

        for (int i = active.Count - 1; i >= 0; i--)
        {
            Despawn(active[i]);
        }
    }

    public GameObject Spawn()
    {
        GameObject p;
        if (inactive.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactive[0];
            p.SetActive(true);
            inactive.RemoveAt(0);
            p.GetComponent<IPoolable>().Reactivation();
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, offscreen, Quaternion.identity);
            p.GetComponent<IPoolable>().InitialActivation(this);
            p.transform.parent = parent;
        }
        active.Add(p);
        return p;
    }

    public GameObject Spawn(Vector3 pos)
    {
        GameObject p;
        if (inactive.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactive[0];
            p.SetActive(true);
            inactive.RemoveAt(0);
            p.GetComponent<IPoolable>().Reactivation();
            p.transform.position = pos;
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, pos, Quaternion.identity);
            p.GetComponent<IPoolable>().InitialActivation(this);
            p.transform.parent = parent;
        }
        active.Add(p);
        return p;
    }

    public void Despawn(GameObject obj)
    {
        obj.transform.position = offscreen;
        inactive.Add(obj);
        active.Remove(obj);
        obj.SetActive(false);
    }

    float shortestDist;
    public Transform ClosestUnitToLocation(Vector3 p, ref float detectionDist, ref Transform closest)
    {
        shortestDist = detectionDist;
        foreach (GameObject go in active)
        {
            if (Vector3.SqrMagnitude(p - go.transform.position) < shortestDist)
            {
                closest = go.transform;
                shortestDist = Vector3.SqrMagnitude(p - go.transform.position);
            }
        }
        return closest;
    }
}

//public class ObjectPoolMonoBehavior : MonoBehaviour
//{
//    public GameObject prefab;
//    public List<GameObject> inactive;
//    public List<GameObject> active;

//    public GameObject Spawn()
//    {
//        GameObject pf;
//        if (inactive.Count == 0)
//        {
//            pf = Instantiate(prefab);
//            pf.transform.SetParent(transform);
//            //pf.GetComponent<IPoolble>().InitialActivation(this);
//        }
//        else
//        {
//            //Pop from pool
//            inactive[0].SetActive(true);
//            pf = inactive[0];
//            inactive.RemoveAt(0);
//            //pf.GetComponent<IPoolble>().ReActivation(this);
//        }
//        return pf;
//    }

//    public void Despawn(GameObject obj)
//    {
//        inactive.Add(obj);
//        active.Remove(obj);
//        obj.SetActive(false);
//    }

//    float shortestDist;
//    public Transform ClosestUnitToLocation(Vector3 p, ref float detectionDist, ref Transform closest)
//    {
//        shortestDist = detectionDist;
//        foreach (GameObject go in active)
//        {
//            if (Vector3.SqrMagnitude(p - go.transform.position) < shortestDist)
//            {
//                closest = go.transform;
//                shortestDist = Vector3.SqrMagnitude(p - go.transform.position);
//            }
//        }
//        return closest;
//    }
//}

//public class ObjectPool
//{
//    GameObject pf;
//    Vector3 offscreen = new Vector3(-100, -100, -100);

//    //Pool
//    [HideInInspector] public List<GameObject> actives;
//    [HideInInspector] public List<GameObject> allSpawned;

//    public ObjectPool(GameObject pf)
//    {
//        this.pf = pf;
//        actives = new List<GameObject>();
//        allSpawned = new List<GameObject>();
//    }

//    public virtual GameObject Spawn(Vector3 p, Quaternion r)
//    {
//        GameObject go = PopFromPool();
//        go.GetComponent<IPoolable>().Reactivation(p, r);
//        return go;
//    }

//    protected GameObject PopFromPool()
//    {
//        foreach (GameObject go in allSpawned)
//        {
//            if (!go.activeSelf)
//            {
//                actives.Add(go);
//                go.SetActive(true);
//                return go;
//            }
//        }

//        //Instantiate
//        GameObject e = GameObject.Instantiate(pf, offscreen, Quaternion.identity) as GameObject;
//        e.GetComponent<IPoolable>().InitialActivation(this);
//        actives.Add(e);
//        allSpawned.Add(e);
//        return e;
//    }

//    public void ReturnToPool(GameObject go)
//    {
//        go.SetActive(false);
//        actives.Remove(go);
//    }

    
//}
