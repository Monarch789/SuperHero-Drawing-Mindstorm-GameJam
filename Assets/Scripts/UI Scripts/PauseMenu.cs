using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    // Start is called before the first frame update

    public static PauseMenu Instance { get; private set; }
    [SerializeField] private Button pauseButton;

    public event EventHandler OnPauseButtonClick;
    public event EventHandler OnPlayButtonClick;

    //private bool gamePaused = false;

    private void Awake() {
        Instance = this;

        pauseButton.onClick.AddListener(()=> { 
            OnPauseButtonClick?.Invoke(this, EventArgs.Empty);
        });
    }
}
