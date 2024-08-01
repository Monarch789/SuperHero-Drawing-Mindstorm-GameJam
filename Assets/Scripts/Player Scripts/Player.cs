using System;
using UnityEngine;

public class Player : MonoBehaviour, IHasProgress, IHasDeathEffect{
    //Singleton
    public static Player Instance { get; private set; }

    //strings for health and damage
    private const string PlayerMaxHealthString = "MaxPlayerHealth";
    private const string PlayerDamageString = "PlayerDamage";


    //events for health and damage sounds
    public event EventHandler OnHealthIncreased;
    public event EventHandler OnDamageIncreased;

    //event to say to draw manager to start drawing
    public class OnPlayerTouchEventArgs : EventArgs { public Camera lookCamera; }

    public event EventHandler<OnPlayerTouchEventArgs> OnPlayerTouch;
    public event EventHandler OnDrawComplete;

    public event EventHandler OnPlayerPathFollowed;

    //event to send the draw manager and player line follow to cancel movement
    public event EventHandler OnPlayerMoveStop;

    //event to send the player follow Line to move towards next position
    public event EventHandler OnMoveTowrdsNextPoint;

    //healrh events
    public event EventHandler<IHasProgress.OnProgressChangeEventAgs> OnProgressChanged;
    public event EventHandler OnPlayerDeath;

    //death effect event
    public event EventHandler OnDeath;

    //reference of camera
    private Camera mainCam;

    //bool to see if the player started drawing
    private bool didDrawFromPlayer;

    //bool to see if the the player has followed the line so he can start drawing again
    private bool hasFollowedPath;

    //bool to see if the player can atack
    private bool canAttack;

    //bool to see that if the gaem is paused only then allow line drawing
    private bool isGamePaused;

    //reference of the player scipts
    private PlayerFollowLine followLine;
    private PlayerManager playerManager;

    //references for checking if the player collided with the floor
    [SerializeField] private PlayerCollideFloor sideCollider;
    [SerializeField] private PlayerCollideFloorDownwards downCollider;
    [SerializeField] private GameObject PlayerModel;


    //health and damage
    private float MaxHealth;
    private float health;
    private float damage;

    private void Awake()
    {
        Instance = this;

        followLine = GetComponent<PlayerFollowLine>();
        playerManager = GetComponent<PlayerManager>();

        MaxHealth = PlayerPrefs.GetFloat(PlayerMaxHealthString,50);
        damage = PlayerPrefs.GetFloat(PlayerDamageString,5);

    }

    private void Start()
    {
        mainCam = Camera.main;
        didDrawFromPlayer = false;
        hasFollowedPath = true;
        canAttack = false;
        isGamePaused = false;

        health = MaxHealth;

        InputManager.Instance.OnTouchStarted += InputManager_OnTouchStarted;
        InputManager.Instance.OnTouchEnded += InputManager_OnTouchEnded;
        followLine.OnPathFollowed += FollowLine_OnPathFollowed;
        Enemy.OnAttack += Enemy_OnAttack;

        sideCollider.OnPlayerCollideWithFloorFromSide += LeftCollider_OnPlayerCollideWithFloorFromSide;
        downCollider.OnColliderFloorFromDown += DownCollider_OnColliderFloorFromDown;

        playerManager.OnPlayerCanAttack += PlayerManager_OnPlayerCanAttack;
        
        SpikeWall.OnPlayerCollisionWithSpikeWall += SpikeWall_OnPlayerCollisionWithSpikeWall;
        HealthBuff.OnHealthAdd += HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd += DamageBuff_OnDamageAdd;

        GameManager.Instance.OnPause += GameManager_OnPause;
        GameManager.Instance.OnUnPause += GameManager_OnUnPause;

        IncreaseBuffsUI.Instance.OnHealthInreased += IncreaseBuffs_OnHealthInreased;
        IncreaseBuffsUI.Instance.OnDamageInreased += IncreaseBuffs_OnDamageInreased;

    }

    private void IncreaseBuffs_OnDamageInreased(object sender, EventArgs e) {
        damage += 5;

        OnDamageIncreased?.Invoke(this, EventArgs.Empty);

        PlayerPrefs.SetFloat(PlayerDamageString,damage);
        PlayerPrefs.Save();
    }

    private void IncreaseBuffs_OnHealthInreased(object sender, EventArgs e) {
        MaxHealth += 10;

        OnHealthIncreased?.Invoke(this, EventArgs.Empty);

        PlayerPrefs.SetFloat(PlayerMaxHealthString,MaxHealth);
        PlayerPrefs.Save();

        health = MaxHealth;
    }

    private void GameManager_OnUnPause(object sender, EventArgs e)
    {
        isGamePaused = false;
    }

