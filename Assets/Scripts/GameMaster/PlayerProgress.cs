public class PlayerProgress
{

    int primaryWeapon;
    int secondaryWeapon;

    public void SetPrimary(int primaryIndex)
    {
        primaryWeapon = primaryIndex;
    }

    public void SetSecondary(int secondaryIndex)
    {
        secondaryWeapon = secondaryIndex;
    }

    public int GetPrimaryWeapon()
    {
        return primaryWeapon;
    }

    public int GetSecondaryWeapon()
    {
        return secondaryWeapon;
    }

    public void SetPrimarySecondary(int primary, int secondary)
    {
        primaryWeapon = primary;
        secondaryWeapon = secondary;
    }
}