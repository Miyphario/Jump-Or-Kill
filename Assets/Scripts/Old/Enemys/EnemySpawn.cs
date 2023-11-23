using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    /*
    private float subtract_interval = 0.05f;

    private float spawnTime = 4f;
    private int max_time_to_add = 4;
    private int time_to_add = 0;

    private int max_enemy_for_wave = 1;
    private int max_mine_for_wave = 1;

    public GameObject enemy;
    public GameObject mine;
    public GameObject money;
    public GameObject boost;

    private int max_time_to_add_money = 5;
    private int time_to_add_money = 0;
    private float money_spawn_time = 4f;
    private int max_money_spawn = 5;
    private int cur_max_money_spawn = 0;
    private float money_spawn_interval = 0.5f;
    private int current_money_spawned = 0;
    private int current_money_line = 1;
    private int current_money_cost;

    [HideInInspector] public float chance_to_gem = 0.02f;
    [HideInInspector] public float chance_to_other_money = 12f;
    private PriceType current_money_type = PriceType.money;

    [HideInInspector] public float chance_to_faid_kit = -1f; //First aid kit chance | Value is changing bottom
    [HideInInspector] public float chance_to_defuse_kit = -1f; //Defuse kit chance
    [HideInInspector] public float chance_to_cloud = -1f; //Cloud chance
    [HideInInspector] public float chance_to_speed = -1f; //Speed chance

    public GameObject[] meleeWeapons;
    public GameObject[] rangeWeapons;

    private float time_to_spawn_boost = 10f;

    public GameObject boosts_list;

    public float mine_spawn_chance = 20f;
    public GameSounds sound_manager;

    public GameObject player;

    [HideInInspector] public Transform[] road_pos = new Transform[3];

    private void Start()
    {
        // Write roads transform to road_pos array
        road_pos[0] = GameObject.Find("PosRoad1").transform;
        road_pos[1] = GameObject.Find("PosRoad2").transform;
        road_pos[2] = GameObject.Find("PosRoad3").transform;

        // Create boosts list and load buyed boosts
        GameObject bo = Instantiate(boosts_list, new Vector3(0, 0, 0), Quaternion.identity);
        BoostsList bs = bo.transform.GetComponent<BoostsList>();
        bs.LoadBuyedBoosts();

        // Set boosts chances
        if (bs != null && bs.boosts.Count > 0)
        {
            foreach (BoostData s in bs.boosts)
            {
                if (s.buyed)
                {
                    switch (s.boost_type)
                    {
                        case BoostType.speed:
                            chance_to_speed = 26f;
                            break;

                        case BoostType.health:
                            chance_to_faid_kit = 2f;
                            break;

                        case BoostType.defuse_kit:
                            chance_to_defuse_kit = 10f;
                            break;

                        case BoostType.cloud:
                            chance_to_cloud = 15f;
                            break;
                    }
                }
            }
        }

        Destroy(bo);

        // Enemy spawn timer
        StartCoroutine(SpawnEnemy(Random.Range(spawnTime / 1.5f, spawnTime * 1.5f)));

        // Money spawn timer
        StartCoroutine(SpawnMoney(Random.Range(money_spawn_time / 1.5f, money_spawn_time * 1.5f)));

        // Boosts spawn timer
        StartCoroutine(SpawnBoosts(Random.Range(time_to_spawn_boost / 1.5f, time_to_spawn_boost * 1.5f)));

        // 
        for (int i = 0; i < meleeWeapons.Length; i++)
        {
            meleeWeapons[i].transform.GetComponent<WeaponObj>().weaponSlot = i;
        }

        for (int i = 0; i < rangeWeapons.Length; i++)
        {
            rangeWeapons[i].transform.GetComponent<WeaponObj>().weaponSlot = i;
        }
    }

    IEnumerator SpawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        int randRoad;
        int tempRoad;
        Vector3 spawnPos;

        BaseEntity pe = player.transform.GetComponent<BaseEntity>();
        float ds = Random.Range(0f, 100f); //Double spawn
        if (ds <= 8f)
        {
            float ec = Random.Range(0f, 100f); //Chance to spawn double mine and enemy
            int es = ec <= 4 ? max_enemy_for_wave : max_enemy_for_wave / 4;

            for (int i = 0;i < es;i++)
            {
                randRoad = Random.Range(1, 4);
                tempRoad = randRoad - 1;
                spawnPos = new Vector3(player.transform.position.x + 19.5f + i, road_pos[tempRoad].position.y, 0f);

                InitMine(mine, spawnPos, Quaternion.Euler(0, 0, 0), null, randRoad);

                randRoad = Random.Range(1, 4);
                tempRoad = randRoad - 1;
                spawnPos = new Vector3(player.transform.position.x + 19.5f + i, road_pos[tempRoad].position.y, 0f);

                InitEnemy(enemy, spawnPos, Quaternion.Euler(0, 180, 0), null, randRoad);
            }
        }
        else
        {
            float c = Random.Range(0f, 100f); //Chance to spawn mine
            if (c <= mine_spawn_chance)
            {
                for (int i = 0;i < Random.Range(Mathf.Clamp(max_mine_for_wave / 4, 1, max_mine_for_wave), max_mine_for_wave + 1);i++)
                {
                    randRoad = Random.Range(1, 4);
                    tempRoad = randRoad - 1;
                    spawnPos = new Vector3(player.transform.position.x + 19.5f + i, road_pos[tempRoad].position.y, 0f);

                    InitMine(mine, spawnPos, Quaternion.Euler(0, 0, 0), null, randRoad);
                }
            }
            else
            {
                for (int i = 0; i < Random.Range(Mathf.Clamp(max_enemy_for_wave / 4, 1, max_enemy_for_wave), max_enemy_for_wave + 1); i++)
                {
                    randRoad = Random.Range(1, 4);
                    tempRoad = randRoad - 1;
                    spawnPos = new Vector3(player.transform.position.x + 19.5f + i, road_pos[tempRoad].position.y, 0f);

                    InitEnemy(enemy, spawnPos, Quaternion.Euler(0, 180, 0), null, randRoad);
                }
            }
        }

        StartCoroutine(SpawnEnemy(Random.Range(spawnTime / 1.5f, spawnTime * 1.5f)));
    }

    public void AddToInterval()
    {
        spawnTime = Mathf.Clamp(spawnTime - subtract_interval, 0.5f, 100f);
        time_to_spawn_boost = Mathf.Clamp(time_to_spawn_boost - 0.01f, 1f, 100f);

        time_to_add++;
        time_to_add_money++;

        if (time_to_add > max_time_to_add)
        {
            max_enemy_for_wave++;
            max_mine_for_wave++;
            time_to_add = 0;
        }

        if (time_to_add_money > max_time_to_add_money)
        {
            max_money_spawn++;
            time_to_add_money = 0;
        }
    }

    IEnumerator SpawnMoney(float time)
    {
        yield return new WaitForSeconds(time);
        
        if (cur_max_money_spawn <= 0)
        {
            cur_max_money_spawn = Random.Range(Mathf.Clamp(max_money_spawn / 5, 1, max_money_spawn), max_money_spawn + 1);
            current_money_line = Random.Range(1, 4);

            bool other_money = false;

            if (ManagerScript.AccessEvent(EventType.christmass))
            {
                float l = Random.Range(0f, 100f);
                if (l <= chance_to_other_money)
                {
                    current_money_type = PriceType.snowballs;
                    other_money = true;
                }
            }

            if (!other_money)
            {
                current_money_type = PriceType.money;

                float r = Random.Range(0f, 100f);
                if (r <= 2f)
                {
                    current_money_cost = 10;
                }
                else if (r <= 8f)
                {
                    current_money_cost = 5;
                }
                else
                {
                    current_money_cost = 1;
                }
            }
        }

        if (current_money_spawned < cur_max_money_spawn)
        {
            Vector3 spawnPos = new Vector3(player.transform.position.x + 19.5f, road_pos[current_money_line - 1].position.y, 0f);

            GameObject m = Instantiate(money, spawnPos, Quaternion.identity);
            Money sm = m.transform.GetComponent<Money>();
            sm.line = current_money_line;
            sm.player = player;

            float ch = Random.Range(0f, 100f);
            if (ch <= chance_to_gem)
            {
                sm.money = 1;
                sm.type = PriceType.gems;
                sm.UpdateMoneySprite();
            }
            else
            {
                sm.type = current_money_type;

                switch (current_money_type)
                {
                    case PriceType.money:
                        sm.money = current_money_cost;
                        sm.UpdateMoneySprite();
                        break;

                    case PriceType.snowballs:
                        sm.money = 1;
                        sm.UpdateMoneySprite();
                        break;
                }
            }

            current_money_spawned++;
            StartCoroutine(SpawnMoney(money_spawn_interval));
        }
        else
        {
            current_money_spawned = 0;
            cur_max_money_spawn = 0;
            StartCoroutine(SpawnMoney(Random.Range(money_spawn_time / 1.5f, money_spawn_time * 1.5f)));
        }
    }

    IEnumerator SpawnBoosts(float time)
    {
        yield return new WaitForSeconds(time);

        int l = Random.Range(1, 4);
        Vector3 spawnPos = new Vector3(player.transform.position.x + 19.5f, road_pos[l - 1].position.y, 0f);
        GameObject b = Instantiate(boost, spawnPos, Quaternion.identity);
        Boost bs = b.transform.GetComponent<Boost>();
        bs.line = l;
        bs.player = player;

        float ch = Random.Range(0f, 100f);
        // First aid kit
        if (ch <= chance_to_faid_kit)
        {
            float m = Random.Range(0f, 100f);
            bs.my_data = new BoostData(BoostType.health, 0f, m <= 5f ? 2f : 1f, false);

            bs.UpdateSprite();
        }
        // Defuse kit
        else if (ch <= chance_to_defuse_kit)
        {
            float m = Random.Range(0f, 100f);
            bs.my_data = new BoostData(BoostType.defuse_kit, 0f, m <= 10f ? 2f : 1f, true);

            bs.UpdateSprite();
        }
        // Cloud
        else if (ch <= chance_to_cloud)
        {
            bs.my_data = new BoostData(BoostType.cloud, Random.Range(4f, 8f), Random.Range(0.3f, 0.6f), false);

            bs.UpdateSprite();
        }
        // Energy drink
        else if (ch <= chance_to_speed)
        {
            bs.my_data = new BoostData(BoostType.speed, Random.Range(4f, 8f), Random.Range(1.2f, 2f), false);

            bs.UpdateSprite();
        }
        else
        {
            Destroy(b);
        }

        StartCoroutine(SpawnBoosts(Random.Range(time_to_spawn_boost / 1.5f, time_to_spawn_boost * 1.5f)));
    }

    public void InitEnemy(GameObject obj, Vector3 position, Quaternion quaternion, Transform parent, int line)
    {
        GameObject h = Instantiate(obj, position, quaternion, parent);

        BaseEntity he = h.transform.GetComponent<BaseEntity>();
        he.line = line;
        he.lineToMove = line;
        he.enemy_spawn = transform.GetComponent<EnemySpawn>();

        // Can enemy change line or no
        float ch = Random.Range(0f, 100f);
        he.canLineChange = ch <= 28f ? true : false;

        he.player = player;
        he.sound_manager = sound_manager;

        // Chance to spawn range enemy
        float wch = Random.Range(0f, 100f);
        if (wch <= 99f)
        {
            if (rangeWeapons.Length > 0)
            {
                int w = Random.Range(0, rangeWeapons.Length);
                he.my_weapon = rangeWeapons[w].transform.GetComponent<WeaponObj>();
                he.reloadSpeed = 2f;
                he.CreateWeapon();
            }
        }
        else
        {
            if (meleeWeapons.Length > 0)
            {
                int w = Random.Range(0, meleeWeapons.Length);
                he.my_weapon = meleeWeapons[w].transform.GetComponent<WeaponObj>();
                he.CreateWeapon();
            }
        }
    }

    public void InitMine(GameObject obj, Vector3 position, Quaternion quaternion, Transform parent, int line)
    {
        GameObject h = Instantiate(obj, position, quaternion, parent);

        Destructible hd = h.transform.GetComponent<Destructible>();
        hd.line = line;
        hd.victim_tag = "Player";
        hd.player = player;

        BaseEntity pe = player.transform.GetComponent<BaseEntity>();
        if (pe.defuse)
        {
            hd.Action("defuse");
            Effect eff = pe.my_effects.GetEffectByEnum(BoostType.defuse_kit);
            eff.effect.modify--;
            eff.UpdateEffect();
        }
    }
    */
}
