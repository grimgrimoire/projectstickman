using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour, IShootBullet
{

    public float accuracy;
    public float fireRateDelay;
    public int bulletPerShot;
    public int damage;
    public float damageFalloff;
    public Transform weaponTarget;
    public GameObject bulletPrefab;
    public AudioClip gunfire;
    public AudioSource audioSource;

    bool isTriggerHeld;
    IEnumerator gunFire;
    Transform player;
    GameObject bullet;
    int targetMask;

    public GunScript()
    {
        gunFire = ShowGunFire();
        targetMask = ConstMask.MASK_ENEMY | ConstMask.MASK_WORLD | ConstMask.MASK_PROJECTILE | ConstMask.MASK_SPAWNAREA;
    }

    void Start()
    {
        bullet = GameObject.Instantiate(bulletPrefab);
    }

    void Update()
    {

    }

    public void HoldTrigger()
    {
        StartCoroutine(gunFire);
    }

    IEnumerator ShowGunFire()
    {
        player = GameObject.FindGameObjectWithTag(ConstMask.TAG_PLAYER).GetComponent<Transform>();
        do
        {
            Vector2 target = weaponTarget.transform.right * player.localScale.x;
            target += (Random.insideUnitCircle) * (1 - (accuracy * 0.01f));
            RaycastHit2D hit = Physics2D.Raycast(weaponTarget.transform.position, target, Mathf.Infinity, targetMask);
            if (hit)
            {
                bullet.transform.position = weaponTarget.position;
                bullet.GetComponent<Projectile>().SetTargetPosition(hit.point);
                if (hit.collider.gameObject.tag.Equals(ConstMask.TAG_ENEMY))
                {
                    hit.collider.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage, hit.point);
                }
                if (hit.collider.gameObject.tag.Equals(ConstMask.TAG_PROJECTILE))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            audioSource.PlayOneShot(gunfire);

            yield return new WaitForSeconds(fireRateDelay);
        } while (true);

    }

    public void RemoveTrigger()
    {
        StopCoroutine(gunFire);
    }
}
