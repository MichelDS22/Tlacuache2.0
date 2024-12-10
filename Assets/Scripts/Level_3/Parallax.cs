using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Parallax : MonoBehaviour
{
    public float speed = 0.5f;  
    public float tilingSpeed = 1f;  
    private RawImage rawImage;  
    private float currentOffsetX = 0f;  


    public float Parallax_Vel_Alpha;
    public Player_Level_3_Controller Player;

    void Start()
    {
        rawImage = GetComponent<RawImage>();  
    }

    void Update()
    {

        currentOffsetX += (speed*Parallax_Vel_Alpha) * Time.deltaTime;  

        rawImage.uvRect = new Rect(currentOffsetX * tilingSpeed, 0f, 1f, 1f);


        if (Player.Game_Status_Switch == 3)
        {
            Vel_Decrease(0);
        }
        Vel_Increase(2.5f);
    }
    void Vel_Increase(float Value_)
    {
        float Value = Value_;

        if (Parallax_Vel_Alpha < Value)
        {
            Parallax_Vel_Alpha += .0001f;
        }
    }
    void Vel_Decrease(float Value_)
    {
        float Value = Value_;

        if (Parallax_Vel_Alpha > Value)
        {
            Parallax_Vel_Alpha -= .01f;

            if (Parallax_Vel_Alpha < Value)
            {
                Parallax_Vel_Alpha = Value;
            }

        }
    }


}