using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
/*RESOURCES USED:::::::::::::::::::::::::::::::::::::::::::
http://www.xnadevelopment.com/tutorials/thewizardjumping/thewizardjumping.shtml
https://www.youtube.com/watch?v=ZLxIShw-7ac&list=PL667AC2BF84D85779&index=25
http://www.spikie.be/blog/page/Building-a-main-menu-and-loading-screens-in-XNA-Page-5.aspx
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
/*
    As of 10/3/2015
    TODO::
    1.)Fix many bugs
        -Collision Bugs, Character Stuck in some movements, character not properly stopping on some bounding box positions.
        -Before generating the spriteContainer check values against those contained in the inventory to eliminate multiple spawning items.
    2.)Implementations
        -Implement Character Sprite Switching functionality even if gameplay doesnt quite dictate it yet.
*/
namespace COSC438GameDesignNewREV
{
    public class Physics
    {
        private bool[] eventStates;
        private bool jumpState = false;
        private bool climbState = false;
        private bool HASBAG = false;
        private bool interActiveProximity = false;
        private bool clickLock = false;
        private bool GreenState = false;
        private bool RedState = false;
        private bool YellowState = false;
        private bool flashLightView = false;
        //TWO Constants assume a 20x12 grid and a 40Pixel Character Width
        private const int yOffset = 40;
        private const int MAPSIZE = 240;
        private const int ZONEOFFSET = 52;
        private float currCycle = 5;
        // private const int decayRate = .005f;
        //Useful Primitives
        private int characterNum;
        private bool IDOWN = false;
        private bool TDOWN = false;
        private int ACTIVELEVEL;
        private int healthOffset = 0;
        private int tick = 0;
        private int prevHealthOffset = 0;
        //MonoStuff & Objects    
        private Game1 gameObj;       
        private GridLayout[] genMaps;
        private MouseState checkMouseState;
        private MouseState checkPrevMouseState;
        private KeyboardState checKeyBoardState;
        private SpriteBatch sprites;
        private GraphicsDeviceManager graphics;
        private Vector2 velocity;
        private GameTime gameTime;
        private List<Rectangle> aniBoxes;
        private Random cordGen;
        private List<SpikeTile> spikes;
        //Getters/Setters  
        public bool FlashLightView
        {
            get
            {
                return flashLightView;
            }
            set
            {
                flashLightView = value;
            }
        }
        public int YOffSet
        {
            get
            {
                return yOffset;
            }
        }  
        public bool InterActiveProximity
        {
            get
            {
                return interActiveProximity;
            }
            set
            {
                interActiveProximity = value;
            }
        } 
        public bool[] EventStates
        {
            get
            {
                return eventStates;
            }
            set
            {
                eventStates = value;
            }
        }
        public List<SpikeTile> Spikes
        {
            get
            {
                return spikes;
            }
            set
            {
                spikes = value;
            }
        }
        public List<Rectangle> AniBoxes
        {
            get
            {
                return aniBoxes;
            }
            set
            {
                aniBoxes = value;
            }

        }
        public int ACTIVELEVELFunc
        {
            get
            {
                return ACTIVELEVEL;
            }
            set
            {
                ACTIVELEVEL = value;
            }
        }
        public bool IDOWNFunc
        {
            get
            {
                return IDOWN;
            }
        }
        public bool TDOWNFunc
        {
            get
            {
                return TDOWN;
            }
            set
            {
                TDOWN = value;
            }
        }
        //TODO Determine all parameters needed to spawn a physics class with functionality.
        public Physics(Game1 gameObj, int characterNum, SpriteBatch sprites, GraphicsDeviceManager graphics)
        {
            ACTIVELEVEL = 1;
            this.gameObj = gameObj;
            this.characterNum = characterNum;
            this.sprites = sprites;
            this.graphics = graphics;
            eventStates = new bool[5];
        }
        //Chaining method for most phyics processing
        public void ProcessInputFunctions(MouseState mouseState, KeyboardState keyBoardState, GameTime gameTime)
        {
            this.gameTime = gameTime;
            //capture mouse state
            this.checkMouseState = mouseState;
            //Process mouse state
            CheckMouseInput(checkMouseState);
            //Capture the keyboard state
            this.checKeyBoardState = keyBoardState;
            //Process keyboard state
            CheckKeyBoardInput();
            if (velocity.Y < 10)
            {
                velocity.Y += .2f;
            }
            if (!SweepCollision())
            {
                gameObj.currPlayerPositionFunc.Y += (int)velocity.Y;
                gameObj.currPlayerPositionFunc.X += (int)velocity.X;
            }
            clock(1);
            adjustHealth();
        }
        //Used for health bars and scoring
        public void clock(float timeCycle)
        {           
            var clock = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currCycle -= clock;
            if(currCycle <= 0)
            {
                currCycle = timeCycle;
                healthOffset += 1;              
            }          
        }
        public void adjustHealth()
        {
            gameObj.STATICSPRITESFunc[99] = Tuple.Create(99, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/HealthRed.png")), new Rectangle(200 - healthOffset, 0, 0 +  healthOffset, 40));
        }
        //Load new map using map matrix/grid
        public void loadLevel(int moveState)
        {
            /*
            Update characters starting position, when  movestate == 1 we are going up a level, when -1 we are coming down
            this determines which side of the screen we should start at.
            */          
            //Nullify velocity so character stands still upon entry to level
            velocity.X = 0;
            velocity.Y = 0;
            //Not jumping initially
            jumpState = false;
            //Update pointer to grid to ensure we dont use an outdated grid
            genMaps = gameObj.Grid;
            //Load relevant level
            prevHealthOffset = healthOffset;
            GreenState = false;
            YellowState = false;
            RedState = false;
            gameObj.STATICSPRITESFunc[91] = Tuple.Create(-5, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 320, 40, 40));
            gameObj.STATICSPRITESFunc[92] = Tuple.Create(-5, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 320, 40, 40));
            gameObj.STATICSPRITESFunc[93] = Tuple.Create(-5, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 320, 40, 40));
            switch (ACTIVELEVEL)
            {                
                case 0:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = 12;
                            gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = graphics.PreferredBackBufferWidth - 40;
                            gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        genMaps[0] = gameObj.Grid[0];
                        break;
                    }
                case 1:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = 12;
                            gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = graphics.PreferredBackBufferWidth - 40;
                            gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        if (eventStates[1])
                        {
                            genMaps[1] = gameObj.Grid[7];
                        }
                        else
                        {
                            genMaps[1] = gameObj.Grid[1];
                        }
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if(eventStates[1] && moveState == 1)
                        {                           
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[1] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
                case 2:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[3].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[3].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[2].X; 
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[2].Y;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                    
                        if (eventStates[2])
                        {
                            gameObj.Grid[2].generateGrid(9);
                            genMaps[2] = gameObj.Grid[2];
                        }
                        else
                        {
                            genMaps[2] = gameObj.Grid[2];
                        }
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if (eventStates[2] && moveState == 1)
                        {                            
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[2] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
                case 3:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[5].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[5].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[4].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[4].Y;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        genMaps[3] = gameObj.Grid[3];
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if (eventStates[3] && moveState == 1)
                        {                            
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[3] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
                case 4:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[7].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[7].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[6].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[6].Y;
                        }                   
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        genMaps[4] = gameObj.Grid[4];
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if (eventStates[4] && moveState == 1)
                        {                           
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[4] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
                case 6:
                    {
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        genMaps[6] = gameObj.Grid[6];
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if (eventStates[4] && moveState == 1)
                        {                           
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[4] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
                case 8:
                    {                       
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        aniBoxes = new List<Rectangle>();
                        gameObj.Grid[8].generateGrid(8);
                        genMaps[8] = gameObj.Grid[8];
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        if (eventStates[2] && moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (eventStates[2] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }
                        break;
                    }
            }
        }
        //TODO:: IMPLEMENT MOUSE INTERACTIONS
        //Process mouse location and possible clicks.
        public void CheckMouseInput(MouseState k)
        {
            MoveBlocks();      
        }
        public void MoveBlocks()
        {
            foreach (OhmTile tile in genMaps[ACTIVELEVEL].OhmTile)
            {
                if (checkMouseState.LeftButton == ButtonState.Pressed)
                {
                    Rectangle playerMouseBox = new Rectangle(checkMouseState.X, checkMouseState.Y, 10, 10);
                    if (tile.Box.Intersects((playerMouseBox)))
                    {
                        {
                            tile.Box = new Rectangle(checkMouseState.X - checkPrevMouseState.X, checkMouseState.Y - checkPrevMouseState.Y, tile.Box.Width, tile.Box.Height);
                        }
                    }
                }
            }          
        }
        //TODO Fix Bug's in JUMP
        public void Jump()
        {           
                if (checKeyBoardState.IsKeyDown(Keys.Space) && jumpState == false)
                {
                    gameObj.currPlayerPositionFunc.Y -= 10;
                    velocity.Y = -10f;
                    jumpState = true;
                }
                if (jumpState == true && !climbState)
                {
                    velocity.Y += .4f;
                }
                //Ran off bottom of screen
                if (gameObj.currPlayerPositionFunc.Y >= graphics.PreferredBackBufferHeight)
                {
                    gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                    jumpState = false;
                    velocity.Y = 0;
                }
                //Ran off top of screen
                if (gameObj.currPlayerPositionFunc.Y < 0)
                {
                    velocity.Y = 1f;
                }            
        }
        //All Keyboard input will be processed here
        public void CheckKeyBoardInput()
        {
            IDOWN = false;
            //End Game Case
            if (checKeyBoardState.IsKeyDown(Keys.Escape))
            {
                gameObj.Exit();
            }
            //Right Movement Controlled By D
            else if (checKeyBoardState.IsKeyDown(Keys.D)&& ACTIVELEVEL != 8)
            {
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5;
            }
            //Left Movement Controlled by A
            else if (checKeyBoardState.IsKeyDown(Keys.A) && ACTIVELEVEL != 8)
            {
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 5;
            }
            else
            {
                velocity.X = 0f;
            }
            if (checKeyBoardState.IsKeyDown(Keys.W))
            {

                if (ladderCollision(new Rectangle((int)(gameObj.currPlayerPositionFunc.X + velocity.X), (int)(gameObj.currPlayerPositionFunc.Y + velocity.Y - yOffset), 40, 40)))
                {
                    velocity.Y = -1;
                    gameObj.currPlayerPositionFunc.Y += (int)velocity.Y;
                    climbState = true;
                }          
            }
            climbState = false;
            //Checks for jump keypress
            Jump();
            //TODO: OPEN INVENTORY SPRITE SCREEN(LOAD TO FRAME)
            //TODO  Find a good way to implement this functionality
            if (checKeyBoardState.IsKeyDown(Keys.I) && HASBAG == true)
            {
                if (IDOWN)
                {
                    IDOWN = false;
                }
                else
                {
                    IDOWN = true;
                }
            }
            //Check to ensure that we have the throwable item equipt
            if (checKeyBoardState.IsKeyDown(Keys.T) && gameObj.getINV.ContainsKey(3) && gameObj.getINV[3].Equipt)
            {
                if (TDOWN)
                {
                    TDOWN = false;
                }
                else
                {
                    TDOWN = true;
                }
                throwItem();               
            }
            //Remove item from character and return to inventory bag
            if (checKeyBoardState.IsKeyDown(Keys.U))
            {
                unequiptItem();
            }
            if (checKeyBoardState.IsKeyDown(Keys.R))
            {
                restartLevel();
            }
            //M is for minigame interaction elements
            if (checKeyBoardState.IsKeyDown(Keys.M) && interActiveProximity && ACTIVELEVEL == 2 && !eventStates[2])
            {
                CraneGameSimulation();
            }
            //Checks for item key presses, these equipt items in inventory if applicable.
            checkItemUse();
            //Check if we need to load a lower level
            if (gameObj.currPlayerPositionFunc.X <= 0 && ACTIVELEVEL > -1 && ACTIVELEVEL < 6)
            {
                if (ACTIVELEVEL != 0)
                {
                    ACTIVELEVEL -= 1;
                    loadLevel(-1);                  
                }
            }
            //Check if we need to load a higher level
            if (gameObj.currPlayerPositionFunc.X >= graphics.PreferredBackBufferWidth && ACTIVELEVEL > -1 && ACTIVELEVEL < 6)
            {
                if (ACTIVELEVEL != 4)
                {
                    ACTIVELEVEL += 1;
                    loadLevel(1);
                }
            }
            //Flash light not accessible until after this stage.
            if (eventStates[2])
            {
                checkFlashLight();
            }
        }
        public void CraneGameSimulation()
        {
            ACTIVELEVEL = 8;
            eventStates[2] = true;
            loadLevel(1);
        }
        public void restartLevel()
        {
            healthOffset = prevHealthOffset;
            loadLevel(7);
        }
        //TODO:: INCREASE PRECISION OF WEAK POINT
        public void checkTrajectoryEvent1()
        {
            Rectangle intersect1 = new Rectangle(240, 200 - 40, 40, 40);
            for (int x = 0; x < aniBoxes.Count; x++)
            {              
                sprites.Draw(gameObj.getINV[3].Image, new Rectangle(aniBoxes[x].X, aniBoxes[x].Y, 40, 40), Color.White);
                if (aniBoxes[x].Intersects(intersect1))
                {                  
                    ACTIVELEVEL = 6;
                    eventStates[1] = true;
                    dangerZoneEvent();
                }
            }        
        }
        public void checkFlashLight()
        {
            if (gameObj.getINV.ContainsKey(8) && gameObj.getINV.ContainsKey(9))
            {
                if (gameObj.getINV[8].Equipt && gameObj.getINV[9].Consumed)
                {
                    FlashLightView = true;
                }
            }
        }
        public void scoreWires()
        {
            double resistanceGreen = 0.0;
            double resistanceRed = 0.0;
            double resistanceYellow = 0.0;
            double currentGreen = 0.0;
            double currentRed = 0.0;
            double currentYellow = 0.0;
            int[] tileCount = new int[3];
            foreach (OhmDropTile tile in genMaps[ACTIVELEVEL].OhmDropTile)
            {
                if(tile.Occupied)
                {
                    switch(tile.Colour)
                    {
                        case 1:
                            {
                                tileCount[0] += 1;
                                resistanceGreen += tile.HoldVal;
                                break;
                            }
                        case 2:
                            {
                                tileCount[1] += 1;
                                resistanceRed += tile.HoldVal;
                                break;
                            }
                        case 3:
                            {
                                tileCount[2] += 1;
                                resistanceYellow += tile.HoldVal;
                                break;
                            }
                    }
                }
                currentGreen = 240.0 / resistanceGreen;
                currentRed = 480.0 / resistanceRed;
                currentYellow = 600.0 / resistanceYellow;
                //Check true
                if(currentGreen > 1.5 && currentGreen < 2.0 && tileCount[0] == 2)
                {                                        
                    gameObj.STATICSPRITESFunc[91] =  Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 320, 40, 40));
                    GreenState = true;
                }
                if (currentRed > 1.5 && currentRed < 2.0 && tileCount[1] == 2)
                {
                    gameObj.STATICSPRITESFunc[92] = Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 240, 40, 40));
                    RedState = true;
                }
                if (currentYellow > 1.5 && currentYellow < 2.0 && tileCount[2] == 2)
                {
                    gameObj.STATICSPRITESFunc[93] = Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 160, 40, 40));
                    YellowState = true;
                }
            }
            if(GreenState && RedState && YellowState)
            {
                ACTIVELEVEL = 2;
                loadLevel(1);
            }
        }
        public void ohmCollisionDetection()
        {
            foreach(OhmDropTile tile in genMaps[ACTIVELEVEL].OhmDropTile)
            {
                foreach(OhmTile tile2 in genMaps[ACTIVELEVEL].OhmTile)
                {
                    if (tile.Box.Intersects(tile2.Box))
                    {

                        if(tile.Colour == tile2.Colour)
                        {
                            tile2.Box = tile.Box;
                            tile2.Placed = true;
                            tile.Occupied = true;
                            tile.HoldVal = tile2.Ohmval;
                        }
                    }
                }
            }
        }
        public void dangerZoneSimulation()
        {
            Console.WriteLine(spikes.Count);
            if(spikes.Count == 0)
            {
                eventStates[1] = true;
                ACTIVELEVEL = 1;
                loadLevel(1);
            }
            cordGen = new Random();           
            int temp = cordGen.Next(1000 - (tick * 19));
            if (temp < spikes.Count && !spikes[temp].Falling)
            {
                tick++;
                spikes[temp].Falling = true;
            }  
            for(int x = 0; x < spikes.Count; x++)
            {
                Rectangle newLoc = new Rectangle(spikes[x].Box.X, spikes[x].Box.Y + 10,spikes[x].Box.Width, spikes[x].Box.Height);
                sprites.Draw(spikes[x].Image, spikes[x].Box, Color.White);
                if (spikes[x].Falling)
                {
                    spikes[x].Box = newLoc;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height));
        }  
        public void dangerZoneEvent()
        {
            cordGen = new Random();
            spikes = new List<SpikeTile>();          
            for (int x  = 0; x < 50; x++)
            {        
                spikes.Add(new SpikeTile(x, new Rectangle(cordGen.Next(0, graphics.PreferredBackBufferWidth), 0, 40, 40), gameObj));
            }            
        }
        public bool DangerZoneCollision( Rectangle PlayerLoc)
        {
            //Collision for platform tiles require movement from top collisions
            foreach (SpikeTile tile in spikes.ToList())
            {
                foreach(PlatForm ptile in genMaps[ACTIVELEVEL].PlatFormTile.ToList())
                {                                 
                    if(tile.Box.Intersects(ptile.Box) && (gameObj.currPlayerPositionFunc.X >= ptile.Box.Left - 5 && gameObj.currPlayerPositionFunc.X + 40 <= ptile.Box.Right + 5))
                    {
                        genMaps[ACTIVELEVEL].PlatFormTile.Remove(ptile);
                        spikes.Remove(tile);
                    }
                    if (tile.Box.Intersects(ptile.Box))
                    {
                        spikes.Remove(tile);
                    }
                }
                if(tile.Box.Intersects(PlayerLoc))
                {
                    healthOffset += 20;
                    spikes.Remove(tile);
                }
                if(tile.Box.Y > graphics.PreferredBackBufferHeight)
                {
                    spikes.Remove(tile);
                }
            }
            return false;
         }       
        //TODO IMPLEMENT DIRECTONAL CASES BASED OFF PLAYER POSITION AND MOUSE OFFSETS
        public void throwItem()
        {
            gameObj.getINV[3].Equipt = false;
            gameObj.getINV[3].Consumed = true;  
            Vector2 currPos = new Vector2();
            Vector2 targetPos = new Vector2();
            /*                
            Current position of item is the players location with standard offsets  
            */             
            currPos.X = gameObj.currPlayerPositionFunc.X + gameObj.ActivePlayer.Width;
            currPos.Y = gameObj.currPlayerPositionFunc.Y - gameObj.ActivePlayer.Height;
            /*
            Target position is the location of the mouse cursor, obviously before launch this will need to be updated with a decay rate for velocity of x and y
            but for now this will suffice for general demonstration
            */
            targetPos.X = checkMouseState.X;
            targetPos.Y = checkMouseState.Y - 20;          
            int rise = (int)(targetPos.Y - currPos.Y);
            int run = (int)(targetPos.X - currPos.X);
            //Scaled rise/run allows for ten animations      
            int scaledRise = (int)(-rise * .2);
            int scaledRun = (int)(run * .2);
            //Xmove and Ymove need to be a factor of rise/ run
            int xMove = scaledRun;
            int yMove = -scaledRise;
            /*
            This needs to be updated to allow for throwing the object in any direction,
            Not a complicated fix, but still a timesink
            */
            while (currPos.X < targetPos.X && currPos.Y > targetPos.Y)
            {
                aniBoxes.Add(new Rectangle((int)currPos.X, (int)currPos.Y, 40, 40));
                currPos.X += xMove;
                currPos.Y += yMove;
            }           
        }
        //When a throwable item is equipted, this method will display the path based on the current velocity.
        //TODO IMPLEMENT THIS FUNCTIONALITY.
        public void projectTrajectory()
        {
        }
        //Simple check to see if an item is equipt, this should be used before any item dependant event occurs
        public bool itemEquiptCheck()
        {
            for (int x = 0; x < 10; x++)
            {
                if (gameObj.getINV.ContainsKey(x))
                {
                    if (gameObj.getINV[x].Equipt)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //IMPORTANT:: Map generation dictates that items start at number 3 and grow from there.
        public void checkItemUse()
        {
            if (checKeyBoardState.IsKeyDown(Keys.D1) && !itemEquiptCheck())
            {
                Console.WriteLine("Mining Axe Equipt");
                equiptItem(3);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D2) && !itemEquiptCheck())
            {
                Console.WriteLine("Item 2 Equipted");
                equiptItem(5);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D3) && !itemEquiptCheck())
            {
                Console.WriteLine("Item 3 Equipted");
                equiptItem(6);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D4) && !itemEquiptCheck())
            {
                Console.WriteLine("Health Pack Equipt");
                equiptItem(7);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D6) && !itemEquiptCheck())
            {
                Console.WriteLine("Flash light equipt");
                equiptItem(8);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D7) && gameObj.getINV[8].Collected)
            {
                Console.WriteLine("Battery Consumed");
                equiptItem(9);
            }
        }
        //Triggered by Keypress U, simply unequipts the equipted item and returns it to inventory
        public void unequiptItem()
        {            
                for (int x = 0; x < 10; x++)
                {
                    if (gameObj.getINV.ContainsKey(x))
                    {
                        if (gameObj.getINV[x].Equipt)
                        {
                            gameObj.getINV[x].Equipt = false;
                        }
                    }
                }            
        }
        //When prompted by the user, equipt an item provided it exist within our inventory container
        public void equiptItem(int itemVal)
        {
            switch(itemVal)
            {                
                case 3:
                    {
                        if (gameObj.getINV.ContainsKey(3) && gameObj.getINV[3].Collected && !gameObj.getINV[3].Consumed)
                        {
                            Console.WriteLine("Mining Axe Equipt");
                            gameObj.getINV[3].Equipt = true;
                            gameObj.getINV[3].ThrowAble = true;
                        }
                        break;
                    }
                case 5:
                    {
                        if (gameObj.getINV.ContainsKey(5) && gameObj.getINV[5].Collected && !gameObj.getINV[5].Consumed)
                        {
                            Console.WriteLine("I1 Equipt");
                            gameObj.getINV[5].Equipt = true;
                        }
                        break;
                    }
                case 6:
                    {
                        if (gameObj.getINV.ContainsKey(6) && gameObj.getINV[6].Collected && !gameObj.getINV[6].Consumed)
                        {
                            Console.WriteLine("I1 Equipt");
                            gameObj.getINV[6].Equipt = true;
                        }
                        break;
                    }
                case 7:
                    {
                        if (gameObj.getINV.ContainsKey(7) && gameObj.getINV[7].Collected && !gameObj.getINV[7].Consumed)
                        { 
                            gameObj.getINV[7].Equipt = true;                                                
                            healthOffset = 0;
                        }
                        break;
                    }
                case 8:
                    {
                        if (gameObj.getINV.ContainsKey(8) && gameObj.getINV[8].Collected && !gameObj.getINV[8].Consumed)
                        {
                            gameObj.getINV[8].Equipt = true;
                        }
                        break;
                    }
                case 9:
                    {
                        if (gameObj.getINV.ContainsKey(9) && gameObj.getINV[9].Collected)
                        {
                            gameObj.getINV[9].Consumed = true;
                        }
                        break;
                    }
            }
        }
        /*
        Used to collision detection, the sweep aspect just means it will check various points along the movement based on x and y velocity
        ive left out the sweeping points for now as they like to disrupt some collision detections and offsets
         */
        public bool SweepCollision()
        {           
            //Need to implement sweeping detection to fix collision graph offsets              
            int checkX = gameObj.currPlayerPositionFunc.X + (int)velocity.X;
            int checkY = gameObj.currPlayerPositionFunc.Y + (int)velocity.Y;                
            Rectangle checkPlayerNewState = new Rectangle(checkX, checkY - gameObj.ActivePlayer.Height + yOffset, 40, 40);       
                if (CollisionDetection(checkPlayerNewState))
                {
                    return true;
                }
            return false;     
        }
        //Standard game collision detection algorithm, citation at top of file.
        public bool CollisionDetection(Rectangle checkPlayerNewState)
        {
            //Interactable Item for level 2 (Crane)
            if(checkPlayerNewState.Intersects(gameObj.STATICSPRITESFunc[97].Item3) && ACTIVELEVEL == 2)
            {
                interActiveProximity = true;
            }
            else
            {
                interActiveProximity = false;
            }
            //Collision for platform tiles require movement from top collisions
            foreach (PlatForm tile in genMaps[ACTIVELEVEL].PlatFormTile)
            {
                //Right side collison with offsets at maximum velocity
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                    //Console.WriteLine("Right Collision");
                    return true;
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                    //Console.WriteLine("Left Collision");
                    return true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top  && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2 + 10) && checkPlayerNewState.Right >= tile.Box.Left  && checkPlayerNewState.Left <= tile.Box.Right + 1)
                {
                    jumpState = false;
                    velocity.Y = 0;
                    //Console.WriteLine("Top Collision");                                                                                                                           
                    return true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + (gameObj.ActivePlayer.Height / 2 - 10 ) && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left  && checkPlayerNewState.Left <= tile.Box.Right + 1)
                {
                    //Console.WriteLine("Bot Collision");                       
                    velocity.Y = 1;
                    return true;
                }
            }
            foreach (CollisionTile tile in genMaps[ACTIVELEVEL].CollisionTile)
            {
                //Right side collison with offsets
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {
                    //Console.WriteLine("Right Collision");
                    return true;
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {
                    //Console.WriteLine("Left Collision");
                    return true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    jumpState = false;
                    velocity.Y = 0;
                    //Console.WriteLine("Top Collision");
                    return true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    //Console.WriteLine("Bot Collision");
                    velocity.Y = 1;
                    return true;
                }
            }
            foreach (ItemTile tile in genMaps[ACTIVELEVEL].ItemTile)
            {
                //Right side collison with offsets
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {
                    if (HASBAG)
                    {
                        gameObj.SetInv(tile.ItemKey, tile);
                        tile.Collected = true;
                    }
                    if (tile.ItemKey == 4)
                    {
                        HASBAG = true;
                        tile.Consumed = true;
                    }                   
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {
                    if (HASBAG)
                    {
                        gameObj.SetInv(tile.ItemKey, tile);
                        tile.Collected = true;
                    }
                    if (tile.ItemKey == 4)
                    {
                        HASBAG = true;
                        tile.Consumed = true;
                    }                  
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    if (HASBAG)
                    {
                        gameObj.SetInv(tile.ItemKey, tile);
                        tile.Collected = true;
                    }
                    if (tile.ItemKey == 4)
                    {
                        HASBAG = true;
                        tile.Consumed = true;
                    }                   
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    if (HASBAG)
                    {
                        gameObj.SetInv(tile.ItemKey, tile);
                        tile.Collected = true;
                    }
                    if (tile.ItemKey == 4)
                    {
                        HASBAG = true;
                        tile.Consumed = true;
                    }
                }
            }          
            return false;
        }
        public bool ladderCollision(Rectangle player)
        {
            foreach (LadderTile tile in genMaps[ACTIVELEVEL].LadderTile)
            {
                if(tile.Box.Intersects(player))
                {
                    return true;
                }                  
            }
            return false;
        }
    }
}
