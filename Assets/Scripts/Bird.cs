using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour
{

    [SerializeField]
    private float _force = 1000;
    [SerializeField]
    private float _maxDragDistance = 4.5f;
    [SerializeField]
    private GameObject _guideLine;
    [SerializeField]
    private ParticleSystem _particleTrail;
    
    protected Rigidbody2D _rigidbody2D;
    private SpriteRenderer _render;
    private Vector2 _startPosition;
    

    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(ResetAfterDelay());
    }

    protected virtual IEnumerator ResetAfterDelay()
    {
        _particleTrail.Pause();
        yield return new WaitForSeconds(3);
        
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _particleTrail.Clear();
    }

    private void OnMouseDown()
    {
        _render.color = Color.red;
        _guideLine.SetActive(true);
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidbody2D.position;
        var diretion = _startPosition - currentPosition;
        diretion.Normalize();
        
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(diretion * _force);
        
        _guideLine.SetActive(false);
        _particleTrail.Play();
        _render.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var desiredPosition = (mousePosition.x > _startPosition.x) ? new Vector2(_startPosition.x, mousePosition.y) : mousePosition;

        var distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            var direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        _rigidbody2D.position = desiredPosition;

        var guideDirection = _startPosition - desiredPosition;
        guideDirection.Normalize();
        _guideLine.transform.right = guideDirection;
        _guideLine.GetComponent<GuideLine>()
            .SetTotal(Vector2.Distance(desiredPosition, _startPosition));
    }

	private void OnBecameInvisible()
	{
        if(gameObject.active)
            StartCoroutine(ResetAfterDelay());
	}
}
