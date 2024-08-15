using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject optionPrefab;
    [SerializeField] private GameObject[] weapons;

    public Transform weaponsContainer;
    public WeaponSwitch weaponSwitch;
    // Start is called before the first frame update
    void Start()
    {
        weaponsContainer = ShopManager._instance.weaponsContainer;
        weaponSwitch = weaponsContainer.GetComponent<WeaponSwitch>();
        PopulateShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateShop()
    {
        foreach (GameObject weapon in weapons)
        {
            GameObject weaponOption = Instantiate(optionPrefab,transform);
            WeaponOption weaponOptionScript = weaponOption.GetComponent<WeaponOption>();
            weaponOptionScript.Initialize(weapon, weaponSwitch, weaponsContainer);
        }
    }
}
