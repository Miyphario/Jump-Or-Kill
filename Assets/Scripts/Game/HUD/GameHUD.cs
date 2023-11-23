using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }
    [SerializeField] private Text _infoText;

    private int _currentFps;

    [SerializeField] private List<Button> pauseButtonsDisable = new();
    [SerializeField] private GameObject _pauseMenu;

    [Header("Player")]
    [SerializeField] private List<GameObject> _playerHealthLayers = new();

    [Header("HUDs")]
    [SerializeField] private MobileControls _mobileControls;

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

        _mobileControls.Init();

        StartCoroutine(UpdateFps());

        GameController.Instance.OnPlayerCreated += HandlePlayerCreated;
    }

    private void HandlePlayerCreated(ThisIsPlayer player)
    {
        UpdateHearts(false);
    }

    private void Start()
    {
        //UpdateHearts(false);
    }

    private void Update()
    {
        _infoText.text = "fps: " + _currentFps +
            "  Distance: " + Mathf.FloorToInt(GameController.Instance.DistanceTraveled) +
            "  Money: " + GameController.Instance.PlayerData.money +
            "  Snowballs: " + GameController.Instance.PlayerData.snowballs +
            "  Gems: " + GameController.Instance.PlayerData.gems +
            "\nKills: " + GameController.Instance.Player.Kills;
    }

    public IEnumerator UpdateFps()
    {
        while (true)
        {
            _currentFps = (int)(1f / Time.unscaledDeltaTime);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TogglePause()
    {
        GameController.TogglePause();

        _pauseMenu.SetActive(GameController.GameIsPaused);
        
        foreach (Button b in pauseButtonsDisable)
        {
            b.interactable = !GameController.GameIsPaused;
        }
    }

    public void UpdateHearts(bool withParticles)
    {
        // Check how many hearts is enabled
        int maxHearts = 0;
        foreach(GameObject hl in _playerHealthLayers)
        {
            for(int i = 0; i < hl.transform.childCount; i++)
            {
                Image oneHeart = hl.transform.GetChild(i).transform.GetComponent<Image>();
                if (oneHeart.enabled)
                    maxHearts++;
            }
        }
        
        if (maxHearts != GameController.Instance.Player.Health)
        {
            bool plus = true;
            int maxHeartsDiff = Mathf.FloorToInt(GameController.Instance.Player.Health - maxHearts);

            if (maxHeartsDiff > 0)
            {
                plus = false;
            }
            else if (maxHeartsDiff < 0)
            {
                plus = true;
            }

            foreach (GameObject hl in _playerHealthLayers)
            {
                if (maxHeartsDiff != 0)
                {
                    for (int i = 0; i < hl.transform.childCount; i++)
                    {
                        if (maxHeartsDiff != 0)
                        {
                            Image oneHeart = hl.transform.GetChild(i).transform.GetComponent<Image>();

                            // Disable hearts if max hearts are more than player health
                            if (plus)
                            {
                                if (oneHeart.enabled)
                                {
                                    oneHeart.enabled = false;
                                    maxHeartsDiff++;
                                    if (withParticles)
                                    {
                                        Vector3 htPos = oneHeart.rectTransform.position;
                                        CreateParticles(new Vector3(htPos.x, htPos.y - 0.5f, 0f));
                                    }
                                }
                            }

                            // Enable more hearts if max hearts are less than player health
                            else
                            {
                                if (!oneHeart.enabled)
                                {
                                    oneHeart.enabled = true;
                                    maxHeartsDiff--;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
        }
    }

    public void CreateParticles(Vector3 position)
    {
        Vector3 htPos = GameController.Instance.MainCamera.ScreenToWorldPoint(position);
        Instantiate(PrefabManager.Instance.HeartParticles, position, Quaternion.Euler(-90, 0, 0));
    }
}