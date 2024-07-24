using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>(); // This will find the Animator in the child GameObject
    }

    private void Start()
    {
        playerManager.OnWaveStart += PlayerManager_OnWaveStart;
        playerManager.OnPlayerCanAttack += PlayerManager_OnPlayerCanAttack;
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        switch (playerManager.GetState())
        {
            case PlayerManager.PlayerStates.Running:
                animator.SetInteger("anim", 1);
                break;
            case PlayerManager.PlayerStates.Jumping:
                animator.SetInteger("anim", 2);
                break;
            case PlayerManager.PlayerStates.Attacking:
                animator.SetInteger("anim", 3);
                break;
            default:
                animator.SetInteger("anim", 0);
                break;
        }
    }

    private void PlayerManager_OnWaveStart(object sender, System.EventArgs e)
    {
        animator.SetInteger("anim", 1); // Running
    }

    private void PlayerManager_OnPlayerCanAttack(object sender, System.EventArgs e)
    {
        animator.SetInteger("anim", 3); // Attacking
    }

    private void OnDestroy()
    {
        playerManager.OnWaveStart -= PlayerManager_OnWaveStart;
        playerManager.OnPlayerCanAttack -= PlayerManager_OnPlayerCanAttack;
    }
}
