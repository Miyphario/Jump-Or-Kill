using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
/*
public static class SaveSystem
{

    //Scores
    public static void SaveScore(BaseEntity entity)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/scores.sav";

        if (!File.Exists(path))
        {
            FileStream file = File.Create(path);

            Scores score = new Scores(entity);

            List<Scores> data = new List<Scores>();
            data.Add(score);

            formatter.Serialize(file, data);
            file.Close();
        }
        else
        {
            FileStream file = File.OpenRead(path);
            List<Scores> data = formatter.Deserialize(file) as List<Scores>;
            file.Close();

            Scores score = new Scores(entity);

            if (data != null)
            {
                data.Add(score);
            }
            else
            {
                data = new List<Scores>();
                data.Add(score);
            }

            int m = data.Min(x => x.distance_traveled);
            int d = 0;

            if (data.Count > 20)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].distance_traveled < m)
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

    public static Scores LoadBestScore()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/scores.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File SCORES not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                List<Scores> data = formatter.Deserialize(file) as List<Scores>;
                file.Close();

                if (data != null)
                {
                    int b = 0;
                    int findex = 0;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].distance_traveled > b)
                        {
                            b = data[i].distance_traveled;
                            findex = i;
                        }
                    }

                    return data[findex];
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public static List<Scores> LoadScores()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/scores.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File SCORES not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                List<Scores> data = formatter.Deserialize(file) as List<Scores>;
                file.Close();

                return data;
            }
        }
    }

    //Datas
    public static void SaveData(GameData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/data.sav";

        FileStream file = File.Create(path);

        formatter.Serialize(file, saveData);
        file.Close();
    }

    public static GameData LoadData()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/data.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File DATA not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                GameData data = formatter.Deserialize(file) as GameData;
                file.Close();

                return data;
            }
        }
    }

    //Skins
    public static void SaveSkins(List<Skin> skins)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/skins.sav";
        FileStream file = File.Create(path);

        SkinsData data = new SkinsData(skins);

        formatter.Serialize(file, data);
        file.Close();
    }

    public static List<Skin> LoadSkins()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/skins.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File SKINS not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                SkinsData data = formatter.Deserialize(file) as SkinsData;
                file.Close();

                return data.skins;
            }
        }
    }

    //Weapons
    public static void SaveWeapons(List<Weapon> weapons)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/weapons.sav";
        FileStream file = File.Create(path);

        WeaponData data = new WeaponData(weapons);

        formatter.Serialize(file, data);
        file.Close();
    }

    public static List<Weapon> LoadWeapons()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/weapons.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File WEAPONS not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                WeaponData data = formatter.Deserialize(file) as WeaponData;
                file.Close();

                return data.weapons;
            }
        }
    }

    //Boosts
    public static void SaveBoosts(List<BoostData> boosts)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/boosts.sav";
        FileStream file = File.Create(path);

        formatter.Serialize(file, boosts);
        file.Close();
    }

    public static List<BoostData> LoadBoosts()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Debug.Log("Directory not exists");
            return null;
        }
        else
        {
            string path = Application.persistentDataPath + "/saves/boosts.sav";
            if (!File.Exists(path))
            {
                Debug.Log("File BOOSTS not exists");
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(path);

                List<BoostData> data = formatter.Deserialize(file) as List<BoostData>;
                file.Close();

                return data;
            }
        }
    }
    
    //Other
    public static void ResetGameProgress()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saves";
        if (Directory.Exists(path))
        {
            Debug.Log("Directory SAVES exists, deleting...");
            DirectoryInfo di = new DirectoryInfo(path + "/");
            foreach (FileInfo f in di.EnumerateFiles())
            {
                f.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete();
            }
            Directory.Delete(path);
            Debug.Log("Directory SAVES deleted.");
        }
    }

    public static void ResetScores()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saves";
        if (Directory.Exists(path))
        {
            if (File.Exists(path + "/scores.sav"))
            {
                File.Delete(path + "/scores.sav");
            }    
        }
    }
    
}
*/