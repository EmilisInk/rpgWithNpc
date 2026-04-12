using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public ItemData equippedWeapon;
    public System.Action onEquipmentChanged;

    public void EquipWeapon(ItemData item)
    {
        if(!item.isWeapon) return;
        
        equippedWeapon = item;
        onEquipmentChanged?.Invoke();
    }

    public int GetDamage()
    {
        return equippedWeapon != null ? equippedWeapon.damage : 5;
    }
}
