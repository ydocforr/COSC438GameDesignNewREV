using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class OhmDropTile : Tile
        {
            private bool occupied;
            private double holdVal;
            public OhmDropTile(int key, Rectangle newBox, Game1 game)
                : base(game)
            {
            this.box = newBox;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTileImages/OhmDropTile.png"));
            switch (key)
                {
                    case 1:
                        {                         
                        this.colour = 1;
                        break;
                        }
                case 2:
                    {                    
                        this.colour = 2;
                        break;
                    }
                case 3:
                    {
                        this.colour = 3;
                        break;
                    }
                }
            }
        public bool Occupied
        {
            get
            {
                return occupied;
            }
            set
            {
                occupied = value;
            }
        }
        public double HoldVal
        {
            get
            {
                return holdVal;
            }
            set
            {
                holdVal = value;
            }
        }
    }
}
