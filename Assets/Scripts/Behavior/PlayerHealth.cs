using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Transform OriginPoint;

    public int totalHealth = 3;
    public RectTransform hearthUI;

    //Game Over
    public RectTransform gameOverMenu;
    public GameObject hordes;

    private int health;
    private float hearthSize = 16f;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Player _controller;


    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }
    public void AddDamage(int amount)
    {
        health -= amount;

        //Visual
        StartCoroutine("VisualFeedback");
        //Game Over
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
            DisableEnemies();
        }
        hearthUI.sizeDelta = new Vector2(hearthSize * health, hearthSize);
        Debug.Log("Player got damaged. His current health is " + health);
    }
    public void AddHealth(int amount)
    {
        health += amount;

        //Max
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        hearthUI.sizeDelta = new Vector2(hearthSize * health, hearthSize);
        Debug.Log("Player got some life. His current health is " + health);
    }
    private IEnumerable VisualFeedback()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
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
    }
    private void OnDisable()
    {
        gameOverMenu.gameObject.SetActive(true);
        hordes.SetActive(false);
        _animator.enabled = false;
        _controller.enabled = false;

        //para el respawn
        health = 3;
        hearthUI.sizeDelta = new Vector2(hearthSize * health, hearthSize);
        _controller.transform.position = new Vector2(OriginPoint.transform.position.x, OriginPoint.transform.position.y);

    }
}