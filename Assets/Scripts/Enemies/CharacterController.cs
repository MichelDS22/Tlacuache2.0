using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    private float fireAnimationTimer;
    private bool isFiring;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        fireAnimationTimer = 0f;
        isFiring = false;
    }

    private void Update()
    {
        if (!isFiring)
        {
            fireAnimationTimer += Time.deltaTime;

            if (fireAnimationTimer >= 10f)
            {
                isFiring = true;
                fireAnimationTimer = 0f; // Reiniciar el tiempo para que no se siga acumulando
                _animator.SetBool("isFiring", true);
            }
        }
        else
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                // Detecta cuando la animación "Fire" ha terminado
                isFiring = false;
                _animator.SetBool("isFiring", false); // Volver a Idle
            }
        }
    }
}


