using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int EHealth = 1; // Vida inicial del enemigo.
    

    // M�todo para recibir da�o.
    public void AddDamage()
    {
        EHealth -= 1; // Reducir la vida seg�n el da�o recibido.

        if (EHealth <= 0 )
        {
            EHealth = 0; // Evitar que sea negativo (opcional).
            gameObject.SetActive(false); // Desactivar el enemigo.
        }
    }
}
