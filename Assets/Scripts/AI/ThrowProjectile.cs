using UnityEngine;
using System.Collections;

public class ThrowProjectile : MonoBehaviour {

    public GameObject prefabSpear;
    public GameObject bossSpear;
    public GameObject parent;
    Vector2 playerPosition;

    public void TargetPlayer()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
    }

    public void UpdateTargeting()
    {
        Vector2 newPlayerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        if ((parent.transform.localScale.x > 0 && newPlayerPosition.x > parent.transform.position.x) || (parent.transform.localScale.x < 0 && newPlayerPosition.x < parent.transform.position.x))
            TargetPlayer();
    }

    public void ThrowProjectileAtPlayer()
    {
        UpdateTargeting();
        GameObject spear = Instantiate(prefabSpear);
        spear.transform.position = bossSpear.transform.position;
        spear.transform.rotation = bossSpear.transform.rotation;
        spear.GetComponent<Projectile>().minSpeed = 40;
        spear.GetComponent<Projectile>().speed = 40;
        spear.GetComponent<Projectile>().SetTargetPosition(playerPosition + new Vector2(0, 1));
    }
}
