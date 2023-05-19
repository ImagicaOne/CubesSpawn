using System.Collections.Generic;
using UnityEngine;

public class ObjectsInteractionTask3 : MonoBehaviour
{
    // TODO: Получите доступ ко всем объектам сцены со скриптом Lamp
    // TODO: При нажатии на кнопку 2 на клавиатуре вызывайте у всех полученных объектов метод Interact

    private Lamp[] lamps;

    private void Start()
    {
        lamps = FindObjectsOfType<Lamp>();              
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            foreach (Lamp lamp in lamps)
            {
                lamp.Interact();
            }
        }
    }
}