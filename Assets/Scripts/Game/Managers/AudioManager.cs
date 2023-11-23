using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip[] _footsteps;
    public AudioClip[] Footsteps => _footsteps;
    [SerializeField] private AudioClip[] _coinPickup;
    public AudioClip[] CoinPickup => _coinPickup;
    [SerializeField] private AudioClip[] _snowballPickup;
    public AudioClip[] SnowballPickup => _snowballPickup;
    [SerializeField] private AudioClip _gemPickup;
    public AudioClip GemPickup => _gemPickup;
    [SerializeField] private AudioClip _boostPickup;
    public AudioClip BoostPickup => _boostPickup;
    [SerializeField] private AudioClip _mineExplode;
    public AudioClip MineExplode => _mineExplode;
    [SerializeField] private AudioClip _mineDiffuse;
    public AudioClip MineDiffuse => _mineDiffuse;

    [SerializeField] private AudioClip _changeLine;
    public AudioClip ChangeLine => _changeLine;
    [SerializeField] private AudioClip _creatureOnGrounded;
    public AudioClip CreatureOnGrounded => _creatureOnGrounded;

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
    }
}
