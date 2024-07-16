using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{

    //Singleton
    public static EnemyManager Instance {  get; private set; }

    private List<Enemy> SeenEnemyList;

    //event to send enemies to say that they should ready their attack
    public event EventHandler OnEnemyShouldReadyAttack;

    //bool to see if it the enemy turn
    private bool isEnemyTurn;

    private void Awake() {
        Instance = this;

        SeenEnemyList = new List<Enemy>();
    }

    private void Start() {
        isEnemyTurn = false;

        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;

        OnEnemyShouldReadyAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Player_OnPlayerPathFollowed(object sender, System.EventArgs e) {
        foreach (Enemy enemy in SeenEnemyList) {
            if(enemy != null)
                Destroy(enemy.gameObject);
        }
    }

    public void AddEnemyInSeenList(Enemy enemy) {
        SeenEnemyList.Add(enemy);
    }

}
