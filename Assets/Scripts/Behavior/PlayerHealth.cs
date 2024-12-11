using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Transform OriginPoint;

    public int totalHealth = 3;
    public RectTransform vidas1;
    public RectTransform vidas2;
    public RectTransform vidas3;


    //Game Over

    private int health;
    private float hearthSize = 21f;

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
        VidasHUD();

    }
    public void AddHealth(int amount)
    {
        health += amount;

        //Max
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        VidasHUD();
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
        _animator.enabled = false;
        _controller.enabled = false;

        //para el respawn
        health = 3;
        VidasHUD();
        _controller.transform.position = new Vector2(OriginPoint.transform.position.x, OriginPoint.transform.position.y);

    }
    public void  VidasHUD()
    {
        if (health == 3)
        {
            vidas3.gameObject.SetActive(true);
            vidas2.gameObject.SetActive(true);
            vidas1.gameObject.SetActive(true);

            Debug.Log("Player got damaged. His current health is " + health);
        }
        if (health == 2)
        {
            vidas3.gameObject.SetActive(false);
            vidas2.gameObject.SetActive(true);
            vidas1.gameObject.SetActive(true);
            Debug.Log("Player got damaged. His current health is " + health);
        }
        if(health == 1)
        {
            vidas3.gameObject.SetActive(false);
            vidas2.gameObject.SetActive(false);
            vidas1.gameObject.SetActive(true);

            Debug.Log("Player got damaged. His current health is " + health);
        }
    }
}