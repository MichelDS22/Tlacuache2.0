using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform OriginPoint;
    public float longIdleTime = 5f;
    public float speed = 3.5f;
    public float jumpForce = 3.5f;

    public Transform groundCheck; //para saber que está apoyado en el suelo
    public LayerMask groundLayer; //El tipo de caja de layer, seleccionar la layer que es el suelo, para checar la colisión
    public float groundCheckRadius; // para ver que tan grande es el piso


    private Rigidbody2D _rigibody;
    private Animator _animator;

    //LongIdle
    private float _longIdleTimer;

    //Vectores de movimiento
    private Vector2 _movement;
    private bool _facingRigth = true;
    private bool _isGrounded; // estoy en el suelo si o no

    //Ataque
    private bool _isAttacking;



    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        gameObject.transform.position = new Vector2(OriginPoint.transform.position.x, OriginPoint.transform.position.y);
    }
    void Update() //Aquí checamos los comandos durante cada ciclo, para ver si el jugador decide moverse o hacer algo
    {
        if (_isAttacking == false)
        {
            //Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal"); //Raw para dar el valor final
            _movement = new Vector2(horizontalInput, 0f);
            //Flipping
            if (horizontalInput < 0f && _facingRigth == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRigth == false)
            {
                Flip();
            }
        }

        //Checa el piso
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // el overlap sirve para checar con la layer, como el piso está en una layer y el jugador en otra, si se tocan, aquí se hará verdadero

        //Checa el salto
        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            _rigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Checa si Ataca
        if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigibody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }
    private void FixedUpdate() //después del input del jugador, aquí le decimos que se mueva el componente del rigibody
    {
        if (_isAttacking == false)
        {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigibody.velocity = new Vector2(horizontalVelocity, _rigibody.velocity.y);
        }
    }

    private void LateUpdate() //una vez que terminamos de mover, aquí cambiamos las animaciones correspondientes
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigibody.velocity.y);

        //Animator
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) //de la layer del amimator 
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
        // Long Idle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _longIdleTimer += Time.deltaTime;

            if (_longIdleTimer >= longIdleTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            _longIdleTimer = 0f; 
        }
    }

    private void Flip() //para que el sprite voltee hacia la dirección que estoy moviendo
    {
        _facingRigth = !_facingRigth; // si es falso voltea el valor a verdadero y viceverza
        float localSclaeX = transform.localScale.x;
        localSclaeX = localSclaeX * -1f;
        transform.localScale = new Vector3(localSclaeX, transform.localScale.y, transform.localScale.z);
    }
}
