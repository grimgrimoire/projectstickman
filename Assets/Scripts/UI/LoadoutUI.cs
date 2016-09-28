using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LoadoutUI : MonoBehaviour, IPointerClickHandler
{

    public RectTransform weaponListParent;
    public GameObject weaponUIPrefab;
    public Image primary;
    public Image secondary;
    public Text primaryName;
    public Text secondaryName;

    private bool isPrimaryList = true;

    // Use this for initialization
    void Start()
    {
        LoadPrimaryWeapon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == primary.name)
        {
            isPrimaryList = true;
            ClearList();
            LoadPrimaryWeapon();
        }
        else if (eventData.pointerEnter.name == secondary.name)
        {
            isPrimaryList = false;
            ClearList();
            LoadSecondaryWeapon();
        }
        else
        {
            Transform find = weaponListParent.FindChild(eventData.pointerEnter.name);
            if (find != null)
            {
                int index = find.GetSiblingIndex();
                Debug.Log("Item in index " + index);
            }
        }
    }

    public void LoadPrimaryWeapon()
    {
        for (int i = 0; i < WeaponsList.TOTAL_PRIMARY; i++)
        {
            AddWeaponUIToList(WeaponsList.GetPrimaryWeaponOnIndex(i));
        }
    }

    public void LoadSecondaryWeapon()
    {
        for (int i = 0; i < WeaponsList.TOTAL_SECONDARY; i++)
        {
            AddWeaponUIToList(WeaponsList.GetSecondaryWeaponOnIndex(i));
        }
    }

    private void ClearList()
    {
        for (int i = weaponListParent.childCount - 1; i >= 0; i--)
        {
            Destroy(weaponListParent.GetChild(i).gameObject);
        }
    }

    private void AddWeaponUIToList(WeaponsPrefab weapon)
    {
        GameObject instance = Instantiate(weaponUIPrefab);
        instance.transform.SetParent(weaponListParent);
        instance.name = weapon.name;
        Image image = instance.transform.FindChild("Image").GetComponent<Image>();
        Text name = instance.GetComponentInChildren<Text>();
        name.text = weapon.name;
        image.sprite = Resources.LoadAll<Sprite>("Images/Weapon1")[weapon.spriteIndex];
    }

}
