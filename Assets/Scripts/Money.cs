using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour{

    public static Money Instance {  get; private set; }

    //events to send Money UI
    public event EventHandler OnMoneyIncreased;
    public event EventHandler OnMoneyDecreased;

    private int money;

    private void Awake() {
        Instance = this;

        money = 0;
    }


    public int GetMoney() {
        return money;
    }
    public void IncreaseMoney(int in_mon) {
        money += in_mon;

        OnMoneyIncreased?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseMoney(int de_mon) {
        money -= de_mon;

        OnMoneyDecreased?.Invoke(this, EventArgs.Empty);
    }
}
