using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Money
    [Header("Money")]
    [SerializeField] private GameObject _moneyObject;

    private int _minCoinsInLine = 3;
    private int _maxCoinsInLine = 11;
    private int CoinsInLine =>
        Mathf.FloorToInt((_maxCoinsInLine - _minCoinsInLine) * GameController.Instance.PlayerSpeedPercent / 100f + _minCoinsInLine);

    // Time to spawn next line of money
    private float _minTimeToMoneyLine = 3f;
    private float _maxTimeToMoneyLine = 7f;
    private float TimeToMoneyLine => 
        _maxTimeToMoneyLine - _minTimeToMoneyLine - ((_maxTimeToMoneyLine - _minTimeToMoneyLine) * GameController.Instance.PlayerSpeedPercent / 100f) + _minTimeToMoneyLine;

    [SerializeField] private float _snowballChance = 35f;
    [SerializeField] private float _gemChance = 0.02f;
    #endregion /Money

    #region Enemies
    [Header("Enemies")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int maxEnemiesInLine = 2;
    private int curEnemyInLine = 0;
    [SerializeField] private int increaseEnemiesInLine = 0;
    [SerializeField] private int increaseEnemiesInLineMax = 20;

    // Enemies chances
    [SerializeField] private float rangeEnemyChance = 35f; // Chance to spawn range weapon enemy
    [SerializeField] private float lineChangingEnemyChance = 35f; // Chance to spawn line change enemy

    [SerializeField] private int maxQueueEnemies = 50;

    private float _minTimeToEnemyLine = 3f;
    private float _maxTimeToEnemyLine = 7f;
    private float TimeToEnemyLine =>
        _maxTimeToEnemyLine - _minTimeToEnemyLine - ((_maxTimeToEnemyLine - _minTimeToEnemyLine) * GameController.Instance.PlayerSpeedPercent / 100f) + _minTimeToEnemyLine;
    #endregion /Enemies

    #region Destructible
    [Header("Destructible")]
    // Destructible use enemy coroutine and timers
    [SerializeField] private GameObject destructiblePrefab;
    [SerializeField] private int maxDestructibleInLine = 2;
    private int curDestructibleInLine = 0;
    [SerializeField] private int increaseDestructibleInLine = 0;
    [SerializeField] private int increaseDestructibleInLineMax = 10;

    [SerializeField] private int maxQueueDestructible = 50;
    #endregion /Destructible

    public void Init()
    {
        GameController.Instance.OnSpeedUpdated += HandleUpdateSpeed;

        StartCoroutine(SpawnMoneyIE());
        StartCoroutine(SpawnEnemyIE());
    }

    public void HandleUpdateSpeed(float speed)
    {
        increaseEnemiesInLine++;
        if (increaseEnemiesInLine > increaseEnemiesInLineMax)
        {
            maxEnemiesInLine++;
            increaseEnemiesInLine = 0;
            increaseEnemiesInLineMax += 5;
        }

        increaseDestructibleInLine++;
        if (increaseDestructibleInLine > increaseDestructibleInLineMax)
        {
            maxDestructibleInLine++;
            increaseDestructibleInLine = 0;
            increaseDestructibleInLineMax += 10;
        }
    }

    public IEnumerator SpawnMoneyIE()
    {
        while (true)
        {
            float timeToMoney = TimeToMoneyLine;
            yield return new WaitForSeconds(Random.Range(timeToMoney / 1.3f, timeToMoney * 1.5f));

            Vector2 spawnPos = new(15f, 0f);
            int curLine = Random.Range(1, 4);
            switch (curLine)
            {
                case 1:
                    spawnPos.y = GameController.Instance.RoadYs[0];
                    break;

                case 2:
                    spawnPos.y = GameController.Instance.RoadYs[1];
                    break;

                case 3:
                    spawnPos.y = GameController.Instance.RoadYs[2];
                    break;
            }

            int curCoinCost = 1;
            MoneyType moneyType = MoneyType.coin;

            if (GameController.CurrentEvent == EventType.christmass)
            {
                float chSnow = Random.Range(0f, 100f);
                if (chSnow <= _snowballChance)
                {
                    moneyType = MoneyType.snowball;
                }
            }
            else
            {
                float chCost = Random.Range(0f, 100f);
                if (chCost <= 8f)
                {
                    curCoinCost = 10;
                }
                else if (chCost <= 16f)
                {
                    curCoinCost = 5;
                }
            }

            int spawnedCoins = 0;
            int maxCoins = CoinsInLine;
            int curMaxCoins = Random.Range(Mathf.FloorToInt(maxCoins / 1.5f), maxCoins + 1);
            for (int i = 0; i < curMaxCoins; i++)
            {
                int queue = GameController.Instance.MoneyQueueObject.transform.childCount;

                // Temp variables if spawned gem, these vars set to default
                MoneyType tempType = moneyType;
                int tempCost = curCoinCost;

                float chGem = Random.Range(0f, 100f);
                if (chGem <= _gemChance)
                {
                    moneyType = MoneyType.gem;
                    curCoinCost = 1;
                }

                bool spawnNewCoin = true;
                MoneyPickable money = null;

                if (queue > 0)
                {
                    for (int j = 0; j < queue; j++)
                    {
                        MoneyPickable mon = GameController.Instance.MoneyQueueObject.transform.GetChild(j).GetComponent<MoneyPickable>();
                        if (mon.InQueue)
                        {
                            money = mon;
                            spawnNewCoin = false;
                            break;
                        }
                    }
                }

                if (spawnNewCoin)
                {
                    money = Instantiate(_moneyObject, GameController.Instance.MoneyQueueObject.transform).GetComponent<MoneyPickable>();
                }

                if (money != null)
                {
                    money.Init(spawnPos, curLine, curCoinCost, moneyType);
                }

                spawnedCoins++;
                spawnPos.x += 1.5f;
                moneyType = tempType;
                curCoinCost = tempCost;

                if (spawnedCoins >= curMaxCoins)
                    break;
            }
        }
    }

    // Spawn enemies and destructibles
    public IEnumerator SpawnEnemyIE()
    {
        while (true)
        {
            float timeToEnemy = TimeToEnemyLine;
            yield return new WaitForSeconds(Random.Range(timeToEnemy / 1.4f, timeToEnemy * 1.5f));

            float xPos = 15f;
            int curLine = Random.Range(1, 4);
            float yPos = 0f;

            // Set Y pos
            switch (curLine)
            {
                case 1:
                    yPos = GameController.Instance.RoadYs[0];
                    break;

                case 2:
                    yPos = GameController.Instance.RoadYs[1];
                    break;

                case 3:
                    yPos = GameController.Instance.RoadYs[2];
                    break;
            }

            curEnemyInLine = 0;
            curDestructibleInLine = 0;
            int curMaxEn = 0;
            int curMaxDes = 0;

            // Chance to spawn enemy or destructible or enemy + destructible
            bool spawnEnemy = false;
            bool spawnDestructible = false;

            {
                float chSp = Random.Range(0f, 100f);
                if (chSp <= 60)
                {
                    spawnEnemy = true;
                }
                else if (chSp > 60f && chSp <= 80f)
                {
                    spawnDestructible = true;
                }
                else
                {
                    spawnEnemy = true;
                    spawnDestructible = true;
                }
            }

            if (spawnEnemy)
                // How many enemies are spawning in current line (+ 0.3f need to often spawn max enemies in line)
                curMaxEn = Mathf.Clamp(Mathf.FloorToInt(Random.Range(maxEnemiesInLine / 1.5f, maxEnemiesInLine + 0.3f)), 1, 20);

            if (spawnDestructible)
            {
                // How many destructibles are spawning in current line (+ 0.3f need to often spawn max destructibles in line)

                if (spawnEnemy)
                {
                    curMaxDes = Mathf.Clamp(Mathf.FloorToInt(Random.Range(maxDestructibleInLine / 3f, maxDestructibleInLine / 2f + 0.3f)), 1, 20);
                }
                else
                {
                    curMaxDes = Mathf.Clamp(Mathf.FloorToInt(Random.Range(maxDestructibleInLine / 1.5f, maxDestructibleInLine + 0.3f)), 1, 20);
                }
            }

            for (int i = 0; i < curMaxEn + curMaxDes; i++)
            {
                // Object queues
                int enQueue, desQueue;

                // Second line
                int secLine = 0;
                float secLinePosY = 0f;

                // Set second line position
                {
                    float chSecLine = Random.Range(0f, 100f);
                    if (chSecLine <= 40f)
                    {
                        secLine = Random.Range(1, 4);

                        switch (secLine)
                        {
                            case 1:
                                secLinePosY = GameController.Instance.RoadYs[0];
                                break;

                            case 2:
                                secLinePosY = GameController.Instance.RoadYs[1];
                                break;

                            case 3:
                                secLinePosY = GameController.Instance.RoadYs[2];
                                break;
                        }
                    }
                }

                if (spawnEnemy)
                {
                    bool canSpawn = true;

                    if (spawnDestructible)
                    {
                        if (curDestructibleInLine < curMaxDes)
                        {
                            float chSpawnDes = Random.Range(0f, 100f);
                            if (chSpawnDes <= 40f)
                            {
                                canSpawn = false;
                            }
                        }
                    }

                    if (canSpawn)
                    {
                        enQueue = GameController.Instance.EnemiesQueueObject.transform.childCount;

                        // Enemy spawning attributes
                        int enSpawnLine = secLine > 0 ? secLine : curLine; // Line
                        float enSpawnY = secLine > 0 ? secLinePosY : yPos; // Y position
                        Vector3 enPos = new(xPos + i * 1.5f, enSpawnY, 0f); // Spawn position
                        Quaternion enRot = Quaternion.Euler(0f, 180f, 0f); // Spawn rotation


                        // Enemy is ranged or can he's change line
                        bool eRange = false;
                        bool eLineChg = false;

                        // Chance to range enemy
                        float chRgEn = Random.Range(0f, 100f);
                        if (chRgEn <= rangeEnemyChance)
                        {
                            eRange = true;
                        }

                        // Chance to line change enemy
                        float chLineChg = Random.Range(0f, 100f);
                        if (chLineChg <= lineChangingEnemyChance)
                        {
                            eLineChg = true;
                        }


                        // If enemies queue is empty
                        if (enQueue <= 0)
                        {
                            InstantiateEnemy(enPos, enRot, enSpawnLine, eRange, eLineChg);
                        }
                        // If enemies in queue is contains
                        else
                        {
                            // Can we get gameobject from queue
                            bool canGet = false;

                            for (int j = 0; j < enQueue; j++)
                            {
                                ThisIsEnemy en = GameController.Instance.EnemiesQueueObject.transform.GetChild(j).transform.GetComponent<ThisIsEnemy>();
                                if (en.InQueue)
                                {
                                    en.Init(eRange, eLineChg);
                                    en.SetFromQueue(enPos, enSpawnLine);
                                    canGet = true;
                                    break;
                                }
                            }

                            // If all enemies in queue is busy, spawn new enemy if queue is not full
                            if (!canGet)
                            {
                                if (enQueue < maxQueueEnemies)
                                {
                                    InstantiateEnemy(enPos, enRot, enSpawnLine, eRange, eLineChg);
                                }
                            }
                        }

                        curEnemyInLine++;
                    }
                }

                if (spawnDestructible)
                {
                    bool canSpawn = true;

                    if (spawnEnemy)
                    {
                        if (curEnemyInLine < curMaxEn)
                        {
                            float chSpawnEn = Random.Range(0f, 100f);
                            if (chSpawnEn <= 40f)
                            {
                                canSpawn = false;
                            }
                        }
                    }

                    if (canSpawn)
                    {
                        desQueue = GameController.Instance.DestructibleQueueObject.transform.childCount;

                        // Destructible spawning attributes
                        int spawnLine = secLine > 0 ? secLine : curLine; // Line
                        float spawnY = secLine > 0 ? secLinePosY : yPos; // Y position
                        Vector3 posit = new(xPos + i * 1.5f, spawnY, 0f); // Spawn position
                        Quaternion rotat = Quaternion.Euler(0f, 180f, 0f); // Spawn rotation

                        // If destructible queue is empty
                        if (desQueue <= 0)
                        {
                            InstantiateDestructible(posit, rotat, spawnLine);
                        }
                        // If destructible in queue is contains
                        else
                        {
                            // Can we get gameobject from queue
                            bool canGet = false;

                            for (int j = 0; j < desQueue; j++)
                            {
                                Destructible des = GameController.Instance.DestructibleQueueObject.transform.GetChild(j).transform.GetComponent<Destructible>();
                                if (des.InQueue)
                                {
                                    des.SetFromQueue(posit, spawnLine);
                                    canGet = true;
                                    break;
                                }
                            }

                            // If all enemies in queue is busy, spawn new enemy if queue is not full
                            if (!canGet)
                            {
                                if (desQueue < maxQueueDestructible)
                                {
                                    InstantiateDestructible(posit, rotat, spawnLine);
                                }
                            }
                        }

                        curDestructibleInLine++;
                    }
                }

                if (curEnemyInLine + curDestructibleInLine >= curMaxEn)
                    break;
            }
        }
    }

    public void InstantiateEnemy(Vector3 position, Quaternion rotation, int currentLine, bool isRange, bool canChangeLine)
    {
        ThisIsEnemy en = Instantiate(_enemyPrefab, position, rotation, GameController.Instance.EnemiesQueueObject.transform).transform.GetComponent<ThisIsEnemy>();
        en.Init(isRange, canChangeLine, currentLine, GameController.Instance.Player);
    }

    public void InstantiateDestructible(Vector3 position, Quaternion rotation, int currentLine)
    {
        Destructible des = Instantiate(destructiblePrefab, position, rotation, GameController.Instance.DestructibleQueueObject.transform).transform.GetComponent<Destructible>();
        des.Init(currentLine, GameController.Instance.Player);
    }
}
