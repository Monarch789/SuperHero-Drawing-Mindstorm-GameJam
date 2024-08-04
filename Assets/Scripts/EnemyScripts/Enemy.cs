using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour,IHasProgress,IHasDeathEffect{
    //reference of Enemy Scriptable Object
    [SerializeField] private EnemySO enemyData;

    //health since we cant subtract from SO
    private float health;
    private float damage;

    //event to send player to reduce its health 
    public class OnAttackEventArgs : EventArgs { public float Damage; }
    public static event EventHandler<OnAttackEventArgs> OnAttack;

    public event EventHandler<IHasProgress.OnProgressChangeEventAgs> OnProgressChanged;

    public event EventHandler OnDeath;

    //event to send EnemyManager
    public static event EventHandler OnEnemyDeath;

    //events for animation
    public event EventHandler OnDead;
    public event EventHandler OnThisAttack;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.TryGetComponent(out Player player)) {
            //attack the player first then reduce health
            OnAttack?.Invoke(this, new OnAttackEventArgs { Damage = damage });
            OnThisAttack?.Invoke(this,EventArgs.Empty);

            health -= player.GetDamage();

            health = Mathf.Clamp(health, 0, enemyData.Health);

            MobileVibrations.Instance.VibrateMobile();

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventAgs { progressAmount = health / enemyData.Health });

            if(health <= 0) {
                //enemy is dead
                OnDeath?.Invoke(this, EventArgs.Empty);
                OnEnemyDeath?.Invoke(this, EventArgs.Empty);
                OnDead?.Invoke(this, EventArgs.Empty);


                Money.Instance.IncreaseMoney(enemyData.moneyIncrease);
            }
        }
    }


    private void Start() {
        health = enemyData.Health;
        damage = enemyData.Damage;
    }
}
