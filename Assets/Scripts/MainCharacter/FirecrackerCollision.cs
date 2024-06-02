using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FirecrackerCollision : MonoBehaviour
{
    private Rigidbody rb;

    private bool groundHit;

    public float fovRadius;

    public LayerMask enemyMask;

    public AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fovRadius = 100.0f;
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(StartFirecracker), 1.0f);
    }

    private void StartFirecracker()
    {
        audioSource.Play();
        Invoke(nameof(DestroyFirecracker), 9.0f);
    }

    private void DestroyFirecracker()
    {
        gameObject.SetActive(false);
    }
}
