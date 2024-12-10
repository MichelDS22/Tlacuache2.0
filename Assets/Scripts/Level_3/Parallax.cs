using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Parallax : MonoBehaviour
{
    public float speed = 0.5f;  // La velocidad de desplazamiento
    public float tilingSpeed = 1f;  // La velocidad de tiling en el eje X
    private RawImage rawImage;  // El componente RawImage que contiene la textura
     public float Parallax_Vel_Alpha;
    public Player_Level_3_Controller Player;
    void Start()
    {
        rawImage = GetComponent<RawImage>();  // Obtener el componente RawImage
    }

    void Update()
    {

            // Desplazar la textura en el eje X según el tiempo y la velocidad
            float offsetX = Time.time * speed;  // Controla el movimiento de la textura
            rawImage.uvRect = new Rect(offsetX * (tilingSpeed*Parallax_Vel_Alpha), 0f, 1f, 1f); // Ajustamos uvRect para movimiento


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