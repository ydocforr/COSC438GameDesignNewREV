using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class PlayerTile : Tile
    {
        private Texture2D images;
        public PlayerTile(int key, Rectangle newBox,  Game1 game, Texture2D pImage)
           : base(game)
        {
            this.active = true;
            switch(key)
            {
                case 1:
                    {                                                                   
                        this.image = pImage;
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/PlayerImages/EngineerSpriteSheet.png"));
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/PlayerImages/MinerSpriteSheet.png"));
                        break;
                    }               
            }          
            this.Box = newBox;
        }
        public Texture2D Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
            }
        }
        public void swapImage(Texture2D currPImg)
        {
            this.image = currPImg;
        }
    }
}
