
using UnityEngine;

public class EnemyAnimation : MonoBehaviour{
    private const string AttackTrigger = "Attack";
    private const string DeadBool = "Dead";


    private Animator animator;

    [SerializeField] private Enemy enemy;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {

        enemy.OnDead += Enemy_OnDead;
        enemy.OnThisAttack += Enemy_OnThisAttack;
    }

    private void Enemy_OnThisAttack(object sender, System.EventArgs e) {
        animator.SetTrigger(AttackTrigger);
    }

    private void Enemy_OnDead(object sender, System.EventArgs e) {
        animator.SetBool(DeadBool,true);
    }

    private void OnDestroy() {
        enemy.OnDead -= Enemy_OnDead;
        enemy.OnThisAttack -= Enemy_OnThisAttack;
    }
}
