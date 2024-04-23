using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponItem leftWeapon;
    public WeaponItem rightWeapon;

    public WeaponSlotManager weaponSlotManager;
    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
        weaponSlotManager.LoadWeaponOnSLot(leftWeapon, true);
        weaponSlotManager.LoadWeaponOnSLot(rightWeapon, false);
    }
}
