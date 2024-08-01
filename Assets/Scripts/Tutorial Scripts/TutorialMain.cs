using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMain : MonoBehaviour{

    //Singleton
    public static TutorialMain Instance {  get; private set; }

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
    private const string TotalLevelsCompletedString = "LevelsCompleted";


    private const string AnimatorGoodComplete = "GoodComplete";
    private const string AnimatorBadComplete = "BadComplete";

    private Animator animator;

    //get the steps completed and 0 if not saved
    private int StepsDone;


    private void Awake() {
        animator = GetComponent<Animator>();

        StepsDone = PlayerPrefs.GetInt(TutorialSteps, 0);

        Step1GameObject.SetActive(false);
        Step2GameObject.SetActive(false);
        Step3GameObject.SetActive(false);

        if(StepsDone == 0) {
            Step1GameObject.SetActive(true);
        }
        else if(StepsDone == 1) {
            Step2GameObject.SetActive(true);
        }
        else if(StepsDone == 2) {
            Step3GameObject.SetActive(true);
        }

        Instance = this;

        step1 = Step1GameObject.GetComponent<Step1>();
        step2 = Step2GameObject.GetComponent<Step2>();
        step3 = Step3GameObject.GetComponent<Step3>();
    }

    private void Start() {
        Time.timeScale = 1f;

        step1.OnStepComplete += Step1_OnStepComplete;
        step2.OnStepComplete += Step2_OnStepComplete;
        step3.OnStepComplete += Step3_OnStepComplete;

        Player.Instance.OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(object sender, EventArgs e) {
        animator.SetTrigger(AnimatorBadComplete);

        StartCoroutine(NextStepDelay(false));

    }

    private void Step3_OnStepComplete(object sender, EventArgs e) {
        if(StepsDone == 2) {

            PlayerPrefs.SetInt(TutorialSteps, 0);

            if(PlayerPrefs.GetInt(TotalLevelsCompletedString,-1) < 0) {
                PlayerPrefs.SetInt(TotalLevelsCompletedString,0);
            }

            PlayerPrefs.Save();

            StepsDone = 3;

            animator.SetTrigger(AnimatorGoodComplete);

            StartCoroutine(NextStepDelay(true));
        }
    }

    private void Step2_OnStepComplete(object sender, EventArgs e) {
        if(StepsDone == 1) {

            PlayerPrefs.SetInt(TutorialSteps,2);
            PlayerPrefs.Save();

            StepsDone = 2;

            animator.SetTrigger(AnimatorGoodComplete);

            StartCoroutine(NextStepDelay(false));
        }
    }

    private void Step1_OnStepComplete(object sender, EventArgs e) {
        //only save if this step hasnt been done
        
        if (StepsDone == 0) {

            PlayerPrefs.SetInt(TutorialSteps, 1);
            PlayerPrefs.Save();

            StepsDone = 1;

            animator.SetTrigger(AnimatorGoodComplete);

            StartCoroutine(NextStepDelay(false));
        }
    }

    private void OnDestroy() {
        step1.OnStepComplete -= Step1_OnStepComplete;
        step2.OnStepComplete -= Step2_OnStepComplete;
        step3.OnStepComplete -= Step3_OnStepComplete;

        Player.Instance.OnDeath -= Player_OnDeath;
    }

    private IEnumerator NextStepDelay(bool isLastStep) {

        yield return new WaitForSeconds(0.75f);

        if (isLastStep) {
            Loader.LoadScene(Loader.GameScenes.Level1);
        }
        else {
            Loader.LoadScene(Loader.GameScenes.TutorialScene);
        }
    }
}
