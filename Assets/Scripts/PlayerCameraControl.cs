using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
	public float moveCamDistance;
	public float yOffset;
	private void Update()
	{
		float inputX = Player.GetAxisRaw("Horizontal");
		transform.localPosition = new Vector3(inputX * moveCamDistance, yOffset, 0);
	}
}
