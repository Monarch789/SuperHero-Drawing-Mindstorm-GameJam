using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string AnimationInteger = "anim";

    private Animator animator;
    private PlayerManager playerManager;

    [SerializeField] private GameObject trailLinePrefab; // Reference to the trail_line prefab
    [SerializeField] private Transform trailTransform; // Reference to the Trail GameObject

    //saving the scale of the trail
    private Vector3 scaleOfTrail;

    private GameObject trailObject; // Reference to the instantiated trail object
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>(); // This will find the Animator in the child GameObject
    }

    private void Start()
    {
        playerManager.OnPlayerMoveStateChange += PlayerManager_OnPlayerMoveStateChange;
        Player.Instance.OnProgressChanged += Player_OnProgressChanged;
    }

    private void Player_OnProgressChanged(object sender, IHasProgress.OnProgressChangeEventAgs e) {
        trailObject.transform.localScale = scaleOfTrail * e.progressAmount;
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

            trailObject = Instantiate(trailLinePrefab, Player.Instance.transform);

            scaleOfTrail = trailObject.transform.localScale;

            Debug.Log("Trail Line instantiated at the Trail position.");
        }
        else {
            Debug.LogError("Trail Line prefab or Trail Transform is not assigned.");
        }
    }

    private void OnDestroy(){
        playerManager.OnPlayerMoveStateChange -= PlayerManager_OnPlayerMoveStateChange;
        Player.Instance.OnProgressChanged -= Player_OnProgressChanged;
    }
}
