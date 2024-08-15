using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public GameObject[] weapons;                // The array that holds all the weapons that the player has
    public int startingWeaponIndex = 0;         // The weapon index that the player will start with
    private int weaponIndex;


    public List<GameObject> trailEffects; //Store trail effects to destroy them if necessary

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the starting active weapon is the one selected by the user in startingWeaponIndex
        weaponIndex = startingWeaponIndex;
        SetActiveWeapon(weaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
            SetActiveWeapon(0);
        if (Input.GetKeyDown("2"))
            SetActiveWeapon(1);
        if (Input.GetKeyDown("3"))
            SetActiveWeapon(2);
        if (Input.GetKeyDown("4"))
            SetActiveWeapon(3);
        if (Input.GetKeyDown("5"))
            SetActiveWeapon(4);
        if (Input.GetKeyDown("6"))
            SetActiveWeapon(5);
        if (Input.GetKeyDown("7"))
            SetActiveWeapon(6);
        if (Input.GetKeyDown("8"))
            SetActiveWeapon(7);
        if (Input.GetKeyDown("9"))
            SetActiveWeapon(8);

        // Allow the user to scroll through the weapons
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            NextWeapon();
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            PreviousWeapon();
    }

    public void SetActiveWeapon(int index)
    {
        DestroyTrails();

        // Make sure this weapon exists before trying to switch to it
        if (index >= weapons.Length || index < 0)
        {
            Debug.LogWarning("Tried to switch to a weapon that does not exist.  Make sure you have all the correct weapons in your weapons array.");
            return;
        }

        // Make sure the weaponIndex references the correct weapon
        weaponIndex = index;

        // Start be deactivating all weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i])
                weapons[i].SetActive(false);
        }

        // Activate the one weapon that we want
        if (weapons[index])
            weapons[index].SetActive(true);
    }

    public void NextWeapon()
    {
        weaponIndex++;
        if (weaponIndex > weapons.Length - 1)
            weaponIndex = 0;
        SetActiveWeapon(weaponIndex);
    }

    public void PreviousWeapon()
    {
        weaponIndex--;
        if (weaponIndex < 0)
            weaponIndex = weapons.Length - 1;
        SetActiveWeapon(weaponIndex);
    }

    void DestroyTrails() 
    {
        foreach (GameObject trail in trailEffects)
        {
            if(trail)
                Destroy(trail);
        }

        trailEffects.Clear();
    }
}
