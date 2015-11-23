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
        private int pKey;
        private int dispProp;
        public PromptTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            this.pKey = key;
            this.active = false;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Mprompt.png"));
            this.Box = newBox;
            switch(key)
            {
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Mprompt.png"));
                        active = true;
                        dispProp = 1;
                        break;
                    }      
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Sprompt.png"));
                        dispProp = 1;
                        break;
                    }
            }
        }
        public int Pkey
        {
            get
            {
                return pKey;
            }
            set
            {
                pKey = value;
            }
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
