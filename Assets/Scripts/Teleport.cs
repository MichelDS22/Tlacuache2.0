using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string level;
    public void TeleportBBY()
    {
        SceneManager.LoadScene(level);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        int Mapas = collision.gameObject.GetComponent<Player>().TotalMapas;
        if (collision.CompareTag("Player") && Mapas == 5)
        {
            TeleportBBY();
        }
    }
}
