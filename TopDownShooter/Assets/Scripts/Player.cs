﻿using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int speed;
    public int health;

    private new Rigidbody2D rigidbody;
    private Vector2 moveAmount;
    private Animator animator;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        animator.SetBool("isRunning", moveInput != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        UpdateHealthUI(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        GameObject currentWeapon = GameObject.FindGameObjectWithTag("Weapon");
        Instantiate(weaponToEquip, new Vector2(currentWeapon.transform.position.x, currentWeapon.transform.position.y), currentWeapon.transform.rotation, transform);
        Destroy(currentWeapon);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
        }
    }
}
