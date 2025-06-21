using UnityEngine;
using UnityEngine.UI;

public class RewardPanelUI : MonoBehaviour
{
    public Text rewardMessageText;

    //public void RefreshMessage()
    //{
      //  if (rewardMessageText != null && Implementation.Instance != null)
       // {
         //   rewardMessageText.text = Implementation.Instance.GetRewardMessage();
        //}
    //}

    public void OnWatchAdButtonPressed()
    {
        Implementation.Instance.ShowRewardedVideo();
        //gameObject.SetActive(false);
    }

    public void OnCancelButtonPressed()
    {
        gameObject.SetActive(false);
    }
}
