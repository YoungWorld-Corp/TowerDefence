using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected int max_health = 50;
    protected int health = 50;
    protected float speed = 5f;

    protected HPBar hp_bar;

    public int Health
    {
        get { return health; } 
        set { health = value; }
    }
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            _originSpeed = value;
        }
    }
    protected float _originSpeed;  

    private Vector3 target;
    private int waypointIndex = 0;
    private MobSpawner _spawner = null;
    private int id;
    
    List<Vector3> wayPoints;
    private bool _isDestroying = false;
    
    private CcStatus _ccStatus; 

    void Start()
    {
    }

    void Update()
    {
        if (_isDestroying) return;
        
        CheckCC();

        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, target) <= 0.05f)
        {
            GetNextWaypoint();
        }
    }

    public void Initialize(
        MobType mobType,
        string resourcePath,
        List<Vector3> wayPoints,
        int mobId,
        MobSpawner spawner,
        int health,
        float speed
    )
    {
        transform.Translate(wayPoints[0]);
        SetSpriteRenderer(resourcePath);
        this.wayPoints = wayPoints;
        this._spawner = spawner;
        target = wayPoints[1];

        id = mobId;
        this.health = health;
        this.max_health = health;
        this.Speed = speed;

        hp_bar = GetComponentInChildren<HPBar>();
        hp_bar.SetHP(1f);
    }

    void SetSpriteRenderer(string resourcePath)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        spriteRenderer.sprite = Resources.Load(resourcePath, typeof(Sprite)) as Sprite;
        spriteRenderer.sortingOrder = 1;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= wayPoints.Count - 1)
        {
            GameState.Instance.OnChangeLife(-1);
            Die();
            return;
        }

        waypointIndex++;
        
        target = wayPoints[waypointIndex];
    }
    
    //health

    public void TakeDamage(int amount)
    {
        health -= amount;
        hp_bar.SetHP((float)health/max_health);
        if (health <= 0f)
        {
            Die();
        }
        
        GameState.Instance.damageTextSystem.AddDamageInfo(new DamageTextInfo(
            amount, gameObject, 1
        ));
    }
    
    public void TakeCc(CcStatus status)
    {
        if (status.slowDuration > 0)
        {
            _ccStatus.slowRate = status.slowRate;
            _ccStatus.slowDuration = status.slowDuration;
        }
        
        if (status.poisonDuration > 0)
        {
            _ccStatus.poisonRate = status.poisonRate;
            _ccStatus.poisonDuration = status.poisonDuration;
        }
        
        if (status.stunDuration > 0)
        {
            _ccStatus.stunDuration = status.stunDuration;
        }
    }
    
    public int GetID()
    {
        return id;
    }
    
    private void CheckCC()
    {
        if (_ccStatus.slowDuration > 0)
        {
            if (Mathf.Approximately(speed, _originSpeed))
            {
                speed *= _ccStatus.slowRate;
            }
            
            _ccStatus.slowDuration -= Time.deltaTime;
            if (_ccStatus.slowDuration <= 0)
            {
                speed = _originSpeed;
            }
        }
        
        if (_ccStatus.poisonDuration > 0)
        {
            _ccStatus.poisonDuration -= Time.deltaTime;
            if (_ccStatus.poisonDuration <= 0)
            {
                health -= (int)(_ccStatus.poisonRate * _ccStatus.poisonDuration);
            }
        }
        
        if (_ccStatus.stunDuration > 0)
        {
            if (speed != 0)
            {
                speed = 0f;
            }
            
            _ccStatus.stunDuration -= Time.deltaTime;
            if (_ccStatus.stunDuration <= 0)
            {
                speed = _originSpeed;
            }
        }
    }

    private void Die()
    {
        if (_isDestroying) return;
        
        _spawner.NotifyDie();
        
        //Destroy job
        _isDestroying = true;
        Destroy(this.gameObject);
    }
}

public struct CcStatus
{
    public float slowRate;
    public float slowDuration;
    
    public float poisonRate;
    public float poisonDuration;
    
    public float stunDuration;
}