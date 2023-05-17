using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; //Скорость персонажа
    public float jumpForce = 7f; //Сила прыжка
    public Transform groundCheck;
    public Transform fstCheck;
    public Transform sndCheck;
    public Transform endCheck;
    public Transform lavaCheck;//Объект проверяющий землю под персонажем
    public float checkRadius; //Область проверки
    public LayerMask whatIsGround;
    public LayerMask whatIsEnd;
    public LayerMask whatIsLava;
    public LayerMask whatIsFst;
    public LayerMask whatIsSnd;//Маска слоя
    private bool isGrounded;
    private bool isLava;
    private bool isEnd;
    private bool isFst;
    private bool isSnd;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public Text MyText;
    private float moveHorizontal;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        MyText = GetComponent<Text>();
    }

    private void MovementLogic()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = true;
        }
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        if (isGrounded)
            if (moveHorizontal != 0)
                anim.Play("Run");
            else if (moveHorizontal == 0)
                anim.Play("idle");
    }
    
    private void VLave()
    {
        if (isLava)
        {
            rb.transform.position = new Vector3(-22.0f, -2.0f, 0.0f);
        }
    }
    private void SndBoostChek()
    {
        if (isSnd)
        {
            rb.transform.localScale = new Vector3(1.0f, 0.98f, 1.0f);
        }
    }
    private void EndChek()
    {
        if (isEnd)
        {
            rb.transform.position = new Vector3(96.63f, -2.09f, 0.0f);
        }
    }
    private void FstBoostChek()
    {
        if (isFst)
        {
            rb.transform.localScale = new Vector3(2.0f, 1.98f, 1.0f);
            jumpForce = 10f;
        }
    }
    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.Play("jump");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
            rb.transform.position = new Vector3(-22.0f, -2.0f, 0.0f);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
       checkRadius, whatIsGround);
        isLava = Physics2D.OverlapCircle(lavaCheck.position,
       checkRadius, whatIsLava);
        isFst = Physics2D.OverlapCircle(fstCheck.position,
       checkRadius, whatIsFst);
        isSnd = Physics2D.OverlapCircle(sndCheck.position,
       checkRadius, whatIsSnd);
        isEnd = Physics2D.OverlapCircle(endCheck.position,
       checkRadius, whatIsEnd);
        MovementLogic();
        JumpLogic();
        VLave();
        FstBoostChek();
        SndBoostChek();
        EndChek();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
