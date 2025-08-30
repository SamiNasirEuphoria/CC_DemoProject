using UnityEngine;
using UnityEngine.Networking;

public class ShareManager : MonoBehaviour
{
    [Tooltip("Game Play Store Link.")]
    // Play Store link of your game
    public string gameLink = "https://play.google.com/store/apps/details?id=com.MMonlinesolutions.TheSuperb10";

    [Tooltip("Custom share message.")]
    // Message to be shared with the link
    public string shareMessage = "I'm playing The Superb 10! Check it out: ";

    /// <summary>
    /// Share the game on WhatsApp.
    /// </summary>
    public void ShareOnWhatsApp()
    {
        string url = "https://wa.me/?text=" + UnityWebRequest.EscapeURL(shareMessage + gameLink);
        Application.OpenURL(url);
    }

    /// <summary>
    /// Share the game on Facebook.
    /// </summary>
    public void ShareOnFacebook()
    {
        string url = "https://www.facebook.com/sharer/sharer.php?u=" + UnityWebRequest.EscapeURL(gameLink);
        Application.OpenURL(url);
    }
}
