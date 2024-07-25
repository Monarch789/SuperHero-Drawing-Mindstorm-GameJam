using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string AnimationInteger = "anim";

    private Animator animator;
    private PlayerManager playerManager;

    [SerializeField] private GameObject trailLinePrefab; // Reference to the trail_line prefab
    [SerializeField] private Transform trailTransform; // Reference to the Trail GameObject

    private GameObject trailObject; // Reference to the instantiated trail object
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>(); // This will find the Animator in the child GameObject
    }

    private void Start()
    {
        playerManager.OnPlayerMoveStateChange += PlayerManager_OnPlayerMoveStateChange;   
    }

    private void PlayerManager_OnPlayerMoveStateChange(object sender, PlayerManager.OnMoveStateChangeEventArgs e) {
        if(e.state == PlayerManager.PlayerMoveStates.Idle) {
            animator.SetInteger(AnimationInteger,0);
        }
        else if(e.state == PlayerManager.PlayerMoveStates.Running) {
            animator.SetInteger(AnimationInteger, 1);
        }
        else if(e.state == PlayerManager.PlayerMoveStates.Jumping) {
            animator.SetInteger(AnimationInteger, 2);
        }
        else {
            animator.SetInteger(AnimationInteger, 3);
            OnAttackStart();
        }
    }

    private void OnAttackStart() {
        // Instantiate the trailLinePrefab at the Trail position
        if (trailLinePrefab != null && trailTransform != null) {
            if (trailObject != null) {
                Destroy(trailObject); // Optionally destroy the old trail if it exists
            }

            trailObject = Instantiate(trailLinePrefab, trailTransform.position, Quaternion.identity, trailTransform);
            Debug.Log("Trail Line instantiated at the Trail position.");
        }
        else {
            Debug.LogError("Trail Line prefab or Trail Transform is not assigned.");
        }
    }

    private void OnDestroy(){
        playerManager.OnPlayerMoveStateChange -= PlayerManager_OnPlayerMoveStateChange;
    }
}
