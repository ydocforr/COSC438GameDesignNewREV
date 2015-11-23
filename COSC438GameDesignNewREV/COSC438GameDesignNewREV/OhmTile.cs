using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace COSC438GameDesignNewREV
{    
    public class OhmTile : Tile
    {
        private double ohmVal;
        private bool placed;
        public OhmTile(int key, Rectangle newBox, Game1 game)
            : base(game)
        {
            switch (key)
            {
                //Green Resistors
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/15OhmGreen.png"));
                        this.placed = false;
                        this.ohmVal = 15.0;
                        this.colour = 1;
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/150OhmGreen.png"));
                        this.placed = false;
                        this.ohmVal = 150.0;
                        this.colour = 1;
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/1500OhmGreen.png"));
                        this.placed = false;
                        this.ohmVal = 1500.0;
                        this.colour = 1;
                        break;
                    }
                case -3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/2.2OhmGreen.png"));
                        this.placed = false;
                        this.ohmVal = 2.2;
                        this.colour = 1;
                        break;
                    }
                //Red Resistors
                case 4:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/2.2OhmRed.png"));
                        this.placed = false;
                        this.ohmVal = 2.20;
                        this.colour = 2;
                        break;
                    }
                case 5:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/120OhmRed.png"));
                        this.placed = false;
                        this.ohmVal = 120.0;
                        this.Colour = 2;
                        break;
                    }
                case 6:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/240OhmRed.png"));
                        this.placed = false;
                        this.ohmVal = 240.0;
                        this.Colour = 2;
                        break;
                    }
                case 7:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/1500OhmRed.png"));
                        this.placed = false;
                        this.ohmVal = 1500.0;
                        this.Colour = 2;
                        break;
                    }
                //Yellow Resistors
                case 8:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/2.2OhmYellow.png"));
                        this.placed = false;
                        this.ohmVal = 2.20;
                        this.Colour = 3;
                        break;
                    }
                case 9:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/47OhmYellow.png"));
                        this.placed = false;
                        this.ohmVal = 47.0;
                        this.Colour = 3;
                        break;
                    }
                case 10:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/270OhmYellow.png"));
                        this.placed = false;
                        this.ohmVal = 270.0;
                        this.Colour = 3;
                        break;
                    }
                case 11:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/470OhmYellow.png"));
                        this.placed = false;
                        this.ohmVal = 470.0;
                        this.Colour = 3;
                        break;
                    }
            }
            this.Box = newBox;
        }
        public double Ohmval
        {
            get
            {
                return ohmVal;
            }
            set
            {
                ohmVal = value;
            }
        }
        public bool Placed
        {
            get
            {
                return placed;
            }
            set
            {
                placed = value;
            }
        }
    }
}
