using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundButton : MonoBehaviour
{
	bool isBlack = true;
	public PlayerCameraControl pcc;
	public TextMeshProUGUI buttonText;

	public void ToggleBlack()
	{
		isBlack = !isBlack;

		pcc.yOffset = isBlack? 80 : 0;

		buttonText.text = isBlack ? "��� ���̱�" : "��� �����";

	}

	private void Start()
	{
		ToggleBlack();
	}

}

