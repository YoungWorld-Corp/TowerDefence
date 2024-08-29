using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MobSpawner3D : MobSpawner
{
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.mobSpawner = this;
    }

    public override void StartPhase(int stage)
    {
        spawnRule = MobData.GetMobSpawnRule(stage);
        spawnState = new SpawnState(spawnRule);
        wayPointsWorld = GetWaypointObjectsFromScene();
    }

    // Update is called once per frame
    void Update()
    {
        spawnRule.delta += Time.deltaTime;
        if (spawnRule.spawnGap < spawnRule.delta && spawnRule.num > 0)
        {
            spawnRule.delta = 0;
            spawnRule.num--;
            SpawnMob(spawnRule.mobType, spawnRule.Hp, spawnRule.speed);
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    void SpawnMob(MobType mobType, int Hp, float speed)
    {
        GameObject sharkObject =  Instantiate(Prefab_Shark, wayPointsWorld[0], Quaternion.identity);
        Mob sharkMob = sharkObject.GetComponent<Mob>();

        int mobId = spawnRule.GetNextMobId();
        sharkMob.name = "Mob_" + mobId;
        sharkMob.Initialize(mobType, "Characters/Shark", wayPointsWorld, mobId, this, Hp, speed);
    }

    List<Vector3> GetWaypointObjectsFromScene()
    {
        // var waypoint3Ds = FindObjectsByType<Waypoint3D>(FindObjectsSortMode.InstanceID);
        // List<Vector3> wayPoints = new List<Vector3>();
        // foreach (var waypoint in waypoint3Ds)
        // {
        //     wayPoints.Add(waypoint.transform.position);
        // }
        //
        List<Vector3> wayPoints = new List<Vector3>();
        wayPoints.Add(new Vector3(-1, 0, -1));
        wayPoints.Add(new Vector3(4, 0, -1));
        wayPoints.Add(new Vector3(4, 0, 4));

        return wayPoints;
    }

    public override void NotifyDie()
    {
        spawnState.remain--;
        if (spawnState.remain == 0)
        {
            GameState.Instance.SetGamePhase(EGamePhase.Picking);
        }
    }
}

