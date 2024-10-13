using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterNameUI : UIBase
{
    public TextMeshProUGUI nameText;
    protected override void Awake()
    {
        base.Awake();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
