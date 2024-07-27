using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "scriptable objects/item data", order = 2)]
public class ItemData : ScriptableObject
{
    public string name;
    public int value;
    public Sprite icon;
    public GameObject gamePrefab;
    public int stackAmount;
    public bool stackable;
    public GameObject UIPrefab;
    public Vector3 spawnOffset;
    //need to do something about stacking items
    

    public ItemData Clone()
    {
        Debug.Log("creating clone...");
        ItemData itemClone = ScriptableObject.CreateInstance<ItemData>();
        itemClone.name = name;
        itemClone.value = value;
        itemClone.icon = icon;
        itemClone.gamePrefab = gamePrefab;
        itemClone.stackAmount = stackAmount;
        itemClone.stackable = stackable;
        itemClone.UIPrefab = UIPrefab;
        itemClone.spawnOffset = spawnOffset;
        return (itemClone);
    }
}
