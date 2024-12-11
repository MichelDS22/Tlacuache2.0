using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Transform OriginPoint;
    public int totalHealth = 3;

    public RectTransform vidas1;
    public RectTransform vidas2;
    public RectTransform vidas3;

    // Game Over
    private int health;
    private bool _isDead = false; 
    private bool _isInvulnerable = false; 

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Player _controller;

    public GameObject HUD;
    public GameObject MenuPerder;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Player>();
    }

    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        if (_isDead || _isInvulnerable) return; 

        health -= amount;

        StartCoroutine(VisualFeedback()); // Cambia el color y aplica cooldown.

        if (health <= 0)
        {
            health = 0;
            Die(); // Llamar a la lógica de muerte.
        }

        VidasHUD();
    }

    public void AddHealth(int amount)
    {
        if (_isDead) return; // No se puede curar si está muerto.

        health += amount;

        // Máximo permitido.
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        VidasHUD();
        Debug.Log("Player got some life. His current health is " + health);
    }

    private IEnumerator VisualFeedback()
    {
        _isInvulnerable = true; // Activa el estado de invulnerabilidad.
        Debug.Log("Player is invulnerable");

        // Cambiar color a rojo.
        _renderer.color = Color.red;

        // Espera el tiempo del cooldown.
        yield return new WaitForSeconds(2.5f);

        // Restaurar el color original.
        _renderer.color = Color.white;

        // Desactiva el estado de invulnerabilidad.
        _isInvulnerable = false;
        Debug.Log("Player is no longer invulnerable");
    }

    private void Die()
    {
        _isDead = true; // Marca al jugador como muerto.
        DisableEnemies();

        // Lógica de la pantalla de derrota.
        MenuPerder.gameObject.SetActive(true);
        HUD.SetActive(false);

        // Desactiva el control y animación del jugador.
        _animator.enabled = false;
        _controller.enabled = false;
        GetComponent<Collider2D>().enabled = false; // Desactiva las colisiones.
    }

    private void DisableEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void OnEnable()
    {
        health = totalHealth;
        _isDead = false; // Reinicia el estado del jugador.
        GetComponent<Collider2D>().enabled = true; // Reactiva el colisionador.
    }

    public void VidasHUD()
    {
        vidas3.gameObject.SetActive(health >= 3);
        vidas2.gameObject.SetActive(health >= 2);
        vidas1.gameObject.SetActive(health >= 1);

        Debug.Log("Player got damaged. His current health is " + health);
    }
}
