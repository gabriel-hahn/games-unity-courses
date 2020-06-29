using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    public Enemy[] enemies;
    public float spawnOffset;

    private int halfHealth;
    private Animator animator;

    private void Start()
    {
        halfHealth = health / 2;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
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
