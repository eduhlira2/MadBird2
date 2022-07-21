using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[SelectionBase]
public class Monster : MonoBehaviour
{
    public int _pointsToAddMonster;
    public int _pointsToAddCrate;
    
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private ParticleSystem _particleSystem;

    private bool _hasDied;

    private void Awake()
    {
        
        _hasDied = false;
        
    }

    private void Update()
    {
        
        
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
