using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Equipment equipment;
    public Inventory inventory;
    public Transform gridParent;
    public GameObject slotPrefab;
    
    InventorySlotUI[] slots;
    
    void Start()
    {
        slots = new InventorySlotUI[inventory.maxSlots];

        for (int i = 0; i < slots.Length; i++)
        {
            var obj = Instantiate(slotPrefab, gridParent);
            slots[i] = obj.GetComponent<InventorySlotUI>();
            slots[i].equipment = equipment;
        }

        inventory.onInventoryChanged += Refresh;
        Refresh();
    }

    void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.slots.Count)
            {
                slots[i].Set(inventory.slots[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gridParent.gameObject.SetActive(!gridParent.gameObject.activeSelf);
        }
    }
}
