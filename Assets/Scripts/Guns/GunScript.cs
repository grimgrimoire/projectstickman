using UnityEngine;

public abstract class GunScript : MonoBehaviour, IShootBullet<GunParameter>
{
    public abstract void ShootBullet(GunParameter gparam);
}
