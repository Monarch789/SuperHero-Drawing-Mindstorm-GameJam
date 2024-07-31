using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMain : MonoBehaviour{

    //Singleton
    public static TutorialMain Instance {  get; private set; }

    [SerializeField] private Button NextButton;
    [SerializeField] private Button RetryButton;

    //references of all the gameObjects of steps
    [SerializeField] private GameObject Step1GameObject;
    [SerializeField] private GameObject Step2GameObject;
    [SerializeField] private GameObject Step3GameObject;

    //references of all the steps scripts to see if it should activate the next button
    private Step1 step1;
    private Step2 step2;
    private Step3 step3;

    //int to see how many steps hae been done
    private const string TutorialSteps = "TutorialSteps";

    //get the steps completed and 0 if not saved
    private int StepsDone;

    //event to send all the steps objects to activate
    public class OnStepObjectActivateEventArgs:EventArgs { public int stepsNumber; }
    public event EventHandler<OnStepObjectActivateEventArgs> OnStepObjectActivate;

    public static event EventHandler OnTutorialComplete;

    private void Awake() {
        NextButton.onClick.AddListener(() => {
            if(StepsDone == 3) {
                OnTutorialComplete?.Invoke(this, EventArgs.Empty);

                Loader.LoadScene(Loader.GameScenes.SampleScene);
            }
            else 
                Loader.LoadScene(Loader.GameScenes.TutorialScene);
        });

        RetryButton.onClick.AddListener(() => {
            //subtract one from steps done if the user completed something and stil wishes to retry
            if (NextButton.interactable) {
                // the user completed this step

                //save it so that it is subtracted by 1 so that it loads correct step
                PlayerPrefs.SetInt(TutorialSteps,StepsDone-1);
                PlayerPrefs.Save();
            }

            //load the tutorial scene
            Loader.LoadScene(Loader.GameScenes.TutorialScene);

        });

        StepsDone = PlayerPrefs.GetInt(TutorialSteps, 0);

        Instance = this;

        step1 = Step1GameObject.GetComponent<Step1>();
        step2 = Step2GameObject.GetComponent<Step2>();
        step3 = Step3GameObject.GetComponent<Step3>();
    }

    private void Start() {
        NextButton.interactable = false;

        Time.timeScale = 1f;

        StartCoroutine(EventSendDelay());

        step1.OnStepComplete += Step1_OnStepComplete;
        step2.OnStepComplete += Step2_OnStepComplete;
        step3.OnStepComplete += Step3_OnStepComplete;
    }

    private void Step3_OnStepComplete(object sender, EventArgs e) {
        if(StepsDone == 2) {
            PlayerPrefs.SetInt(TutorialSteps, 0);
            PlayerPrefs.Save();

            StepsDone = 3;

            NextButton.interactable = true;
        }
    }

    private void Step2_OnStepComplete(object sender, EventArgs e) {
        if(StepsDone == 1) {
            PlayerPrefs.SetInt(TutorialSteps,2);
            PlayerPrefs.Save();

            StepsDone = 2;

            NextButton.interactable = true;
        }
    }

    private void Step1_OnStepComplete(object sender, EventArgs e) {
        //only save if this step hasnt been done
        
        if (StepsDone == 0) {
            PlayerPrefs.SetInt(TutorialSteps, 1);
            PlayerPrefs.Save();

            StepsDone = 1;

            NextButton.interactable = true;
        }
    }

    //couroutine to send event after all have been initialized
    private IEnumerator EventSendDelay() {
        yield return new WaitForSeconds(0.8f);

        OnStepObjectActivate?.Invoke(this, new OnStepObjectActivateEventArgs { stepsNumber = StepsDone });
    }
}
