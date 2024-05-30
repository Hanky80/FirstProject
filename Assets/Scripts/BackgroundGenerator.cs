using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
	/* �÷��̾ ������ ��� ���鿡 �����ϸ�
	 * �÷��̾ �̵��ϴ� ���⿡ ���ο� ��� ������Ʈ�� �����ϰ�
	 * Ÿ�ϸ��� ��ġ�� ��ġ*/

	public GameObject[] backgroundPrefabs;	//������ ��� ������
	public Transform playerTransform;   //�÷��̾��� ��ġ�� �ľ��ؼ� ��� ���� ��ġ�� ����
	public Transform lastBackground;    //���������� ������ ��� ������Ʈ�� transform
	public Transform remainBackground;  //���鿡�� �÷��̾ �̵��ϸ� ����� ��� ������Ʈ�� transform

	SpriteRenderer sampleSR;			//ũ�⸦ �Ǵ��� ���� SpriteRenderer
	float sampleWidth;					//���� ũ��(x�� ����)

	public float generateDistance; //�÷��̾ �������κ��� �󸶱��� �ٰ������� �� Background�� ��������

	private void Awake()
	{
		sampleSR = backgroundPrefabs[0].transform.GetChild(0).GetComponent<SpriteRenderer>();
		sampleWidth = sampleSR.size.x;
	}

	public int selectedNum;

	private void Start()
	{
		selectedNum = Random.Range(0, backgroundPrefabs.Length);

		GameObject willDestroy = lastBackground.gameObject;

		lastBackground = Instantiate(backgroundPrefabs[selectedNum], lastBackground.position, Quaternion.identity).transform;

		Destroy(willDestroy);
	}

	private void Update()
	{
		//remainBackground�� ����
		if (remainBackground != null)
		{
			//�÷��̾ remainBackground���� generateDistance���� �־�����
			float maxX = remainBackground.position.x + (sampleWidth / 2);
			float minX = remainBackground.position.x - (sampleWidth / 2);

			//�÷��̾ ������ �����Ǿ��ִ� ��׶���κ��� ���ذŸ� �̻� �־�������
			if(playerTransform.position.x > maxX + generateDistance || 
				playerTransform.position.x < minX - generateDistance)
			{
				Destroy(remainBackground.gameObject);
				remainBackground = null;
			}

		}
		else
		{

			var ranNum = Random.Range(0, backgroundPrefabs.Length);
			var backgroundPrefab = backgroundPrefabs[ranNum];

			//remain Background�� ������ �� ��׶��带 ����
			float maxX = lastBackground.position.x + (sampleWidth / 2); // ������ �� ��ġ (x��ǥ)
			float minX = lastBackground.position.x - (sampleWidth / 2); // ���� �� ��ġ (x��ǥ)

			//�÷��̾ ������ ���� �����ߴ���
			if (playerTransform.position.x > maxX - generateDistance)
			{
				remainBackground = lastBackground;
				lastBackground =
				Instantiate(backgroundPrefab,
					lastBackground.position + new Vector3(sampleWidth, 0, 0),
					Quaternion.identity).transform;
			}

			//�÷��̾ ���� ���� �����ߴ���
			if (playerTransform.position.x < minX + generateDistance)
			{
				remainBackground = lastBackground;
				lastBackground =
				Instantiate(backgroundPrefab,
					lastBackground.position - new Vector3(sampleWidth, 0, 0),
					Quaternion.identity).transform;
			}
		}
	}
}
