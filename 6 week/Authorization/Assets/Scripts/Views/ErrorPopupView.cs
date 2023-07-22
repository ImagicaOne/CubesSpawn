using TMPro;
using UnityEngine;

public class ErrorPopupView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _text;
    
    private const string errorTitle = "Error message\r\n\r\n";

    public void ShowMessage(int code)
    {
        _text.text = ErrorMessageProvider.Instance.GetErrorMessage(code);;
        gameObject.SetActive(true);
    }
}

