using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{

    //Singleton
    public static EnemyManager Instance {  get; private set; }

    private List<Enemy> SeenEnemyList;

    //reference of how many waves there are
    [SerializeField] private int NumberOfEnemies;

    //event to send enemies to say that they should ready their attack
    public event EventHandler OnEnemyShouldReadyAttack;
    public event EventHandler OnEnemyShouldAttack;

    //event to send when an enemy takes damage
    public class OnTakeDamageEventArgs :EventArgs { public Enemy enemy; public float damage; }
    public event EventHandler<OnTakeDamageEventArgs> OnTakeDamage;

    //event to send player Manager to start again
    public event EventHandler OnStartAgain;

    //Timer variables
    private float MaxWaitTime;
    private float Timer;
    private bool ShouldStartTimer;

    private bool allEnemiesKilled;

    //int to see how may enemies have been killed
    private int EnemiesKiled;

    private void Awake() {
        Instance = this;

        SeenEnemyList = new List<Enemy>();
    }

    private void Start() {
        MaxWaitTime = 2f;
        Timer = 0f;
        ShouldStartTimer = false;
        allEnemiesKilled = false;
        EnemiesKiled = 0;

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
        PlayerManager.Instance.OnEnemyStartAttack += PlayerManager_OnEnemyStartAttack;
        PlayerManager.Instance.OnWaveStart += PlayerManager_OnNewWaveStart;
        Enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath(object sender, EventArgs e) {
        EnemiesKiled++;

        if(EnemiesKiled == NumberOfEnemies) {
            allEnemiesKilled = true;
        }
    }

    private void PlayerManager_OnNewWaveStart(object sender, EventArgs e) {
        OnEnemyShouldReadyAttack?.Invoke(this,EventArgs.Empty);
    }

    private void PlayerManager_OnEnemyStartAttack(object sender, EventArgs e) {
        OnEnemyShouldAttack?.Invoke(this, EventArgs.Empty);

        ShouldStartTimer = true;
    }

    private void Player_OnPlayerPathFollowed(object sender, System.EventArgs e) {
        foreach (Enemy enemy in SeenEnemyList) {
            if (enemy.gameObject.activeSelf) {
                OnTakeDamage?.Invoke(this, new OnTakeDamageEventArgs {enemy = enemy,damage = Player.Instance.GetDamage() });
            }
        }

        SeenEnemyList.Clear();
    }

    public void AddEnemyInSeenList(Enemy enemy) {
        SeenEnemyList.Add(enemy);
    }

    private void Update() {
        if (!allEnemiesKilled && ShouldStartTimer) {
            Timer += Time.deltaTime;

            if(Timer > MaxWaitTime) {
                ShouldStartTimer = false;
                Timer = 0f;

                OnStartAgain?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
