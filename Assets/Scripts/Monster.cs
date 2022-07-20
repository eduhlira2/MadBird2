using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private ParticleSystem _particleSystem;

    private bool _hasDied;

    private void Awake()
    {
        _hasDied = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (ShouldDieFromCollision(col))
        {
            StartCoroutine(Die());
        }
            
    }

    private IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _sprite;
        _particleSystem.Play();
        
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
            return true;
        }
        
        var bird = col.gameObject.GetComponent<Bird>();
        if (bird)
        {
            return true;
        }
        
        return false;
    }
}
