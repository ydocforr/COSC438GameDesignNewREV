using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class PromptTile : Tile
    {
        public PromptTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            this.active = false;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Mprompt.png"));
            this.Box = newBox;
        }
        public void activate()
        {
            this.active = true;
        }
        public void deactivate()
        {
            this.active = false;
        }
    }
}
