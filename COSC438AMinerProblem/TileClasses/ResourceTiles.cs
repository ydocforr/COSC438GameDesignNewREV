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
        //Important item properties;
        private int itemKey;
        private int initHeight = 120;
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
        public ResourceTiles(int key, Rectangle newBox, Game1 game)
           : base(game)
        {        
            this.active = true;
            this.itemKey = key;       
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ResourceImages/BurstBar.png"));                                             
            this.Box = newBox;
        }
        public void adjustResource(int adjustAmount)
        {
            Rectangle newRect = new Rectangle(this.box.X, this.box.Y, adjustAmount, this.box.Height);
            this.box = newRect;            
        }
    }
}
