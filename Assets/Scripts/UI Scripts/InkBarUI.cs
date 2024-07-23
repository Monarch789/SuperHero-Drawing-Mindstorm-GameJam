using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkBarUI : MonoBehaviour{

    //reference of image to fill the bar once the player is done drawing
    [SerializeField] private Image barImage;

    private void Start() {
        Hide();

        Player.Instance.OnPlayerTouch += Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        Hide();

        barImage.fillAmount = 1f;
    }

    private void Player_OnPlayerTouch(object sender, Player.OnPlayerTouchEventArgs e) {
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        Player.Instance.OnPlayerTouch -= Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete -= Player_OnDrawComplete;
    }

}
