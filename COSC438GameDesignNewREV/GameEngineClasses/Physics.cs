using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using System.Threading;
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
    public enum ActivePlayer
    {
        Miner,
        Engineer,
        Medic,
        Foreman
    }
=======
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
        private bool deadCheck;
        //TWO Constants assume a 20x12 grid and a 40Pixel Character Width
        private const int yOffset = 40;
        private const int gravity = 1;
        private const int MAPSIZE = 240;
        private const int ZONEOFFSET = 52;
        private float currCycle = 10;
        private float resourceOffset;
        // private const int decayRate = .005f;
        //Useful Primitives
        private bool burstAvaliable = true;
        private int burstExhausted;
        private int characterNum;
        private bool IDOWN = false;
        private bool TDOWN = false;
        private int ACTIVELEVEL;
        private int healthOffset = 0;
        private int prevHealthOffset = 0;
        //MonoStuff & Objects    
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        private KeyboardState prevKEYState;
        private CharDirection characterFacing;
        private ActivePlayer activePlayer;
=======
        private CharDirection characterFacing;
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        List<SplitSpikeTile> splitSpikes1;
        List<SplitSpikeTile> splitSpikes2;
        List<SplitSpikeTile> splitSpikes3;
        List<SplitSpikeTile> splitSpikes4;
        List<SplitSpikeTile> splitSpikes5;
        List<SplitSpikeTile> splitSpikes6;
=======
        private Texture2D axeAniBoxImage;
        private List<SplitSpikeTile> splitSpikes;
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        }       
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            activePlayer = ActivePlayer.Miner;
=======
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
            prevKEYState = keyBoardState;
            //capture mouse state
            this.checkMouseState = mouseState;
            //Process mouse state
            CheckMouseInput(checkMouseState);
            //Capture the keyboard state
            this.checKeyBoardState = keyBoardState;
            //Process keyboard state
            CheckKeyBoardInput();
            if (!SweepCollisionVertical())
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
            if (gameObj.GetGameState == GameState.Playing)
            {
                applyGravity();
                clock(1);
                adjustHealth();
                //Burst resource only exist in lvl 1 event aka lvl 6
                if (ACTIVELEVEL == 6)
                {
                    //burstClock(2);
                }   
                if (ACTIVELEVEL == 2)
                {
                    interactiveDetection(gameObj.PlayerBox);
                }           
            }
        }
        public void applyGravity()
        {
            Rectangle newBox;
            if (velocity.Y < 10)
=======
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            {
                gameObj.currPlayerPositionFunc.Y += (int)velocity.Y;
               
            }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
                        newBox = new Rectangle((int)(tile.Box.X + tile.XVelocity), (int)(tile.Box.Y + tile.YVelocity), 40, 40);
                        tile.Box = newBox;
                    }
                    if (tile.Box.Y >= graphics.PreferredBackBufferHeight)
                    {
                        newBox = new Rectangle((int)(tile.Box.X + tile.XVelocity), graphics.PreferredBackBufferHeight - 40, 40, 40);
                        tile.XVelocity = 0;
                        tile.YVelocity = 0;
                        tile.MotionState = false;
                        tile.Box = newBox;
                    }                                                                                                     
                }
            }
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
        /*
        public void burstClock(float timeCycle)
        {
            var clock = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currCycle -= clock;
            if (currCycle <= 0)
            {
                currCycle = timeCycle;
                burstAvaliable = true;
            }
        }
        */
        public void adjustHealth()
        {
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            int maxIdx = 0;           
            for (int y = 0; y < genMaps[ACTIVELEVEL].GuiTiles.Count; y++)
            {              
                //Make sure its a gui display tile
                if (genMaps[ACTIVELEVEL].GuiTiles[y].GetType() == typeof(ScoreTiles) && ((ScoreTiles)genMaps[ACTIVELEVEL].GuiTiles[y]).ItemKey <= 5)
                {
                    //Since healthbar tile keys are left to right least to greatest, we want to scan through and find the highest healthbar tile that is not already depleted.
                    if( (!((ScoreTiles)genMaps[ACTIVELEVEL].GuiTiles[y]).Depleted) )  
                     {
                        maxIdx = y;
                     }                                                                         
                }                               
            }
            if(maxIdx == 0)
            {              
                deadCheck = true;
                gameObj.GetGameState = GameState.Dead;
            }
            ((ScoreTiles)genMaps[ACTIVELEVEL].GuiTiles[maxIdx]).adjustHealth(healthOffset);
            healthOffset = 0;
        }     
