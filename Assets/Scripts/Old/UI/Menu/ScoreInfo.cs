using UnityEngine;
using UnityEngine.UI;

public class ScoreInfo : MonoBehaviour
{
    [HideInInspector]
    public Scores score;
    public Text info_text;
    public Text date_text;
    public RectTransform rect_transform;

    private void Start()
    {
        rect_transform.anchoredPosition3D = new Vector3(0, 0, 0);
    }

    public void UpdateScore()
    {
        info_text.text = "Distance: " + score.distance_traveled + "\nKills: " + score.kills + "\nMoney: " + score.money_taked;
        date_text.text = "Date: " + score.save_time;
    }
}
