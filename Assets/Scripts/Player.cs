using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private enum State
	{
		Idle = 0,
		Walk = 1,
		Jump = 2,
		Sit = 3,
	}

	State state;
    SpriteRenderer spriteRenderer;
	Rigidbody2D rb;
	Animator animator;

	public float moveSpeed; //이동 속도
	public float jumpSpeed; //점프했을때 이동속도
	public float jumpPower; //점프력

	bool isJumping; //점프중인지 검사하는 키
	bool isSitting; //앉은 자세인지 검사하는 키
	
	bool onPlat; //내려갈 수 있는 발판 위인지 검사하는 키
	bool isGrounded; //땅에 발이 닿아있는 상태인지 검사

	private void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		state = State.Idle;
	}

	public static float GetAxisRaw(string s)
	{
		return 1f;
	}

	bool GetButtonDown(string s)
	{
		return false;
	}

	private void Update()
	{
		switch (state)
		{
			case State.Idle: //서있는 상태
				//Walk 상태로 전이
				if (GetAxisRaw("Horizontal")!= 0)
				{
					state = State.Walk;
				}
				//Jump 상태로 전이 조건(Condition)
				if (GetButtonDown("Jump")) //점프 키가 눌리면
				{
					rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); //점프를 하고
					//상태 전이(Transition)
					state = State.Jump;//현재 상태는 점프중으로
				}
				//Sit 상태로 전이 조건
				if(GetAxisRaw("Vertical") < 0)
				{
					state = State.Sit;
				}
				break;
			case State.Walk:
				float x = GetAxisRaw("Horizontal"); //x축 입력
				rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);//rigidbody에 운동량 대입
				//Idle 상태로 전이
				if (rb.velocity.x == 0)
				{
					state = State.Idle;
					break;
				}
				spriteRenderer.flipX = x < 0;
				//Jump 상태로 전이
				if (GetButtonDown("Jump")) //점프 키가 눌리면
				{
					rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); //점프를 하고
					state = State.Jump;//현재 상태는 점프중으로
				}
				//Sit 상태로 전이
				if (GetAxisRaw("Vertical") < 0)
				{
					state = State.Sit;
				}
				break;
			case State.Jump:
				//상태 전이 조건(Condition)
				rb.velocity = new Vector2(GetAxisRaw("Horizontal") * jumpSpeed, rb.velocity.y);//rigidbody에 운동량 대입
				if (isGrounded && rb.velocity.y == 0) //발이 땅에 닿아 있고, Y축으로 운동량이 없을때
				{
					//상태 전이(Transition)
					state = State.Idle;// 서있는 상태로 되돌아감
				}
				break;
			case State.Sit:
				if(GetAxisRaw("Vertical") >= 0)
				{
					state = State.Idle;
				}
				if (GetButtonDown("Jump") && onPlat)
				{
					gameObject.layer = LayerMask.NameToLayer("Throughing");
					Invoke("ReturnLayer", 0.5f);//0.5초 후에 원래 레이어로 되돌아감
				}
				break;
		}

		animator.SetInteger("State", (int)state);

		//isSitting = GetAxisRaw("Vertical") < 0; //앉은 자세 검사
		////점프 입력
		//if (GetButtonDown("Jump") && isJumping == false && isSitting == false)
		//{
		//	//점프중에는 점프를 못하게
		//	isJumping = true;
		//	//점프력 만큼 Y축 위로 AddForce
		//	rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		//}

		////밑으로 누른 상태로 점프키를 누르면
		//if (isSitting && onPlat && GetButtonDown("Jump"))
		//{
		//	//바닥 플랫폼을 넘어가도록
		//	gameObject.layer = LayerMask.NameToLayer("Throughing");
		//	Invoke("ReturnLayer", 0.5f);//0.25초 후에 원래 레이어로 되돌아감
		//}
	}

	void ReturnLayer()
	{
		gameObject.layer = LayerMask.NameToLayer("Normal");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		isGrounded = true;
		if(collision.collider.tag == "Plat")
		{
			onPlat = true;
		}

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		isGrounded = false;
		onPlat = false;
	}

}
