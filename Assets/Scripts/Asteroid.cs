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

	private bool _hasLaunch = false;
	private Rigidbody2D _rigidbody2D;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_hasLaunch = false;
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
		}

		var boss = collision.gameObject.GetComponent<Boss>();
		if (boss)
		{
			StartCoroutine(Explosion());
		}
	}

	IEnumerator Explosion()
	{
		_rigidbody2D.velocity = Vector2.zero;

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
