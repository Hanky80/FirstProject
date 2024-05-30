using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	Canvas canvas;

	private void Awake()
	{
		canvas = GetComponentInParent<Canvas>();
	}

	Vector2 savedPos;

	public void OnBeginDrag(PointerEventData eventData)
	{

		savedPos = transform.position;

	}

	public void OnDrag(PointerEventData eventData)
	{

		Vector3 view = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		Vector2 canvasPos = canvas.renderingDisplaySize * view;

		GetComponent<RectTransform>().anchoredPosition = canvasPos;

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = savedPos;
	}
}

