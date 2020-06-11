using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    public float attackSpeed;

    private float attackTime;

    private void Update()
    {
        if (player != null)
        {
            UpdateEnemyMovement();
        }
    }

    private void UpdateEnemyMovement()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            return;
        }

        if (Time.time >= attackTime)
        {
            StartCoroutine(Attack());
            attackTime = Time.time + timeBetweenAttacks;
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
