using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


struct MobSpawnRule
{
    public MobType mobType;
    public int num;
    public float spawnGap;
    public float delta;
    public int Hp;
    public float speed;

    private static int mobId = 0;

    public MobSpawnRule(MobType mobId, int num, int Hp, float speed, float spawnGap)
    {
        this.mobType=mobId;
        this.num=num;
        this.Hp=Hp;
        this.speed=speed;
        this.spawnGap=spawnGap;
        this.delta = 0;
    }

    public int GetNextMobId()
    {
        return mobId++;
    }
}
struct SpawnState
{
    public int remain;
    public SpawnState(MobSpawnRule spawnRule)
    {
        remain = spawnRule.num;
    }
}

public enum MobType
{
    Shark = 0,
    JellyFish = 1,
}

public class MobSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject Prefab_Shark;
    MobSpawnRule spawnRule;
    List<Vector3> wayPointsWorld;
    SpawnState spawnState;

    

    // Start is called before the first frame update
    void Start()
    {
        TilemapGenerator tilemapGenerator = GetComponentInParent<TilemapGenerator>();
        List<Vector2Int> wayPoints = GetDefaultWayPoints();
        tilemapGenerator.InitializeTilemaps(wayPoints);

        GameState.Instance.mobSpawner = this;
    }

    public void StartPhase(int stage)
    {
        spawnRule = MobData.GetMobSpawnRule(stage);
        spawnState = new SpawnState(spawnRule);

        List<Vector2Int> wayPoints = GetDefaultWayPoints();
        wayPointsWorld = CellToWorld(wayPoints);
    }

    // Update is called once per frame
    void Update()
    {
        spawnRule.delta += Time.deltaTime;
        if (spawnRule.spawnGap < spawnRule.delta && spawnRule.num > 0)
        {
            spawnRule.delta = 0;
            spawnRule.num--;
            SpawnMob(spawnRule.mobType, wayPointsWorld, spawnRule.Hp, spawnRule.speed);
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    void SpawnMob(MobType mobType, List<Vector3> wayPointsWorld, int Hp, float speed)
    {
        GameObject sharkObject =  Instantiate(Prefab_Shark, wayPointsWorld[0], Quaternion.identity);
        Mob sharkMob = sharkObject.GetComponent<Mob>();

        int mobId = spawnRule.GetNextMobId();
        sharkMob.name = "Mob_" + mobId;
        sharkMob.Initialize(mobType, "Characters/Shark", wayPointsWorld, mobId, this);
        sharkMob.health = Hp;
        sharkMob.speed = speed;
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

    public void NotifyDie()
    {
        spawnState.remain--;
        if (spawnState.remain == 0)
        {
            GameState.Instance.SetGamePhase(EGamePhase.Picking);
        }
    }
}

