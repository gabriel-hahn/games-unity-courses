using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }   
    }
}
