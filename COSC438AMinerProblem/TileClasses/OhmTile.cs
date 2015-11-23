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
        private bool inTransit;
        private Point returnState;
        public OhmTile(int key, Rectangle newBox, Game1 game, int retX, int retY)
            : base(game)
        {
            switch (key)
            {
                //Green Resistors
                case 1:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 15.000;
                        this.colour = 1;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 2:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 150.000;
                        this.colour = 1;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 1500.000;
                        this.colour = 1;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case -3:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 2.200;
                        this.colour = 1;
                        returnState = new Point(retX, retY);
                        break;
                    }
                //Red Resistors
                case 4:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 2.2000;
                        this.colour = 2;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 5:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 120.000;
                        this.Colour = 2;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 6:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 240.000;
                        this.Colour = 2;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 7:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 1500.000;
                        this.Colour = 2;
                        returnState = new Point(retX, retY);
                        break;
                    }
                //Yellow Resistors
                case 8:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 2.2000;
                        this.Colour = 3;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 9:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 47.000;
                        this.Colour = 3;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 10:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 270.000;
                        this.Colour = 3;
                        returnState = new Point(retX, retY);
                        break;
                    }
                case 11:
                    {
                        this.image = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GeneralTileImages/Resistor.png"));
                        this.placed = false;
                        this.ohmVal = 470.000;
                        this.Colour = 3;
                        returnState = new Point(retX, retY);
                        break;
                    }
            }
            this.Box = newBox;
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
        public bool InTransit
        {
            get
            {
                return inTransit;
            }
            set
            {
                inTransit = value;
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
