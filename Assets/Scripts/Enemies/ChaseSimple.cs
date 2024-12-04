using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSimple : MonoBehaviour
{
    public float speed = 2f;
    public float playerAware = 5f;
    public float stopSmoothTime = 0.3f; // Tiempo para que la velocidad llegue a 0 al detenerse

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Transform playerTransform;

    private bool _facingRight;

    private float _currentSpeed; // Velocidad actual (puede ser 0 o speed)

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // If the player is within the detection range, chase the player
        if (distanceToPlayer <= playerAware)
        {
            _animator.SetBool("isChasing", true);
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float horizontalVelocity = direction.x * speed;

            // Asigna la velocidad actual utilizando la interpolación lineal (lerp)
            _currentSpeed = Mathf.Lerp(_currentSpeed, horizontalVelocity, stopSmoothTime * Time.deltaTime);
            _rigidbody.velocity = new Vector2(_currentSpeed, _rigidbody.velocity.y);

            // Flip the enemy's sprite based on the player's position
            if (direction.x > 0 && !_facingRight || direction.x < 0 && _facingRight)
            {
                Flip();
            }
        }
        else
        {
            // Si el jugador está fuera del rango de detección, disminuye la velocidad suavemente hasta llegar a 0
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, stopSmoothTime * Time.deltaTime);
            _rigidbody.velocity = new Vector2(_currentSpeed, _rigidbody.velocity.y);
            _animator.SetBool("isChasing", false);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
