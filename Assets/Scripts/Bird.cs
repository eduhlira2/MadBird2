using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour
{
    private bool _canTouch = true;
    private int _birdLife = 3;
    public GameObject[] _extrabirds;
    public GameObject redBird;
    [SerializeField]
    private SpriteRenderer _spriteHelmet;

    [SerializeField]
    private Animator _Extrabird2;
    [SerializeField]
    private Animator _Extrabird3;
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
        _spriteHelmet = _spriteHelmet.GetComponent<SpriteRenderer>();
        _render = GetComponent<SpriteRenderer>();
        _Extrabird2 = _Extrabird2.GetComponent<Animator>();
        _Extrabird3 = _Extrabird3.GetComponent<Animator>();
    }

    private void Start()
    {
        //Debug.Log("A vida do bird eh: "+ _birdLife);
       _render.color = Color.clear;
       _spriteHelmet.color = Color.clear;
       _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
        StartCoroutine(StartAnimBird());
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (_canTouch == true)
        {
            _birdLife = _birdLife - 1;
            _canTouch = false;
        }
        
        StartCoroutine(ResetAfterDelay());
        
    }

    protected virtual IEnumerator StartAnimBird()
    {
        yield return new WaitForSeconds(2);
        _render.color = Color.white;
        _spriteHelmet.color = Color.white;
        if (_birdLife == 3)
        {
            _extrabirds[0].SetActive(false); 
        }
        if (_birdLife == 2)
        {
            _Extrabird2.Play("Bird2ExtraLife");
            _extrabirds[1].SetActive(false); 
        }
        if (_birdLife == 1)
        {
            _Extrabird3.Play("Bird3ExtraLife");
            _extrabirds[2].SetActive(false); 
        }
        
    }
    protected virtual IEnumerator ResetAfterDelay()
    {
        //Debug.Log("A vida do bird eh: "+ _birdLife);
        _particleTrail.Pause();
        yield return new WaitForSeconds(3);
        _render.color = Color.clear;
        _spriteHelmet.color = Color.clear;
        _canTouch = true;
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _particleTrail.Clear();
       
        if (_birdLife == 2)
        {
            _Extrabird2.Play("Bird2ExtraLife");
        }
        if (_birdLife == 1)
        {
            _Extrabird3.Play("Bird3ExtraLife");
        }
        StartCoroutine(StartAnimBird());
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
