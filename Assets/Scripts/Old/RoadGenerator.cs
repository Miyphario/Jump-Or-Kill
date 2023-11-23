using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class RoadGenerator : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;

    public GameObject create_pos;
    public GameObject env_pos;

    [HideInInspector] public bool canCreate = true;
    [HideInInspector] public GameController gameController;

    public GameObject env_parent;

    public List<GameObject> env_winter = new List<GameObject>();
    public List<GameObject> env_summer = new List<GameObject>();

    public List<SpriteRenderer> my_bg_sprites = new List<SpriteRenderer>();

    public Sprite bg_summer, bg_winter;

    private int env = 0;
    private int max_env = 0;
    private GameObject prev_tree;

    public List<GameObject> environments = new List<GameObject>();

    private void Start()
    {
        player = GameObject.Find("Player");
        env = 0;
        max_env = Random.Range(4, 8 + 1);

        if (env_parent.transform.childCount > 0)
        {
            for (int i = 0; i < env_parent.transform.childCount; i++)
            {
                GameObject o = env_parent.transform.GetChild(i).gameObject;
                if (o != null)
                    Destroy(o);
            }
        }

        EventType et = ManagerScript.GetCurrentSeason();

        switch (et)
        {
            case EventType.winter:
                for (int i = 0; i < env_winter.Count; i++)
                {
                    environments.Add(env_winter[i]);
                }

                foreach (SpriteRenderer s in my_bg_sprites)
                {
                    s.sprite = bg_winter;
                }
                break;

            case EventType.summer:
                for (int i = 0; i < env_summer.Count; i++)
                {
                    environments.Add(env_summer[i]);
                }

                foreach (SpriteRenderer s in my_bg_sprites)
                {
                    s.sprite = bg_summer;
                }
                break;
        }

        env_winter.Clear();
        env_summer.Clear();

        StartCoroutine(CreateTree());
    }

    public void GenerateRoad()
    {
        if (canCreate)
        {
            GameObject pf = gameController.road_prefab;
            GameObject g = Instantiate(pf, create_pos.transform.position, Quaternion.identity, transform.parent);
            RoadGenerator rg = g.transform.GetComponent<RoadGenerator>();
            rg.gameController = gameController;
            canCreate = false;
        }
    }

    IEnumerator CreateTree()
    {
        while (env < max_env)
        {
            float posY = Random.Range(env_pos.transform.position.y - 0.25f, env_pos.transform.position.y + 0.25f);
            GameObject t = Instantiate(environments[Random.Range(0, environments.Count)], new Vector3(env_pos.transform.position.x + (env * Random.Range(3f, 4f)) + 1f, posY, 0), Quaternion.identity, env_parent.transform);
            SpriteRenderer tsp = t.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();

            if (env > 0)
            {
                int zo;
                if (posY > prev_tree.transform.position.y)
                {
                    zo = prev_tree.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sortingOrder;
                    tsp.sortingOrder = zo - 1;
                }
            }

            prev_tree = t;
            env++;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
*/