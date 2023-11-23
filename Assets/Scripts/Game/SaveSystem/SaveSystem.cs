using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveScoreData(ScoreData scoreData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saves/scores.sav";

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        if (!File.Exists(path))
        {
            FileStream file = File.Create(path);

            List<ScoreData> data = new List<ScoreData>();
            data.Add(scoreData);

            formatter.Serialize(file, data);
            file.Close();
        }
        else
        {
            FileStream file = File.OpenRead(path);
            List<ScoreData> data = formatter.Deserialize(file) as List<ScoreData>;
            file.Close();

            if (data != null)
            {
                data.Add(scoreData);
            }
            else
            {
                data = new List<ScoreData>();
                data.Add(scoreData);
            }

            // Remove old datas
            int m = data.Min(x => x.distanceTraveled);
            int d = 0;

            if (data.Count > 30)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].distanceTraveled < m)
                    {
                        d = i;
                    }
                }

                data.RemoveAt(d);
            }

            FileStream fileC = File.Create(path);

            formatter.Serialize(fileC, data);
            fileC.Close();
        }
    }

    public static List<ScoreData> LoadScoreData()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/scores.sav";
            if (!File.Exists(path))
            {
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                List<ScoreData> data = formatter.Deserialize(file) as List<ScoreData>;
                file.Close();

                return data;
            }
        }
    }


    public static void SavePlayerData(PlayerData playerData, bool add)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saves/playerData.sav";

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        // Check file to rewrite
        bool reWrite = false;

        if (!add)
        {
            reWrite = true;
        }
        else
        {
            if (!File.Exists(path))
            {
                reWrite = true;
            }
            else
            {
                FileStream file = File.OpenRead(path);

                PlayerData data = formatter.Deserialize(file) as PlayerData;
                file.Close();

                data.money += playerData.money;
                data.snowballs += playerData.snowballs;
                data.gems += playerData.gems;
                data.adsMoney += playerData.adsMoney;

                FileStream fileC = File.Create(path);

                formatter.Serialize(fileC, data);
                fileC.Close();
            }
        }

        if (reWrite)
        {
            FileStream file = File.Create(path);

            formatter.Serialize(file, playerData);
            file.Close();
        }
    }

    public static PlayerData LoadPlayerData()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/playerData.sav";
            if (!File.Exists(path))
            {
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                PlayerData data = formatter.Deserialize(file) as PlayerData;
                file.Close();

                return data;
            }
        }
    }


    public static void SaveWeaponData()
    {

    }
}
