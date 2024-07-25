using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour{

    private Animator animator;

    //reference of Text
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void Start() {
        moneyText.text = "Money:" + Money.Instance.GetMoney();

        Money.Instance.OnMoneyIncreased += Money_OnMoneyIncreased;
        Money.Instance.OnMoneyDecreased += Money_OnMoneyDecreased;
    }

    private void Money_OnMoneyDecreased(object sender, System.EventArgs e) {
        moneyText.text = "Money:" + Money.Instance.GetMoney();

        animator.SetTrigger("MoneyDecrease");
    }

    private void Money_OnMoneyIncreased(object sender, System.EventArgs e) {
        moneyText.text = "Money:" + Money.Instance.GetMoney();

        animator.SetTrigger("MoneyIncrease");
    }



}
