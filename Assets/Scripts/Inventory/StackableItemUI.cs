using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StackableItemUI : ItemUI
{
    public int stackAmount = 1;
    public TextMeshProUGUI text;


    public void UpdateText()
    {
        text.text = stackAmount.ToString();
    }
}
