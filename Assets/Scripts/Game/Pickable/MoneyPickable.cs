using UnityEngine;

public enum MoneyType
{
    coin,
    snowball,
    gem
}

public class MoneyPickable : Pickable
{
    [SerializeField] private int _cost = 1;
    [SerializeField] private MoneyType _moneyType = MoneyType.coin;

    public void Init(Vector2 position, int line, int cost, MoneyType moneyType)
    {
        transform.position = position;
        _cost = cost;
        _moneyType = moneyType;
        Init(line);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Creature pl = collision.transform.GetComponent<Creature>();
            if (pl.CurrentLine == _currentLine || pl.LineToMove == _currentLine)
            {
                AudioClip aCl = AudioManager.Instance.CoinPickup[Random.Range(0, AudioManager.Instance.CoinPickup.Length)];
                float aPitch = Random.Range(0.8f, 1.2f);
                float aVol = 0.8f;

                switch (_moneyType)
                {
                    case MoneyType.coin:
                        GameController.Instance.PlayerData.money += _cost;
                        Instantiate(PrefabManager.Instance.MoneyPickupParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                        break;

                    case MoneyType.snowball:
                        GameController.Instance.PlayerData.snowballs += _cost;
                        Instantiate(PrefabManager.Instance.SnowballPickupParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                        aVol = 0.7f;
                        aCl = AudioManager.Instance.SnowballPickup[Random.Range(0, AudioManager.Instance.SnowballPickup.Length)];
                        break;

                    case MoneyType.gem:
                        GameController.Instance.PlayerData.gems += _cost;
                        Instantiate(PrefabManager.Instance.GemPickupParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                        aCl = AudioManager.Instance.GemPickup;
                        break;
                }

                AudioPlay(aCl, aPitch, aVol);
                AddToQueue();
            }
        }
    }

    protected override void SetSprite()
    {
        switch (_moneyType)
        {
            case MoneyType.coin:
                foreach (Sprite s in PrefabManager.Instance.MoneySprites)
                {
                    if (s == null) continue;
                    string spName = "coin_" + _cost;
                    if (s.name == spName)
                    {
                        SetSprite(s);
                        break;
                    }
                }
                break;

            case MoneyType.snowball:
                SetSprite(PrefabManager.Instance.SnowballSprite);
                break;

            case MoneyType.gem:
                SetSprite(PrefabManager.Instance.GemSprite);
                break;
        }
    }
}
