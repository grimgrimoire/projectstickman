using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour, IShootBullet
{

    public float accuracy;
    public float fireRateDelay;
    public int bulletPerShot;
    public int minDamage;
    public int maxDamage;
    public float damageFalloff;
    public bool isTwoHanded;
    public bool isAutomatic;
    public int penetration = 1;
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
        targetMask = ConstMask.MASK_ENEMY | ConstMask.MASK_WORLD | ConstMask.MASK_SPAWNAREA;
    }

    void Start()
    {
        animator.SetBool("IsTwoHanded", isTwoHanded);
    }

    void Update()
    {

    }

    public void ChangeWeapon(WeaponsPrefab weapon)
    {
        accuracy = weapon.accuracy;
        fireRateDelay = weapon.fireRateDelay;
        bulletPerShot = weapon.bulletPerShot;
        minDamage = weapon.minDamage;
        maxDamage = weapon.maxDamage;
        damageFalloff = weapon.damageFalloff;
        isTwoHanded = weapon.isTwoHanded;
        //isAutomatic = weapon.isAutomatic;
        isAutomatic = true;
        penetration = weapon.penetration;
        weaponTarget.localPosition = weapon.weaponTargetPosition;
        animator.SetBool("IsTwoHanded", isTwoHanded);
        GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Images/Weapon1")[weapon.spriteIndex];
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
        yield return new WaitForEndOfFrame();
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
        for (int i = 0; i < bulletPerShot; i++)
        {
            if (penetration == 1)
                ShootBullet();
            else
                PenetrationBullet();
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
            CreateBulletObject(hit.point);
            BulletHitAction(hit);
            //if (hit.collider.gameObject.tag.Equals(ConstMask.TAG_PROJECTILE))
            //{
            //    Destroy(hit.collider.gameObject);
            //}
        }
    }

    private void PenetrationBullet()
    {
        Vector2 target = weaponTarget.transform.right * player.localScale.x;
        target += (Random.insideUnitCircle) * (1 - (accuracy * 0.01f));
        RaycastHit2D[] multiHit = Physics2D.RaycastAll(weaponTarget.transform.position, target, Mathf.Infinity, targetMask);
        int totalHit = 0;
        foreach(RaycastHit2D hit in multiHit)
        {
            totalHit++;
            BulletHitAction(hit);
            if (totalHit == penetration || totalHit == multiHit.Length || hit.collider.tag == ConstMask.TAG_WORLD) {
                CreateBulletObject(hit.point);
                break;
            }
        }
    }

    private void BulletHitAction(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.tag.Equals(ConstMask.TAG_ENEMY))
        {
            if (Vector2.Distance(weaponTarget.position, hit.point) < damageFalloff)
                hit.collider.gameObject.GetComponent<BaseEnemy>().TakeDamage(Random.Range(minDamage, maxDamage), hit.point);
        }
    }

    private void CreateBulletObject(Vector2 hit)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = weaponTarget.position;
        bullet.transform.rotation = weaponTarget.rotation;
        if (player.transform.localScale.x < 0)
            bullet.transform.Rotate(0, 0, 180);
        bullet.GetComponent<Projectile>().SetTargetPosition(hit);
    }

    public void RemoveTrigger()
    {
        if (isAutomatic)
            StopCoroutine(gunFire);
    }
}
