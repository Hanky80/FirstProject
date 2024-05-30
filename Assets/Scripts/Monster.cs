using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	private enum State
	{
		Idle,
		Chase
	}

	State state;
	Rigidbody2D rb;
	Animator animator;
	SpriteRenderer spriteRenderer;

	public Transform target;
	public float moveSpeed;
	public float detectDistance;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		//target = FindObjectOfType<Player>().transform;
	}

	private void Update()
	{
		switch (state)
		{
			case State.Idle:
				if (Vector2.Distance(transform.position, target.position) < detectDistance)
				{
					state = State.Chase;
				}
				break;
			case State.Chase:

				if (target.position.x > transform.position.x)
				{
					spriteRenderer.flipX = true;
					rb.velocity = Vector2.right * moveSpeed;
				}
				else if (target.position.x < transform.position.x)
				{
					spriteRenderer.flipX = false;
					rb.velocity = Vector2.left * moveSpeed;
				}

				if (Vector2.Distance(transform.position, target.position) > detectDistance)
				{
					state = State.Idle;
				}

				break;
		}
		animator.SetInteger("State", (int)state);
	}

}
