using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public GameObject explosion;
    public GameObject soundObject;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }

        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
