
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SoundEffectSO", fileName = "SoundEffectSO")]
public class SoundEffectSO : ScriptableObject{

    //Gameplay Sound Effects
    public AudioClip[] MonsterAttacks;
    public AudioClip[] Fireball;
    public AudioClip[] Run;
    public AudioClip[] Jump;
    public AudioClip[] PlayerDeath;
    public AudioClip[] MonsterDeath;
    public AudioClip[] HealthBuff;
    public AudioClip[] DamageBuff;
    public AudioClip[] DrawingAudio;
    
    //UI Sound Effects
    public AudioClip[] MoneyIncreased;
    public AudioClip[] MoneyDecreased;
    public AudioClip[] ButtonTap;
    public AudioClip[] OneStar;
    public AudioClip[] TwoStar;
    public AudioClip[] ThreeStar;
    public AudioClip[] LevelPassed;
    public AudioClip[] LevelFailed;

}
