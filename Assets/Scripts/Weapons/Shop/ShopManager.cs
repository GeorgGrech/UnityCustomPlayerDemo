using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public static ShopManager _instance;

    public int money;

    public List<string> unlockedWeapons;
    public Transform weaponsContainer;
    // Start is called before the first frame update
    void Start()
    {
        weaponsContainer = GameObject.Find("WeaponsContainer").transform;
    }
    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void DecrementMoney(int amount)
    {
        money -= amount;
        if (money < 0)
        {
            money = 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {

    }

    public void SetUnlockedWeapon(string weaponName)
    {

    }

    public void SetStarterUnlocked()
    {
        WeaponSwitch weaponSwitch = weaponsContainer.GetComponent<WeaponSwitch>();
        foreach(string weaponName in unlockedWeapons)
        {
            unlockedWeapons.Add(weaponName);
        }
    }
}
