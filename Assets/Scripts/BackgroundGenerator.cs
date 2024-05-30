using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
	/* 플레이어가 생성된 배경 경계면에 도달하면
	 * 플레이어가 이동하는 방향에 새로운 배경 오브젝트를 생성하고
	 * 타일링된 위치에 배치*/

	public GameObject[] backgroundPrefabs;	//생성할 배경 프리팹
	public Transform playerTransform;   //플레이어의 위치를 파악해서 배경 생성 위치를 결정
	public Transform lastBackground;    //마지막으로 생성된 배경 오브젝트의 transform
	public Transform remainBackground;  //경계면에서 플레이어가 이동하면 사라질 배경 오브젝트의 transform

	SpriteRenderer sampleSR;			//크기를 판단할 기준 SpriteRenderer
	float sampleWidth;					//기준 크기(x축 넓이)

	public float generateDistance; //플레이어가 경계면으로부터 얼마까지 다가왔을때 새 Background를 생성할지

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
		//remainBackground를 삭제
		if (remainBackground != null)
		{
			//플레이어가 remainBackground에서 generateDistance보다 멀어지면
			float maxX = remainBackground.position.x + (sampleWidth / 2);
			float minX = remainBackground.position.x - (sampleWidth / 2);

			//플레이어가 이전에 생성되어있던 백그라운드로부터 기준거리 이상 멀어졌는지
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

			//remain Background가 없으면 새 백그라운드를 생성
			float maxX = lastBackground.position.x + (sampleWidth / 2); // 오른쪽 끝 위치 (x좌표)
			float minX = lastBackground.position.x - (sampleWidth / 2); // 왼쪽 끝 위치 (x좌표)

			//플레이어가 오른쪽 끝에 도달했는지
			if (playerTransform.position.x > maxX - generateDistance)
			{
				remainBackground = lastBackground;
				lastBackground =
				Instantiate(backgroundPrefab,
					lastBackground.position + new Vector3(sampleWidth, 0, 0),
					Quaternion.identity).transform;
			}

			//플레이어가 왼쪽 끝에 도달했는지
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
