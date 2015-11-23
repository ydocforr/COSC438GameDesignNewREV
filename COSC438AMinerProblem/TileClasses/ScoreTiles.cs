using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class ScoreTiles : Tile
    {
        //Important item properties
        private int dispRemaining;
        private bool depleted;
        private int itemKey;
        public int ItemKey
        {
            get
            {
                return itemKey;
            }
            set
            {
                itemKey = value;
            }
        }
        public int DispRemaining
        {
            get
            {
                return dispRemaining;
            }
            set
            {
                dispRemaining = value;
            }
        }
        public bool Depleted
        {
            get
            {
                return depleted;
            }
            set
            {
                depleted = value;
            }
        }
        public ScoreTiles(int key, Rectangle newBox,Game1 game)
           : base(game)
        {
            this.active = true;
            this.itemKey = key;
            switch(key)
            {
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthGreen_01.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthGreen_02.png"));
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthGreen_03.png"));
                        break;
                    }               
                case 4:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthGreen_05.png"));
                        break;
                    }
                case 5:
                    {
                        this.depleted = true; 
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthRed_01.png"));
                        break;
                    }
                case 6:
                    {
                        this.depleted = true;
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthRed_02.png"));
                        break;
                    }
                case 7:
                    {
                        this.depleted = true;
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthRed_03.png"));
                        break;
                    }
                case 8:
                    {
                        this.depleted = true;
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ScoreTileImages/HealthRed_05.png"));
                        break;
                    }
            }           
            this.Box = newBox;
        }
        public void adjustHealth(int adjustAmount)
        {
            Rectangle newRect = new Rectangle(this.box.X, this.box.Y, this.box.Width - adjustAmount, this.box.Height);
            if(this.box.Width <= 0)
            {
                depleted = true;
            }
            this.box = newRect;
        }
    }
}
