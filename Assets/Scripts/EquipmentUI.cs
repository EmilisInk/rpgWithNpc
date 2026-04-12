using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Equipment equipment;
    public Image weaponIcon;
    
    void Start()
    {
        equipment.onEquipmentChanged += Refresh;
        Refresh();
    }

   
    void Refresh()
    {
        if (equipment.equippedWeapon != null)
        {
            weaponIcon.sprite = equipment.equippedWeapon.icon;
            weaponIcon.enabled = true;
        }
        else
        {
            weaponIcon.enabled = false;
        }
    }
}
