using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol1 : MonoBehaviour
{
    public int damage = 1;
    public float speed = 1f;
	public float wallAware = 0.5f;
	public LayerMask groundLayer;
	public float playerAware = 3f;
	public float aimingTime = 0.5f;
	public float shootingTime = 1.5f;


	private Rigidbody2D _rigidbody;
	private Animator _animator;

	// Movement
	private Vector2 _movement;
	private bool _facingRight;

	private bool _isAttacking;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
	{
		if (transform.localScale.x < 0f) {
			_facingRight = false;
		} else if (transform.localScale.x > 0f) {
			_facingRight = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 direction = Vector2.right;

		if (_facingRight == false) {
			direction = Vector2.left;
		}

		if (_isAttacking == false) {
			if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer)) {
                Debug.DrawRay(transform.position, transform.forward, Color.green); 
				Flip();
			}
		}

	}

	private void FixedUpdate()
	{
		float horizontalVelocity = speed;

		if (_facingRight == false) {
			horizontalVelocity = horizontalVelocity * -1f;
		}

		if (_isAttacking) {
			horizontalVelocity = 0f;
		}
		_rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
	}

	private void LateUpdate()
	{
		_animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (_isAttacking == false && collision.CompareTag("Player")) {
			StartCoroutine(Attack());
		}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		float localScaleX = transform.localScale.x;
		localScaleX = localScaleX * -1f;
		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}

	private IEnumerator Attack()
	{
		
		_isAttacking = true;

		yield return new WaitForSeconds(aimingTime);

		_animator.SetTrigger("Attack");

		yield return new WaitForSeconds(shootingTime);

		_isAttacking = false;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking == true && collision.CompareTag("Player"))
        {
            //player get hurt
            collision.SendMessageUpwards("AddDamage", damage);
            Debug.Log("DAÑO BRUTAL PAPEADO");
        }
    }

    private void OnEnable()
    {
        _isAttacking = false;
    }
	private void OnDisable()
	{
		StopCoroutine("AimAndShoot");
		_isAttacking = false;
	}
}
