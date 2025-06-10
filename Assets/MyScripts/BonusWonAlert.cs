using UnityEngine;
using UnityEngine.UI;
using GameVanilla.Game.Scenes;
public class BonusWonAlert : MonoBehaviour
{
    public Image background;
    public GameObject diamondParticle;
    public GameObject tileObject;
    public void ChangeTexture()
    {
        Texture2D texture = Resources.Load<Texture2D>("Game/Background");
        Sprite newSprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        background.sprite = newSprite;
    }
    public void PopupClosed()
    {
        tileObject.SetActive(true);
        diamondParticle.SetActive(false);
        ChangeTexture();
        gameObject.SetActive(false);
    }
    public Text bonusScoreText;
    private float totalScore;
    private void OnEnable()
    {
        tileObject.SetActive(false);
        GameScene.bonusCheck = false;
    }
    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("Dollar");
        bonusScoreText.text = totalScore.ToString();

    }
}
