using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Parallax : MonoBehaviour
{
    public float speed = 0.5f;
    private RawImage rawImage;
    private float currentOffsetX = 0f;

    public float Vel_Decrease_Value = .01f;
    public float Vel_Increase_Value = .0001f;

    public float Parallax_Vel_Alpha;
    public Player_Level_3_Controller Player;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {

        currentOffsetX += (speed * Parallax_Vel_Alpha) * Time.deltaTime;

        rawImage.uvRect = new Rect(currentOffsetX, 0f, rawImage.uvRect.width, rawImage.uvRect.height);


        if (Player.Game_Status_Switch == 3)
        {
            Vel_Decrease((int)0);
        }
        else
        {
            Vel_Increase(2.5f);
        }
    }



    void Vel_Increase(float Value_)
    {
        float Value = Value_;

        if (Parallax_Vel_Alpha < Value)
        {
            Parallax_Vel_Alpha += Vel_Increase_Value;

        }

    }
    void Vel_Decrease(float Value_)
    {
        float Value = Value_;

        if (Parallax_Vel_Alpha > Value)
        {
            Parallax_Vel_Alpha -= Vel_Decrease_Value;
        }
        if (Parallax_Vel_Alpha < Value)
        {
            Parallax_Vel_Alpha =Value;
        }
    }


}