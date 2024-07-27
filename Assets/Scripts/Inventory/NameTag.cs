using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTag : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform nameTagTransform;
    public static NameTag instance;
    public TextMeshProUGUI nameTagText;

    private void Awake()
    {
        instance = this;
    }

    public void Display(string name, Vector3 newPosition)
    {
        nameTagText.text = name;
        Vector2 newSizeDelta = nameTagTransform.sizeDelta;
        newSizeDelta.x = name.Length * 12.742f;
        nameTagTransform.sizeDelta = newSizeDelta;
        newPosition = newPosition + new Vector3(30, -25, 0);
        nameTagTransform.position = newPosition;
    }

    public void Hide()
    {
        nameTagTransform.position = new Vector3(0, 100000000, 0);
    }
}
