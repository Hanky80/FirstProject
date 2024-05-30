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

	public float moveSpeed; //�̵� �ӵ�
	public float jumpSpeed; //���������� �̵��ӵ�
	public float jumpPower; //������

	bool isJumping; //���������� �˻��ϴ� Ű
	bool isSitting; //���� �ڼ����� �˻��ϴ� Ű
	
	bool onPlat; //������ �� �ִ� ���� ������ �˻��ϴ� Ű
	bool isGrounded; //���� ���� ����ִ� �������� �˻�

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
			case State.Idle: //���ִ� ����
				//Walk ���·� ����
				if (GetAxisRaw("Horizontal")!= 0)
				{
					state = State.Walk;
				}
				//Jump ���·� ���� ����(Condition)
				if (GetButtonDown("Jump")) //���� Ű�� ������
				{
					rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); //������ �ϰ�
					//���� ����(Transition)
					state = State.Jump;//���� ���´� ����������
				}
				//Sit ���·� ���� ����
				if(GetAxisRaw("Vertical") < 0)
				{
					state = State.Sit;
				}
				break;
			case State.Walk:
				float x = GetAxisRaw("Horizontal"); //x�� �Է�
				rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);//rigidbody�� ��� ����
				//Idle ���·� ����
				if (rb.velocity.x == 0)
				{
					state = State.Idle;
					break;
				}
				spriteRenderer.flipX = x < 0;
				//Jump ���·� ����
				if (GetButtonDown("Jump")) //���� Ű�� ������
				{
					rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); //������ �ϰ�
					state = State.Jump;//���� ���´� ����������
				}
				//Sit ���·� ����
				if (GetAxisRaw("Vertical") < 0)
				{
					state = State.Sit;
				}
				break;
			case State.Jump:
				//���� ���� ����(Condition)
				rb.velocity = new Vector2(GetAxisRaw("Horizontal") * jumpSpeed, rb.velocity.y);//rigidbody�� ��� ����
				if (isGrounded && rb.velocity.y == 0) //���� ���� ��� �ְ�, Y������ ����� ������
				{
					//���� ����(Transition)
					state = State.Idle;// ���ִ� ���·� �ǵ��ư�
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
					Invoke("ReturnLayer", 0.5f);//0.5�� �Ŀ� ���� ���̾�� �ǵ��ư�
				}
				break;
		}

		animator.SetInteger("State", (int)state);

		//isSitting = GetAxisRaw("Vertical") < 0; //���� �ڼ� �˻�
		////���� �Է�
		//if (GetButtonDown("Jump") && isJumping == false && isSitting == false)
		//{
		//	//�����߿��� ������ ���ϰ�
		//	isJumping = true;
		//	//������ ��ŭ Y�� ���� AddForce
		//	rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		//}

		////������ ���� ���·� ����Ű�� ������
		//if (isSitting && onPlat && GetButtonDown("Jump"))
		//{
		//	//�ٴ� �÷����� �Ѿ����
		//	gameObject.layer = LayerMask.NameToLayer("Throughing");
		//	Invoke("ReturnLayer", 0.5f);//0.25�� �Ŀ� ���� ���̾�� �ǵ��ư�
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
