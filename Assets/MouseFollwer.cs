using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class MouseFollwer : MonoBehaviour
{
	public TextMeshPro tmp;
	StringBuilder sb = new StringBuilder();

	private void Update()
	{
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

		sb.Clear();
		sb.AppendLine(((Vector2)Input.mousePosition).ToString());
		sb.AppendLine(((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).ToString());

		tmp.text = sb.ToString();
	}
}

