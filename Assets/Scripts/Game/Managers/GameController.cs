using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; }
    public static GameController Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private float _playerSpeed = 3f;
    public float PlayerSpeed => _playerSpeed;
    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData => playerData;
    private ThisIsPlayer _player;
    public ThisIsPlayer Player => _player;
    private float _distanceTraveled;
    public float DistanceTraveled => _distanceTraveled;
    public Camera MainCamera { get; private set; }

    [SerializeField] private EnvironmentScroller road;

    [Header("Queues")]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _moneyQueueObject;
    public GameObject MoneyQueueObject => _moneyQueueObject;
    [SerializeField] private GameObject _enemiesQueueObject;
    public GameObject EnemiesQueueObject => _enemiesQueueObject;
    [SerializeField] private GameObject _destructibleQueueObject;
    public GameObject DestructibleQueueObject => _destructibleQueueObject;

    [SerializeField] private float _enemySpeedOffset = 0.8f;
    public float EnemySpeedOffset => _enemySpeedOffset;

    private bool refreshSpeed = false;

    [SerializeField] private static EventType _currentEvent;
    public static EventType CurrentEvent => _currentEvent;

    [Space]
    [SerializeField] private float[] _roadYs;
    public float[] RoadYs => _roadYs;

    private float timeToUpdateSpeed = 2f;

    public event Action<ThisIsPlayer> OnPlayerCreated;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        _currentEvent = ManagerScript.GetCurrentEvent();
        MainCamera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(AddToPlayerSpeed());
        _player = Instantiate(PrefabManager.Instance.PlayerPrefab, new Vector3(-5f, 0.35f, 0f), Quaternion.identity).GetComponent<ThisIsPlayer>();
        OnPlayerCreated?.Invoke(_player);
    }

    public GameObject LoadCurrentWeapon()
    {
        GameObject wlo = Instantiate(PrefabManager.Instance.WeaponsListPrefab, transform);
        GameObject wp = wlo.GetComponent<ItemsList>().currentItem;
        Destroy(wlo);
        return wp;
    }

    public void GlobalUpdateSpeed(float value)
    {
        _playerSpeed += value;

        road.UpdateSpeed();

        _spawner.UpdateSpeed();

        for (int i = 0; i < _moneyQueueObject.transform.childCount; i++)
        {
            Pickable pck = _moneyQueueObject.transform.GetChild(i).transform.GetComponent<Pickable>();
            pck.UpdateSpeed();
        }

        for (int i = 0; i < _enemiesQueueObject.transform.childCount; i++)
        {
            ThisIsEnemy en = _enemiesQueueObject.transform.GetChild(i).transform.GetComponent<ThisIsEnemy>();
            en.UpdateSpeed();
        }
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
#endif

        if (!GameIsPaused)
        {
            if (refreshSpeed)
            {
                GlobalUpdateSpeed(0.02f);

                refreshSpeed = false;
            }

            _distanceTraveled += _playerSpeed / 2f * Time.deltaTime;
        }
    }

    public IEnumerator AddToPlayerSpeed()
    {
        while (_playerSpeed < 18)
        {
            if (!GameIsPaused)
            {
                yield return new WaitForSeconds(timeToUpdateSpeed);
                GlobalUpdateSpeed(0.02f);
                timeToUpdateSpeed = Mathf.Clamp(timeToUpdateSpeed + 0.005f, 2f, 5f);
            }
        }
    }

    public static void TogglePause()
    {
        GameIsPaused = !GameIsPaused;
        switch(GameIsPaused)
        {
            case false:
                Time.timeScale = 1f;
                break;

            case true:
                Time.timeScale = 0f;
                break;
        }
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void TakeScreenshot()
    {
        string path = Application.persistentDataPath + "/screenshots";
        int files_count;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string[] f = Directory.GetFiles(path + "/");
        files_count = f.Length;

        ScreenCapture.CaptureScreenshot(path + "/jumporkill_" + files_count + ".png");
    }

    //public void SaveScore()
    //{
    //    Creature ep = _player.GetComponent<Creature>();
    //    SaveSystem.SaveScore(ep);

    //    GameData data = SaveSystem.LoadData();
    //    if (data == null)
    //    {
    //        data = new GameData(ep);
    //    }
    //    else
    //    {
    //        data.money += ep.money;
    //        data.gems += ep.gems;
    //        data.snowballs += ep.snowballs;
    //    }

    //    SaveSystem.SaveData(data);
    //}
}

    /*
    private void Start()
    {
        if (score_prefab != null)
        {
            List<Scores> t = SaveSystem.LoadScores();

            if (t != null)
            {
                foreach (Scores s in t)
                {
                    Instantiate(score_prefab, new Vector3(s.last_position[0], s.last_position[1], 0), Quaternion.identity);
                }
            }
        }
        /*
        for (int i = 0; i < 2; i++)
        {
            GameObject road = Instantiate(road_prefab, new Vector3(-6 + (i * 25.58f), 0, 0), Quaternion.identity, road_parent.transform);
            RoadGenerator rg = road.transform.GetComponent<RoadGenerator>();
            rg.gameController = transform.GetComponent<GameController>();
        }
        SkinsList list = GetSkins();
        list.LoadBuyedSkins();

        if (list.skins != null)
        {
            string sn = "";
            for (int i = 0; i < list.skins.Count; i++)
            {
                if (list.skins[i].selected)
                {
                    sn = list.skins[i].skin_name;
                    break;
                }
            }

            GameObject ls = list.LoadSkinByName(sn);
            if (ls != null)
            {
                if (player.GetComponent<BaseEntity>().my_skin != ls)
                {
                    player.GetComponent<BaseEntity>().ChangeSkin(ls);
                    DestroySkinsList();
                }
            }
        }
        GameObject wp = Instantiate(weapons_list, new Vector3(0, 0, 0), Quaternion.identity);
        WeaponsList wList = wp.GetComponent<WeaponsList>();
        wList.LoadBuyedWeapons();

        if (wList != null)
        {
            string wn = "";
            string wArm = "";
            for (int i = 0;i < wList.weapons.Count;i++)
            {
                if (wList.weapons[i].selected)
                {
                    wn = wList.weapons[i].weapon_name;
                    wArm = wList.weapons[i].arm;
                    break;
                }
            }

            GameObject lw = wList.LoadWeaponByName(wn);
            if (lw != null)
            {
                BaseEntity pl = player.GetComponent<BaseEntity>();
                if (pl.my_weapon != lw)
                {
                    pl.my_weapon = lw.transform.GetComponent<WeaponObj>();
                    pl.CreateWeapon();
                    Destroy(wp);
                }
            }
        }

        Destroy(wp);
    }

    public static bool IsMobile()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public SkinsList GetSkins()
    {
        if (skins_list_to_destroy != null)
        {
            SkinsList sl = skins_list_to_destroy.transform.GetComponent<SkinsList>();
            sl.LoadBuyedSkins();

            return sl;
        }
        else
        {
            skins_list_to_destroy = Instantiate(skins_list, new Vector3(0, 0, 0), Quaternion.identity);
            SkinsList sl = skins_list_to_destroy.transform.GetComponent<SkinsList>();
            sl.LoadBuyedSkins();

            return sl;
        }
    }

    public void DestroySkinsList()
    {
        if (skins_list_to_destroy != null)
            Destroy(skins_list_to_destroy);
    }
        */