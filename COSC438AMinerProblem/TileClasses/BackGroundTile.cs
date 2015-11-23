using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class BackGroundTile : Tile
    {
        public BackGroundTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            switch(key)
            {
                case -3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/WireYellow.png"));
                        break;
                    }
                case -2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/WireRed.png"));
                        break;
                    }
                case -1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/WireGreen.png"));
                        break;
                    }
                case 0:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/BGTile.png"));
                        break;
                    }
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/FillerBackground.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ResourceImages/BurstBarUnderlay.png"));
                        break;
                    }
            }
            
            this.Box = newBox;
        }
    }
}