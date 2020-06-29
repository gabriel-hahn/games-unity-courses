using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int pickupChance;
    public float speed;
    public float timeBetweenAttacks;

    public GameObject[] pickups;
    public GameObject deathEffect;

    [HideInInspector]
    public Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void MoveCurrentEnemyToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            CheckDropWeapon();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void CheckDropWeapon()
    {
        int randomNumber = Random.Range(0, 101);

        if (randomNumber < pickupChance)
        {
            GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
            Instantiate(randomPickup, transform.position, transform.rotation);
        }
    }
}
