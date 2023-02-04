using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    MobSpawnRule spawnRule;
    List<Vector3> wayPointsWorld;

    // Start is called before the first frame update
    void Start()
    {
        spawnRule = new MobSpawnRule(0, 10, 1, 0);

        TilemapGenerator tilemapGenerator = GetComponentInParent<TilemapGenerator>();
        List<Vector2Int> wayPoints = GetDefaultWayPoints();
        tilemapGenerator.InitializeTilemaps(wayPoints);
        wayPointsWorld = CellToWorld(wayPoints);
    }

    // Update is called once per frame
    void Update()
    {
        spawnRule.delta += Time.deltaTime;
        if (spawnRule.time < spawnRule.delta && spawnRule.num > 0)
        {
            spawnRule.delta = 0;
            spawnRule.num--;
            SpawnMob(spawnRule.mobId, wayPointsWorld);
        }
    }
    
    void SpawnMob(int mobId, List<Vector3> wayPointsWorld)
    {
        GameObject mobObj = new GameObject();
        Mob mob = mobObj.AddComponent<Mob>() as Mob;
        mob.Initialize(mobId, "Shark", wayPointsWorld);
    }

    List<Vector2Int> GetDefaultWayPoints()
    {
        List<Vector2Int> wayPoints = new List<Vector2Int>();
        wayPoints.Add(new Vector2Int(0, 0));
        wayPoints.Add(new Vector2Int(1, 0));
        wayPoints.Add(new Vector2Int(1, -7));
        wayPoints.Add(new Vector2Int(3, -7));
        wayPoints.Add(new Vector2Int(3, 0));
        wayPoints.Add(new Vector2Int(5, 0));
        wayPoints.Add(new Vector2Int(5, -7));
        wayPoints.Add(new Vector2Int(7, -7));
        wayPoints.Add(new Vector2Int(7, 0));
        wayPoints.Add(new Vector2Int(9, 0));
        wayPoints.Add(new Vector2Int(9, -7));
        wayPoints.Add(new Vector2Int(11, -7));
        wayPoints.Add(new Vector2Int(11, 0));
        wayPoints.Add(new Vector2Int(13, 0));
        wayPoints.Add(new Vector2Int(13, -7));
        return wayPoints;
    }

    List<Vector3> CellToWorld(List<Vector2Int> wayPoints)
    {
        List<Vector3> worldWayPoints = new List<Vector3>();
        foreach(var point in wayPoints)
        {
            Vector3 world = tilemap.CellToWorld(new Vector3Int(point.x, point.y, 0));
            world.x += 0.5f;
            worldWayPoints.Add(world);
        }
        return worldWayPoints;
    }
}

struct MobSpawnRule
{
    public int mobId;
    public int num;
    public float time;
    public float delta;

    public MobSpawnRule(int mobId, int num, float time, float delta)
    {
        this.mobId=mobId;
        this.num=num;
        this.time=time;
        this.delta=delta;
    }
}