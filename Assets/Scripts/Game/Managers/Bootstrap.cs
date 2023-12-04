using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PrefabManager _prefabManager;
    [SerializeField] private GameHUD _gameHud;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnvironmentScroller _scroller;

    private void Awake()
    {
        _prefabManager.Init();
        _gameController.Init();
        _audioManager.Init();
        _gameHud.Init();
        _scroller.Init();
        _spawner.Init();
    }
}
