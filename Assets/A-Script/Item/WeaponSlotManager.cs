using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamgeCollider leftHandDmgCollider;
    DamgeCollider rightHandDmgCollider;

    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }
    public void LoadWeaponOnSLot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDmgCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDmgCollider();
        }
    }

    #region Handle Weapon's dng Collider
    private void LoadLeftWeaponDmgCollider()
    {
        leftHandDmgCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamgeCollider>();
    }
    private void LoadRightWeaponDmgCollider()
    {
        rightHandDmgCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamgeCollider>();
    }
    public void OpenRightDmgCollider()
    {
        rightHandDmgCollider.EnableDmgCollider();
    }
    public void OpenLeftDmgCollider()
    {
        leftHandDmgCollider.EnableDmgCollider();
    }
    public void CloseRightDmgCollider()
    {
        rightHandDmgCollider.DisableDmgCollider();
    }
    public void CloseLeftDmgColiider()
    {
        leftHandDmgCollider.DisableDmgCollider();
    }
    #endregion
}
