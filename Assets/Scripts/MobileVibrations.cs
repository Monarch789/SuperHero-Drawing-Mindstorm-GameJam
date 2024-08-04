using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileVibrations : MonoBehaviour {

    public static MobileVibrations Instance { get; private set; }

    private const string VibrateString = "Vibrate";

    private bool CanVibrate;
    public void VibrateMobile() {
        if(CanVibrate)
            Handheld.Vibrate();
    }

    private void Awake() {
        Instance = this;

        CanVibrate = PlayerPrefs.GetInt(VibrateString,1) == 1;
    }

    private void Start() {
        OptionsUI.OnVibrationToggle += OptionsUI_OnVibrationToggle;
    }

    private void OptionsUI_OnVibrationToggle(object sender, OptionsUI.OnVibrationToggleEventArgs e) {
        CanVibrate = e.CanVibrate;
    }

    private void OnDestroy() {
        OptionsUI.OnVibrationToggle -= OptionsUI_OnVibrationToggle;
    }
}
