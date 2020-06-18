using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float timeBetweenAttacks;

    [HideInInspector]
    public Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void MoveCurrentEnemyToPlayer()
    {
        print("Moving " + transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