=======
           // gameObj.STATICSPRITESFunc[99] = Tuple.Create(99, Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/HealthRed.png")), new Rectangle(100 - healthOffset, 0, 0 +  healthOffset, 20));
        }
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        //Load new map using map matrix/grid
        public void loadLevel(int moveState)
        {
            gameObj.GetGameState = GameState.Loading;
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[5].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[5].Y;
                        }
                        else if (eventStates[1] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[4].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[4].Y;
                        }
                        break;
                    }
                case 2:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[7].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[7].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[6].X; 
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[6].Y;
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[7].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[7].Y;
                        }
                        else if (eventStates[2] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[6].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[6].Y;
                        }
                        break;
                    }
                case 3:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[9].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[9].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[8].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[8].Y;
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[9].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[9].Y;
                        }
                        else if (eventStates[3] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[8].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[8].Y;
                        }
                        break;
                    }
                case 4:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[11].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[11].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[10].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[10].Y;
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[11].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[11].Y;
                        }
                        else if (eventStates[4] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[4].GetMap.playerStartLocs[10].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[4].GetMap.playerStartLocs[10].Y;
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[13].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[13].Y;
                        }
                        else if (eventStates[4] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[12].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[12].Y;
                        }
                        break;
                    }
                case 7:
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
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[15].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[15].Y;
                        }
                        else if (eventStates[2] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[14].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[14].Y;
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
            else if (checKeyBoardState.IsKeyDown(Keys.B) && ACTIVELEVEL == 6)
            {               
                    if (characterFacing == CharDirection.Right)
                    {
                        velocity.X = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 4;
                    }
                    if (characterFacing == CharDirection.Left)
                    {
                        velocity.X = -(float)(gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 4;
                    }               
            }
            //Right Movement Controlled By D
            else if (checKeyBoardState.IsKeyDown(Keys.D))
            {
                characterFacing = CharDirection.Right;
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (ACTIVELEVEL == 6 && activePlayer == ActivePlayer.Miner)
                {
                    velocity.X = (float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 1.25);
                }
                else
                {
                    velocity.X = (float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5));
                }
=======
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5;
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            }
            //Left Movement Controlled by A
            else if (checKeyBoardState.IsKeyDown(Keys.A))
            {
                characterFacing = CharDirection.Left;
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (ACTIVELEVEL == 6 && activePlayer == ActivePlayer.Miner)
                {
                    velocity.X = -(float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 1.25);
                }
                else
                {
                    velocity.X = -(float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5));
                }
            }           
