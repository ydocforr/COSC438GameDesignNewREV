using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{
    public class InventoryDisplayTileItem : Tile
    {
        private int itemKey;
        public InventoryDisplayTileItem(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            this.itemKey = key;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InventoryLocationTile.png"));
            this.Box = newBox;
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
        public void ActivateGUIDisplay()
        {
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/MiningAxe.png"));            
        }
        public void UNActivateGUIDisplay()
        {
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InventoryLocationTile.png"));
        }
    }
}