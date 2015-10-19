using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace COSC438GameDesignNewREV
{
    public class MapContainer
    {
        public Dictionary<int, Tuple<int, Texture2D, Rectangle>> STATICSPRITES;
        public Dictionary<int, Tuple<int, Texture2D, Rectangle>> LevelSTATICSPRITES;
        GraphicsDevice gd;
        GraphicsDeviceManager gdm;
        public MapContainer(Game1 game)
        {
            gd = game.GraphicsDevice;
            gdm = game.graphicsFunc;
            STATICSPRITES = new Dictionary<int, Tuple<int, Texture2D, Rectangle>>();            
            //IMPORTANT:: load sprites in in the order you want to be displayed if they overlap. NOTE:: Key value maps to order of loading, First INT value in tuple is for knowing the prpose of each sprite
            //I.E Sprite Containing tuple value 0 should map to level zero, all static elements that will be drawn regardless of map will be given value -1.
            STATICSPRITES.Add(0, Tuple.Create(-1, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/background.jpg")), new Rectangle(0, 0, 800, 480)));
            STATICSPRITES.Add(1, Tuple.Create(-1, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/Up.png")), new Rectangle(gdm.PreferredBackBufferWidth - 10, gdm.PreferredBackBufferHeight - 100, 10, 100)));
            STATICSPRITES.Add(2, Tuple.Create(-1, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/Down.png")), new Rectangle(0, gdm.PreferredBackBufferHeight - 100, 10, 100)));
            STATICSPRITES.Add(3, Tuple.Create(-1, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/CaveTop.png")), new Rectangle(0, gdm.PreferredBackBufferHeight - 100, 10, 100)));
            STATICSPRITES.Add(4, Tuple.Create(0, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/LVL0.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(5, Tuple.Create(1, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/Lvl1.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(6, Tuple.Create(2, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/LvL2.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(7, Tuple.Create(3, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/LVL3.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(8, Tuple.Create(4, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/LVL4.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(9, Tuple.Create(5, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/LVL5.png")), new Rectangle((gdm.PreferredBackBufferWidth / 2) - 100, 0, 200, 80)));
            STATICSPRITES.Add(10, Tuple.Create(7, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/HealthGreen.png")), new Rectangle(0, 0, 200, 40)));
            STATICSPRITES.Add(11, Tuple.Create(6, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/HealthRed.png")), new Rectangle(0, 0, 0, 40)));
            STATICSPRITES.Add(12, Tuple.Create(-2, Texture2D.FromStream(gd, TitleContainer.OpenStream(@"Images/Crane.png")), new Rectangle(160, gdm.PreferredBackBufferHeight - 160, 80, 160)));
            game.STATICSPRITESFunc = STATICSPRITES;
        }
        /*
        NOTE playerStartLocs are used for eventBased coordinates, after event 1 occurs you need to adjust starting and ending locations based off the terrain changes.
        NOTE Each Coordinate pair is for a given level
        I.E 0,1 are for LVL 1; 2,3 are for LVL 2; 3,4 are for LVL 3 ETC
        */
        public Point[] playerStartLocs ={new Point(770,359),new Point(10,369),new Point(770,359),new Point(10,439),new Point(770,369), new Point(10,399)};
        //Used for drawing inventory items on the map and in our bag
        public String[] inventoryONMAP = {"","","",@"Images/I1.png" , @"Images/I2.png" , @"Images/I3.png" , @"Images/I4.png" , @"Images/FirstAid.png" , @"Images/I6.png" ,
            @"Images/I7.png" , @"Images/I8.png" , @"Images/I9.png" ,@"Images/I10.png"};
        public String[] inventoryINBAG = {"","","",@"Images/I1BAG.png" , @"Images/I2BAG.png" , @"Images/I3.png" , @"Images/I4.png" , @"Images/FirstAidINBag.png" , @"Images/I6.png" ,
            @"Images/I7.png" , @"Images/I8.png" , @"Images/I9.png" ,@"Images/I10.png"};
        //Positional coordinates for where each item belongs in the bag
        public Point[] bagLocs = {new Point(0, 0),new Point(0, 0),new Point(0, 0),new Point(400, 320) , new Point(480, 320), new Point(560, 320), new Point(640, 320), new Point(720, 320) , new Point(400,240),
            new Point(480, 240), new Point(560, 240), new Point(640, 240), new Point(720, 240)
        };
        //Map matricies for all levels
        public int[,] levelZero = new int[,]
                          {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelOne = new int[,]
                          {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,3,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0},
                           { 0,2,2,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,4,0,0,2,2,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,2,2,0,0,0,2,2,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,2,2,0,0,0,0,2,2,0,0,0,0,0,0,0}};
        public int[,] levelOnePostEvent = new int[,]
                          {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,2,2,2,2,2,2,2,2,0,0,0,0,0,0},
                           { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0},
                           { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0}};
        public int[,] levelTwo = new int[,]
                          {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,1,1,0,0,2,2,0,0,0,0,0,-1,0,0},
                           { 2,2,2,2,2,2,1,1,0,0,2,2,2,2,2,2,2,-1,2,2}};
        public int[,] levelTwoPostEvent = new int[,]
                         {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,-1,0,0},
                           { 0,0,0,0,0,0,1,1,1,1,2,2,0,0,0,0,0,-1,0,0},
                           { 2,2,2,2,2,2,1,1,1,1,2,2,2,2,2,2,2,-1,2,2}};
        public int[,] levelThree = new int[,]
                        {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,2,2,0,0,0,0,0,0,7,0,-2,-2,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,2,2,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0},
                           { 2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelThreePostEvent = new int[,]
                        {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelFour = new int[,]
                           {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelFourPostEvent = new int[,]
                           {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelFive = new int[,]
                           {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        public int[,] levelFivePostEvent = new int[,]
                           {{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                           { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
    }
}
