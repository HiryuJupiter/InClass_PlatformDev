using System.Collections;
using UnityEngine;

public class BulletLight : MonoBehaviour, IPoolable
{
    [SerializeField] float fadeSpeed = 5f;
    Light light;

    public void Despawn()
    {
    }

    public void InitialActivation(Pool pool)
    {
    }

    public void Reactivation()
    {
    }

    public void Reactivation(Vector3 pos)
    {
    }

    public void Reactivation(Vector3 pos, Quaternion rot)
    {
    }

    public void SetStartIntensity (Light l)
    {
        light = GetComponent<Light>();
        light.intensity = l.intensity;
        light.range = l.range;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut ()
    {
        while (light.intensity > 0f)
        {
            light.intensity -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        Destroy(gameObject);
    }
}