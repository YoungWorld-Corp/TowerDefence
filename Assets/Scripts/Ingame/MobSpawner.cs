using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    MobSpawnRule spawnRule;
    Vector3 _startLoc = new Vector3(-10.5f, 4.15f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        spawnRule = new MobSpawnRule(0, 10, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        spawnRule.delta += Time.deltaTime;
        if (spawnRule.time < spawnRule.delta && spawnRule.num > 0)
        {
            spawnRule.delta = 0;
            spawnRule.num--;
            SpawnMob(spawnRule.mobId, _startLoc);
        }
    }
    
    void SpawnMob(int mobId, Vector3 startLoc)
    {
        GameObject mobObj = new GameObject();
        Mob mob = mobObj.AddComponent<Mob>() as Mob;
        mob.Initialize(mobId, "Shark", startLoc);
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