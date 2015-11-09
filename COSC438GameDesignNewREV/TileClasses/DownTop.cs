using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class DownTop : Tile
    {
        public DownTop(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
<<<<<<< HEAD
            this.active = true;
=======
            this.tileType = "X";
>>>>>>> origin/master
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/DownTop.png"));
            this.Box = newBox;
        }
    }
}
