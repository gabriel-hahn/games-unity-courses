using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    private void Update()
    {
        UpdateWeaponRotation();
        ShotValidation();
    }

    private void UpdateWeaponRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 40, Vector3.forward);

        transform.rotation = rotation;
    }

    private void ShotValidation()
    {
        if (Input.GetMouseButton(0) && Time.time >= shotTime)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation * projectile.transform.rotation);
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
