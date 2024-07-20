using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IHasProgress{
    //reference of Enemy Scriptable Object
    [SerializeField] private EnemySO enemyData;

    //health since we cant subtract from SO
    private float health;


    //image to show isread to attack reference
    [SerializeField] private Image readyToAttackImage;

    //bool to see if its attacking this turn
    private bool isAttacking;

    //bool to see if the player collided while following the line
    private bool didCollideWhileFollowingTheLine;

    //event to send player to reduce its health 
    public class OnAttackEventArgs : EventArgs { public float Damage; }
    public static event EventHandler<OnAttackEventArgs> OnAttack;

    public event EventHandler<IHasProgress.OnProgressChangeEventAgs> OnProgressChanged;

    private void OnTriggerEnter2D(Collider2D other) {
        if(didCollideWhileFollowingTheLine && other.gameObject.TryGetComponent(out Player player)) {
            EnemyManager.Instance.AddEnemyInSeenList(this);
        }
    }


    private void Start() {
        didCollideWhileFollowingTheLine = false;

        health = enemyData.Health;

        readyToAttackImage.gameObject.SetActive(false);

        //spawn the enemy model
        Instantiate(enemyData.EnemyModelPrefab,transform);

        EnemyManager.Instance.OnEnemyShouldReadyAttack += EnemyManager_OnEnemyShouldReadyAttack;
        EnemyManager.Instance.OnEnemyShouldAttack += EnemyManager_OnEnemyShouldAttack;
        EnemyManager.Instance.OnTakeDamage += EnemyManager_OnTakeDamage;

        Player.Instance.OnDrawComplete += Player_OnDrawComplete;
        Player.Instance.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
        Player.Instance.OnPlayerMoveStop += Player_OnPlayerMoveStop;
    }

    private void Player_OnPlayerMoveStop(object sender, EventArgs e) {
        didCollideWhileFollowingTheLine = false;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        didCollideWhileFollowingTheLine = false;
    }

    private void Player_OnDrawComplete(object sender, EventArgs e) {
        didCollideWhileFollowingTheLine = true;
    }

    private void EnemyManager_OnTakeDamage(object sender, EnemyManager.OnTakeDamageEventArgs e) {
        if(e.enemy == this) {
            //this is the enemy that should take damage
            health -= e.damage;

            //send event to make the health bar
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventAgs { progressAmount = health/enemyData.Health });
            if(health <= 0) {
                gameObject.SetActive(false);
            }
        }
    }

    private void EnemyManager_OnEnemyShouldAttack(object sender, EventArgs e) {
        if (isAttacking && gameObject.activeSelf) {
            //if the enemy is not dead and it was attacking

            OnAttack?.Invoke(this, new OnAttackEventArgs { Damage = enemyData.Damage });
            readyToAttackImage.gameObject.SetActive(false);
        }
    }

    private void EnemyManager_OnEnemyShouldReadyAttack(object sender, EventArgs e) {
        
        isAttacking = UnityEngine.Random.Range(0, 2) == 0;

        if (isAttacking) {
            readyToAttackImage.gameObject.SetActive(true);
        }
    }
}
