using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BonusPopupAlert : MonoBehaviour
{
    public GameObject tilePool;
    public Image background;
    public GameObject diamondParticle;
    private void OnEnable()
    {
        tilePool.SetActive(false);
       
        Texture2D texture = Resources.Load<Texture2D>("Game/BackgroundDiamond");
        Sprite newSprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );

        background.sprite = newSprite ;
    }
    public void ClosePopup()
    {
        diamondParticle.SetActive(true);
        tilePool.SetActive(true);
        gameObject.SetActive(false);
    }
}
