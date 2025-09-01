// Copyright (C) 2017-2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;

using GameVanilla.Core;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// The class used for the color bomb + color bomb combo.
    /// </summary>
    public class TwoColorBombCombo : ColorBombCombo
    {
        ///// <summary>
        ///// Resolves this combo.
        ///// </summary>
        ///// <param name="board">The game board.</param>
        ///// <param name="tiles">The tiles destroyed by the combo.</param>
        ///// <param name="fxPool">The pool to use for the visual effects.</param>
        //public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        //{

        //    if (tiles == null || tiles.Count == 0)
        //        return;

        //    // Use the first tile in the list as the center tile
        //    var centerTile = tiles[0];
        //    var x = centerTile.GetComponent<Tile>().x;
        //    var y = centerTile.GetComponent<Tile>().y;

        //    // Clear the input tiles list and repopulate it
        //    tiles.Clear();

        //    // Add all surrounding tiles (cross + diagonals)
        //    tiles.Add(board.GetTile(x - 1, y - 1));
        //    tiles.Add(board.GetTile(x - 2, y - 2));
        //    tiles.Add(board.GetTile(x - 3, y - 3));

        //    tiles.Add(board.GetTile(x, y - 1));
        //    tiles.Add(board.GetTile(x, y - 2));
        //    tiles.Add(board.GetTile(x, y - 3));

        //    tiles.Add(board.GetTile(x + 1, y - 1));
        //    tiles.Add(board.GetTile(x + 2, y - 2));
        //    tiles.Add(board.GetTile(x + 3, y - 3));

        //    tiles.Add(board.GetTile(x - 1, y));
        //    tiles.Add(board.GetTile(x - 2, y));
        //    tiles.Add(board.GetTile(x - 3, y));

        //    tiles.Add(centerTile); // center

        //    tiles.Add(board.GetTile(x + 1, y));
        //    tiles.Add(board.GetTile(x + 2, y));
        //    tiles.Add(board.GetTile(x + 3, y));

        //    tiles.Add(board.GetTile(x - 1, y + 1));
        //    tiles.Add(board.GetTile(x - 2, y + 2));
        //    tiles.Add(board.GetTile(x - 3, y + 3));

        //    tiles.Add(board.GetTile(x, y + 1));
        //    tiles.Add(board.GetTile(x, y + 2));
        //    tiles.Add(board.GetTile(x, y + 3));

        //    tiles.Add(board.GetTile(x + 1, y + 1));
        //    tiles.Add(board.GetTile(x + 2, y + 2));
        //    tiles.Add(board.GetTile(x + 3, y + 3));


        //    foreach (var tile in tiles)
        //    {
        //        if (tile != null && (tile.GetComponent<Candy>() != null || tile.GetComponent<ColorBomb>() != null))
        //        {
        //            board.ExplodeTileNonRecursive(tile);
        //        }
        //    }

        //    SoundManager.instance.PlaySound("ColorBomb");

        //    board.ApplyGravity();
        //}



        /// <summary>
        /// Resolves this combo.
        /// </summary>
        /// <param name="board">The game board.</param>
        /// <param name="tiles">The tiles destroyed by the combo.</param>
        /// <param name="fxPool">The pool to use for the visual effects.</param>
        ///

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

            //foreach (var tile in tiles.ToArray())
            //{
            //    if (tile != null && tile.GetComponent<Candy>() != null &&
            //        tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
            //    {
            //        tile.GetComponent<SpriteRenderer>().sprite = newSprite;
            //    }
            //}

            foreach (var tile in tiles.ToArray())
            {
                if (tile != null && tile.GetComponent<Candy>() != null &&
                    tile.GetComponent<Candy>().color == candy.GetComponent<Candy>().color)
                {
                    board.ExplodeTileNonRecursive(tile);
                }
            }

            SoundManager.instance.PlaySound("ColorBomb");

            board.ApplyGravity();
        }

    }
}
