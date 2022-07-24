using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
public class GreenBird : Bird
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    
    [SerializeField]
    private float _forceOfExplosion;

    private CircleCollider2D _collider2D;
    
    private bool _alreadyExploded;

    [SerializeField]
    private AudioClip _explosionSFX;

    protected override void Awake()
    {
        base.Awake();
        _alreadyExploded = false;
        _collider2D = GetComponent<CircleCollider2D>();
    }

    protected override IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("CutScene");
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        if (_alreadyExploded)
            return;
        
        _alreadyExploded = true;
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        _particleSystem.Play();
        _collider2D.radius *= _forceOfExplosion;
        AudioSource.PlayClipAtPoint(_explosionSFX, Vector3.zero, Single.MaxValue);
        yield return new WaitForSeconds(0.3f);
        _collider2D.radius /= _forceOfExplosion;
    }
}
