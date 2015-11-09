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
        private int itemKey;
        private Texture2D bagImage;
        public ItemTile(int key, Rectangle newBox, String imgLoc, Game1 game)
           : base(game)
        {
            this.itemKey = key;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(imgLoc));          
            this.Box = newBox;
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
