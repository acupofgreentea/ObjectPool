using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Renderer rend;

    private const string GROUND_TAG = "Ground";

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        rend.material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(GROUND_TAG)) return;

        Destroy(gameObject);
    }
}
