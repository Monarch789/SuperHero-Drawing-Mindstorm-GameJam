using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour{

    //image to show isread to attack reference
    [SerializeField] private Image readyToAttackImage;

    //bool to see if its attacking this turn
    private bool isAttacking;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.TryGetComponent(out Player player)) {
            EnemyManager.Instance.AddEnemyInSeenList(this);
        }
    }


    private void Start() {
        readyToAttackImage.gameObject.SetActive(false);

        EnemyManager.Instance.OnEnemyShouldReadyAttack += EnemyManager_OnEnemyShouldReadyAttack;
        EnemyManager.Instance.OnEnemyShouldAttack += EnemyManager_OnEnemyShouldAttack;
    }

    private void EnemyManager_OnEnemyShouldAttack(object sender, EventArgs e) {
        if (isAttacking) {
            Debug.Log("Attack");
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
