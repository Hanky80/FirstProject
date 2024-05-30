using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCtrl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public GameObject Controller;

	Transform cTransform;
	Transform sTransform;

	private void Start()
	{
		cTransform = Controller.transform;
		sTransform = cTransform.GetChild(0);
		Controller.SetActive(false);
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		print("OnBeginDrag");
		//cTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Controller.SetActive(true);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		print("OnEndDrag");
		Controller.SetActive(false);
	}
}

