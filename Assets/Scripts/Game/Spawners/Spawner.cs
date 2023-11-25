using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Money
    [Header("Money")]
    [SerializeField] private GameObject moneyObject;

    [SerializeField] private int maxCoinsSpawned = 3;
    private int curCoinsSpawned = 0;
    [SerializeField] private int increaseCoinsInLine = 0;
    [SerializeField] private int increaseCoinsInLineMax = 25;

    [SerializeField] private int maxQueueMoneys = 50;

    // Time to spawn next line of money
    [SerializeField] private float timeToNextMoneyLine = 7f;

    [SerializeField] private float snowballChance = 35f;
    [SerializeField] private float gemChance = 0.02f;

    private Coroutine moneyCoroutine;
    #endregion /Money

    #region Enemies
    [Header("Enemies")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemiesInLine = 2;
    private int curEnemyInLine = 0;
    [SerializeField] private int increaseEnemiesInLine = 0;
    [SerializeField] private int increaseEnemiesInLineMax = 20;

    // Enemies chances
    [SerializeField] private float rangeEnemyChance = 35f; // Chance to spawn range weapon enemy
    [SerializeField] private float lineChangingEnemyChance = 35f; // Chance to spawn line change enemy

    [SerializeField] private int maxQueueEnemies = 50;

    // Time for spawn next line of enemies
    [SerializeField] private float timeToNextEnemyLine = 7f;

    private Coroutine enemiesCoroutine;
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

    private void Awake()
    {
        moneyCoroutine = StartCoroutine(SpawnMoney());

        enemiesCoroutine = StartCoroutine(SpawnEnemy());
    }

    public void UpdateSpeed()
    {
        timeToNextMoneyLine = Mathf.Clamp(timeToNextMoneyLine - 0.006f, 0.1f, 10f);
        timeToNextEnemyLine = Mathf.Clamp(timeToNextEnemyLine - 0.006f, 0.1f, 10f);

        increaseCoinsInLine++;
        if (increaseCoinsInLine > increaseCoinsInLineMax)
        {
            maxCoinsSpawned++;
            increaseCoinsInLine = 0;
            increaseCoinsInLineMax += 3;
        }

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

    public IEnumerator SpawnMoney()
    {
        // Set X and Y coordinates
        float xPos = 15f;
        int curLine = Random.Range(1, 4);
        float yPos = 0f;
        switch(curLine)
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

        curCoinsSpawned = 0;

        // How many coins are spawning in current line (+ 0.3f need to often spawn max coins in line)
        int curMaxCoins = Mathf.FloorToInt(Random.Range(maxCoinsSpawned / 1.5f, maxCoinsSpawned + 0.3f));
        int curCoinCost = 1;
        MoneyType mTp = MoneyType.coin;

        // Spawn snowballs on christmass event
        if (GameController.CurrentEvent == EventType.christmass)
        {
            float chSm = Random.Range(0f, 100f);
            if (chSm <= snowballChance)
            {
                mTp = MoneyType.snowball;
            }
        }
        
        // Chances to spawn more expensive coins
        if (mTp == MoneyType.coin)
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

        yield return new WaitForSeconds(Random.Range(timeToNextMoneyLine / 1.3f, timeToNextMoneyLine * 1.5f));


        for (int i = 0; i < curMaxCoins; i++)
        {
            int mQueue = GameController.Instance.MoneyQueueObject.transform.childCount;

            // Temp variables if spawned gem, these vars set to default
            MoneyType tMTp = mTp;
            int tCost = curCoinCost;

            // Chance to spawn gem
            float chGm = Random.Range(0f, 100f);
            if (chGm <= gemChance)
            {
                mTp = MoneyType.gem;
                curCoinCost = 1;
            }

            // If moneys queue is empty
            if (mQueue <= 0)
            {
                MoneyPickable mon = Instantiate(moneyObject, new Vector3(xPos + i * 1.5f, yPos, 0f), Quaternion.identity, GameController.Instance.MoneyQueueObject.transform).transform.GetComponent<MoneyPickable>();
                mon.Init(curLine, curCoinCost, mTp);
            }
            // If money in queue is contains
            else
            {
                // Can we get game object from queue
                bool canGet = false;

                for (int j = 0; j < mQueue; j++)
                {
                    MoneyPickable mon = GameController.Instance.MoneyQueueObject.transform.GetChild(j).transform.GetComponent<MoneyPickable>();
                    if (mon.InQueue)
                    {
                        mon.Init(curLine, curCoinCost, mTp);
                        mon.SetFromQueue(new Vector3(xPos + i * 1.5f, yPos, 0f), curLine);
                        canGet = true;
                        break;
                    }
                }

                // If all coins in queue is busy, spawn new coin if queue is not full
                if (!canGet)
                {
                    if (mQueue < maxQueueMoneys)
                    {
                        MoneyPickable mon = Instantiate(moneyObject, new Vector3(xPos + i * 1.5f, yPos, 0f), Quaternion.identity, GameController.Instance.MoneyQueueObject.transform).transform.GetComponent<MoneyPickable>();
                        mon.Init(curLine, curCoinCost, mTp);
                    }
                }
            }

            curCoinsSpawned++;

            // Set current money type and current money cost to default
            mTp = tMTp;
            curCoinCost = tCost;

            // If all coins spawned stop all coroutine and start new coroutine
            if (curCoinsSpawned >= curMaxCoins)
            {
                StopCoroutine(moneyCoroutine);
                moneyCoroutine = StartCoroutine(SpawnMoney());
            }
        }
    }

    // Spawn enemies and destructibles
    public IEnumerator SpawnEnemy()
    {
        // Set X and Y pos
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

        yield return new WaitForSeconds(Random.Range(timeToNextEnemyLine / 1.4f, timeToNextEnemyLine * 1.5f));

        int baseZOrder = 0;
        int destZOrder = 0;

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
                        InstantiateEnemy(enPos, enRot, enSpawnLine, baseZOrder, eRange, eLineChg);
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
                                en.SetFromQueue(enPos, enSpawnLine, baseZOrder);
                                canGet = true;
                                break;
                            }
                        }

                        // If all enemies in queue is busy, spawn new enemy if queue is not full
                        if (!canGet)
                        {
                            if (enQueue < maxQueueEnemies)
                            {
                                InstantiateEnemy(enPos, enRot, enSpawnLine, baseZOrder, eRange, eLineChg);
                            }
                        }
                    }

                    baseZOrder += 15;
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
                        InstantiateDestructible(posit, rotat, spawnLine, destZOrder);
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
                                des.Init(destZOrder);
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
                                InstantiateDestructible(posit, rotat, spawnLine, destZOrder);
                            }
                        }
                    }

                    destZOrder += 1;
                    curDestructibleInLine++;
                }
            }

            // If all enemies and destructibles spawned stop all coroutine and start new coroutine
            if (curEnemyInLine + curDestructibleInLine >= curMaxEn)
            {
                StopCoroutine(enemiesCoroutine);
                enemiesCoroutine = StartCoroutine(SpawnEnemy());
            }
        }
    }

    public void InstantiateEnemy(Vector3 position, Quaternion rotation, int currentLine, int ZOrder, bool isRange, bool canChangeLine)
    {
        ThisIsEnemy en = Instantiate(enemyPrefab, position, rotation, GameController.Instance.EnemiesQueueObject.transform).transform.GetComponent<ThisIsEnemy>();
        en.Init(isRange, canChangeLine, ZOrder, currentLine, GameController.Instance.Player);
    }

    public void InstantiateDestructible(Vector3 position, Quaternion rotation, int currentLine, int ZOrder)
    {
        Destructible des = Instantiate(destructiblePrefab, position, rotation, GameController.Instance.DestructibleQueueObject.transform).transform.GetComponent<Destructible>();
        des.Init(ZOrder, currentLine, GameController.Instance.Player);
    }
}
