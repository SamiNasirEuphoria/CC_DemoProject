using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameVanilla.Game.Common;

public class WaterColorBombDisposed : MonoBehaviour
{
    public GameObject waterparticle;
    public static WaterColorBombDisposed instance;
    public static WaterColorBombDisposed Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }

    }
    public void Disposed(Sprite sprite,List<GameObject> tiles, Tile candy, GameBoard board)
    {
      StartCoroutine(DestroyCandies(sprite,tiles, candy, board));
    }
     IEnumerator DestroyCandies(Sprite sprite,List<GameObject> tiles, Tile candy, GameBoard board)
    {
        foreach (var tile in tiles.ToArray())
        {
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
            {
                Instantiate(waterparticle, tile.transform.position, tile.transform.rotation);
            }
        }
        yield return new WaitForSeconds(2.0f);
        foreach (var tile in tiles.ToArray())
        {
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
            {
                tile.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
        foreach (var tile in tiles.ToArray())
        {
            if (tile != null && tile.GetComponent<Candy>() != null &&
                tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
            {
                board.ExplodeTileNonRecursive(tile);
            }
        }
        board.ApplyGravity();
    }
}
