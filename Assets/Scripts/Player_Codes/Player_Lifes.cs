using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Lifes : MonoBehaviour
{
    public int health_Player;

    public Player_Controller Player;
    public Image[] Hearts;
    public SpriteRenderer Player_Sprite;


    private bool Damage = true;

    void Update()
    {
        if (health_Player == 0)
        {
            GameOver();
        }

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (health_Player <= i)
            {
                Hearts[i].gameObject.SetActive(false);
            }
            else
            {
                Hearts[i].gameObject.SetActive(true);
            }

        }
    }


    public void GameOver()
    {

        Player.Lock_Controls = true;

    }

    public void Finish_Game()
    {
        Player.Lock_Controls = true;

    }
    public void Damage_Player()
    {

        if (Damage)
        {
            Damage = false;
            StartCoroutine(Cooldown());
            StartCoroutine(Damage_Effect());
        }
    }

    IEnumerator Cooldown()
    {
        health_Player -= 1;
        yield return new WaitForSeconds(2F);
        Damage = true;
    }

    IEnumerator Damage_Effect()
    {
        while (true)
        {
            if (!Damage)
            {
                Player_Sprite.enabled = false;
                yield return new WaitForSeconds(.1f);
                Player_Sprite.enabled = true;
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                Player_Sprite.enabled = true;
                yield return null;
            }
        }


    }
}
