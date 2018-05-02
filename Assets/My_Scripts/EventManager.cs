using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public float eventTimer;
    private float timer;

    private void Start()
    {
        timer = eventTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            OnClicked();
            timer = eventTimer;
        }
    }
}
