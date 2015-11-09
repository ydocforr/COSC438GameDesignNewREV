using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class CraneTile : Tile
    {
        public CraneTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            switch(key)
            {
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_01.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_02.png"));
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_03.png"));
                        break;
                    }
                case 4:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_04.png"));
                        break;
                    }
                case 5:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_05.png"));
                        break;
                    }
                case 6:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_06.png"));
                        break;
                    }
                case 7:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_07.png"));
                        break;
                    }
                case 8:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images//LVL2Resources/CraneFiller_08.png"));
                        break;
                    }
            }       
            this.Box = newBox;
        }
    }
}
