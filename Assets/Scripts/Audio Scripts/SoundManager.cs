using UnityEngine;

public class SoundManager : MonoBehaviour{
    //Singelton
    public static SoundManager Instance {  get; private set; }

    [SerializeField] private SoundEffectSO sounds;

    [SerializeField] private bool isNotOnMainMenu;

    //string to save sound effect volume
    private const string SoundEffectVolumeString = "SoundEffectVolume";

    private float volume;


    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(SoundEffectVolumeString,1f);
    }

    private void Start() {
        OptionsUI.OnSFXValueChanged += OptionsUI_OnSFXValueChanged;

        if (isNotOnMainMenu) {
            PlayerManager.Instance.OnPlayerMoveStateChange += PlayerManager_OnPlayerMoveStateChange;
            Enemy.OnAttack += Enemy_OnAttack;
            Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
            Player.Instance.OnDeath += Player_OnDeath;

            Money.Instance.OnMoneyIncreased += Money_OnMoneyIncreased;
            Money.Instance.OnMoneyDecreased += Money_OnMoneyDecreased;

            GameManager.Instance.OnLevelFailed += GameManager_OnLevelFailed;
            GameManager.Instance.OnLevelPassed += GameManager_OnLevelPassed;

            Player.Instance.OnHealthIncreased += Player_OnHealthIncreased;
            Player.Instance.OnDamageIncreased += Player_OnDamageIncreased;
        }
    }

    private void Player_OnDamageIncreased(object sender, System.EventArgs e) {
        PlaySound(sounds.DamageBuff,Player.Instance.transform.position);
    }

    private void Player_OnHealthIncreased(object sender, System.EventArgs e) {
        PlaySound(sounds.HealthBuff, Player.Instance.transform.position);
    }

    private void GameManager_OnLevelPassed(object sender, GameManager.OnLevelCompletedEventArgs e) {
        PlaySound(sounds.LevelPassed,transform.position);

        if(e.Stars == 1) {
            PlaySound(sounds.OneStar,transform.position);
        }
        else if(e.Stars == 2) {
            PlaySound(sounds.TwoStar, transform.position);
        }
        else if(e.Stars == 3) {
            PlaySound(sounds.ThreeStar, transform.position);
        }
        
    }

    private void GameManager_OnLevelFailed(object sender, System.EventArgs e) {
        PlaySound(sounds.LevelFailed,transform.position);
    }

    private void Money_OnMoneyDecreased(object sender, System.EventArgs e) {
        PlaySound(sounds.MoneyDecreased,transform.position);
    }

    private void Money_OnMoneyIncreased(object sender, System.EventArgs e) {
        PlaySound(sounds.MoneyIncreased, transform.position);
    }

    private void Player_OnDeath(object sender, System.EventArgs e) {
        PlaySound(sounds.PlayerDeath, Player.Instance.transform.position);
    }

    private void Enemy_OnEnemyDeath(object sender, System.EventArgs e) {
        Enemy enemy = sender as Enemy;

        PlaySound(sounds.MonsterDeath,enemy.transform.position);
    }

    private void Enemy_OnAttack(object sender, Enemy.OnAttackEventArgs e) {
        Enemy enemy = sender as Enemy;

        PlaySound(sounds.MonsterAttacks,enemy.transform.position);
    }

    private void PlayerManager_OnPlayerMoveStateChange(object sender, PlayerManager.OnMoveStateChangeEventArgs e) {
        if(e.state == PlayerManager.PlayerMoveStates.Jumping) {
            PlaySound(sounds.Jump,PlayerManager.Instance.transform.position);
        }
    }

    private void OptionsUI_OnSFXValueChanged(object sender, OptionsUI.OnValueChangedEventArgs e) {
        volume = e.newValue;

        PlayerPrefs.SetFloat(SoundEffectVolumeString,volume);
        PlayerPrefs.Save();
    }
    private void PlaySound(AudioClip[] sounds, Vector3 PositionOfPlay) {
        PlaySound(sounds[Random.Range(0,sounds.Length)], PositionOfPlay);
    }
    private void PlaySound(AudioClip sound, Vector3 PositionOfPlay) {
        AudioSource.PlayClipAtPoint(sound,PositionOfPlay,volume*2);
    }

    public float GetVolume() {
        return volume;
    }

    private void OnDestroy() {
        OptionsUI.OnSFXValueChanged -= OptionsUI_OnSFXValueChanged;
    }

    public void PlayButtonTapSound() {
        PlaySound(sounds.ButtonTap, Camera.main.transform.position);
    }

    public void PlayRunningSound() {
        PlaySound(sounds.Run, Player.Instance.transform.position);
    }
    public void PlayFireballSound() {
        PlaySound(sounds.Fireball, Player.Instance.transform.position);
    }

    public void PlayLevelCompleteSound(bool WhichSound) {

        if (WhichSound) {
            PlaySound(sounds.LevelPassed,Camera.main.transform.position);
        }
        else {
            PlaySound(sounds.LevelFailed, Camera.main.transform.position);
        }
    }

    public void PlayDrawingSound() {
        PlaySound(sounds.DrawingAudio,Camera.main.transform.position);
    }

}
