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
    public enum CharDirection
    {
        Left,
        Right,
        Up,
        Down
    }
    public class Physics 
    {
        private bool[] eventStates;
        private bool jumpState = false;
        private bool climbState = false;
        private bool HASBAG = false;
        private bool interActiveProximity = false;
        private bool GreenState = false;
        private bool RedState = false;
        private bool YellowState = false;
        private bool flashLightView = false;
        //TWO Constants assume a 20x12 grid and a 40Pixel Character Width
        private const int yOffset = 40;
        private const int gravity = 1;
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
        private CharDirection characterFacing;
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
        private Texture2D axeAniBoxImage;
        private List<SplitSpikeTile> splitSpikes;
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
        public List<SplitSpikeTile> SplitSpikes
        {
            get
            {
                return splitSpikes;
            }
            set
            {
                splitSpikes = value;
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
            characterFacing = CharDirection.Right;
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
            if (!SweepCollisionVertical())
            {
                gameObj.currPlayerPositionFunc.Y += (int)velocity.Y;
               
            }
            if(!SweepCollisionHorizontal())
            {
                gameObj.currPlayerPositionFunc.X += (int)velocity.X;
            }
            //Ran off bottom of screen
            if (gameObj.currPlayerPositionFunc.Y >= graphics.PreferredBackBufferHeight)
            {
                gameObj.currPlayerPositionFunc.Y = graphics.PreferredBackBufferHeight;
                jumpState = false;
                velocity.Y = 0;
            }
            applyGravity();
            clock(1);
            adjustHealth();
        }
        public void applyGravity()
        {
            Rectangle newBox;
            if (velocity.Y < 10)
            {
                velocity.Y += .2f;
            }
            foreach(ItemTile tile in genMaps[ACTIVELEVEL].ItemTile)
            {
                //If the tile is in a current motion state then we need to apply gravity to the tile
                if(tile.MotionState)
                {
                    if (tile.YVelocity < 3)
                    {
                        tile.YVelocity += .2f;
                    }
                    tile.YVelocity += 2;
                    if (tile.XVelocity > 0)
                    {
                        tile.XVelocity -= .01f;
                    }
                    if(!ItemSweepCollision(tile))
                    {
                        Console.WriteLine("NO COL:");
                        newBox = new Rectangle((int)(tile.Box.X + tile.XVelocity), (int)(tile.Box.Y + tile.YVelocity), 40, 40);
                        tile.Box = newBox;
                    }
                    if (tile.Box.Y >= graphics.PreferredBackBufferHeight)
                    {
                        Console.WriteLine("Fell Off Bot");
                        newBox = new Rectangle((int)(tile.Box.X + tile.XVelocity), graphics.PreferredBackBufferHeight - 40, 40, 40);
                        tile.XVelocity = 0;
                        tile.YVelocity = 0;
                        tile.MotionState = false;
                        tile.Box = newBox;
                    }                                                                                                     
                }
            }
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
           // gameObj.STATICSPRITESFunc[99] = Tuple.Create(99, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/HealthRed.png")), new Rectangle(100 - healthOffset, 0, 0 +  healthOffset, 20));
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
                if ((checKeyBoardState.IsKeyDown(Keys.Space) || (checKeyBoardState.IsKeyDown(Keys.W)))  && jumpState == false )
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
                characterFacing = CharDirection.Right;
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5;
            }
            //Left Movement Controlled by A
            else if (checKeyBoardState.IsKeyDown(Keys.A) && ACTIVELEVEL != 8)
            {
                characterFacing = CharDirection.Left;
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
                    characterFacing = CharDirection.Up;
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
            //Check if we have the mining age euipt
            if (checKeyBoardState.IsKeyDown(Keys.T) && gameObj.checkInventoryItem(1))
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
            GUIINVUpdate();
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
                //checkFlashLight();
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
            int containerIdx = 0;
            for (int y = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count; y++)
            {
                if (genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 1)
                {
                    containerIdx = y;
                }
            }
            if (genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersect1))
                {
                    Console.WriteLine("testingcoll");             
                    ACTIVELEVEL = 6;
                    eventStates[1] = true;
                    aniBoxes.Clear();
                    dangerZoneEvent();
                }
                   
        }
        /*
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
        */
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
                if(currentGreen > 1.5 && currentGreen < 2.0 && tileCount[0] == 2)
                {                                        
                    //gameObj.STATICSPRITESFunc[91] =  Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 320, 40, 40));
                    GreenState = true;
                }
                if (currentRed > 1.5 && currentRed < 2.0 && tileCount[1] == 2)
                {
                    //gameObj.STATICSPRITESFunc[92] = Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 240, 40, 40));
                    RedState = true;
                }
                if (currentYellow > 1.5 && currentYellow < 2.0 && tileCount[2] == 2)
                {
                    //gameObj.STATICSPRITESFunc[93] = Tuple.Create(-3, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/MiniGameTiles/Check.png")), new Rectangle(600, graphics.PreferredBackBufferHeight - 160, 40, 40));
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
            if (splitSpikes.Count == 0)
            {
                eventStates[1] = true;
                ACTIVELEVEL = 1;
                loadLevel(1);
            }
            cordGen = new Random();
            int temp = cordGen.Next(1000);
            int count = 0;
            foreach (SplitSpikeTile spike in splitSpikes)
            {
                    spike.Falling = true;
                count++;
                Console.WriteLine(count);
            }
            foreach (SplitSpikeTile spike in splitSpikes)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }                  
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height));
        }  
        public void dangerZoneEvent()
        {
            cordGen = new Random();
            Console.WriteLine("Test");     
            splitSpikes = new List<SplitSpikeTile>();
            for (int x  = 0; x < 100; x++)
            {
                if (cordGen.Next(20) < 10)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(cordGen.Next(0, graphics.PreferredBackBufferWidth), 0, 20, 40), gameObj);
                    tempVar.XVelocity = 0;
                    tempVar.YVelocity = 4;
                    splitSpikes.Add(tempVar);
                }
                else
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(cordGen.Next(0, graphics.PreferredBackBufferWidth), 0, 20, 40), gameObj);
                    if (x % 2 == 0)
                    {
                        tempVar.XVelocity = 1;
                        tempVar.YVelocity = 4;
                    }
                    else
                    {
                        tempVar.XVelocity = -1;
                        tempVar.YVelocity = 4;
                    }
                    splitSpikes.Add(tempVar);
                }
            }       
        }
        public bool DangerZoneCollision( Rectangle PlayerLoc)
        {
            //Collision for platform tiles require movement from top collisions           
                foreach (SplitSpikeTile tile2 in splitSpikes.ToList())
                {
                    foreach (PlatFormTile ptile in genMaps[ACTIVELEVEL].PlatFormTile.ToList())
                    {                       
                        //If the platform box was currently shielding the player, remove that platform box as well as the spike tile.
                        //This forces the player to not stand under one box for the entire simulation
                        if (tile2.Box.Intersects(ptile.Box) && (gameObj.currPlayerPositionFunc.X >= ptile.Box.Left - 2 && gameObj.currPlayerPositionFunc.X <= ptile.Box.Right + 2))
                        {
                            genMaps[ACTIVELEVEL].PlatFormTile.Remove(ptile);
                            splitSpikes.Remove(tile2);
                        }
                        //Destroy spike tile on impact with playform box
                        if (tile2.Box.Intersects(ptile.Box))
                        {
                            splitSpikes.Remove(tile2);
                        }
                    }                  
                    if (tile2.Box.Intersects(PlayerLoc))
                    {
                        healthOffset += 20;
                        splitSpikes.Remove(tile2);
                    }                 
                    if(tile2.Box.Y > graphics.PreferredBackBufferHeight)
                    {
                        splitSpikes.Remove(tile2);
                    }              
            }
            Console.WriteLine(splitSpikes.Count);
            return false;
         }              
        public void throwItem()
        {
            Rectangle newBox;
            Console.WriteLine("Test");
            int containerIdx = 0;
            for(int y  = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count;y++)
            {
                if(genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 1)
                {
                    containerIdx = y;
                }
            }
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].MotionState = true;
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Equipt = false;
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Collected = false;
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
            newBox = new Rectangle((int)currPos.X,(int)currPos.Y, 40, 40);
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = (int)((targetPos.X - currPos.X) * .25);
            //Hard Cap
            if(genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity > 8)
            {
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = 8;
            }
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].YVelocity = (int)((targetPos.Y - currPos.Y)*.25);
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box = newBox;
            /*
            This needs to be updated to allow for throwing the object in any direction,
            Not a complicated fix, but still a timesink
            */
        }
        //FIN METHOD
        public void GUIINVUpdate()
        {
            List<int> activeIdx = new List<int>();
            bool tempFlag = false;
            //If our item is collected, we need to check make sure the gui displays it
            for (int x = 0; x < gameObj.InventoryContainer.Count; x++)
            {
                if (gameObj.InventoryContainer[x].Collected)
                {
                    activeIdx.Add(gameObj.InventoryContainer[x].ItemKey);
                }                                                                            
            }
            for (int y = 0; y < genMaps[ACTIVELEVEL].GuiTiles.Count; y++)
            {
                //Make sure its a gui display tile
                if (genMaps[ACTIVELEVEL].GuiTiles[y].GetType() == typeof(InventoryDisplayTileItem))
                {
                    //Check the valid index's for collected tiles
                    for (int x = 0; x < activeIdx.Count; x++)
                    {            
                        //Check all collected items key's     
                        if (((InventoryDisplayTileItem)genMaps[ACTIVELEVEL].GuiTiles[y]).ItemKey == activeIdx[x])
                        {
                            tempFlag = true;
                            ((InventoryDisplayTileItem)genMaps[ACTIVELEVEL].GuiTiles[y]).ActivateGUIDisplay();
                        }
                    }
                    //If we didnt find the gui tile in an active state we make sure not to draw the tile
                    if(!tempFlag)
                    {
                        ((InventoryDisplayTileItem)genMaps[ACTIVELEVEL].GuiTiles[y]).UNActivateGUIDisplay();
                    }
                }
            }
        }
        //Simple check to see if an item is equipt, this should be used before any item dependant event occurs
        public bool itemEquiptCheck()
        {
            for (int x = 0; x < gameObj.InventoryContainer.Count; x++)
            {              
                    if (gameObj.InventoryContainer[x].Equipt)
                    {
                        return true;
                    }         
            }
            return false;
        }
        public void checkItemUse()
        {
            if (checKeyBoardState.IsKeyDown(Keys.D1) && !itemEquiptCheck())
            {
                //Console.WriteLine("Mining Axe Equipt");
                equiptItem(1);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D2) && !itemEquiptCheck())
            {
                //Console.WriteLine("FlashLight Equipt");
                equiptItem(2);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D3) && !itemEquiptCheck() && gameObj.InventoryContainer[2].Collected)
            {
               // Console.WriteLine("Batteries Loaded");
                equiptItem(3);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D4) && !itemEquiptCheck())
            {
               // Console.WriteLine("MedKit consumed");
                equiptItem(4);
            }          
        }
        //Triggered by Keypress U, simply unequipts the equipted item and returns it to inventory
        public void unequiptItem()
        {            
                for (int x = 0; x < gameObj.InventoryContainer.Count; x++)
                {
                        if (gameObj.InventoryContainer[x].Equipt)
                        {
                            gameObj.InventoryContainer[x].Equipt = false;
                        }                                         
                }            
        }
        //When prompted by the user, equipt an item provided it exist within our inventory container
        public void equiptItem(int itemVal)
        {
            switch(itemVal)
            {                
                case 1:
                    {
                        if (gameObj.InventoryContainer[0].Collected && !gameObj.InventoryContainer[0].Consumed)
                        {
                            Console.WriteLine("Mining Axe Equipt");
                            gameObj.InventoryContainer[0].Equipt = true;
                            gameObj.InventoryContainer[0].ThrowAble = true;
                        }
                        break;
                    }
                case 2:
                    {
                        if (gameObj.InventoryContainer[2].Collected)
                        {
                            Console.WriteLine("Flash Light Equipt");
                            gameObj.InventoryContainer[2].Equipt = true;
                        }
                        break;
                    }
                case 3:
                    {
                        if (gameObj.InventoryContainer[3].Collected && !gameObj.InventoryContainer[3].Consumed)
                        {
                            Console.WriteLine("Batteries Consumed");
                            gameObj.InventoryContainer[3].Consumed = true;
                        }
                        break;
                    }
                case 4:
                    {
                        if (gameObj.InventoryContainer[4].Collected && !gameObj.InventoryContainer[4].Consumed)
                        {
                            Console.WriteLine("MedKit Equipt");
                            gameObj.InventoryContainer[4].Equipt = true;
                        }
                        break;
                    }
                    /*
                case 5:
                    {
                        if (gameObj.getINV.ContainsKey(8) && gameObj.getINV[8].Collected && !gameObj.getINV[8].Consumed)
                        {
                            gameObj.getINV[8].Equipt = true;
                        }
                        break;
                    }
                case 6:
                    {
                        if (gameObj.getINV.ContainsKey(9) && gameObj.getINV[9].Collected)
                        {
                            gameObj.getINV[9].Consumed = true;
                        }
                        break;
                    }
                    */
            }
        }
        /*
        Used to collision detection, the sweep aspect just means it will check various points along the movement based on x and y velocity
        ive left out the sweeping points for now as they like to disrupt some collision detections and offsets
         */
        public bool ItemSweepCollision(ItemTile itemCheck)
        {
            //Need to implement sweeping detection to fix collision graph offsets                       
            if (ItemCollisionDetection(itemCheck))
            {
                itemCheck.MotionState = false;
                return true;
            }
            
            return false;
        }
        public bool ItemCollisionDetection(ItemTile itemCheck)
        {
            Rectangle newBox;
            int checkX = itemCheck.Box.X + (int)itemCheck.XVelocity;
            int checkY = itemCheck.Box.Y + (int)itemCheck.YVelocity;
            ItemTile checkItemSweep = new ItemTile(0, new Rectangle(checkX, checkY, 20, 20), genMaps[ACTIVELEVEL].GetMap.Item1, gameObj);
            //Collision for platform tiles require movement from top collisions
            foreach (PlatFormTile tile in genMaps[ACTIVELEVEL].PlatFormTile)
            {
                //Top
                if (checkItemSweep.Box.Bottom >= tile.Box.Top && checkItemSweep.Box.Bottom <= tile.Box.Top  && checkItemSweep.Box.Right >= tile.Box.Left  && checkItemSweep.Box.Left <= tile.Box.Right )
                {
                    Console.WriteLine("Top Item Collision");
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Top - 1, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }   
                //Bottom
                if (checkItemSweep.Box.Top <= tile.Box.Bottom - yOffset && checkItemSweep.Box.Top >= tile.Box.Bottom  && checkItemSweep.Box.Right >= tile.Box.Left&& checkItemSweep.Box.Left <= tile.Box.Right)
                {
                    Console.WriteLine("Bot Item Collision");
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Bottom + itemCheck.Box.Height, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }                                              
                if (checkItemSweep.Box.Left >= tile.Box.Left && checkItemSweep.Box.Left <= tile.Box.Right && checkItemSweep.Box.Top <= tile.Box.Bottom  && checkItemSweep.Box.Bottom >= tile.Box.Top)
                {                  
                    newBox = new Rectangle(tile.Box.Right + 2, itemCheck.Box.Y, 40, 40);
                    itemCheck.Box = newBox;               
                    Console.WriteLine("Right Item Collision");                      
                    return true;
                }
                //Left side collisions with offsets
                if (checkItemSweep.Box.Right <= tile.Box.Right && checkItemSweep.Box.Right >= tile.Box.Left && checkItemSweep.Box.Top <= tile.Box.Bottom && checkItemSweep.Box.Bottom >= tile.Box.Top)
                {                  
                    Console.WriteLine("Left Item Collision");
                    newBox = new Rectangle(tile.Box.Left - 2 - tile.Box.Width, itemCheck.Box.Y, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }                                          
            }
            return false;
        }
        public bool SweepCollisionVertical()
        {
            //Need to implement sweeping detection to fix collision graph offsets              
            int checkX = gameObj.currPlayerPositionFunc.X;
            int checkY = gameObj.currPlayerPositionFunc.Y + (int)velocity.Y;                
            Rectangle checkPlayerNewState = new Rectangle(checkX, checkY - gameObj.ActivePlayer.Height + yOffset, 40, 40);       
            if (CollisionDetection(checkPlayerNewState))
            {
                    return true;
            }
            return false;     
        }
        public bool SweepCollisionHorizontal()
        {
            //Need to implement sweeping detection to fix collision graph offsets              
            int checkX = gameObj.currPlayerPositionFunc.X + (int)velocity.X;
            int checkY = gameObj.currPlayerPositionFunc.Y;                
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
            //Collision for platform tiles require movement from top collisions
            foreach (PlatFormTile tile in genMaps[ACTIVELEVEL].PlatFormTile)
            {
                //Right side collison with offsets at maximum velocity
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                  //  Console.WriteLine("Right Collision");    
                    gameObj.currPlayerPositionFunc.X = tile.Box.Right + 2;
                    return true;
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                   // Console.WriteLine("Left Collision");
                    gameObj.currPlayerPositionFunc.X = tile.Box.Left - 2 - gameObj.activePlayerFunc.Width;
                    return true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) + 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                   // Console.WriteLine("Top Collision");
                    jumpState = false;
                    velocity.Y = 0;
                    gameObj.currPlayerPositionFunc.Y = tile.Box.Top - 1;                                                                                                                       
                    return true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + gameObj.ActivePlayer.Height-YOffSet-2  && checkPlayerNewState.Top >= tile.Box.Bottom -1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                   // Console.WriteLine("Bot Collision");                       
                    velocity.Y = 1;
                    gameObj.currPlayerPositionFunc.Y = tile.Box.Bottom + gameObj.ActivePlayer.Height + 2;
                    return true;
                }
            }           
            foreach (ItemTile tile in genMaps[ACTIVELEVEL].ItemTile)
            {
                //Right side collison with offsets
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {                  
                        gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                        tile.Collected = true;                                                    
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
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