=======
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 5;
            }
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            else
            {
                if (!iceCollision(gameObj.PlayerBox))
                {
                    velocity.X = 0f;
                }
                else
                {
                    if (activePlayer == ActivePlayer.Miner)
                    {
                        velocity.X = velocity.X -= .5f;
                    }
                    else if (activePlayer == ActivePlayer.Engineer)
                    {
                        velocity.X = velocity.X -= .9f;
                    }
                }
                if(velocity.X < 0)
                {
                    velocity.X = 0;
                }
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            if (checKeyBoardState.IsKeyDown(Keys.T) && gameObj.checkInventoryItem(0))
=======
            if (checKeyBoardState.IsKeyDown(Keys.T) && gameObj.checkInventoryItem(1))
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
            if (checKeyBoardState.IsKeyDown(Keys.M) && interactiveDetection(gameObj.PlayerBox) && ACTIVELEVEL == 2)
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
            if(gameObj.currPlayerPositionFunc.X <= 0)
            {
                gameObj.currPlayerPositionFunc.X = 0;
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
            if (gameObj.currPlayerPositionFunc.X >= graphics.PreferredBackBufferWidth)
            {
                gameObj.currPlayerPositionFunc.X = graphics.PreferredBackBufferWidth;
            }
            //Flash light not accessible until after this stage.
            if (eventStates[2])
            {
                //checkFlashLight();
            }
        }
        public void CraneGameSimulation()
        {          
            ACTIVELEVEL = 7;
            eventStates[2] = true;
            gameObj.GetGameState = GameState.CurrentMenu;
        }
        public void restartLevel()
        {
            healthOffset = prevHealthOffset;
            loadLevel(ACTIVELEVEL);
        }
        //TODO:: INCREASE PRECISION OF WEAK POINT
        public void checkTrajectoryEvent1()
        {
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            Rectangle intersectPoint1 = new Rectangle(160, 120, 40, 40);
            Rectangle intersectPoint2 = new Rectangle(200, 160, 40, 40);
            Rectangle intersectPoint3 = new Rectangle(240, 200, 40, 40);
            int containerIdx = 0;
            for (int y = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count; y++)
            {
                if (genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 0)
=======
            Rectangle intersect1 = new Rectangle(240, 200 - 40, 40, 40);
            int containerIdx = 0;
            for (int y = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count; y++)
            {
                if (genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 1)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {
                    containerIdx = y;
                }
            }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            Console.WriteLine(genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.X + " Box xCord");
            Console.WriteLine(genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Y + " Box YCord");
            if (genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersectPoint1) || genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersectPoint2) || genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersectPoint3))
                {

                    gameObj.InventoryContainer.RemoveAt(0);      
                    ACTIVELEVEL = 6;
                    eventStates[1] = true;
                    gameObj.GetGameState = GameState.DZMenu;
=======
            if (genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersect1))
                {
                    Console.WriteLine("testingcoll");             
                    ACTIVELEVEL = 6;
                    eventStates[1] = true;
                    aniBoxes.Clear();
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            if (splitSpikes4.Count == 0)
=======
            if (splitSpikes.Count == 0)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            {
                eventStates[1] = true;
                ACTIVELEVEL = 1;
                loadLevel(1);
            }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            //Wave 1
            foreach (SplitSpikeTile spike in splitSpikes1)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes1)
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            }        
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height),splitSpikes1);
            if(splitSpikes1.Count > 0)
            {
                return;
            }
            //WAVE 2
            foreach (SplitSpikeTile spike in splitSpikes2)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes2)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes2);
            //Wave 3
            foreach (SplitSpikeTile spike in splitSpikes3)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes3)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes3);
            if(splitSpikes3.Count > 0)
            {
                return;
            }
            //Wave 4
            foreach (SplitSpikeTile spike in splitSpikes4)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes4)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes4);
