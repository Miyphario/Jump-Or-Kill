using UnityEngine;
using UnityEngine.UI;

public class MobileControls : MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _attackButton;

    public void Init()
    {
        GameController.Instance.OnPlayerCreated += HandlePlayerCreated;
    }

    private void HandlePlayerCreated(ThisIsPlayer player)
    {
        _upButton.onClick.AddListener(() => player.ChangeLine(-1));
        _downButton.onClick.AddListener(() => player.ChangeLine(1));
        _jumpButton.onClick.AddListener(() => player.Jump());
        _attackButton.onClick.AddListener(() => player.StartAttack());
    }
}
