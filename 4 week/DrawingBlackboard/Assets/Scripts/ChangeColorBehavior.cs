using UnityEngine;
using UnityEngine.UI;

public class ChangeColorBehavior : MonoBehaviour
{
    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
