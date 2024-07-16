using System;
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
    }

    private void EnemyManager_OnEnemyShouldReadyAttack(object sender, EventArgs e) {
        float randomFloat = UnityEngine.Random.Range(0.0f, 1.0f);

        isAttacking = randomFloat <= 0.5f;

        if (isAttacking) {
            readyToAttackImage.gameObject.SetActive(true);
        }
    }
}
