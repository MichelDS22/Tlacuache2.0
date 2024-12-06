using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{   
    
    [SerializeField] private float Jumpforce = 10f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float GroundDistance = 0.25f;
    [SerializeField] private float JumpTime = 0.3f;

    public float speed;
    public Animator Player_Animator;

    private Rigidbody2D RigidB;
    private Vector2 moveVel;



    private bool IsGrounded = false;
    private bool IsJumping = false;
    private float JumpTimer;
    private float horizontalInput;

    public bool Lock_Controls;
    void Start()
    {
        RigidB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

;

        if (!Lock_Controls) 
        {
            IsGrounded = Physics2D.OverlapCircle(feetpos.position, GroundDistance, GroundLayer);
            horizontalInput = Input.GetAxisRaw("Horizontal");

            Jumping();
            idle_Player();
            Player_Animator.SetFloat("Horizontal_Run", Input.GetAxisRaw("Horizontal"));

        }
        else
        {
            Player_Animator.SetFloat("Horizontal_Run", 0);
        }
    }

    void FixedUpdate()
    {
       RigidB.velocity = new Vector2(horizontalInput * speed , RigidB.velocity.y);    
    }


    public void Jumping()
    {
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            print("jump");
            IsJumping = true;
            RigidB.velocity = Vector2.up * Jumpforce;
        }
        if (IsJumping && Input.GetButton("Jump"))
        {

            if (JumpTimer < JumpTime)
            {
                RigidB.velocity = Vector2.up * Jumpforce;

                JumpTimer += Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            IsJumping = false;
            JumpTimer = 0;
        }
    }


    public void idle_Player()
    {

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case 1:
                Player_Animator.SetFloat("Horizontal_Idle", 1);
                Player_Animator.SetFloat("Vertical_Idle", 0);
                break;
            case -1:
                Player_Animator.SetFloat("Horizontal_Idle", -1);
                Player_Animator.SetFloat("Vertical_Idle", 0);
                break;
            default: break;
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (feetpos == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(feetpos.position, GroundDistance);
    }

}


