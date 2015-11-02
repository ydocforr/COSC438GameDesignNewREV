using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class SplitSpikeTile : Tile
    {
        private bool falling;
        private bool split;
        private int xVelocity;
        private int yVelocity;
        public SplitSpikeTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameCaveEventTileImages/SplitSpikeTile.png"));
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
        public bool Split
        {
            get
            {
                return split;
            }
            set
            {
                split = value;
            }
        }
        public int XVelocity
        {
            get
            {
                return xVelocity;
            }
            set
            {
                xVelocity = value;
            }
        }
        public int YVelocity
        {
            get
            {
                return yVelocity;
            }
            set
            {
                yVelocity = value;
            }
        }
    }
}
