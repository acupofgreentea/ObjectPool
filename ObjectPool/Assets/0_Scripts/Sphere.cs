using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Sphere : MonoBehaviour
{
    private Renderer rend;

    private Rigidbody rb;

    private const string GROUND_TAG = "Ground";

    public Action<Sphere> SphereDisable;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;

        rend.material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(GROUND_TAG)) return;

        SphereDisable?.Invoke(this);
    }
}
