using System.Collections;
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
        bool empty = true;
        for(int i = 0; i < sellContainer.GetComponent<CellUI>().slots.Length; i++)
        {
            if(sellContainer.GetComponent<CellUI>().slots[i].childCount > 0)
            {
                empty = false;
                sellContainer.gameObject.SetActive(false);
                inventoryContainer.gameObject.SetActive(false);
                welcomeContainer.gameObject.SetActive(true);
                inventoryContainer.GetComponent<InventoryUI>().inventoryData.gold += sellContainer.GetComponent<CellUI>().goldOnSell;
                Destroy(sellContainer.GetComponent<CellUI>().slots[i].GetChild(0));
            }
        }
        empty = true;
    }
}
