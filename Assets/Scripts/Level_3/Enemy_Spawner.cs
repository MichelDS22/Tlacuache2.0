using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public Player_Level_3_Controller Player;
    public GameObject Enemy_clon;
    public float EnemySpawnTime = 2f;
    private float Enemy_SpawnTime_Alpha = 1f;

    private float TimerEnemySpawn;

    private Vector3 Spawner_Position;

    void Update()
    {
        Spawner_Position = new Vector3(transform.position.x, transform.position.y);
        if (Player.Game_Status_Switch == 2)
        {
            SpawnTime_Decrease(.6f);
            SpawnLoop();
        }
    }
    void SpawnTime_Decrease(float Value_)
    {
        float Value = Value_;

        if (Enemy_SpawnTime_Alpha > Value)
        {
            Enemy_SpawnTime_Alpha -= .0001f;

            if (Enemy_SpawnTime_Alpha < Value)
            {
                Enemy_SpawnTime_Alpha = Value;
            }

        }
    }

    void SpawnLoop()
    {

        TimerEnemySpawn += Time.deltaTime;
        if (TimerEnemySpawn >= (EnemySpawnTime*Enemy_SpawnTime_Alpha))
        {
            GameObject clone = Instantiate(Enemy_clon, transform.position, Quaternion.identity);
            clone.tag = "EnemySpawned";
            TimerEnemySpawn = 0f;
        }
    }

}
   
