using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Crate : MonoBehaviour
{
    [SerializeField]
    private AudioClip _hitSFX;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_rigidbody2D.velocity.magnitude > 1)
        {
            AudioSource.PlayClipAtPoint(_hitSFX, Vector3.zero, Single.MaxValue);
        }
    }

}
