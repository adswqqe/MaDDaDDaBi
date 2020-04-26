using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public Action<int> clickAddSoppingBaskeBtn;
    [SerializeField]
    Text numberText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void countNuber(int add)
    {
        if ((int.Parse(numberText.text) + add) <= 1)
            numberText.text = 1.ToString();
        else
            numberText.text = (int.Parse(numberText.text) + add).ToString(); ;
    }

    public void OnShoppingBaskeBtn(Text number)
    {
        gameObject.SetActive(false);
        clickAddSoppingBaskeBtn?.Invoke(int.Parse(number.text));
        number.text = 1.ToString();
    }

}
