using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase2 : MonoBehaviour
{
    public int damage = 1;
    public float speed = 2f;
    public float verticalSpeed = 1f; // Velocidad vertical para el movimiento en el eje Y
    public float playerAware = 5f;
    public float stopSmoothTime = 0.3f; // Tiempo para que la velocidad llegue a 0 al detenerse

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Transform playerTransform;

    private bool _facingRight;
    private bool _returning = false;

    private bool _isChasing; // Variable para controlar si está persiguiendo al jugador
    private float _currentSpeed; // Velocidad actual en el eje X (puede ser 0 o speed)
    private float _currentVerticalSpeed; // Velocidad actual en el eje Y (puede ser 0 o verticalSpeed)
    private float _lastPlayerYPosition; // Última posición Y del jugador

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _lastPlayerYPosition = playerTransform.position.y;
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // If the player is within the detection range, chase the player
        if (distanceToPlayer <= playerAware)
        {
            _isChasing = true;
            _animator.SetBool("isChasing", true);
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float horizontalVelocity = direction.x * speed;

            // Asigna la velocidad actual en el eje X utilizando la interpolación lineal (lerp)
            _currentSpeed = Mathf.Lerp(_currentSpeed, horizontalVelocity, stopSmoothTime * Time.deltaTime);
            _rigidbody.velocity = new Vector2(_currentSpeed, _rigidbody.velocity.y); // Mantenemos la velocidad en el eje Y

            // Flip the enemy's sprite based on the player's position
            if (direction.x > 0 && !_facingRight || direction.x < 0 && _facingRight)
            {
                Flip();
            }

            // Controlamos el movimiento en el eje Y solo si el jugador cambia su posición en ese eje
            if (playerTransform.position.y != _lastPlayerYPosition)
            {
                _currentVerticalSpeed = Mathf.Lerp(_currentVerticalSpeed, verticalSpeed, stopSmoothTime * Time.deltaTime);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _currentVerticalSpeed);
            }

            // Actualizamos la última posición Y del jugador para la próxima comparación
            _lastPlayerYPosition = playerTransform.position.y;
        }
        else
        {
            _isChasing = false;
            // Si el jugador está fuera del rango de detección, disminuye la velocidad suavemente hasta llegar a 0 en el eje X
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, stopSmoothTime * Time.deltaTime);
            _rigidbody.velocity = new Vector2(_currentSpeed, _rigidbody.velocity.y); // Mantenemos la velocidad en el eje Y
            _animator.SetBool("isChasing", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_returning == false && collision.CompareTag("Player"))
        {
            //player get hurt
            collision.SendMessageUpwards("AddDamage", damage);
            Debug.Log("DAÑO BRUTAL PAPEADO");
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
