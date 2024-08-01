
using UnityEngine;

public class ActiveFromAttackToDraw : MonoBehaviour{

    private void Start() {
        gameObject.SetActive(false);

        PlayerManager.Instance.OnPlayerCanAttack += PlayerManager_OnPlayerCanAttack;
        Player.Instance.OnPlayerTouch += Player_OnPlayerTouch;
    }

    private void Player_OnPlayerTouch(object sender, Player.OnPlayerTouchEventArgs e) {
        gameObject.SetActive(false);
    }

    private void PlayerManager_OnPlayerCanAttack(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
    }
}
