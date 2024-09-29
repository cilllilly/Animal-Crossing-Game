using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject slotPrefab;
    public RectTransform layoutTransform;
    public RectTransform clickedItem;
    public Canvas canvas;
    public RectTransform previousSlot;
    public bool selling = false;
    public CanvasGroup inventoryCG;
    public CellUI cellUI;
    public enum clickType { left, right, middle};
    public clickType lastClicked;

    public RectTransform slotContainer;

    public List<RectTransform> itemSlotRTs;
    public List<RectTransform> itemUIRTs;
    public static InventoryUI instance;

    public InventoryData currentInventory;

    public TextMeshProUGUI goldDisplayText;

    public bool canDropItem = false;
    public Transform playerTransform;

    public GameObject sellContainer;

    //When item is already clicked and click a new item group or swap

    private void Awake()
    {
        instance = this;
        //Populate layout transform with slot prefabs
        UpdateUI();
        this.enabled = false;
    }

    public void ClearSlots()
    {
        for(int i = 0; i < itemSlotRTs.Count; i++)
        {
            if (itemSlotRTs[i].childCount > 0)
            {
                GameObject child = itemSlotRTs[i].GetChild(0).gameObject;
                Destroy(child);
            }
        }
    }

    public void UpdateUI()
    {
        goldDisplayText.text = "Gold Amount: \n" + currentInventory.gold.ToString();
        ClearSlots();
        PopulateSlots();
    }

    public void PopulateSlots()
    {
        for(int i = 0; i < currentInventory.inventory.Length; i++)
        {
            if(currentInventory.inventory[i] != null)
            {
                GameObject newUIObject = GameObject.Instantiate(currentInventory.inventory[i].UIPrefab, itemSlotRTs[i]);
                //line above causes error for some reason. says ui prefab is null
                if (currentInventory.inventory[i].stackable)
                {
                    StackableItemUI stackableUI = newUIObject.GetComponent<StackableItemUI>();
                    stackableUI.stackAmount = currentInventory.inventory[i].stackAmount;
                    stackableUI.UpdateImage(currentInventory.inventory[i].icon);
                    stackableUI.UpdateText();
                }
                newUIObject.GetComponent<ItemUI>().itemData = currentInventory.inventory[i];
            }
        }
    }

    public void UpdateClickedType()
    {
        if (Input.GetMouseButton(0))
        {
            lastClicked = clickType.left;
        }
        else if (Input.GetMouseButton(1))
        {
            lastClicked = clickType.right;
        }
        else if(Input.GetMouseButton(2))
        {
            lastClicked = clickType.middle;
        }
    }

    public void ItemUIClicked(RectTransform itemsRT) 
    {

        if (selling)
        {
            cellUI.AddItemToSell(itemsRT);
            Debug.Log(itemsRT);

            
        }
        else { 
            if(lastClicked == clickType.left)
            {
                Debug.Log(clickedItem);
                //not holding item
                if (clickedItem == null)
                {
                    PickUp(itemsRT);
                    Debug.Log("pick up");
                }
                else if (clickedItem != null)
                {
                    StackableItemUI firstClicked = clickedItem.GetComponent<StackableItemUI>();
                    StackableItemUI secondClicked = itemsRT.GetComponent<StackableItemUI>();
                    if (firstClicked != null && secondClicked != null && firstClicked.itemName == secondClicked.itemName)
                    {
                        Debug.Log("stack item");
                        StackItems(firstClicked, secondClicked);
                    }
                    else
                    {
                        SwapItems(itemsRT);
                    }

                }
               
            }
            if (lastClicked == clickType.right) 
            {
                StackableItemUI stackableClicked = itemsRT.GetComponent<StackableItemUI>();
                if (clickedItem == null)
                { 
                    if (stackableClicked == null || stackableClicked.stackAmount == 1)
                    {
                        PickUp(itemsRT);
                    }
                    else if (stackableClicked != null && stackableClicked.stackAmount > 1)
                    {
                        stackableClicked.stackAmount -= 1;
                        stackableClicked.UpdateText();
                        GameObject newItemUI = GameObject.Instantiate(itemsRT.gameObject, slotContainer);
                        PickUp(newItemUI.GetComponent<RectTransform>());
                        clickedItem.GetComponent<StackableItemUI>().stackAmount = 1;
                        clickedItem.GetComponent<StackableItemUI>().UpdateText();
                    }
                }
                else if(clickedItem != null)
                {
                    StackableItemUI stackableHeld = clickedItem.GetComponent<StackableItemUI>();
                    if(stackableClicked != null && stackableHeld != null)
                    {
                        if(stackableClicked.stackAmount > 1)
                        {
                            stackableClicked.stackAmount -= 1;
                            stackableClicked.UpdateText();
                        
                        }
                        else if(stackableClicked.stackAmount == 1)
                        {
                            Destroy(stackableClicked.gameObject);
                        }
                        stackableHeld.stackAmount += 1;
                        stackableHeld.UpdateText();
                    }
                }
            }
       }
        
    }
    public void PickUp(RectTransform itemsRT)
    {
        clickedItem = itemsRT;
        previousSlot = itemsRT.parent as RectTransform;
        clickedItem.SetParent(slotContainer);
        clickedItem.GetComponent<Image>().raycastTarget = false;
        FollowCursor();
    }
    public void StackItems(StackableItemUI firstClicked, StackableItemUI secondClicked)
    {
        secondClicked.stackAmount += firstClicked.stackAmount;
        secondClicked.UpdateText();
        Destroy(firstClicked.gameObject);
    }
    public void SwapItems(RectTransform itemsRT)
    {
        SlotUIClicked(itemsRT.parent as RectTransform);
        itemsRT.SetParent(previousSlot);
        itemsRT.anchoredPosition = new Vector2(previousSlot.rect.width / 2, -previousSlot.rect.height / 2);
    }

    public void SlotUIClicked(RectTransform clickedSlotRT)
    {
        if (clickedItem != null)
        {
            clickedItem.SetParent(clickedSlotRT);
            clickedItem.GetComponent<Image>().raycastTarget = true;
            clickedItem.anchoredPosition = new Vector2(clickedSlotRT.rect.width / 2, -clickedSlotRT.rect.height/2);
            clickedItem = null;
        }
    }

    void FollowCursor()
    {
        if (clickedItem != null)
        {
            Debug.Log("Following cursor");
            Debug.Log(clickedItem);
            Vector2 newPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out newPosition);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out newPosition);
            //newPosition = newPosition + new Vector2(185, -135);
            //clickedItem.anchoredPosition = newPosition;
            clickedItem.anchoredPosition = newPosition;
            
        }
        
    }

    void DropItem()
    {
        if(clickedItem != null && canDropItem && Input.GetMouseButtonDown(0))
        {
            ItemData itemData = clickedItem.GetComponent<ItemUI>().itemData;
            Vector3 spawnPosition = playerTransform.position + itemData.spawnOffset + (playerTransform.forward * 1);
            GameObject spawnedItem = GameObject.Instantiate(itemData.gamePrefab, spawnPosition, playerTransform.rotation);
            currentInventory.RemoveItem(itemData);
            Destroy(clickedItem.gameObject);
            clickedItem = null;
        }
    }

    void Update()
    {
        UpdateClickedType();
        FollowCursor();
        DropItem();
    }

    public void LeftDropBoundaries()
    {
        canDropItem = false;
    }

    public void EnterDropBoundary()
    {
        canDropItem = true;
    }
}
