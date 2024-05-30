using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

	bool isClick;
	Vector3 clickPos;
	Vector3 clickCamPos;
	Vector3 screenRes = new Vector3(1f/1920f,1f/1080f,1f);
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClick = true;
			clickPos = Input.mousePosition;
			clickCamPos	= Camera.main.transform.position;
		}
		if (Input.GetMouseButtonUp(0))
		{
			isClick = false;
		}
		if (isClick)
		{
			var movePos = clickPos - Input.mousePosition;

			var targetPos = clickCamPos + movePos;



			Camera.main.transform.position = targetPos;

		}


	}



}

