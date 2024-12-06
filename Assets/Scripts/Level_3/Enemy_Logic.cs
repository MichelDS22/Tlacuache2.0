using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Logic : MonoBehaviour
{

    public Player_Level_3_Controller Player;

    [SerializeField] private Rigidbody2D rigidB_Enemy;
    [SerializeField] private float Enemy_Vel = 2f;
    [SerializeField] private float Enemy_Vel_Alpha = 1f;
    void Update()
    {
        if (tag == "EnemySpawned")
        {
            rigidB_Enemy.velocity = Vector2.left * (Enemy_Vel*Enemy_Vel_Alpha);
        }

        if (Player.Game_Status_Switch == 3)
        {
            Vel_Decrease(0);
        }
            Vel_Increase(2.5f);


    }

    void Vel_Increase(float Value_)
    {
        float Value = Value_;

        if (Enemy_Vel_Alpha < Value)
        {
            Enemy_Vel_Alpha += .0001f;
        }
    }
    void Vel_Decrease(float Value_)
    {
        float Value = Value_;

        if (Enemy_Vel_Alpha > Value)
        {
            Enemy_Vel_Alpha -= .01f;

            if (Enemy_Vel_Alpha < Value)
            {
                Enemy_Vel_Alpha = Value;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.Game_Status_Switch = 3;
        }

        if (collision.tag == "Enemy_Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
