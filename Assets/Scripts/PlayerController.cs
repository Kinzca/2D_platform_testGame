using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRB ;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator  anim;
    private SpriteRenderer theSR;//SRָͼƬRBָ������ص����

    public float knockBackForce, knockBackLength;
    private float knockBackCurrent;

    public float bounceForce;

    public bool stopInput;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPause && !stopInput)
        {
            if (knockBackCurrent <= 0)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);//����Ƿ��ڵ�����
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);//���ˮƽ�ƶ�

                if (theRB.velocity.x < 0)//��ҳ������
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }

                if (isGrounded)
                {
                    canDoubleJump = true;//�ڵ�����ʱ���������
                }
                if (Input.GetButtonDown("Jump"))//��Ծ����
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);


                        SoundManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)//����������
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;


                            SoundManager.instance.PlaySFX(10);
                        }
                    }

                }
            }
            else
            {
                knockBackCurrent -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }
        }
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf .Abs ( theRB .velocity .x ));
    }
    public void KnockBack()
    {
        knockBackCurrent = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce );

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x,bounceForce);
    }
}
