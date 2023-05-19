using System;
using UnityEngine;
using UnityEngine.Events;

public class Toaster : MonoBehaviour
{
    // TODO: Создайте событие TimerIsUp и вызывайте его, когда таймер вышел

    public float Timer { get; private set; } = 3; // Таймер готовности вафли
    private bool _isTimerUp;

    [SerializeField]
    private UnityEvent TimerIsUp;

    private bool waffleDone = false;

    private void Update()
    {
        // Если таймер вышел - выходим из метода
        if (_isTimerUp && waffleDone == false)
        {
            TimerIsUp.Invoke();
            waffleDone = true;
            return;
        }
        
        // Если таймер не вышел  
        if (Timer > 0)
        {
            // Отнимаем время, прошедшее за кадр
            Timer -= Time.deltaTime;
        }
        else
        {
            // Таймер вышел
            _isTimerUp = true;
        }
    }
}