using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour, IShootBullet
{

    public float accuracy;
    public float fireRateDelay;
    public int bulletPerShot;
    public int damage;
    public float damageFalloff;
    public bool isTwoHanded;
    public bool isAutomatic;
    public Transform weaponTarget;
    public GameObject bulletPrefab;
    public Animator animator;

    IEnumerator gunFire;
    Transform player;
    int targetMask;
    float manualFireRateDelay = 0;

    public GunScript()
    {
        gunFire = AutomaticGunFire();
        targetMask = ConstMask.MASK_ENEMY | ConstMask.MASK_WORLD | ConstMask.MASK_PROJECTILE | ConstMask.MASK_SPAWNAREA;
    }

    void Start()
    {
    }

    void Update()
    {

    }

    public void HoldTrigger()
    {
        if (isAutomatic)
            StartCoroutine(gunFire);
        else if (manualFireRateDelay <= 0)
            StartCoroutine(ManualGunFire());
    }

    IEnumerator AutomaticGunFire()
    {
        player = GameObject.FindGameObjectWithTag(ConstMask.TAG_PLAYER).GetComponent<Transform>();
        do
        {
            ShootBullets();
            yield return new WaitForSeconds(fireRateDelay);
        } while (isAutomatic);

    }

    IEnumerator ManualGunFire()
    {
        player = GameObject.FindGameObjectWithTag(ConstMask.TAG_PLAYER).GetComponent<Transform>();
        ShootBullets();
        manualFireRateDelay = fireRateDelay;
        while (manualFireRateDelay > 0)
        {
            manualFireRateDelay -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void ShootBullets()
    {
        for(int i=0; i<bulletPerShot; i++)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Vector2 target = weaponTarget.transform.right * player.localScale.x;
        target += (Random.insideUnitCircle) * (1 - (accuracy * 0.01f));
        RaycastHit2D hit = Physics2D.Raycast(weaponTarget.transform.position, target, Mathf.Infinity, targetMask);
        animator.Play("Shoot", 3);
        if (hit)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = weaponTarget.position;
            bullet.transform.rotation = weaponTarget.rotation;
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
    }

    public void RemoveTrigger()
    {
        if (isAutomatic)
            StopCoroutine(gunFire);
    }
}