    private void GameManager_OnPause(object sender, EventArgs e)
    {
        isGamePaused = true;
    }


    private void DamageBuff_OnDamageAdd(object sender, DamageBuff.OnDamageAddEventArgs e)
    {
        OnDamageIncreased?.Invoke(this, EventArgs.Empty);
        damage += e.addDamage;

    }

    private void HealthBuff_OnHealthAdd(object sender, HealthBuff.OnHealthAddEventArgs e)
    {
        health += e.addHealth;

        OnHealthIncreased?.Invoke(this,EventArgs.Empty);

        health = Mathf.Clamp(health, 0f, MaxHealth);

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventAgs { progressAmount = health / MaxHealth });
    }

    private void SpikeWall_OnPlayerCollisionWithSpikeWall(object sender, EventArgs e)
    {
        ////send events to make th eplayer follow the mext point on the line

        OnMoveTowrdsNextPoint?.Invoke(this, EventArgs.Empty);

        health -= .5f;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventAgs { progressAmount = health/MaxHealth});

        if(health <= 0) {
            OnPlayerMoveStop?.Invoke(this, EventArgs.Empty);
            PlayerModel.SetActive(true);

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            OnDeath?.Invoke(this, EventArgs.Empty);
        }

    }

    private void Enemy_OnAttack(object sender, Enemy.OnAttackEventArgs e)
    {
        health -= e.Damage;

        health = Mathf.Clamp(health, 0f, MaxHealth);

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangeEventAgs { progressAmount = health / MaxHealth });

        if (health <= 0)
        {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void PlayerManager_OnPlayerCanAttack(object sender, EventArgs e)
    {
        canAttack = true;
    }

    private void DownCollider_OnColliderFloorFromDown(object sender, EventArgs e)
    {
        OnMoveTowrdsNextPoint?.Invoke(this, EventArgs.Empty);
    }

    private void LeftCollider_OnPlayerCollideWithFloorFromSide(object sender, EventArgs e)
    {
        OnPlayerMoveStop?.Invoke(this, EventArgs.Empty);

        PlayerModel.SetActive(true);
    }

    private void FollowLine_OnPathFollowed(object sender, EventArgs e)
    {
        hasFollowedPath = true;

        //send event to say to draw manager to destory line
        OnPlayerPathFollowed?.Invoke(this, EventArgs.Empty);

        PlayerModel.SetActive(true);
    }

    private void InputManager_OnTouchEnded(object sender, EventArgs e)
    {
        if (didDrawFromPlayer)
        {
            //the player started from he hitbox so there are lines drawn

            OnDrawComplete?.Invoke(this, EventArgs.Empty);

            PlayerModel.SetActive(false);

            didDrawFromPlayer = false;
        }
    }

    private void InputManager_OnTouchStarted(object sender, System.EventArgs e)
    {
        if (!isGamePaused && canAttack && hasFollowedPath)
        {

            //see if the touch is on the player
            var rayCastHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(InputManager.Instance.GetTouchPosition()));

            if (rayCastHit /*player touch something*/ && rayCastHit.collider.gameObject.TryGetComponent(out Player player))
            {
                //the player started touching from the player hitbox
                OnPlayerTouch?.Invoke(this, new OnPlayerTouchEventArgs { lookCamera = mainCam });

                //player started form hitbo
                didDrawFromPlayer = true;
                hasFollowedPath = false;
                canAttack = false;
            }
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnTouchStarted -= InputManager_OnTouchStarted;
        InputManager.Instance.OnTouchEnded -= InputManager_OnTouchEnded;
        followLine.OnPathFollowed -= FollowLine_OnPathFollowed;
        Enemy.OnAttack -= Enemy_OnAttack;

        sideCollider.OnPlayerCollideWithFloorFromSide -= LeftCollider_OnPlayerCollideWithFloorFromSide;
        downCollider.OnColliderFloorFromDown -= DownCollider_OnColliderFloorFromDown;

        playerManager.OnPlayerCanAttack -= PlayerManager_OnPlayerCanAttack;
        
        SpikeWall.OnPlayerCollisionWithSpikeWall -= SpikeWall_OnPlayerCollisionWithSpikeWall;
        HealthBuff.OnHealthAdd -= HealthBuff_OnHealthAdd;
        DamageBuff.OnDamageAdd -= DamageBuff_OnDamageAdd;

        GameManager.Instance.OnPause -= GameManager_OnPause;
        GameManager.Instance.OnUnPause -= GameManager_OnUnPause;

        IncreaseBuffsUI.Instance.OnHealthInreased -= IncreaseBuffs_OnHealthInreased;
        IncreaseBuffsUI.Instance.OnDamageInreased -= IncreaseBuffs_OnDamageInreased;

    }
}
