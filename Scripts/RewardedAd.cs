using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string adUnitId = "Rewarded_Android";
    private string rewardType = ""; // Can mý süre mi
    public GameObject AdsPanel, MenuObject;
    private bool isAdReady = false;

    void Start()
    {
        Advertisement.Load(adUnitId, this);
    }

    // Bu fonksiyonu butonlara baðlayacaðýz
    public void ShowAdForReward(string type)
    {
        if (!isAdReady)
        {
            Debug.LogWarning("Reklam hazýr deðil babba!");
            return;
        }

        rewardType = type;
        Advertisement.Show(adUnitId, this);
        isAdReady = false;
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(this.adUnitId))
        {
            isAdReady = true;
            Debug.Log("Reklam yüklendi ve hazýr!");
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Reklam yüklenemedi: {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Time.timeScale = 1;
            if (rewardType == "Health")
            {
                PlayerPrefs.SetInt("Health", 3);
                PlayerPrefs.Save();
                HealthSystem.instance.HealthUpdate();
                AdsPanel.SetActive(false);
                MenuObject.SetActive(true);
            }
            else if(rewardType == "TryAgainHealth")
            {
                PlayerPrefs.SetInt("Health", 3);
                PlayerPrefs.Save();
                AdsPanel.SetActive(false);
            }
            else if (rewardType == "Time")
            {
                GameBuilder.Instance.AdsReward();
            }
        }
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Reklam gösterilemedi: {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { Time.timeScale = 0f; }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
