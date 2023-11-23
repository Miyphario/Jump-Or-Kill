using UnityEngine;
using UnityEngine.Advertisements;

/*
public class AdsController : MonoBehaviour, IUnityAdsListener
{
    [HideInInspector]
    public static int show_ads_num = 5;
    [HideInInspector]
    public static int max_ads_num = 5;
    [HideInInspector]
    public bool ads_init = false;
    public bool in_menu = false;

    public HUD game_hud;
    public MenuHUD menu_hud;

    private string gameId = "3950153";
    private string cur_ads_type;

    private string reward_ads = "rewardedVideo";
    private string video_ads = "video";

    private void Start()
    {
        if (!ads_init)
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize(gameId, false);
                Advertisement.AddListener(this);
                cur_ads_type = in_menu ? video_ads : reward_ads;
                ads_init = true;
            }
        }

        if (in_menu)
        {
            show_ads_num--;
            if (show_ads_num <= 0)
            {
                show_ads_num = max_ads_num;
                ShowAds();
            }
        }
    }

    public bool AdsReady()
    {
        if (ads_init)
        {
            if (Advertisement.IsReady(cur_ads_type))
            {
                return true;
            }
        }

        return false;
    }

    public void ShowAds()
    {
        if (ads_init)
        {
            Advertisement.Show(cur_ads_type);
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (in_menu)
        {

        }
        else
        {
            if (game_hud != null)
            {
                if (showResult == ShowResult.Finished)
                {
                    game_hud.TogglePauseAds(false, false);
                }
                else if (showResult == ShowResult.Skipped)
                {
                    game_hud.TogglePauseAds(false, true);
                }
                else if (showResult == ShowResult.Failed)
                {
                    game_hud.TogglePauseAds(false, true);
                }
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {

    }
}
*/