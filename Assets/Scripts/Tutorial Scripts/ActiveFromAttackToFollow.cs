using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFromAttackToFollow : MonoBehaviour{

    private void Start() {
        gameObject.SetActive(false);

        PlayerManager.Instance.OnPlayerCanAttack += PlayerManager_OnPlayerCanAttack;

        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void PlayerManager_OnPlayerCanAttack(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        PlayerManager.Instance.OnPlayerCanAttack -= PlayerManager_OnPlayerCanAttack;

        Player.Instance.OnDrawComplete -= Player_OnDrawComplete;
    }
}
