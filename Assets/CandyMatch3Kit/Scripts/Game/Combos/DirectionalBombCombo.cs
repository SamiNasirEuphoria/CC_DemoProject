using System.Collections.Generic;
using GameVanilla.Core;
using UnityEngine;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// Represents a combo where a directional bomb is swapped.
    /// The direction of the swipe determines if a row or column is cleared.
    /// </summary>
    public class DirectionalBombCombo : Combo
    {
        public static bool check=false;
        public override void Resolve(GameBoard board, List<GameObject> tiles, FxPool fxPool)
        {
            if (tiles == null || tiles.Count < 2)
            {
                Debug.LogWarning("DirectionalBombCombo requires exactly two tiles.");
                return;
            }

            GameObject directionalBombTile = null;
            GameObject otherTile = null;

            foreach (var tile in tiles)
            {
                if (tile != null && tile.GetComponent<DirectionalBomb>() != null)
                    directionalBombTile = tile;
                else
                    otherTile = tile;
            }

            if (directionalBombTile == null || otherTile == null)
            {
                Debug.LogWarning("DirectionalBombCombo requires one directional bomb and one other tile.");
                return;
            }

            Vector2Int dirBombPos = board.GetTilePosition(directionalBombTile);
            Vector2Int otherPos = board.GetTilePosition(otherTile);

            if (dirBombPos.x == -1 || otherPos.x == -1)
            {
                Debug.LogWarning("Could not find tile positions on the board.");
                return;
            }

            // Fix: correctly detect swipe direction
            bool isHorizontal = otherPos.y == dirBombPos.y && Mathf.Abs(otherPos.x - dirBombPos.x) == 1;

            if (!check)
            {
                Debug.Log("DirectionalBombCombo: Horizontal swipe, destroying row.");
                for (int col = 0; col < board.level.width; col++)
                {
                    var tile = board.GetTile(col, dirBombPos.y);
                    if (tile != null)
                    {
                        board.ExplodeTileNonRecursive(tile);
                        tile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                        board.ApplyGravity();
                    }
                }
                check = true;
            }
            else
            {
                Debug.Log("DirectionalBombCombo: Vertical swipe, destroying column.");
                for (int row = 0; row < board.level.height; row++)
                {
                    var tile = board.GetTile(dirBombPos.x, row);
                    if (tile != null)
                    {
                        board.ExplodeTileNonRecursive(tile);
                        tile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                        board.ApplyGravity();
                    }
                }
                check = false;
            }
            SoundManager.instance.PlaySound("ColorBomb");
        }
    }
}
