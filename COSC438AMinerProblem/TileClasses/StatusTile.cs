using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class StatusTile : Tile
    {
        private bool status;
        private int wireColor;
        public StatusTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {    
            switch(key)
            {
                case 1:
                    {
                        wireColor = 1;
                        break;
                    }
                case 2:
                    {
                        wireColor = 2;
                        break;
                    }
                case 3:
                    {
                        wireColor = 3;
                        break;
                    }
            }    
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTileImages/X.png"));      
            this.Box = newBox;
        }
       public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public int WireColor
        {
            get
            {
                return wireColor;
            }
            set
            {
                wireColor = value;
            }
        }
        public void updateStatus(int state)
        {
            if(state == 1)
            {
                this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTileImages/Check.png"));
            }
            else if (state == 2)
            {
                this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTileImages/X.png"));
            }
        }
    }
}
