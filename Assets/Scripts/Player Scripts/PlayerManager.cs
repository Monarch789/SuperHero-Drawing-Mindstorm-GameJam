using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour{
    //Singleton
    public static PlayerManager Instance {  get; private set; }

    //player reference
    private Player player;

    //event to send player to start drawing
    public event EventHandler OnPlayerCanAttack;

    //event to send camera manager to put the camera on waves display
    public event EventHandler OnNewWaveStart;

    //event to send EnemyManager for enemies to start attacking
    public event EventHandler OnEnemyStartAttack;

    //positions for different states
    [SerializeField] private Transform IdlePosition;
    [SerializeField] private Transform AttackingPosition;

    [SerializeField] private LayerMask FloorDeathLayerMask;

    //hitbox to get raycast distance
    private BoxCollider2D hitbox;
    private Rigidbody2D rigidBody;


    private float speed;

    //Different states of the player
    public enum PlayerStates {
        Idle,
        Running,
        Attacking,
        Jumping,
        Falling
    }

    private PlayerStates state;

    //bools to see if the player has reached the desired posiiton
    private bool HasReachedIdlePosition;
    private bool HasReachedAttackingPosition;

    //bool to see if the player is falling so check below
    private bool ShouldCheckBelow;

    //bool to see if it is player turn
    private bool isPlayerTurn;

    private void Awake() {
        Instance = this;
    
        hitbox = GetComponent<BoxCollider2D>();
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        HasReachedAttackingPosition = false;
        HasReachedIdlePosition = false;
        ShouldCheckBelow = false;

        state = PlayerStates.Running;

        isPlayerTurn = true;
        speed = 5f;


        player.OnDrawComplete += Player_OnDrawComplete;
        player.OnPlayerMoveStop += Player_OnPlayerMoveStop;
        player.OnPlayerPathFollowed += Player_OnPlayerPathFollowed;

        EnemyManager.Instance.OnStartAgain += EnemyManager_OnStartAgain;
    }

    private void EnemyManager_OnStartAgain(object sender, EventArgs e) {
        isPlayerTurn = true;

        HasReachedIdlePosition = false;
        HasReachedAttackingPosition = false;
    }

    private void Player_OnPlayerPathFollowed(object sender, EventArgs e) {
        PlayerStop();
    }

    private void Player_OnPlayerMoveStop(object sender, EventArgs e) {
        PlayerStop();
    }

    private void Player_OnDrawComplete(object sender, EventArgs e) {
        state = PlayerStates.Attacking;
    }

    private void Update() {
        if (isPlayerTurn) {

            rigidBody.gravityScale = 0f;

            if (!HasReachedIdlePosition) {
                //the player has not reached idle position

                transform.position = Vector2.MoveTowards(transform.position, IdlePosition.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, IdlePosition.position) < 0.05f) {
                    HasReachedIdlePosition = true;

                    OnNewWaveStart?.Invoke(this,EventArgs.Empty);

                    state = PlayerStates.Jumping;
                }
            }
            else if (!HasReachedAttackingPosition) {
                //the player has reached idle position and is now attacking

                transform.position = Vector2.MoveTowards(transform.position, AttackingPosition.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, AttackingPosition.position) < 0.05f) {
                    HasReachedAttackingPosition = true;
                }
            }
            else {
                //player has reached attacking position

                isPlayerTurn = false;
                OnPlayerCanAttack?.Invoke(this, EventArgs.Empty);
            }
        }

        else if (!HasReachedIdlePosition) {
            //it is not the players turn but he still hasnt reached the idle position

            //move towards the idle Position
            transform.position = Vector2.MoveTowards(transform.position,IdlePosition.position,speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, IdlePosition.position) < 0.05f) {
                HasReachedIdlePosition = true;

                OnEnemyStartAttack?.Invoke(this, EventArgs.Empty);
            }
        }

        if (ShouldCheckBelow) {
            float rayCastDistance = hitbox.size.y/2 + 0.1f;

            RaycastHit2D hitObject = Physics2D.Raycast(transform.position,Vector2.down,rayCastDistance,FloorDeathLayerMask);

            if (hitObject) {
                //player hit something

                if(hitObject.transform.tag == "Floor") {
                    //player hit the floor

                    ShouldCheckBelow = false;

                    state = PlayerStates.Running;
                    HasReachedIdlePosition = false;
                }
                else if(hitObject.transform.tag == "Death") {
                    //player fell down to death
                    ShouldCheckBelow = false;

                    Debug.Log("Death");
                }
            }
        }
    }

    private void PlayerStop() {
        state = PlayerStates.Falling;

        ShouldCheckBelow = true;
    }

}
