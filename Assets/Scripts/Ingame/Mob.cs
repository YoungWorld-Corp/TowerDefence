using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{

    public float speed = 5f;

    private Vector3 target;
    private int waypointIndex = 0;
    private MobSpawner _spawner = null;
    private int id;
    
    List<Vector3> wayPoints;

    void Start()
    {
    }

    void Update()
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, target) <= 0.05f)
        {
            GetNextWaypoint();
        }
    }

    public void Initialize(MobType mobType, string resourcePath, List<Vector3> wayPoints, int mobId, MobSpawner spawner)
    {
        transform.Translate(wayPoints[0]);
        SetSpriteRenderer(resourcePath);
        this.wayPoints = wayPoints;
        this._spawner = spawner;
        target = wayPoints[1];

        id = mobId;
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
            Die();
            return;
        }

        waypointIndex++;
        
        target = wayPoints[waypointIndex];
    }
    
    //health
    
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    
    public int GetID()
    {
        return id;
    }

    private void Die()
    {
        //Destroy job
        Destroy(gameObject);

        _spawner.NotifyDie();
    }
}