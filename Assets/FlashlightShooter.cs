using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightShooter : MonoBehaviour
{
    [Header("Flashlight parameters")]
    public GameObject bulletPrefab;
    public Transform backPoint;
    public Transform frontPoint;
    public Transform firePoint;

    [Header("Bullet parameters")]
    public AudioClip laserSound;
    public float bulletSpeed = 50f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            Shoot();
    }

    void Shoot()
    {
        if (bulletPrefab == null || backPoint == null || frontPoint == null) return;

        Vector3 shootDirection = (frontPoint.position - backPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Automatically add Rigidbody if doesn't exist
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
            rb = bullet.AddComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Ignore the collision between the bullet and the flashlight
        LightBullet lightBullet = bullet.GetComponent<LightBullet>();
        if (lightBullet != null)
            lightBullet.SetIgnoreRoot(transform.root.gameObject);

        PlaySound(laserSound, bullet.transform.position);

        Destroy(bullet, 3f);
    }

    void PlaySound(AudioClip sound, Vector3 position)
    {
        if (sound != null)
            AudioSource.PlayClipAtPoint(sound, position);
    }
}
