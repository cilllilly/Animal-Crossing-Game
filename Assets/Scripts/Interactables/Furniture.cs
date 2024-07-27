using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public InventoryData playerInventoryData;
    public AudioSource audioSource;
    public AudioClip interactSFX;
    public AudioClip negativeSFX;

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
        bool placeInInventory = playerInventoryData.AddItem(itemData);
        if (placeInInventory)
        {
            Destroy(gameObject);
        }
        else
        {
            audioSource.clip = negativeSFX;
            audioSource.Play();
        }
    }
}
