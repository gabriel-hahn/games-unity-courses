using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float timeBetweenSummons;
    public float summonTime;
    public Enemy enemyToSummon;

    private Vector2 targetPosition;
    private Animator animator;

    public override void Start()
    {
        base.Start();

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = new Vector2(randomX, randomY);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);

            return;
        }

        animator.SetBool("isRunning", false);

        if (Time.time >= summonTime)
        {
            summonTime = Time.time + timeBetweenSummons;
            animator.SetTrigger("summon");
        }
    }

    public void Summon()
    {
        if (player == null)
        {
            return;
        }

        Instantiate(enemyToSummon, transform.position, transform.rotation);
    }
}
