using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public Transform[] slots = new Transform[10];
    public int goldOnSell;
    public InventoryUI inventoryUI;

    private void OnEnable()
    {
        inventoryUI.selling = true;
    }
    private void OnDisable()
    {
        inventoryUI.selling = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateTotal()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].childCount > 0)
            {
                goldOnSell += slots[i].GetChild(0).GetComponent<ItemUI>().itemData.value;
            }
        }
        goldText.text = "Gold Amount:" + goldOnSell.ToString();
    }

    public void AddItemToSell(RectTransform itemUIRect)
    {
        bool itemPlaced = false;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].childCount == 0)
            {
                itemPlaced = true;
                RectTransform slotToPlace = slots[i].GetComponent<RectTransform>();
                itemUIRect.SetParent(slotToPlace);
                itemUIRect.anchoredPosition = new Vector2(slotToPlace.rect.width / 2, -slotToPlace.rect.height / 2);
                CalculateTotal();
            }
        }
        if (!itemPlaced)
        {
            //no more spots for items
        }
    }
}
