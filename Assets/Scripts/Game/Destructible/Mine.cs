using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Destructible
{
    public Sprite sprite_base, sprite_to_diffuse, sprite_to_boom;
    public AudioClip explode_sound, defuse_sound;

    public override void Action(string action, Creature creature)
    {
        if (_mySprite != null)
        {
            _audioSource.pitch = Random.Range(0.85f, 1.15f);
            switch (action)
            {
                case "boom":
                    _mySprite.sprite = sprite_to_boom;
                    _activated = false;
                    _audioSource.clip = explode_sound;

                    // Shake player camera
                    if (_myEnemy == GameController.Instance.Player)
                    {
                        StartCoroutine(GameController.Instance.MainCamera.transform.GetComponent<CamController>().Shake(0.15f, 0.25f));
                    }
                    break;

                case "defuse":
                    _mySprite.sprite = sprite_to_diffuse;
                    _activated = false;
                    _audioSource.clip = defuse_sound;
                    break;
            }
            _audioSource.Play();
        }
    }

    public override void SetFromQueue(Vector3 position, int line)
    {
        _mySprite.sprite = sprite_base;

        base.SetFromQueue(position, line);
    }
}
