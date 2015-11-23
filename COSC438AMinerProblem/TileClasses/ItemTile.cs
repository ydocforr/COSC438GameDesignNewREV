using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class ItemTile : Tile
    {
        //Important item properties
        private bool collected;
        private bool consumed;
        private bool equipt;
        private bool throwAble;
        private float yVelocity;
        private float xVelocity;
        private bool motionState;
        private Point returnState;
        private int itemKey;
        private Texture2D bagImage;
        public ItemTile(int key, Rectangle newBox, String imgLoc, Game1 game)
           : base(game)
        {
            this.returnState = new Point(newBox.X, newBox.Y);
            this.itemKey = key;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(imgLoc));          
            this.Box = newBox;
        }
        int CompareTo(ItemTile check)
        {
            if (this.itemKey > check.itemKey)
            {
                return -1;
            }
            return 1;
        }
        public Point ReturnState
        {
            get
            {
                return returnState;
            }
            set
            {
                returnState = value;
            }
        }
        public float XVelocity
        {
            get
            { 
                return xVelocity;
            }
            set
            {
                xVelocity = value;
            }
        }
        public float YVelocity
        {
            get
            {
                return yVelocity;
            }
            set
            {
                yVelocity = value;
            }
        }
        public bool MotionState
        {
            get
            {
                return motionState;
            }
            set
            {
                motionState = value;
            }
        }

        public bool Collected
        {
            get
            {
                return collected;
            }
            set
            {
                collected = value;
            }
        }
        public bool Consumed
        {
            get
            {
                return consumed;
            }
            set
            {
                consumed = value;
            }
        }
        public bool ThrowAble
        {
            get
            {
                return throwAble;
            }
            set
            {
                throwAble = value;
            }
        }
        public bool Equipt
        {
            get
            {
                return equipt;
            }
            set
            {
                equipt = value;
            }
        }
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
    }
}
