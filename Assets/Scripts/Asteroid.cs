using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float _forceLaunch = 500;

	public int _pointsToAddAsteroid;
		
	private bool _hasLaunch = false;
	private Rigidbody2D _rigidbody2D;
	
	[SerializeField]
	private AudioClip _hitSFX;
	
	[SerializeField]
	private AudioClip _explosionSFX;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{

		var bird = collision.gameObject.GetComponent<Bird>();
		if (bird && !_hasLaunch)
		{
			_hasLaunch = true;
			var direction = collision.contacts[0].normal;
			direction.y *= -1;
			_rigidbody2D.AddForce(direction * _forceLaunch);
			AudioSource.PlayClipAtPoint(_hitSFX, Vector3.zero, Single.MaxValue);
		}

		var boss = collision.gameObject.GetComponent<Boss>();
		if (boss)
		{
			PointControl.points = PointControl.points + _pointsToAddAsteroid;
			StartCoroutine(Explosion());
		}
	}

	IEnumerator Explosion()
	{
		_rigidbody2D.velocity = Vector2.zero;
		AudioSource.PlayClipAtPoint(_explosionSFX, Vector3.zero, Single.MaxValue);

		var animator = GetComponent<Animator>();
		animator.SetBool("HasCollided", true);

		var collider = GetComponent<Collider2D>();
		collider.isTrigger = true;

		yield return new WaitForSeconds(5);

		gameObject.SetActive(false);
	}

	private void OnBecameInvisible()
	{
		if(gameObject.active)
			StartCoroutine(Explosion());
	}
}
