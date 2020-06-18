using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;
    public Transform shotPoint;
    public GameObject enemyBullet;

    private float attackTime;
    private Animator animator;

    public override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        MoveCurrentEnemy();
    }

    private void MoveCurrentEnemy()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            MoveCurrentEnemyToPlayer();
        }

        if (Time.time >= attackTime)
        {
            attackTime = Time.time + timeBetweenAttacks;
            animator.SetTrigger("attack");
        }
    }

    public void RangedAttack()
    {
        UpdateBulletRotation();

    }

    private void UpdateBulletRotation()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 40, Vector3.forward);

        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
