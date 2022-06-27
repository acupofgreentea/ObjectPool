using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Sphere sphere;

    [SerializeField] private float spawnRate = 0.01f;

    [SerializeField] private bool useObjectPool;

    private ObjectPool<Sphere> spherePool;

    private float timer = 0;

    private void Awake()
    {
        spherePool = new ObjectPool<Sphere>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 200, 600);
    }

    private void Update()
    {
        if (useObjectPool)
        {
            if (timer <= 0)
            {
                //spawn
                spherePool.Get();
                timer = spawnRate;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            var sph = Instantiate(sphere, GetSpawnPos(), Quaternion.identity);
            Destroy(sph, 1f);
        }

    }

    private Vector3 GetSpawnPos()
    {
        return new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10), transform.position.y, transform.position.z);
    }

    private Sphere CreatePooledObject()
    {
        Sphere instance = Instantiate(sphere, Vector3.zero, Quaternion.identity);

        instance.SphereDisable += ReturnObjectToPool;

        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Sphere instance)
    {
        spherePool.Release(instance);
    }

    private void OnTakeFromPool(Sphere instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = GetSpawnPos();
    }

    private void OnReturnToPool(Sphere instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Sphere instance)
    {
        Destroy(instance.gameObject);
    }
}
