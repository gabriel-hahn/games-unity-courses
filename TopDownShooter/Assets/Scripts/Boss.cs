using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    public Enemy[] enemies;
    public float spawnOffset;
    public GameObject blood;
    public GameObject deathEffect;

    private int halfHealth;
    private Animator animator;
    private Slider healthBar;
    private SceneTransitions sceneTransitions;

    private void Start()
    {
        halfHealth = health / 2;
        animator = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();

        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;

        if (health <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
        }

        if (health < halfHealth)
        {
            animator.SetTrigger("stage2");
        }

        InstantiateNewRandomEnemy();
    }

    private void InstantiateNewRandomEnemy()
    {
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
