// Copyright (C) 2017-2018 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;
using UnityEngine;
using GameVanilla.Core;

namespace GameVanilla.Game.Common
{
    /// <summary>
    /// A special candy that destroys a full row or column based on its movement direction.
    /// </summary>
    public class RocketMissileCandy : Tile
    {
        private Vector3 myposition;
        private Transform myTransform;
        private void Start()
        {
            myTransform = GetComponent<Transform>();
            myposition = myTransform.position;
            Debug.Log("Rocket current X is " + myposition.x + " nd Y is " + myposition.y);
        }
        // Whether the missile was moved horizontally.
        private bool movedHorizontally;

        // Cached list of tiles to destroy.
        private readonly List<GameObject> cachedTiles = new List<GameObject>();

        /// <summary>
        /// Sets the direction of movement before explosion.
        /// </summary>
        /// <param name="isHorizontal">True if moved horizontally; false if vertically.</param>
        public void SetMoveDirection(bool isHorizontal)
        {
            movedHorizontally = isHorizontal;
        }

        /// <summary>
        /// Returns all tiles to be destroyed by the missile.
        /// </summary>
        public override List<GameObject> Explode()
        {
            if (gameObject.activeSelf && TryGetComponent(out Animator animator))
            {
                animator.SetTrigger("Kill");
            }

            cachedTiles.Clear();

            if (movedHorizontally)
            {
                // Destroy full row
                for (int i = 0; i < board.level.width; i++)
                {
                    var tile = board.GetTile(i, y);
                    if (tile != null && tile.gameObject != gameObject) // Skip self to avoid double
                    {
                        cachedTiles.Add(tile.gameObject);
                    }
                }
            }
            else
            {
                // Destroy full column
                for (int j = 0; j < board.level.height; j++)
                {
                    var tile = board.GetTile(x, j);
                    if (tile != null && tile.gameObject != gameObject)
                    {
                        cachedTiles.Add(tile.gameObject);
                    }
                }
            }

            cachedTiles.Add(gameObject); // Include self last
            return cachedTiles;
        }

        /// <summary>
        /// Visual effects for the rocket explosion.
        /// </summary>
        public override void ShowExplosionFx(FxPool pool)
        {
            var fx = pool.colorBombExplosion.GetObject(); // Reuse if you don’t have custom FX
            fx.transform.position = transform.position;

            SoundManager.instance.PlaySound("ColorBomb"); // Or use custom RocketMissile sound
        }

        /// <summary>
        /// Game state update after explosion (optional).
        /// </summary>
        public override void UpdateGameState(GameState state)
        {
            // Nothing to track (no color)
        }
    }
}
