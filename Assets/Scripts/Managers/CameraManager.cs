using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour{

    [SerializeField] private CinemachineVirtualCamera PlayerFollowCam;
    [SerializeField] private CinemachineVirtualCamera WaveCam;


    private void Start() {
        WaveCam.Priority = 0;
        PlayerFollowCam.Priority = 1;

        PlayerManager.Instance.OnWaveStart += PlayerManager_OnNewWaveStart;
    }

    private void PlayerManager_OnNewWaveStart(object sender, System.EventArgs e) {
        PlayerFollowCam.Priority = 0;
        WaveCam.Priority = 1;
    }

    private void OnDestroy() {
        PlayerManager.Instance.OnWaveStart -= PlayerManager_OnNewWaveStart;
    }
}
