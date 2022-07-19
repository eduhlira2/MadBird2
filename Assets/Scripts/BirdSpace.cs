using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpace : Bird
{

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		base.OnCollisionEnter2D(col);

		var direction = _rigidbody2D.velocity.normalized;
		direction *= -1;
		_rigidbody2D.velocity = Vector2.zero;
		_rigidbody2D.velocity = direction;
	}

}