=======
            }                  
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height));
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        }  
        public void dangerZoneEvent()
        {
            Console.WriteLine("Test");
            cordGen = new Random();
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            int tempX;
            int push = 0;
            int tempW = 40;
            splitSpikes1 = new List<SplitSpikeTile>();
            splitSpikes2 = new List<SplitSpikeTile>();
            splitSpikes3 = new List<SplitSpikeTile>();
            splitSpikes4 = new List<SplitSpikeTile>();
            //Generate wave leaving only 300 pixels safe zone on right
            for (int x  = 0; x < 70; x++)
            {
                tempX = push;
                SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                tempVar.YVelocity = 2;
                splitSpikes1.Add(tempVar);
                push += 10;                  
            }
            push = 0;    
            //Generate wave leaving 150 pixel gap in the middle of map
            for (int x = 0; x < 70; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = graphics.PreferredBackBufferWidth - push;              
                SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                tempVar.YVelocity = 2;
                splitSpikes2.Add(tempVar);              
                push += 10;
            }            
            push = 0;
            //Generate wave leaving 150 pixel gap in the middle of map
            for (int x = 0; x < 80; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = push;
                if (tempX < 400 || tempX > 500)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.YVelocity = 4;
                    splitSpikes3.Add(tempVar);
                }
                push += 10;
            }
            push = 0;
            //Bigger gap wave, but x velocity added
            for (int x = 0; x < 80; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = push;
                if (tempX < 300 )
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.XVelocity = 2;
                    tempVar.YVelocity = 4;
                    splitSpikes4.Add(tempVar);
                }
                if (tempX > 700)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.XVelocity = -2;
                    tempVar.YVelocity = 4;
                    splitSpikes4.Add(tempVar);
                }
                push += 10;
            }
        }
        public void DangerZoneCollision(Rectangle PlayerLoc, List<SplitSpikeTile> splitSpikes)
        {
            //Collision for platform tiles require movement from top collisions           
            foreach (SplitSpikeTile tile2 in splitSpikes.ToList())
            {
                if (tile2.Box.Intersects(PlayerLoc))
                {
                    healthOffset += 150;
                    splitSpikes.Remove(tile2);
                }
                if (tile2.Box.Y > graphics.PreferredBackBufferHeight)
                {
                    splitSpikes.Remove(tile2);
                }
            }         
        }                    
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
        public void throwItem()
        {
            Rectangle newBox;
            Console.WriteLine("Test");
            int containerIdx = 0;
            for(int y  = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count;y++)
            {
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if(genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 0)
=======
                if(genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 1)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {                  
=======
                {
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                    activeIdx.Add(gameObj.InventoryContainer[x].ItemKey);
                }                                                                            
            }
            for (int y = 0; y < genMaps[ACTIVELEVEL].GuiTiles.Count; y++)
            {
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                tempFlag = false;
=======
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                //Make sure its a gui display tile
                if (genMaps[ACTIVELEVEL].GuiTiles[y].GetType() == typeof(InventoryDisplayTileItem))
                {
                    //Check the valid index's for collected tiles
                    for (int x = 0; x < activeIdx.Count; x++)
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                    {
                        int t = ((InventoryDisplayTileItem)genMaps[ACTIVELEVEL].GuiTiles[y]).ItemKey;
=======
                    {            
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                equiptItem(0);
=======
                equiptItem(1);
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            }
            if (checKeyBoardState.IsKeyDown(Keys.D2) && !itemEquiptCheck())
            {
                //Console.WriteLine("FlashLight Equipt");
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                equiptItem(1);
=======
                equiptItem(2);
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            }
            if (checKeyBoardState.IsKeyDown(Keys.D3) && !itemEquiptCheck() && gameObj.InventoryContainer[2].Collected)
            {
               // Console.WriteLine("Batteries Loaded");
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                equiptItem(2);
=======
                equiptItem(3);
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            }
            if (checKeyBoardState.IsKeyDown(Keys.D4) && !itemEquiptCheck())
            {
               // Console.WriteLine("MedKit consumed");
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                equiptItem(3);
=======
                equiptItem(4);
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                case 0:
=======
                case 1:
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                    {
                        if (gameObj.InventoryContainer[0].Collected && !gameObj.InventoryContainer[0].Consumed)
                        {
                            Console.WriteLine("Mining Axe Equipt");
                            gameObj.InventoryContainer[0].Equipt = true;
                            gameObj.InventoryContainer[0].ThrowAble = true;
                        }
                        break;
                    }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                case 1:
                    {
                        if (gameObj.InventoryContainer[1].Collected)
                        {
                            Console.WriteLine("Flash Light Equipt");
                            gameObj.InventoryContainer[1].Equipt = true;
                        }
                        break;
                    }
                case 2:
                    {
                        if (gameObj.InventoryContainer[2].Collected && !gameObj.InventoryContainer[2].Consumed)
                        {
                            Console.WriteLine("Batteries Consumed");
                            gameObj.InventoryContainer[2].Consumed = true;
                        }
                        break;
                    }
                case 3:
                    {
                        if (gameObj.InventoryContainer[3].Collected && !gameObj.InventoryContainer[3].Consumed)
                        {
                            Console.WriteLine("MedKit Equipt");
                            gameObj.InventoryContainer[3].Equipt = true;
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
        in the case of throwing an item, the velocity is to great to avoid "sweeping" the collision as the item will simply pass through and avoid all collision detection
        since i have capped the velocity of the throw to a given amount, creating three sweep points at 1/4, 1/2, full velocity works (for now).
         */
        public bool ItemSweepCollision(ItemTile itemCheck)
        {
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            int checkX = itemCheck.Box.X + (int)itemCheck.XVelocity;
            int checkY = itemCheck.Box.Y + (int)itemCheck.YVelocity;
            ItemTile checkItemSweep = new ItemTile(0, new Rectangle(checkX - (int) itemCheck.XVelocity / 4, checkY -(int) itemCheck.YVelocity / 4, 20, 20), genMaps[ACTIVELEVEL].GetMap.Item1, gameObj);
            ItemTile checkItemSweep2 = new ItemTile(0, new Rectangle(checkX - (int)itemCheck.XVelocity / 2, checkY - (int)itemCheck.YVelocity / 2, 20, 20), genMaps[ACTIVELEVEL].GetMap.Item1, gameObj);
            ItemTile checkItemSweep3 = new ItemTile(0, new Rectangle(checkX - (int)itemCheck.XVelocity / 1, checkY - (int)itemCheck.YVelocity / 1, 20, 20), genMaps[ACTIVELEVEL].GetMap.Item1, gameObj);
            //Need to implement sweeping detection to fix collision graph offsets                       
            if (ItemCollisionDetection(checkItemSweep))
            {
                itemCheck.MotionState = false;
                return true;
            }
            if (ItemCollisionDetection(checkItemSweep2))
            {
                itemCheck.MotionState = false;
                return true;
            }
            if (ItemCollisionDetection(checkItemSweep3))
=======
            //Need to implement sweeping detection to fix collision graph offsets                       
            if (ItemCollisionDetection(itemCheck))
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            {
                itemCheck.MotionState = false;
                return true;
            }
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
=======
            
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            return false;
        }
        public bool ItemCollisionDetection(ItemTile itemCheck)
        {
            Rectangle newBox;
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
=======
            int checkX = itemCheck.Box.X + (int)itemCheck.XVelocity;
            int checkY = itemCheck.Box.Y + (int)itemCheck.YVelocity;
            ItemTile checkItemSweep = new ItemTile(0, new Rectangle(checkX, checkY, 20, 20), genMaps[ACTIVELEVEL].GetMap.Item1, gameObj);
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
            //Collision for platform tiles require movement from top collisions
            foreach (PlatFormTile tile in genMaps[ACTIVELEVEL].PlatFormTile)
            {
                //Top
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (itemCheck.Box.Bottom >= tile.Box.Top && itemCheck.Box.Bottom <= tile.Box.Top  && itemCheck.Box.Right >= tile.Box.Left  && itemCheck.Box.Left <= tile.Box.Right )
=======
                if (checkItemSweep.Box.Bottom >= tile.Box.Top && checkItemSweep.Box.Bottom <= tile.Box.Top  && checkItemSweep.Box.Right >= tile.Box.Left  && checkItemSweep.Box.Left <= tile.Box.Right )
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {
                    Console.WriteLine("Top Item Collision");
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Top - 1, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }   
                //Bottom
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (itemCheck.Box.Top <= tile.Box.Bottom - yOffset && itemCheck.Box.Top >= tile.Box.Bottom  && itemCheck.Box.Right >= tile.Box.Left&& itemCheck.Box.Left <= tile.Box.Right)
=======
                if (checkItemSweep.Box.Top <= tile.Box.Bottom - yOffset && checkItemSweep.Box.Top >= tile.Box.Bottom  && checkItemSweep.Box.Right >= tile.Box.Left&& checkItemSweep.Box.Left <= tile.Box.Right)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {
                    Console.WriteLine("Bot Item Collision");
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Bottom + itemCheck.Box.Height, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }                                              
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (itemCheck.Box.Left >= tile.Box.Left && itemCheck.Box.Left <= tile.Box.Right && itemCheck.Box.Top <= tile.Box.Bottom  && itemCheck.Box.Bottom >= tile.Box.Top)
=======
                if (checkItemSweep.Box.Left >= tile.Box.Left && checkItemSweep.Box.Left <= tile.Box.Right && checkItemSweep.Box.Top <= tile.Box.Bottom  && checkItemSweep.Box.Bottom >= tile.Box.Top)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {                  
                    newBox = new Rectangle(tile.Box.Right + 2, itemCheck.Box.Y, 40, 40);
                    itemCheck.Box = newBox;               
                    Console.WriteLine("Right Item Collision");                      
                    return true;
                }
                //Left side collisions with offsets
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                if (itemCheck.Box.Right <= tile.Box.Right && itemCheck.Box.Right >= tile.Box.Left && itemCheck.Box.Top <= tile.Box.Bottom && itemCheck.Box.Bottom >= tile.Box.Top)
=======
                if (checkItemSweep.Box.Right <= tile.Box.Right && checkItemSweep.Box.Right >= tile.Box.Left && checkItemSweep.Box.Top <= tile.Box.Bottom && checkItemSweep.Box.Bottom >= tile.Box.Top)
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                {                  
                    Console.WriteLine("Left Item Collision");
                    newBox = new Rectangle(tile.Box.Left - 2 - tile.Box.Width, itemCheck.Box.Y, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }                                          
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
=======
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
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
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
<<<<<<< HEAD:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                //Right side collison with offsets at maximum velocity
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
=======
                //Right side collison with offsets
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 4) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Height / 4))
                {                  
                        gameObj.InventoryContainer[tile.ItemKey].Collected = true;
>>>>>>> origin/master:COSC438GameDesignNewREV/GameEngineClasses/Physics.cs
                        tile.Collected = true;                                                    
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) + 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                    gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + gameObj.ActivePlayer.Height - YOffSet - 2 && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
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
        public bool interactiveDetection(Rectangle player)
        {
            foreach (CraneTile tile in genMaps[ACTIVELEVEL].CraneTile)
            {
                if (tile.Box.Intersects(player))
                {
                    activatePromp();
                    return true;
                }
            }
            deActivatePromp();        
            return false;
        }
        public bool iceCollision(Rectangle player)
        {
            foreach (SlipperyTile tile in genMaps[ACTIVELEVEL].SlipperyTiles)
            {
                if (tile.Box.Intersects(player))
                {
                    return true;
                }
            }
            return false;
        }
        public void activatePromp()
        {
            foreach (Tile tile in genMaps[ACTIVELEVEL].GuiTiles)
            {
                if (tile.GetType() == typeof(PromptTile))
                {
                    if (!tile.Active)
                    {
                        Console.WriteLine("Activated");
                        ((PromptTile)tile).activate();
                    }
                }           
            }
        }
        public void deActivatePromp()
        {
            foreach (Tile tile in genMaps[ACTIVELEVEL].GuiTiles)
            {
                if (tile.GetType() == typeof(PromptTile))
                {
                    if (tile.Active)
                    {
                        ((PromptTile)tile).deactivate();
                    }
                }
            }
        }
    }
}
