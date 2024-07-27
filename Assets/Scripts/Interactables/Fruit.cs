using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IInteractable
{
    public string name;
    public SpriteRenderer spriteRenderer;
    public Sprite appleSprite;
    public Sprite orangeSprite;
    public ItemData itemData;
    public InventoryData playerInventoryData;
    public ItemData appleData;
    public ItemData orangeData;
    public Rigidbody rb;

    public void SetFruitType(Tree.FruitType fruitType)
    {
        if(fruitType == Tree.FruitType.Apple)
        {
            name = "apple";
            spriteRenderer.sprite = appleSprite;
            itemData = appleData;
        }
        else if(fruitType == Tree.FruitType.Orange)
        {
            name = "orange";
            spriteRenderer.sprite = orangeSprite;
            itemData = orangeData;
        }
    }

    public void Interact()
    {
        bool placeInInventory = playerInventoryData.AddItem(itemData);
        if (placeInInventory)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.AddForce(transform.up * 2);
        }
    }
    
}
