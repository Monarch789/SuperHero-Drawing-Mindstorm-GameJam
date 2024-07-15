using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{

    //Singleton
    public static EnemyManager Instance {  get; private set; }

    private List<Enemy> SeenEnemyList;

    private void Awake() {
        Instance = this;

        SeenEnemyList = new List<Enemy>();
    }

    private void Start() {
        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
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
