
using UnityEngine;
using UnityEngine.UI;

public class BonusLooseAltert : MonoBehaviour
{
    public Image background;
    public GameObject diamondParticle;
    public GameObject tileObject;
    private void OnEnable()
    {
        tileObject.SetActive(false);
    }
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
}
