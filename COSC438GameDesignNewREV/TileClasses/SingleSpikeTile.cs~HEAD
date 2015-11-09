using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class SingleSpikeTile : Tile
    {
        private bool falling;
        public SingleSpikeTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameCaveEventTileImages/SingleSpikeTile.png"));
            this.Box = newBox;
        }
        public bool Falling
        {
            get
            {
                return falling;
            }
            set
            {
                falling = value;
            }
        }
    }
}