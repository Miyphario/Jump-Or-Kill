using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public enum GameTheme
{
    summer,
    winter
}
*/

public class CamController : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed = 0.1f;

    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;

    private void Start()
    {
        EventType et = ManagerScript.GetCurrentSeason();

        switch (et)
        {
            case EventType.winter:
                GameController.Instance.MainCamera.backgroundColor = ManagerScript.MakeColor(225, 236, 244, 0);
                break;

            case EventType.summer:
                GameController.Instance.MainCamera.backgroundColor = ManagerScript.MakeColor(48, 109, 48, 0);
                break;
        }
    }

    private void Update()
    {
        if (!GameController.Instance.Player.InAir /*&& gameController.player.currentLine == gameController.player.lineToMove*/)
        {
            Transform parent = transform.parent.transform;
            Vector3 mPos = new(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
            Vector3 tPos = new(parent.transform.position.x, Mathf.Clamp(GameController.Instance.Player.transform.position.y, _minY, _maxY), parent.transform.position.z);

            parent.position = Vector3.Lerp(mPos, tPos, _lerpSpeed);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, origPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = origPos;
    }
}
