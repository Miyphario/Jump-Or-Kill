using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropType
{
    ninja
}

public class PickableDrop : MonoBehaviour
{
    /*
    [HideInInspector] public BaseEntity my_parent;

    private void Awake()
    {
        if (transform.parent != null)
        {
            my_parent = transform.parent.transform.GetComponent<BaseEntity>();
            my_parent.my_drop = transform.GetComponent<PickableDrop>();
        }
    }

    public void Drop(DropType droptype)
    {
        

        switch (droptype)
        {
            case DropType.ninja:
                if (my_parent.enemy_spawn != null)
                {
                    SpawnBoostOrMoney(4f, 16f);
                }
                break;
        }
    }

    private void SpawnBoost()
    {
        Vector3 pp = transform.parent.transform.position; //Parent position
        EnemySpawn enSp = my_parent.enemy_spawn;

        int l = my_parent.line;

        Vector3 spawnPos = new Vector3(pp.x, my_parent.road_pos[my_parent.line - 1].position.y, 0f);
        GameObject b = Instantiate(enSp.boost, spawnPos, Quaternion.identity);

        Boost bs = b.GetComponent<Boost>();
        MeleeEnemy me = transform.parent.transform.GetComponent<MeleeEnemy>();
        bs.line = l;
        bs.player = me.player;

        float ch = Random.Range(0f, 100f);
        if (ch <= my_parent.enemy_spawn.chance_to_faid_kit) //First aid kit
        {
            float m = Random.Range(0f, 100f);
            bs.my_data = new BoostData(BoostType.health, 0f, m <= 5f ? 2f : 1f, false);

            bs.UpdateSprite();
        }
        else if (ch <= my_parent.enemy_spawn.chance_to_defuse_kit) //Defuse kit
        {
            float m = Random.Range(0f, 100f);
            bs.my_data = new BoostData(BoostType.defuse_kit, 0f, m <= 10f ? 2f : 1f, true);

            bs.UpdateSprite();
        }
        else if (ch <= my_parent.enemy_spawn.chance_to_cloud) //Cloud
        {
            bs.my_data = new BoostData(BoostType.cloud, Random.Range(4f, 8f), Random.Range(0.3f, 0.6f), false);

            bs.UpdateSprite();
        }
        else if (ch <= my_parent.enemy_spawn.chance_to_speed) //Speed
        {
            bs.my_data = new BoostData(BoostType.speed, Random.Range(4f, 8f), Random.Range(1.2f, 2f), false);

            bs.UpdateSprite();
        }
        else
        {
            Destroy(b);
        }
    }

    private void SpawnMoney()
    {

    }

    private void SpawnBoostOrMoney(float ch10, float ch5)
    {
        Vector3 pp = transform.parent.transform.position; //Parent position
        EnemySpawn enSp = my_parent.enemy_spawn;
        MeleeEnemy me = transform.parent.transform.GetComponent<MeleeEnemy>();

        Vector3 spawnPos = new Vector3(pp.x, my_parent.road_pos[my_parent.line - 1].position.y, 0f);

        int l = my_parent.line;

        float ch = Random.Range(0f, 100f);
        if (ch <= my_parent.enemy_spawn.chance_to_speed)
        {
            GameObject b = Instantiate(enSp.boost, spawnPos, Quaternion.identity);

            Boost bs = b.GetComponent<Boost>();
            bs.line = l;
            bs.player = me.player;

            if (ch <= my_parent.enemy_spawn.chance_to_faid_kit) //First aid kit
            {
                float m = Random.Range(0f, 100f);
                bs.my_data = new BoostData(BoostType.health, 0f, m <= 5f ? 2f : 1f, false);

                bs.UpdateSprite();
            }
            else if (ch <= my_parent.enemy_spawn.chance_to_defuse_kit) //Defuse kit
            {
                float m = Random.Range(0f, 100f);
                bs.my_data = new BoostData(BoostType.defuse_kit, 0f, m <= 10f ? 2f : 1f, true);

                bs.UpdateSprite();
            }
            else if (ch <= my_parent.enemy_spawn.chance_to_cloud) //Cloud
            {
                bs.my_data = new BoostData(BoostType.cloud, Random.Range(4f, 8f), Random.Range(0.3f, 0.6f), false);

                bs.UpdateSprite();
            }
            else if (ch <= my_parent.enemy_spawn.chance_to_speed) //Speed
            {
                bs.my_data = new BoostData(BoostType.speed, Random.Range(4f, 8f), Random.Range(1.2f, 2f), false);

                bs.UpdateSprite();
            }
        }
        else
        {
            PriceType cur_money = PriceType.money;
            int cur_money_cost = 1;
            bool other_money = false;

            if (ManagerScript.AccessEvent(EventType.christmass))
            {
                float otCh = Random.Range(0f, 100f);
                if (otCh <= enSp.chance_to_other_money)
                {
                    cur_money = PriceType.snowballs;
                    other_money = true;
                }
            }

            if (!other_money)
            {
                cur_money = PriceType.money;

                float cmCh = Random.Range(0f, 100f);
                if (cmCh <= ch10)
                {
                    cur_money_cost = 10;
                }
                else if (cmCh <= ch5)
                {
                    cur_money_cost = 5;
                }
                else
                {
                    cur_money_cost = 1;
                }
            }

            int money_count = Random.Range(1, 3);
            for (int i = 0; i <= money_count; i++)
            {
                GameObject m = Instantiate(enSp.money, new Vector3(spawnPos.x + i * 1.2f, spawnPos.y,spawnPos.z), Quaternion.identity);
                Money sm = m.transform.GetComponent<Money>();
                sm.line = my_parent.line;
                sm.player = me.player;

                float gemCh = Random.Range(0f, 100f);
                if (gemCh <= enSp.chance_to_gem)
                {
                    sm.money = 1;
                    sm.type = PriceType.gems;
                    sm.UpdateMoneySprite();
                }
                else
                {
                    switch (cur_money)
                    {
                        case PriceType.money:
                            sm.money = cur_money_cost;
                            sm.UpdateMoneySprite();
                            break;

                        case PriceType.snowballs:
                            sm.money = 1;
                            sm.UpdateMoneySprite();
                            break;
                    }
                }
            }
        }
    }
    */
}
