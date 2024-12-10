using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level_3_Controller : MonoBehaviour
{
    public int Game_Status_Switch;
    public Animator Player_Animator;
    public Point_System Player_Points;

    [SerializeField] private Rigidbody2D rigidB;
    [SerializeField] private float Jumpforce = 10f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform feetpos;
    [SerializeField] private float GroundDistance = 0.25f;
    [SerializeField] private float JumpTime = 0.3f;

    private bool IsGrounded = false;
    private bool IsJumping = false;
    private float JumpTimer;


    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(feetpos.position, GroundDistance, GroundLayer);

        switch (Game_Status_Switch)
        {
            case 1:
                Player_Animator.SetInteger("Anim_Status", 0);
                break;

            case 2:
                Player_Animator.SetInteger("Anim_Status", 1);
                Animations_Logic();
                Player_Move();
                break;

            case 3:
                Player_Animator.SetInteger("Anim_Status", 2);
                Player_Points.Win_Lose();
                break;

            default:
                Game_Status_Switch = 1;
                break;
        }



        void Player_Move()
        {
            if (IsGrounded && Input.GetButtonDown("Jump"))
            {
                Player_Animator.SetTrigger("Jump");
                IsJumping = true;
                rigidB.velocity = Vector2.up * Jumpforce;
            }
            if (IsJumping && Input.GetButton("Jump"))
            {
                if (JumpTimer < JumpTime)
                {
                    rigidB.velocity = Vector2.up * Jumpforce;

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



        void Animations_Logic()
        {

            if (!IsGrounded)
            {
                Player_Animator.SetBool("Ground", false);

            }
            else if (IsGrounded)
            {

                Player_Animator.SetBool("Ground", true);
            }
        }


       
    }
}


