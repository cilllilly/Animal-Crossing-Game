using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public string itemName;
    public Image uIImage;
    public ItemData itemData;
    public RectTransform rectTransform;
    public GameObject sellContainer;

    private void Start()
    {
        sellContainer = GameObject.Find("Sell Container");
    }
    public void OnClicked()
    {
        if (sellContainer.activeInHierarchy)
        {
            Debug.Log("Selling");
        }
        else
        {
            InventoryUI.instance.ItemUIClicked(this.transform as RectTransform);
        }
    }

    public void UpdateImage(Sprite newImageSprite)
    {
        uIImage.sprite = newImageSprite;
    }

    public void OnHovered()
    {
        NameTag.instance.Display(itemData.name, rectTransform.position);
    }

    public void OnExit()
    {
        NameTag.instance.Hide();
    }
}
