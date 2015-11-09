using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class ResourceTiles : Tile
    {
        //Important item properties
        private int dispRemaining;
        private bool depleted;
        private bool full;
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
        public bool Full
        {
            get
            {
                return full;
            }
            set
            {
                full = value;
            }
        }
        public ResourceTiles(int key, Rectangle newBox, Game1 game)
           : base(game)
        {
            this.active = true;
            this.itemKey = key;
            switch (key)
            {
                case 0:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ResourceImages/BurstBar_01.png"));
                        break;
                    }
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ResourceImages/BurstBar_02.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ResourceImages/BurstBar_03.png"));
                        break;
                    }              
            }
            this.Box = newBox;
        }
        public void adjustResource(int adjustAmount)
        {
            Rectangle newRect = new Rectangle(this.box.X, this.box.Y, this.box.Width, this.box.Height - adjustAmount);
            Console.WriteLine("Rect Height" + this.box.Height);
            if (this.box.Height <= 0)
            {              
                depleted = true;
                Console.WriteLine("Depleted");
            }                     
            this.box = newRect;            
        }
    }
}
