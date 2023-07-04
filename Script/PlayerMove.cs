using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxspeed;
    public float jumpPower;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //���� ����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Isjumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("Isjumping", true);
        }


        //�ӵ����� �����
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //���� ��ȯ
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //�ȴ� ������ ����
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("Iswalking", false);
        }
        else
        {
            anim.SetBool("Iswalking", true);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //�ִ� �ӵ� ���� (������)
        if (rigid.velocity.x > maxspeed)
        {
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
        }//�ִ�ӵ� ���� (����)
        else if (rigid.velocity.x < maxspeed * (-1))
        {
            rigid.velocity = new Vector2(maxspeed * (-1), rigid.velocity.y);
        }

        //���� ���������� Ȯ��
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("Isjumping", false);

                }
            }
        }
    }




}
