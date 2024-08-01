using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsStarsUI : MonoBehaviour{

    private readonly string[] LevelsStarsStrings = { "Level1Stars", "Level2Stars", "Level3Stars", "Level4Stars", "Level5Stars", "Level6Stars", "Level7Stars", "Level8Stars", "Level9Stars", "Level10Stars", "Level11Stars", "Level12Stars" };
    
    [SerializeField] private Image[] Level1Stars;
    [SerializeField] private Image[] Level2Stars;
    [SerializeField] private Image[] Level3Stars;
    [SerializeField] private Image[] Level4Stars;
    [SerializeField] private Image[] Level5Stars;
    [SerializeField] private Image[] Level6Stars;
    [SerializeField] private Image[] Level7Stars;
    [SerializeField] private Image[] Level8Stars;
    [SerializeField] private Image[] Level9Stars;
    [SerializeField] private Image[] Level10Stars;
    [SerializeField] private Image[] Level11Stars;
    [SerializeField] private Image[] Level12Stars;

    private int Level1StarsInt;
    private int Level2StarsInt;
    private int Level3StarsInt;
    private int Level4StarsInt;
    private int Level5StarsInt;
    private int Level6StarsInt;
    private int Level7StarsInt;
    private int Level8StarsInt;
    private int Level9StarsInt;
    private int Level10StarsInt;
    private int Level11StarsInt;
    private int Level12StarsInt;

    private void Awake() {
        Level1StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[0],0);
        Level2StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[1],0);
        Level3StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[2],0);
        Level4StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[3],0);
        Level5StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[4],0);
        Level6StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[5],0);
        Level7StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[6],0);
        Level8StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[7],0);
        Level9StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[8],0);
        Level10StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[9],0);
        Level11StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[10],0);
        Level12StarsInt = PlayerPrefs.GetInt(LevelsStarsStrings[11],0);
    }

    private void Start() {
        foreach(Image star in Level1Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level2Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level3Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level4Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level5Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level6Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level7Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level8Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level9Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level10Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level11Stars) {
            star.gameObject.SetActive(false);
        }
        foreach (Image star in Level12Stars) {
            star.gameObject.SetActive(false);
        }


        //enable all the stars according to how many there are
        for(int i = 0; i < Level1StarsInt;i++) {
            Level1Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level2StarsInt; i++) {
            Level2Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level3StarsInt; i++) {
            Level3Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level4StarsInt; i++) {
            Level4Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level5StarsInt; i++) {
            Level5Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level6StarsInt; i++) {
            Level6Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level7StarsInt; i++) {
            Level7Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level8StarsInt; i++) {
            Level8Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level9StarsInt; i++) {
            Level9Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level10StarsInt; i++) {
            Level10Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level11StarsInt; i++) {
            Level11Stars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Level12StarsInt; i++) {
            Level12Stars[i].gameObject.SetActive(true);
        }
    }

}
