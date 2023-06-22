using System;
using TMPro;
using UnityEngine;

public class TextFormat : MonoBehaviour
{
    private int _amount = 99999;
    // Start is called before the first frame update
    void Start()
    {
        var textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = Convert.ToDecimal(_amount).ToString("#,#");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
