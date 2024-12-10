using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Parallax : MonoBehaviour
{
    public float speed = 0.5f;  // La velocidad de desplazamiento
    public float tilingSpeed = 1f;  // La velocidad de tiling en el eje X
    private RawImage rawImage;  // El componente RawImage que contiene la textura

    void Start()
    {
        rawImage = GetComponent<RawImage>();  // Obtener el componente RawImage
    }

    void Update()
    {
        // Desplazar la textura en el eje X según el tiempo y la velocidad
        float offsetX = Time.time * speed;  // Controla el movimiento de la textura
        rawImage.uvRect = new Rect(offsetX * tilingSpeed, 0f, 1f, 1f); // Ajustamos uvRect para movimiento

        // Si la textura está configurada para hacer tiling, ajustamos la escala del RawImage
        rawImage.uvRect = new Rect(offsetX, 0f, 1f, 1f);
    }
}