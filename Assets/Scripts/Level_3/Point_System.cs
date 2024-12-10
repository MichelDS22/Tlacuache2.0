using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Point_System : MonoBehaviour
{
    public int puntos = 0;
    public int Max_puntos = 0;

    public GameObject Lose;
    public GameObject Win;

    public TextMeshProUGUI puntosTexto;
    public TextMeshProUGUI puntosTexto_Win;
    public TextMeshProUGUI puntosTexto_Lose;

    private void Start()
    {
        puntosTexto.text = "Puntos: " + puntos.ToString();
        Win.gameObject.SetActive(false);
        Lose.gameObject.SetActive(false);
    }
    public void GanarPunto()
    {
        puntos++; 
        ActualizarUI();
    }

    public void Win_Lose()
    {
        if (puntos >= Max_puntos)
        {
            Win.gameObject.SetActive(true);
            Lose.gameObject.SetActive(false);
        }
        else
        {
            Win.gameObject.SetActive(false);
            Lose.gameObject.SetActive(true);
        }
    }
    void ActualizarUI()
    {
        if (puntosTexto != null)
        {
            puntosTexto.text = "Puntos: " + puntos.ToString();
            puntosTexto_Win.text = "Score " + puntos.ToString();
            puntosTexto_Lose.text = "Score " + puntos.ToString();
        }
    }
}
