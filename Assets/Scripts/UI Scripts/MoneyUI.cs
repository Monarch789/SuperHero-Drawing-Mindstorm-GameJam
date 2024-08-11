
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
        moneyText.text = Money.Instance.GetMoney().ToString();

        Money.Instance.OnMoneyIncreased += Money_OnMoneyIncreased;
        Money.Instance.OnMoneyDecreased += Money_OnMoneyDecreased;

        GameManager.Instance.OnLevelDone += GameManager_OnLevelDone;
    }

    private void GameManager_OnLevelDone(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void Money_OnMoneyDecreased(object sender, System.EventArgs e) {
        moneyText.text = Money.Instance.GetMoney().ToString();

        animator.SetTrigger("MoneyDecrease");
    }

    private void Money_OnMoneyIncreased(object sender, System.EventArgs e) {
        moneyText.text = Money.Instance.GetMoney().ToString();

        animator.SetTrigger("MoneyIncrease");
    }

    private void OnDestroy() {
        GameManager.Instance.OnLevelDone -= GameManager_OnLevelDone;

        Money.Instance.OnMoneyIncreased -= Money_OnMoneyIncreased;
        Money.Instance.OnMoneyDecreased -= Money_OnMoneyDecreased;
    }


}
