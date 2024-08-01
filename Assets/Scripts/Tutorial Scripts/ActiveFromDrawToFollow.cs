
using UnityEngine;

public class ActiveFromDrawToFollow : MonoBehaviour{
    
    private void Start() {
        gameObject.SetActive(false);

        Player.Instance.OnPlayerTouch += Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void Player_OnPlayerTouch(object sender, Player.OnPlayerTouchEventArgs e) {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        Player.Instance.OnPlayerTouch -= Player_OnPlayerTouch;
        Player.Instance.OnDrawComplete -= Player_OnDrawComplete;
    }
}
