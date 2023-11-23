using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class PlayGamesScript : MonoBehaviour
{
    private static PlayGamesScript myClass;

    private void Awake()
    {
        if (!myClass)
        {
            myClass = this;

            /*PlayGamesPlatform.Activate();
            SignIn();*/

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Start()
    {
        
    }

    public static void SignIn()
    {
        //Social.localUser.Authenticate((bool success) => { });
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            switch (result)
            {
                case SignInStatus.Success:
                    Debug.Log("Success SignIn!");
                    break;

                default:
                    Debug.Log("SignIn Failed!");
                    break;
            }
        });
    }

    public static void SignOut()
    {
        if (GetAuthorized())
        {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    public static bool GetAuthorized()
    {
        return Social.Active.localUser.authenticated;
    }

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        if (GetAuthorized())
        {
            Social.ReportProgress(id, 100, success => { });
        }
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        if (GetAuthorized())
        {
            PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
        }
    }

    public static void ShowAchievementUI()
    {
        if (GetAuthorized())
        {
            Social.ShowAchievementsUI();
        }
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (GetAuthorized())
        {
            Social.ReportScore(score, leaderboardId, success => { });
        }
    }

    public static void ShowLeaderboarsUI()
    {
        if (GetAuthorized())
        {
            Social.ShowLeaderboardUI();
        }
    }
    #endregion /Leaderboards
}
