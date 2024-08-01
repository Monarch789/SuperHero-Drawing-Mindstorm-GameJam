
using UnityEngine;

public class Floor : MonoBehaviour{

    private void Start() {
        PlayerManager.Instance.OnFloorDisappear += PlayerManager_OnFloorDisappear;
    }

    private void PlayerManager_OnFloorDisappear(object sender, System.EventArgs e) {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        PlayerManager.Instance.OnFloorDisappear -= PlayerManager_OnFloorDisappear;
    }
}
