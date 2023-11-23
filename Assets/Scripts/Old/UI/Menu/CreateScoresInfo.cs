using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
public class CreateScoresInfo : MonoBehaviour
{
    public GameObject create_prefab;
    public GameObject text_to_disable;

    private List<Scores> scores = new List<Scores>();
    private List<GameObject> score_objects = new List<GameObject>();

    public void UpdateScores()
    {
        scores = SaveSystem.LoadScores();
        if (scores != null)
        {
            scores.Sort((y, x) => x.distance_traveled.CompareTo(y.distance_traveled));
        }

        if (scores != null)
        {
            if (text_to_disable.activeSelf)
                text_to_disable.SetActive(false);

            if (score_objects != null)
            {
                foreach (GameObject g in score_objects)
                {
                    if (g != null)
                        Destroy(g);
                }
                score_objects = new List<GameObject>();
            }

            foreach (Scores s in scores)
            {
                GameObject p = Instantiate(create_prefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
                score_objects.Add(p);
                p.transform.GetComponent<ScoreInfo>().score = s;
                p.transform.GetComponent<ScoreInfo>().UpdateScore();
            }
        }
        else
        {
            if (!text_to_disable.activeSelf)
                text_to_disable.SetActive(true);
        }
    }
}
*/