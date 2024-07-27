using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource sFXSource;
    public AudioClip itemPickUp;
    public static PlayerSFX instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ItemPickUpSFX()
    {
        sFXSource.clip = itemPickUp;
        sFXSource.Play();
    }
}
