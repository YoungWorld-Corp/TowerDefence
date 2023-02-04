using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float speed = 5f;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    public void Initialize(int mobId, string resourcePath, Vector3 startLoc)
    {
        transform.Translate(startLoc);
        SetSpriteRenderer(resourcePath);
    }

    void SetSpriteRenderer(string resourcePath)
    {
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        spriteRenderer.sprite = Resources.Load(resourcePath, typeof(Sprite)) as Sprite;
        spriteRenderer.sortingOrder = 1;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }
}
