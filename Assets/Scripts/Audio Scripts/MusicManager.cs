using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    //Singleton
    public static MusicManager Instance {  get; private set; }
    
    private const string MusicVolumeString = "MusicVolume";

    private AudioSource audioSource;

    private float volume;


    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(MusicVolumeString,1f);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        OptionsUI.OnMusicValueChanged += OptionsUI_OnMusicValueChanged;
    }

    private void OptionsUI_OnMusicValueChanged(object sender, OptionsUI.OnValueChangedEventArgs e) {
        volume = e.newValue;

        PlayerPrefs.SetFloat(MusicVolumeString,volume);
        PlayerPrefs.Save();

        audioSource.volume = volume;
    }

    public float GetVolume() {
        return volume;
    }

}
