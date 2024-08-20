using UnityEngine;

public class Meta
{
    public enum TowerPower
    {
        Level0 = 30,
        Level1 = 50,
        Level2 = 50,
        Level3 = 70,
        Level4 = 80,
        Level5 = 100,
        Level6 = 120,
        Level7 = 140,
        Level8 = 160,
        Level9 = 180,
        Level10 = 200,
        Level11 = 200,
        Level12 = 300,
        Level13 = 500,
    }


    public static GameObject ProjectileFromLevel(int level)
    {
        switch (level)
        {
            case 0: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/SlowProjectile_0");
            case 1: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/StunProjectile_1");
            case 2: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_2");
            case 3:
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_3");
            case 4: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_4");
            case 5: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_5");
            case 6: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_6");
            case 7: case 8: 
                return (GameObject)Resources.Load("Prefabs/Projectiles/SplashProjectile_6");
            case 9: case 10:case 11: case 12: case 13:
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_6");
            default:
                return (GameObject)Resources.Load("Prefabs/Projectiles/Projectile_0");
        }
    }
    
    public static int TowerAttackRadiusFromLevel(int level)
    {
        switch (level)
        {
            case 0: case 1: case 2: case 3:
                return 3;
            case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                return 4;
            case 11: case 12: case 13:
                return 5;
            
            default:
                return 3;
        }
    }
    public static float TowerAttackCooldownFromLevel(int level)
    {
        switch (level)
        {
            case 0:
                return 2f;
            case 1:
                return 2f;
            case 2: case 3:
                return 1f;
            case 4: case 5: case 6: case 7: case 8: case 9: case 10:
                return 0.5f;
            case 11: case 12: case 13:
                return 0.1f;
            
            default:
                return 1f;
        }
    }

    public static int GetTowerPower(int level, int highestNumber)
    {
        if (highestNumber == 1) highestNumber = 20;
        float multiple = 1f + (highestNumber * 0.1f);

        return (int)(GetTowerPowerFromLevel(level) * multiple);
    }
    public static int GetTowerPowerFromLevel(int level)
    {
        TowerPower power;
        switch (level)
        {
            case 0:
                power = TowerPower.Level0;
                break;
            case 1:
                power = TowerPower.Level1;
                break;
            case 2:
                power = TowerPower.Level2;
                break;
            case 3:
                power = TowerPower.Level3;
                break;
            case 4:
                power = TowerPower.Level4;
                break;
            case 5:
                power = TowerPower.Level5;
                break;
            case 6:
                power = TowerPower.Level6;
                break;
            case 7:
                power = TowerPower.Level7;
                break;
            case 8:
                power = TowerPower.Level8;
                break;
            case 9:
                power = TowerPower.Level9;
                break;
            case 10:
                power = TowerPower.Level10;
                break;
            case 11:
                power = TowerPower.Level11;
                break;
            case 12:
                power = TowerPower.Level12;
                break;
            case 13:
                power = TowerPower.Level13;
                break;
            default:
                power = TowerPower.Level0;
                break;
        }

        return (int)power;
    }
}

class MobData
{
    public static MobSpawnRule GetMobSpawnRule(int stage)
    {
        switch (stage)
        {
            case 0:
                return new MobSpawnRule(MobType.Shark, 10, 50, 3, 1);
            case 1:
                return new MobSpawnRule(MobType.Shark, 20, 100, 3, 0.5f);
            case 2:
                return new MobSpawnRule(MobType.Shark, 40, 200, 3, 0.25f);
            case 3:
                return new MobSpawnRule(MobType.Shark, 20, 400, 3, 0.5f);
            case 4:
                return new MobSpawnRule(MobType.Shark, 40, 800, 3, 0.25f);
            case 5:
                return new MobSpawnRule(MobType.Shark, 1, 10000, 0.5f, 0.2f);
            case 6:
                return new MobSpawnRule(MobType.Shark, 100, 3200, 3, 0.1f);
            case 7:
                return new MobSpawnRule(MobType.Shark, 100, 6400, 3, 0.1f);
            case 8:
                return new MobSpawnRule(MobType.Shark, 50, 12800, 3, 0.2f);
            case 9:
                return new MobSpawnRule(MobType.Shark, 50, 25600, 3, 0.2f);
            case 10:
                return new MobSpawnRule(MobType.Shark, 10, 512000, 1, 1f);
            case 11:
                return new MobSpawnRule(MobType.Shark, 100, 102400, 3, 0.1f);
            case 12:
                return new MobSpawnRule(MobType.Shark, 100, 204800, 3, 0.1f);
            case 13:
                return new MobSpawnRule(MobType.Shark, 100, 300000, 3, 0.1f);
            case 14:
                return new MobSpawnRule(MobType.Shark, 100, 400000, 3, 0.1f);
            case 15:
                return new MobSpawnRule(MobType.Shark, 5, 50000000, 3, 1f);
            case 16:
                return new MobSpawnRule(MobType.Shark, 100, 1000000, 3, 0.1f);
            case 17:
                return new MobSpawnRule(MobType.Shark, 100, 700000, 3, 0.05f);
            case 18:
                return new MobSpawnRule(MobType.Shark, 100, 800000, 3, 0.05f);
            case 19:
                return new MobSpawnRule(MobType.Shark, 100, 900000, 3, 0.05f);
            case 20:
                return new MobSpawnRule(MobType.Shark, 100, 1000000, 3, 0.05f);
            default:
                return new MobSpawnRule(MobType.Shark, 100, 50, 3, 0.1f);
        }
    }
}
