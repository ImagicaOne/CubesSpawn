using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script for easy changing every UI element that need to change
public class ChangingUIElement : MonoBehaviour
{
    //specify the type of hero characteristic linked to this UI element
    [SerializeField]
    public StatType statToChange;

    //change UI
    public void Change(object value)
    {
        var textMesh = GetComponent<TextMeshProUGUI>();
        var slider = GetComponent<Slider>();

        if (textMesh != null)
        {
            textMesh.text = value.ToString();
            return;
        }
        if (slider != null)
        {
            slider.value = (int)value;
            return;
        }
    }
}
