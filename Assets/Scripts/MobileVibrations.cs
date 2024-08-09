using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void SetBool(bool newVal) {
        CanVibrate = newVal;

        Debug.Log("Mobile Vibrations:" + CanVibrate);
    }
}
