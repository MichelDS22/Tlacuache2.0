using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int AttackDamage = 1; 
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;

        }
        else
        {
            _isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador está atacando y el objeto tiene la etiqueta correcta.
        if (_isAttacking)
        {

            if (collision.CompareTag("Enemy") || collision.CompareTag("Big Bullet"))
            {
                // Enviar el daño al enemigo.
                collision.SendMessageUpwards("AddDamage", AttackDamage);
            }
        }
    }
}
