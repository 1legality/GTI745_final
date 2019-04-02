using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionPrefab = null;

    private Rigidbody _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 force)
    {
        _rigidbody.AddForce(force);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosionPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}