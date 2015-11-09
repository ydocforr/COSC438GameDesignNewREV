using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class SpikeTile : Tile
    {
        private bool falling;
        public SpikeTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            switch (key)
            {
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/SpikeTileUp.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/SpikeTileLeft.png"));
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/SpikeTileRight.png"));
                        break;
                    }
            }
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
