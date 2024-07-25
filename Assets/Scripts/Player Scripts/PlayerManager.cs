using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton
    public static PlayerManager Instance { get; private set; }

    // Player reference
    private Player player;

    // Event to send player to start drawing
    public event EventHandler OnPlayerCanAttack;

    // Event to send camera manager to put the camera on waves display
    public event EventHandler OnWaveStart;

    // Event to say to the floor to disappear
    public event EventHandler OnFloorDisappear;

    // Positions for different states
    [SerializeField] private Transform IdlePosition;
    [SerializeField] private Transform AttackingPosition;

    [SerializeField] private LayerMask FloorDeathLayerMask;

    // Hitbox to get raycast distance
    private BoxCollider2D hitbox;
    private Rigidbody2D rigidBody;

    private float speed;

    private bool isPlayerDead;

    // Different states of the player
    public enum PlayerMoveStates
    {
        Running,
        Attacking,
        Jumping,
        Idle
    }

    //event for state changes
    public class OnMoveStateChangeEventArgs : EventArgs {
        public PlayerMoveStates state;
    }
    public event EventHandler<OnMoveStateChangeEventArgs> OnPlayerMoveStateChange;

    // Bools to see if the player has reached the desired position
    private bool HasReachedIdlePosition;
    private bool HasReachedAttackingPosition;

    private bool isGameStarted;

    private void Awake()
    {
        Instance = this;

        hitbox = GetComponent<BoxCollider2D>();
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        HasReachedAttackingPosition = false;
        HasReachedIdlePosition = false;
        isGameStarted = false;

        OnPlayerMoveStateChange?.Invoke(this, new OnMoveStateChangeEventArgs { state = PlayerMoveStates.Idle});

        isPlayerDead = false;
        speed = 5f;

        player.OnDrawComplete += Player_OnDrawComplete;
        player.OnPlayerMoveStop += Player_OnPlayerMoveStop;
        player.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;
        player.OnPlayerDeath += Player_OnPlayerDeath;

        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e) {
        isGameStarted = true;

        OnPlayerMoveStateChange?.Invoke(this, new OnMoveStateChangeEventArgs { state = PlayerMoveStates.Running });
    }

    private void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        isPlayerDead = true;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e)
    {
        PlayerStop();
    }

    private void Player_OnPlayerMoveStop(object sender, EventArgs e)
    {
        PlayerStop();
    }

    private void Player_OnDrawComplete(object sender, EventArgs e)
    {
        OnPlayerMoveStateChange?.Invoke(this, new OnMoveStateChangeEventArgs { state = PlayerMoveStates.Attacking });
    }

    private void Update(){
        if (!isPlayerDead && isGameStarted)
        {
            // If the player is alive then make them go towards the idle and attack positions
            rigidBody.gravityScale = 0f;

            if (!HasReachedIdlePosition)
            {
                // The player has not reached idle position
                transform.position = Vector2.MoveTowards(transform.position, IdlePosition.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, IdlePosition.position) < 0.05f)
                {
                    HasReachedIdlePosition = true;

                    OnWaveStart?.Invoke(this, EventArgs.Empty);

                    OnPlayerMoveStateChange?.Invoke(this, new OnMoveStateChangeEventArgs { state = PlayerMoveStates.Jumping });
                }
            }
            else if (!HasReachedAttackingPosition)
            {
                // The player has reached idle position and is now attacking
                transform.position = Vector2.MoveTowards(transform.position, AttackingPosition.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, AttackingPosition.position) < 0.05f)
                {
                    HasReachedAttackingPosition = true;

                    OnFloorDisappear?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                // Player has reached attacking position
                OnPlayerCanAttack?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void PlayerStop(){
        OnPlayerMoveStateChange?.Invoke(this, new OnMoveStateChangeEventArgs { state = PlayerMoveStates.Idle });
    }

    private void OnDestroy(){
        player.OnDrawComplete -= Player_OnDrawComplete;
        player.OnPlayerMoveStop -= Player_OnPlayerMoveStop;
        player.OnPlayerPathFollowed -= Player_OnPlayerPathFollowed;
        player.OnPlayerDeath -= Player_OnPlayerDeath;

        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
    }
}
