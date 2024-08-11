using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour{

    private const string isShopOnAnimatorBool = "isShopOn";

    [SerializeField] private Button ShopButton;

    private Animator animator;

    private bool isShopOn;

    private void Awake() {
        animator = GetComponent<Animator>();

        ShopButton.onClick.AddListener(() => {
            isShopOn = !isShopOn;

            animator.SetBool(isShopOnAnimatorBool,isShopOn);
        });
    }

    private void Start() {
        isShopOn = false;

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
    }
}
