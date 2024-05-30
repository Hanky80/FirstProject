using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어가 통과 가능한 발판
public class ThroughablePlat : MonoBehaviour
{
	//플레이어가 트리거에 들어왔을 때, 발판과 상호작용 하지 않도록 함.
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.layer = LayerMask.NameToLayer("Throughing");
		}
	}
	//플레이어가 트리거를 통과했을 때, 발판과 상호작용 하도록 함.
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.layer = LayerMask.NameToLayer("Normal");
		}
	}
}
