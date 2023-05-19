using UnityEngine;

public class ObjectsInteractionTask5 : MonoBehaviour
{
    // TODO: Подпишитесь на событие ItemSpawned класса Shelf
    // TODO: Если на полке станет более трех предметов - вызовите у объекта Shelf метод Fall
    // TODO: Логика должна работать для обоих полок сцены

    public void ItemSpawned(Shelf shelf)
    {
        //Shelf[] shelfs = FindObjectsOfType<Shelf>();
        //foreach (Shelf shelf in shelfs)
        shelf.Fall();
    }
}