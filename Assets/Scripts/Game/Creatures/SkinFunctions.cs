using System.Collections.Generic;
using UnityEngine;

public class SkinFunctions : MonoBehaviour
{
    private List<SpriteRenderer> _spritesToChange = new();
    private List<int> _defaultSpritesOrder = new();
    private int _ZOrder = -1;

    private bool _isPlayer;
    private Creature _myCreature;

    [SerializeField] private Transform _leftArm;
    public Transform LeftArm => _leftArm;
    [SerializeField] private Transform _rightArm;
    public Transform RightArm => _rightArm;
    [SerializeField] private Transform _attackPoint;
    public Transform AttackPoint => _attackPoint;

    private AudioSource _audioSource;

    public void Init(Creature creature)
    {
        _myCreature = creature;
        if (creature is ThisIsPlayer) _isPlayer = true;

        foreach (var spriteRend in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            _spritesToChange.Add(spriteRend);
        }

        _audioSource = GetComponent<AudioSource>();

        UpdateDefaultValues();
    }

    public void UpdateDefaultValues()
    {
        foreach (SpriteRenderer sp in _spritesToChange)
        {
            _defaultSpritesOrder.Add(sp.sortingOrder);
        }
    }

    private void Start()
    {
        if (_ZOrder > -1)
        {
            ChangeZOrder(_ZOrder);
        }
    }

    public void ChangeSortingLayer(string layer)
    {
        foreach(SpriteRenderer sr in _spritesToChange)
        {
            sr.sortingLayerName = layer;
        }
    }

    public void ChangeZOrder(int value)
    {
        foreach (SpriteRenderer sr in _spritesToChange)
        {
            sr.sortingOrder += value;
        }
    }

    public void UpdateZOrder()
    {
        if (_ZOrder > -1)
        {
            ChangeZOrder(_ZOrder);
        }
    }

    public void SetDefaultZOrder()
    {
        for (int i = 0; i < _spritesToChange.Count; i++)
        {
            _spritesToChange[i].sortingOrder = _defaultSpritesOrder[i];
        }
    }

    public void ChangeMaskInteraction(SpriteMaskInteraction spriteMask)
    {
        foreach (SpriteRenderer sr in _spritesToChange)
        {
            sr.maskInteraction = spriteMask;
        }
    }

    public void AddSpriteToList(SpriteRenderer spriteRenderer)
    {
        _spritesToChange.Add(spriteRenderer);
        _defaultSpritesOrder.Add(spriteRenderer.sortingOrder);
    }

    public void RemoveSpriteFromList(int index)
    {
        _spritesToChange.RemoveAt(index);
        _defaultSpritesOrder.RemoveAt(index);
    }

    public void RemoveLastSpriteFromList()
    {
        RemoveSpriteFromList(_spritesToChange.Count - 1);
    }

    public void Attack()
    {
        _myCreature.Attack();
    }

    public void AudioFootstep()
    {
        if (_myCreature.InAir) return;

        AudioClip clip = AudioManager.Instance.Footsteps[Random.Range(0, AudioManager.Instance.Footsteps.Length)];

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.volume = _isPlayer ? 0.8f : 0.5f;
        _audioSource.PlayOneShot(clip);
    }

    public void AudioPlay(AudioClip clip, float pitch, float volume)
    {
        _audioSource.pitch = pitch;
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(clip);
    }
}
