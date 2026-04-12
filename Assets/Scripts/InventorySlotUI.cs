using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Equipment equipment;
    public Image icon;
    public TextMeshProUGUI amountText;

    private InventorySlot currentSlot;

    private void Start()
    {
        Clear();
    }

    public void OnClick()
    {
        if (currentSlot != null && currentSlot.item.isWeapon)
        {
            print(equipment == null);
            equipment.EquipWeapon(currentSlot.item);
        }
    }
    
    public void Set(InventorySlot slot)
    {
        currentSlot = slot;
        
        icon.sprite = slot.item.icon;
        icon.enabled = true;
        
        amountText.text = slot.amount.ToString();
    }

    public void Clear()
    {
        icon.enabled = false;
        amountText.text = "";
    }
}
