﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : MonoBehaviour, IInteractable
{
    public bool enterTrigger = false;
    public bool interacting = false;
    public GameObject welcomeContainer;
    public GameObject sellContainer;
    public GameObject inventoryContainer;
    public GameObject buyContainer;
    public GameObject finalSell;
    public ItemData furnitureItemData;
    public ItemData appleItemData;
    public ItemData orangeItemData;
    public InventoryData playerInventoryData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (enterTrigger && !interacting)
        {
            interacting = true;
            Debug.Log("Open/close shop");
            welcomeContainer.gameObject.SetActive(true);
            interacting = false; //move this later
            
        }
    }

    public void Exit()
    {
        welcomeContainer.gameObject.SetActive(false);
    }

    public void OpenWelcomeContainer()
    {
        sellContainer.gameObject.SetActive(false);
        inventoryContainer.gameObject.SetActive(false);
        buyContainer.gameObject.SetActive(false);
        welcomeContainer.gameObject.SetActive(true);
    }
    public void OpenSellScreen()
    {

        sellContainer.gameObject.SetActive(true);
        inventoryContainer.gameObject.SetActive(true);
        welcomeContainer.gameObject.SetActive(false);
    }

    public void OpenBuyScreen()
    {
        welcomeContainer.gameObject.SetActive(false);
        buyContainer.gameObject.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enterTrigger = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterTrigger = false;
        }
    }
    public void FinalSell()
    {
        //this function sells item that we place into the sell container
        bool empty = true;
        string itemName = "";
        for(int i = 0; i < sellContainer.GetComponent<CellUI>().slots.Length; i++)
        {
            if(sellContainer.GetComponent<CellUI>().slots[i].childCount > 0)
            {
                empty = false;
                sellContainer.gameObject.SetActive(false);
                inventoryContainer.gameObject.SetActive(false);
                welcomeContainer.gameObject.SetActive(true);
                inventoryContainer.GetComponent<InventoryUI>().inventoryData.gold += sellContainer.GetComponent<CellUI>().goldOnSell;
                
                
                
                
                //itemName = sellContainer.GetComponent<CellUI>().slots[i].GetChild(0).gameObject.GetComponent<ItemUI>().itemData.name;

                for (int j = 0; j < playerInventoryData.inventory.Length; j++)
                {
                    GameObject child = sellContainer.GetComponent<CellUI>().slots[i].GetChild(0).gameObject;
                    if (child.name == "Furniture Item UI(Clone)")
                    {
                        itemName = sellContainer.GetComponent<CellUI>().slots[i].GetChild(0).gameObject.GetComponent<ItemUI>().itemData.name;
                    }
                    else if (child.name == "Stackable Item UI(Clone)")
                    {
                        itemName = sellContainer.GetComponent<CellUI>().slots[i].GetChild(0).gameObject.GetComponent<StackableItemUI>().itemData.name;
                    }
                    if (playerInventoryData.inventory[j].name == itemName)
                    {
                        playerInventoryData.inventory[j] = null;
                        Destroy(child);
                    }
                    else 
                    {
                        Debug.Log("NAMES DO NOT MATCH");
                        Debug.Log(playerInventoryData.inventory[j].name);
                        Debug.Log(itemName);
                        Debug.Log(child.name);
                    }
                }
                
                
                
                
            }
        }
        empty = true;
    }

    public void buyItem(string item)
    {
        if (item == "Furniture" && playerInventoryData.gold >= 50)
        {
            placeItem(furnitureItemData);
            playerInventoryData.gold -= 50;
        }
        else if (item == "Apple" && playerInventoryData.gold >= 10) 
        {
            placeItem(appleItemData);
            playerInventoryData.gold -= 10;
        }
        else if (playerInventoryData.gold >= 5 && item == "Orange")
        {
            placeItem(orangeItemData);
            playerInventoryData.gold -= 5;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void placeItem(ItemData item)
    {
        bool placeInInventory = playerInventoryData.AddItem(item);
        if (placeInInventory)
        {
            Debug.Log("Bought item");
        }
        else
        {
            Debug.Log("Invenotry is full");
        }
    }
}
