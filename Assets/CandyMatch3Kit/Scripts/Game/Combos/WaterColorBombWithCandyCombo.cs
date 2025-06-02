// Copyright (C) 2017-2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;
using System.Collections;

using GameVanilla.Core;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class used for the color bomb + candy combo.
    /// </summary>
    public class WaterColorBombWithCandyCombo : ColorBombCombo
    {
        public Sprite temp;
        /// <summary>
        /// Resolves this combo.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="tiles">The tiles destroyed by the combo.</param>
        /// <param name="fxPool">The pool to use for the visual effects.</param>
        ///
        IEnumerator DestroyCandies(List<GameObject> tiles, Tile candy, GameBoard board)
        {
            yield return new WaitForSeconds(1.0f);
            foreach (var tile in tiles.ToArray())
            {
                if (tile != null && tile.GetComponent<Candy>() != null &&
                    tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
                {
                    board.ExplodeTileNonRecursive(tile);
                }
            }
        }
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            Texture2D texture = Resources.Load<Texture2D>("Game/Water");
            Sprite newSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );



            base.Resolve(board, tiles, fxPool);

            var candy = tileA.GetComponent<Candy>() != null ? tileA : tileB;
              
            foreach (var tile in tiles.ToArray())
            {
                if (tile != null && tile.GetComponent<Candy>() != null &&
                    tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
                {
                    temp = tile.GetComponent<SpriteRenderer>().sprite;
                    tile.GetComponent<SpriteRenderer>().sprite = newSprite;
                }
            }
            WaterColorBombDisposed.instance.Disposed(temp,tiles,candy, board);
            //StartCoroutine(DestroyCandies(tiles,candy,board));
            SoundManager.instance.PlaySound("ColorBomb");

           
        }
    }
}
