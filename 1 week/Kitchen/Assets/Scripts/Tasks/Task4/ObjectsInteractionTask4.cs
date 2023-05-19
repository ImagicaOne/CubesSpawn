using UnityEngine;
using UnityEngine.Events;

public class ObjectsInteractionTask4 : MonoBehaviour
{
    // TODO: Подпишитесь на событие TimerIsUp класса Toaster созданием объекта Waffle в точке WaffleRoot (из папки Prefabs) 
    [SerializeField]
    private GameObject wafleRoot;

    [SerializeField]
    private Waffle waffle;

    public void TimerIsUp()
    {
        waffle.transform.position = wafleRoot.transform.position;    
        Instantiate(waffle);
    }

}