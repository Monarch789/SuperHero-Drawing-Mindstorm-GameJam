using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{

    //Singleton
    public static OptionsUI Instance { get; private set; }

    //events for sending to music and sound manager to change the volume multiplier
    public class OnValueChangedEventArgs:EventArgs { public float newValue; }

    public event EventHandler<OnValueChangedEventArgs> OnMusicValueChanged;
    public event EventHandler<OnValueChangedEventArgs> OnSFXValueChanged;

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] private TextMeshProUGUI MusicPercentText;
    [SerializeField] private TextMeshProUGUI SFXPercentText;

    [SerializeField] private Button BackButton;

    private Animator animator;
    private const string OptionsUIActiveAnimatorTrigger = "OptionsClick";
    private const string OptionsUIBackAnimatorTrigger = "OptionsBackClick";

    private void Awake() {
        animator = GetComponent<Animator>();

        MusicPercentText.text = (Mathf.FloorToInt(MusicSlider.value * 100)).ToString() + "%";
        SFXPercentText.text = (Mathf.FloorToInt(SFXSlider.value * 100)).ToString() + "%";

        BackButton.onClick.AddListener(() => {
            Hide();
        });

        MusicSlider.onValueChanged.AddListener(newSliderValue => {
            OnMusicValueChanged?.Invoke(this, new OnValueChangedEventArgs { newValue = newSliderValue });

            MusicPercentText.text = (Mathf.FloorToInt(MusicSlider.value * 100)).ToString() + "%";
        });

        SFXSlider.onValueChanged.AddListener(newSliderValue => {
            OnSFXValueChanged?.Invoke(this, new OnValueChangedEventArgs { newValue = newSliderValue});
            SFXPercentText.text = (Mathf.FloorToInt(SFXSlider.value * 100)).ToString() + "%";
        });
    }

    private void Start() {
        HideImmediatley();

        MainMenu.Instance.OnOptionsButtonClick += MainMenu_OnOptionsButtonClick;
    }

    private void MainMenu_OnOptionsButtonClick(object sender, System.EventArgs e) {
        Show();
    }

    private void HideImmediatley() {
        gameObject.SetActive(false);
    }

    private void Hide() {
        animator.SetTrigger(OptionsUIBackAnimatorTrigger);

        StartCoroutine(AnimationHideDelay());
    }
    private void Show() {
        gameObject.SetActive(true);

        animator.SetTrigger(OptionsUIActiveAnimatorTrigger);
    }

    private IEnumerator AnimationHideDelay() {
        yield return new WaitForSeconds(.5f);

        HideImmediatley();
    }
    
}
