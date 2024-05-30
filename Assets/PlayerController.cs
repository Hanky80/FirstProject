using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;
	private Rigidbody2D rb;
	public Canvas canvas;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	Vector2 savedAncorMin;
	Vector2 savedAncorMax;
	
	private void Update()
	{
		Move(Input.GetAxis("Horizontal"));
		if (Input.GetButtonDown("Jump"))
		{
			print("Jump");
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}

		Vector3 view = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		Vector2 canvasPos = canvas.renderingDisplaySize * view;

		//print(canvasPos);

	}

	void Move(float dirX)
	{
		Vector2 vel = rb.velocity;
		vel.x = dirX * moveSpeed;
		rb.velocity = vel;
	}

}

