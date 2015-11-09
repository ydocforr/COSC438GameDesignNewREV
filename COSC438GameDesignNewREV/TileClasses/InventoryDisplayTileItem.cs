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
            this.active = true;
            this.itemKey = key;
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/SlipperyTile.png"));
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
            switch (this.itemKey)
            {
                case 0:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/MiningAxe.png"));
                        break;
                    }
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/FlashLight.png"));
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/Battery.png"));
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/MedKitTile.png"));
                        break;
                    }
                case 4:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/ItemImages/MiningAxe.png"));
                        break;
                    }
            }        
        }
        public void UNActivateGUIDisplay()
        {
            this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InventoryLocationTile.png"));
        }
    }
}