using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseBuffsUI : MonoBehaviour{
    //Singleton
    public static IncreaseBuffsUI Instance {  get; private set; }

    [SerializeField] private Button IncreaseHealthButton;
    [SerializeField] private Button IncreaseDamageButton;

    [SerializeField] private TextMeshProUGUI HealthPriceText;
    [SerializeField] private TextMeshProUGUI DamagePriceText;

    //events to send on health and damage increased
    public event EventHandler OnHealthInreased;
    public event EventHandler OnDamageInreased;

    //prices of both
    private int HealthPrice;
    private int DamagePrice;

    private void Awake() {
        Instance = this;

        IncreaseHealthButton.onClick.AddListener(() => {
            Money.Instance.DecreaseMoney(HealthPrice);

            HealthPrice += 10;

            HealthPriceText.text = HealthPrice.ToString();

            OnHealthInreased?.Invoke(this, EventArgs.Empty);
        });
        IncreaseDamageButton.onClick.AddListener(() => {
            Money.Instance.DecreaseMoney(DamagePrice);

            DamagePrice += 10;

            DamagePriceText.text = DamagePrice.ToString();

            OnDamageInreased?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start() {
        HealthPrice = 10;
        DamagePrice = 10;

        HealthPriceText.text = HealthPrice.ToString();
        DamagePriceText.text = DamagePrice.ToString();

        IncreaseHealthButton.interactable = Money.Instance.GetMoney() >= HealthPrice;
        IncreaseDamageButton.interactable = Money.Instance.GetMoney() >= DamagePrice;

        Money.Instance.OnMoneyDecreased += Money_OnMoneyDecreased;

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e) {
        Hide();
    }

    private void Money_OnMoneyDecreased(object sender, EventArgs e) {
        IncreaseHealthButton.interactable = Money.Instance.GetMoney() >= HealthPrice;
        IncreaseDamageButton.interactable = Money.Instance.GetMoney() >= DamagePrice;
    }



    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Money.Instance.OnMoneyDecreased -= Money_OnMoneyDecreased;

        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
    }
}
