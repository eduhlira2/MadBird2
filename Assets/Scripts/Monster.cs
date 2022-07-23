using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
public class Monster : MonoBehaviour
{
    public int _pointsToAddMonster;
    public int _pointsToAddCrate;
    
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private AudioClip _hitSFX;
    
    [SerializeField]
    private AudioClip _deathSFX;

    private Rigidbody2D _rigidbody2D;

    private bool _hasDied;

    private void Awake()
    {
        
        _hasDied = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (ShouldDieFromCollision(col))
        {
            StartCoroutine(Die());
        }
        else if(_rigidbody2D.velocity.magnitude > 1f)
        {
            AudioSource.PlayClipAtPoint(_hitSFX, Vector3.zero, Single.MaxValue);
        }
            
    }

    private IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _sprite;
        _particleSystem.Play();
        AudioSource.PlayClipAtPoint(_deathSFX, Vector3.zero, Single.MaxValue);
        
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private bool ShouldDieFromCollision(Collision2D col)
    {
        if (_hasDied)
        {
            return false;
        }
        
        if (col.contacts[0].normal.y < -0.5)
        {
            PointControl.points = PointControl.points + _pointsToAddCrate;
            Debug.Log("morreu pela Caixa");
            return true;
            
        }
        
        var bird = col.gameObject.GetComponent<Bird>();
        if (bird)
        {
            Debug.Log("morreu pelo Bird");
            PointControl.points = PointControl.points+_pointsToAddMonster;
            return true;
        }
        
        return false;
    }
}
