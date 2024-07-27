using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tree : MonoBehaviour, IInteractable
{
    public enum FruitType {Apple, Orange }
    public FruitType fruit;
    public bool fruitRipe;
    public float growthTime;//growth time is for how long it take for the fruit to grow
    public float currentGrowthTime;
    public Transform fruitPos1;
    public Transform fruitPos2;
    public Transform fruitPos3;
    public GameObject fruit1;
    public GameObject fruit2;
    public GameObject fruit3;
    public GameObject fruitPrefab;
    public AudioSource sFXSource;
    public List<AudioClip> shakeClips;
    // Start is called before the first frame update
    void Start()
    {
        fruitRipe = false;
    }

    public void PlayRandomShakeSound()
    {
        System.Random numGen = new System.Random();
        int randomIndex = numGen.Next(0, shakeClips.Count);
        sFXSource.clip = shakeClips[randomIndex];
        sFXSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(fruit1 == null)
        {
            fruit1 = GameObject.Instantiate(fruitPrefab, fruitPos1.position, fruitPos1.rotation, fruitPos1);
            fruit2 = GameObject.Instantiate(fruitPrefab, fruitPos2.position, fruitPos2.rotation, fruitPos2);
            fruit3 = GameObject.Instantiate(fruitPrefab, fruitPos3.position, fruitPos3.rotation, fruitPos3);
            fruit1.GetComponent<Fruit>().SetFruitType(fruit);
            fruit2.GetComponent<Fruit>().SetFruitType(fruit);
            fruit3.GetComponent<Fruit>().SetFruitType(fruit);
            fruitRipe = false;
        }
        if (!fruitRipe)
        {
            if (currentGrowthTime >= growthTime)
            {
                fruitRipe = true;
            }
            else
            {
                currentGrowthTime += Time.deltaTime;
                Vector3 newScale = Vector3.one * ((currentGrowthTime / growthTime) / 2);
                fruit1.transform.localScale = newScale;
                fruit2.transform.localScale = newScale;
                fruit3.transform.localScale = newScale;
            }
        }
    }

    public void Interact()
    {
        PlayRandomShakeSound();
        if (fruitRipe)
        {
            fruit1.GetComponent<Rigidbody>().isKinematic = false;
            fruit2.GetComponent<Rigidbody>().isKinematic = false;
            fruit3.GetComponent<Rigidbody>().isKinematic = false;
            fruit1 = null;
            fruit2 = null;
            fruit3 = null;
            fruitRipe = false;
            currentGrowthTime = 0;
        }
        
    }
}
