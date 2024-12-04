using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public string scene;
    // public GameObject instructionsText; // Referencia al objeto UI de texto

    private void Start()
    {
        // Desactivar el objeto UI de texto al inicio del juego
        // instructionsText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cargar la escena definida en la variable "scene"
            SceneManager.LoadScene(scene);

            // Desactivar el objeto UI de texto cuando se cambia de escena
            //instructionsText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Colisionó con la puerta, activar el objeto UI de texto y mostrar las instrucciones
            //instructionsText.SetActive(true);

            // Si el objeto UI de texto usa TextMeshPro, puedes configurar el texto aquí
            //TMP_Text tmpText = instructionsText.GetComponent<TMP_Text>();
            //tmpText.text = "Press E";

            // Cambiar el tamaño del texto
            //tmpText.fontSize = 12; // Ajusta el tamaño según lo que necesites

            // Ajustar el espaciado entre caracteres para hacer el texto más pequeño
            //tmpText.characterSpacing = -2.5f; // Ajusta el valor según lo que necesites
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // El jugador ha salido del collider de la puerta, desactivar el objeto UI de texto
            //instructionsText.SetActive(false);
        }
    }
}