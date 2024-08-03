
using UnityEngine;

public class PlayerSound : MonoBehaviour{

    private float MaxRunTime;
    private float MaxFlameTime;

    private bool ShouldStartRunTime;
    private bool ShouldStartFlameTime;

    private PlayerManager playerManager;

    private float Timer;


    private void Awake() {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Start() {
        MaxRunTime = 0.3f;
        MaxFlameTime = 0.6f;
        Timer = 0f;
        ShouldStartFlameTime = false;
        ShouldStartRunTime = false;

        playerManager.OnPlayerMoveStateChange += PlayerManager_OnPlayerMoveStateChange;
    }

    private void PlayerManager_OnPlayerMoveStateChange(object sender, PlayerManager.OnMoveStateChangeEventArgs e) {
        ShouldStartFlameTime = false;
        ShouldStartRunTime = false;
        Timer = MaxFlameTime;

        if (e.state == PlayerManager.PlayerMoveStates.Running) {
            ShouldStartRunTime = true;
        }
        else if (e.state == PlayerManager.PlayerMoveStates.Attacking) {
            ShouldStartFlameTime = true;
        }
    }

    private void Update() {
        if (ShouldStartRunTime) {
            Timer += Time.deltaTime;

            if(Timer >= MaxRunTime) {
                Timer = 0f;
                SoundManager.Instance.PlayRunningSound();
            }
        }

        else if (ShouldStartFlameTime) {
            Timer += Time.deltaTime;

            if (Timer >= MaxFlameTime) {
                Timer = 0f;
                SoundManager.Instance.PlayFireballSound();
            }
        }
    }
}
