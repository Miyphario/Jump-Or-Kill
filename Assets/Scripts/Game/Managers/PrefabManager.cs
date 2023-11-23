using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    [Header("Creatures")]
    [SerializeField] private GameObject _creatureShadow;
    public GameObject CreatureShadow => _creatureShadow;

    [SerializeField] private GameObject _playerPrefab;
    public GameObject PlayerPrefab => _playerPrefab;

    [Header("Enemies")]
    [SerializeField] private GameObject _enemyMeleeWeapon;
    public GameObject EnemyMeleeWeapon => _enemyMeleeWeapon;
    [SerializeField] private GameObject _enemyRangeWeapon;
    public GameObject EnemyRangeWeapon => _enemyRangeWeapon;

    [Header("Saves")]
    [SerializeField] private GameObject _weaponsListPrefab;
    public GameObject WeaponsListPrefab => _weaponsListPrefab;

    [Header("Pickable")]
    [SerializeField] private List<Sprite> _moneySprites;
    public List<Sprite> MoneySprites => _moneySprites;
    [SerializeField] private Sprite _snowballSprite;
    public Sprite SnowballSprite => _snowballSprite;
    [SerializeField] private Sprite _gemSprite;
    public Sprite GemSprite => _gemSprite;

    [Header("Particles")]
    [SerializeField] private GameObject _hitParticles;
    public GameObject HitParticles => _hitParticles;
    [SerializeField] private GameObject _moneyPickupParticles;
    public GameObject MoneyPickupParticles => _moneyPickupParticles;
    [SerializeField] private GameObject _snowballPickupParticles;
    public GameObject SnowballPickupParticles => _snowballPickupParticles;
    [SerializeField] private GameObject _gemPickupParticles;
    public GameObject GemPickupParticles => _gemPickupParticles;
    [SerializeField] private GameObject _heartParticles;
    public GameObject HeartParticles => _heartParticles;

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
