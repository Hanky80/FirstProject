using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�÷��̾ ��� ������ ����
public class ThroughablePlat : MonoBehaviour
{
	//�÷��̾ Ʈ���ſ� ������ ��, ���ǰ� ��ȣ�ۿ� ���� �ʵ��� ��.
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.layer = LayerMask.NameToLayer("Throughing");
		}
	}
	//�÷��̾ Ʈ���Ÿ� ������� ��, ���ǰ� ��ȣ�ۿ� �ϵ��� ��.
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.gameObject.layer = LayerMask.NameToLayer("Normal");
		}
	}
}
