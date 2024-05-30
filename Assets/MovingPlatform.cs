using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public float moveHeight;
	public float moveSpeed;
	Vector3 startPos;
	Vector3 targetPos;
	private void Start()
	{
		startPos = transform.position;
		targetPos = transform.position + Vector3.up * moveHeight;
	}

	private void FixedUpdate()
	{

		transform.position = Vector3.Lerp(startPos, targetPos, (Mathf.Sin(Time.time* moveSpeed) + 1) * .5f);

	}

}

