using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Iventory", menuName = "scriptable objects/inventory data", order = 1)]
public class InventoryData : ScriptableObject
{
    public ItemData[] inventory = new ItemData[40];
    public int gold;

    /// <summary>
    /// add item to inventory in stack or new slot
    /// </summary>
    /// <param name="item"></param>
    /// <returns>return true when placed in inventory, return false when there is no space</returns>
    public bool AddItem(ItemData item)
    {
        ItemData itemClone = item.Clone();
        Debug.Log("");
        bool foundStack = false;
        //Search for place to stack
        if (item.stackable)
        {
            for(int i = 0; i<inventory.GetLength(0); i++)
            {
                if (inventory[i] == null)
                {
                    continue;
                }
                if (item.name == inventory[i].name)
                {
                    inventory[i].stackAmount++;
                    foundStack = true;
                    PlayerSFX.instance.ItemPickUpSFX();
                    return true;
                }
            }
        }
        //put item in new slot if not stackable or no stack was found
        if(!item.stackable || !foundStack)
        {
            for(int i = 0; i<inventory.GetLength(0); i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = itemClone;
                    PlayerSFX.instance.ItemPickUpSFX();
                    return true;
                }
            }
        
        }
        //no space was found
        return false;
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        for(int i = 0; i<inventory.Length; i++)
        {
            if (inventory[i] == itemToRemove)
            {
                inventory[i] = null;
                //may cause memory issues
            }
        }
    }
}
