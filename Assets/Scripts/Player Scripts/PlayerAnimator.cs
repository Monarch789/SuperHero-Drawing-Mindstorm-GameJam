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
        playerManager.OnPlayerCanAttack += Player_OnDrawComplete;
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        var currentState = playerManager.GetState();
        switch (currentState)
        {
            case PlayerManager.PlayerStates.Running:
                if (animator.GetInteger("anim") != 1)
                {
                    Debug.Log("Setting animation to Running from HandleAnimations");
                    animator.SetInteger("anim", 1);
                }
                break;
            case PlayerManager.PlayerStates.Jumping:
                if (animator.GetInteger("anim") != 2)
                {
                    Debug.Log("Setting animation to Jumping from HandleAnimations");
                    animator.SetInteger("anim", 2);
                }
                break;
            case PlayerManager.PlayerStates.Attacking:
                if (animator.GetInteger("anim") != 3)
                {
                    Debug.Log("Setting animation to Attacking from HandleAnimations");
                    animator.SetInteger("anim", 3);
                }
                break;
            default:
                if (animator.GetInteger("anim") != 0)
                {
                    Debug.Log("Setting animation to Default from HandleAnimations");
                    animator.SetInteger("anim", 0);
                }
                break;
        }
    }

    private void PlayerManager_OnWaveStart(object sender, System.EventArgs e)
    {
        if (animator.GetInteger("anim") != 1)
        {
            Debug.Log("Setting animation to Running from PlayerManager_OnWaveStart");
            animator.SetInteger("anim", 1); // Running
        }
    }

    private void Player_OnDrawComplete(object sender, System.EventArgs e)
    {
        if (animator.GetInteger("anim") != 3)
        {
            Debug.Log("Setting animation to Attacking from Player_OnDrawComplete");
            animator.SetInteger("anim", 3); // Attacking
        }
    }

    private void OnDestroy()
    {
        playerManager.OnWaveStart -= PlayerManager_OnWaveStart;
        playerManager.OnPlayerCanAttack -= Player_OnDrawComplete;
    }
}
