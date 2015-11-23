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
namespace COSC438GameDesignNewREV
{
    public enum CharDirection
    {
        Left,
        Right,
        Up,
        Down
    }
    public enum ActivePlayerz
    {
        Miner,
        Engineer,
        Medic,
        Foreman
    }
    public class Physics 
    {
        private Texture2D pathHighlight;
        private List<double> resList;
        private Rectangle showBox;
        private float drawAngel;
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
        private float currCycleRocks = .05f;
        private float resourceOffset;
        // private const int decayRate = .005f;
        //Useful Primitives
        //private bool burstAvaliable = true;
        private int burstExhausted;
        private int burstRemaining;
        private int healthRemaining;
        private int characterNum;
        private bool IDOWN = false;
        private bool TDOWN = false;
        private int ACTIVELEVEL;
        private int healthOffset = 0;
        private int prevHealthOffset = 0;
        //MonoStuff & Objects    
        private KeyboardState prevKEYState;
        private CharDirection characterFacing;
        private ActivePlayerz activePlayer;
        private Game1 gameObj;       
        private GridLayout[] genMaps;
        private MouseState checkMouseState;
        //private MouseState checkPrevMouseState;
        private KeyboardState checKeyBoardState;
        private SpriteBatch sprites;
        private GraphicsDeviceManager graphics;
        private Vector2 velocity;
        private GameTime gameTime;
        private List<Rectangle> aniBoxes;
        private Random cordGen;
        List<SplitSpikeTile> splitSpikes1;
        List<SplitSpikeTile> splitSpikes2;
        List<SplitSpikeTile> splitSpikes3;
        List<SplitSpikeTile> splitSpikes4;
        List<SplitSpikeTile> splitSpikes5;
        List<SplitSpikeTile> splitSpikes6;
        List<SplitSpikeTile> splitSpikes7;
        List<SplitSpikeTile> splitSpikes8;
        //Getters/Setters   
        public ActivePlayerz ActivePlayer
        {
            get
            {
                return activePlayer;
            }
            set
            {
                activePlayer = value;
            }
        }
        public List<double> ResList
        {
            get
            {
                return resList;
            }
            set
            {
                resList = value;
            }
        }
        public CharDirection CharacterFacing
        {
            get
            {
                return characterFacing;
            }
            set
            {
                characterFacing = value;
            }
        }
        public float DrawAngel
        {
            get
            {
                return drawAngel;
            }
        }
        public Texture2D PathHighlight
        {
            get
            {
                return pathHighlight;
            }
        }
        public Rectangle ShowBox
        {
            get
            {
                return showBox;
            }
        }
        public int HealthRemaining
        {
            get
            {
                return healthRemaining;
            }
            set
            {
                healthRemaining = value;
            }
        }
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
            activePlayer = ActivePlayerz.Miner;
            ACTIVELEVEL = 1;
            this.gameObj = gameObj;
            this.characterNum = characterNum;
            this.sprites = sprites;
            this.graphics = graphics;
            eventStates = new bool[5];
            healthRemaining = 200;
            pathHighlight = Texture2D.FromStream(gameObj.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Path.png"));
            resList = new List<double>();
            resList.Add(0.0);
            resList.Add(0.0);
            resList.Add(0.0);           
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
                clock(10);
                returnClock(4);
                adjustHealth();
                if (ACTIVELEVEL < 5)
                {
                    returnItems();
                    playerInteractionDetection();
                }
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
                targetPos.Y = checkMouseState.Y;
                if (characterFacing == CharDirection.Right)
                {
                    showBox = new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - 10, 15, (int)targetPos.X  - gameObj.currPlayerPositionFunc.X - 30);
                    double opposite = (double)(currPos.X - targetPos.X);
                    double adjacent = (double)(targetPos.Y - currPos.Y);
                    drawAngel = (float)Math.Atan2(opposite, adjacent);
                }
                else if (characterFacing == CharDirection.Left)
                {
                    showBox = new Rectangle((int)targetPos.X  - 80, gameObj.currPlayerPositionFunc.Y - 95, 15, gameObj.currPlayerPositionFunc.X - ((int)targetPos.X - 80));
                    double opposite = (double)(currPos.X - targetPos.X);
                    double adjacent = (double)(targetPos.Y - currPos.Y);
                    drawAngel = (float)Math.Atan2(opposite, adjacent);
                }                            
                if (ACTIVELEVEL == 2)
                {
                    interactiveDetection(gameObj.PlayerBox);
                }           
            }
        }
        public void returnItems()
        {
            foreach (ItemTile tile in genMaps[ACTIVELEVEL].ItemTile)
            {
                if (!tile.MotionState && !tile.Collected && !tile.Equipt)
                {
                    Rectangle returnBox = new Rectangle(tile.ReturnState.X, tile.ReturnState.Y, tile.Box.Width, tile.Box.Height);
                    tile.Box = returnBox;
                }
            }
        }
        public void applyGravity()
        {
            Rectangle newBox;
            if (velocity.Y < 10 && ladderCollision(new Rectangle((int)(gameObj.currPlayerPositionFunc.X + velocity.X), (int)(gameObj.currPlayerPositionFunc.Y + velocity.Y - yOffset), 40, 40)))  
            {                      
                velocity.Y += .1f;
            }
            else if (velocity.Y < 10)
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
                    tile.YVelocity += .25f;
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
                healthRemaining -= 1;                 
            }          
        }
        public void returnClock(float timeCycle)
        {
            var clock = (float)gameTime.ElapsedGameTime.TotalSeconds;
            currCycle -= clock;
            if (currCycle <= 0)
            {
                currCycle = timeCycle;
                foreach (ItemTile tile in genMaps[ACTIVELEVEL].ItemTile)
                {
                    if (!tile.Collected && !tile.Consumed && !tile.MotionState)
                    {
                        tile.Box = new Rectangle(tile.ReturnState.X, tile.ReturnState.Y, tile.Image.Width, tile.Image.Height);
                    }
                }               
            }
        }
        public void adjustHealth()
        {
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
                //Minigame1
                if (ACTIVELEVEL == 6)
                {
                    gameObj.GetGameState = GameState.DZMenu;
                }
                    //Minigame2
                else if (ACTIVELEVEL == 7)
                {
                    gameObj.GetGameState = GameState.CurrentMenu;
                }
                    //Minigame3
                else if (ACTIVELEVEL == 8)
                {
                    gameObj.GetGameState = GameState.CurrentMenu;
                }
                gameObj.GetGameState = GameState.Dead;
            }
            ((ScoreTiles)genMaps[ACTIVELEVEL].GuiTiles[maxIdx]).adjustHealth(healthOffset);           
            healthOffset = 0;
        }     
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
                //LVL 0              
                case 0:
                    {
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[1].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[1].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[0].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[0].Y;
                        }                       
                        genMaps[0] = gameObj.Grid[0];
                        break;
                    }
                //LVL 1
                case 1:
                    {
                        //LVL1 PRE EVENT
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
                        //Change Map After event
                        if (eventStates[1])
                        {
                            Console.WriteLine("Test");
                            genMaps[1] = gameObj.Grid[11];
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
                        //LVL1 POST EVENT
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
                //LVL 2
                case 2:
                    {
                        //LVL 2 PRE EVENT
                        if (moveState == 1 && !eventStates[2])
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[7].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[7].Y;
                        }
                        else if (moveState == -1 && !eventStates[2])
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[6].X; 
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[6].Y;
                        }
                         else if (eventStates[2] && moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[9].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[9].Y;
                        }
                        else if (eventStates[2] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[8].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[8].Y;
                        }
                        else
                        {
                            Console.WriteLine("Case Failed");
                        }
                        //Change map after event
                        if (eventStates[2])
                        {
                            genMaps[2] = gameObj.Grid[12];
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
                        //LVL 2 POST EVENT        
                        break;
                    }
                //LVL 3
                case 3:
                    {
                        //LVL 3 PRE EVENT
                        if (moveState == 1 && !eventStates[3])
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[11].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[11].Y;
                        }
                        else if (moveState == -1 && !eventStates[3])
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[10].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[10].Y;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        //Change map after event
                        if (eventStates[3])
                        {
                            //Make method to generate post map
                            genMaps[3] = gameObj.Grid[13];
                        }
                        else
                        {
                            genMaps[3] = gameObj.Grid[3];
                        }
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        //LVL 3 POST EVENT
                        if (eventStates[3] && moveState == 1)
                        {                            
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[13].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[13].Y;
                        }
                        else if (eventStates[3] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[12].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[12].Y;
                        }
                        break;
                    }
                //LVL 4
                case 4:
                    {
                        //LVL 4 PRE EVENT
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[15].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[15].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[14].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[14].Y;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        //toadd
                        if (eventStates[4])
                        {
                            //Make method to generate post map
                            genMaps[4] = gameObj.Grid[14];
                        }
                        else
                        {
                            genMaps[4] = gameObj.Grid[4];
                        }
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        //LVL 4 POST EVENT
                        if (eventStates[4] && moveState == 1)
                        {                           
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[17].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[17].Y;
                        }
                        else if (eventStates[4] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[16].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[16].Y;
                        }
                        break;
                    }
                //LVL 5
                case 5:
                    {
                        //LVL 5 PRE EVENT
                        if (moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[19].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[19].Y;
                        }
                        else if (moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[18].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[18].Y;
                        }
                        /*
                        Clear our animation box so that we can use it for the level event
                        Also load the current levels grid
                        */
                        //toadd
                        if (eventStates[5])
                        {
                            //Make method to generate post map
                            genMaps[5] = gameObj.Grid[15];
                        }
                        else
                        {
                            genMaps[5] = gameObj.Grid[5];
                        }
                        /*
                        When eventState[x] becomes true it means that all conditions were met to perform an event action,
                        these events vary from level to level however its important to keep track of these events in order
                        to determine how we adapt the characters position based off landscape changes.    
                        */
                        //LVL 4 POST EVENT
                        if (eventStates[5] && moveState == 1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[21].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[21].Y;
                        }
                        else if (eventStates[5] && moveState == -1)
                        {
                            gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[20].X;
                            gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[20].Y;
                        }
                        break;
                    }
                //DangerZone
                case 6:
                    {
                        burstRemaining = 120;
                        genMaps[6] = gameObj.Grid[6];                                           
                        gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[23].X;
                        gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[23].Y;                      
                        break;
                    }
                //Current Level
                case 7:
                    {                       
                        genMaps[7] = gameObj.Grid[7];
                        gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[25].X;
                        gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[25].Y;
                        break;
                    }
                //MiniGame 3
                case 8:
                    {
                        genMaps[8] = gameObj.Grid[8];
                        gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[27].X;
                        gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[27].Y;
                        break;
                    }
                case 9:
                    {
                        genMaps[9] = gameObj.Grid[9];
                        gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[29].X;
                        gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[29].Y;
                        break;
                    }
                case 10:
                    {
                        genMaps[10] = gameObj.Grid[10];
                        gameObj.currPlayerPositionFunc.X = gameObj.Grid[0].GetMap.playerStartLocs[31].X;
                        gameObj.currPlayerPositionFunc.Y = gameObj.Grid[0].GetMap.playerStartLocs[31].Y;
                        break;
                    }
            }
        }
        //TODO:: IMPLEMENT MOUSE INTERACTIONS
        //Process mouse location and possible clicks.
        public void CheckMouseInput(MouseState k)
        {
            SelectBlock();
            upDateBlockLoc();
        }
        public bool transitCurrencyCheck(int toDrop)
        {
            foreach (OhmTile tile in genMaps[ACTIVELEVEL].OhmTile)
            {
                if (tile.InTransit)
                {
                    if(toDrop == -1)
                    {                       
                        tile.InTransit = false;
                    }
                    return true;
                }
            }
            return false;
        }
        public void upDateBlockLoc()
        {
            foreach (OhmTile tile in genMaps[ACTIVELEVEL].OhmTile)
            {
                if(tile.InTransit)
                {
                    gameObj.IsMouseVisible = false;
                    tile.Box = new Rectangle(checkMouseState.X, checkMouseState.Y, 40, 40);
                }
            }
        }
        public void SelectBlock()
        {
            //Check each resistor
            foreach (OhmTile tile in genMaps[ACTIVELEVEL].OhmTile)
            {
                //If we click on one and we dont currently have one in transit we can lock the tile to our mouse
                if (checkMouseState.LeftButton == ButtonState.Pressed && !transitCurrencyCheck(1))
                {
                    Rectangle playerMouseBox = new Rectangle(checkMouseState.X, checkMouseState.Y, 10, 10);
                    if (tile.Box.Intersects((playerMouseBox)))
                    {
                        {
                            tile.Placed = false;
                            tile.InTransit = true;
                            return;                      
                        }
                    }
                }
                if (checkMouseState.LeftButton == ButtonState.Released)
                {
                    gameObj.IsMouseVisible = true;
                    transitCurrencyCheck(-1);    
                }                      
            }          
        }
        //TODO Fix Bug's in JUMP
        public void Jump()
        {           
                if (checKeyBoardState.IsKeyDown(Keys.Space) && jumpState == false)
                {
                //gameObj.JumpSound.Play();
                if (ACTIVELEVEL == 7)
                {
                    gameObj.currPlayerPositionFunc.Y -= 15;
                    velocity.Y = -10f;
                    jumpState = true;
                }
                else
                {
                    gameObj.currPlayerPositionFunc.Y -= 10;
                    velocity.Y = -10f;
                    jumpState = true;
                }
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
                if (jumpState == true && characterFacing == CharDirection.Right)
                {
                    gameObj.PlayerClass.UpdateJump(gameTime, 1);
                }
                else if (jumpState == true && characterFacing == CharDirection.Left)
                {
                    gameObj.PlayerClass.UpdateJump(gameTime, 2);
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
            else if (checKeyBoardState.IsKeyDown(Keys.B) && ACTIVELEVEL == 6 && burstRemaining >= 0)
            {
                    //gameObj.BurstSound.Play();
                    burstExhausted += 1;
                    if (characterFacing == CharDirection.Right)
                    {
                        gameObj.PlayerClass.UpdateRight(gameTime);
                        velocity.X = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 3;
                    }
                    if (characterFacing == CharDirection.Left)
                    {
                        gameObj.PlayerClass.UpdateLeft(gameTime);
                        velocity.X = -(float)(gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 3;
                    }               
            }
            //Right Movement Controlled By D
            else if (checKeyBoardState.IsKeyDown(Keys.D))
            {
                gameObj.PlayerClass.UpdateRight(gameTime);
                characterFacing = CharDirection.Right;
                if (ACTIVELEVEL == 6 && activePlayer == ActivePlayerz.Miner)
                {
                    velocity.X = (float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 1.25);
                }
                else
                {
                    velocity.X = (float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5));
                }
            }
            //Left Movement Controlled by A
            else if (checKeyBoardState.IsKeyDown(Keys.A))
            {
                gameObj.PlayerClass.UpdateLeft(gameTime);
                characterFacing = CharDirection.Left;
                if (ACTIVELEVEL == 6 && activePlayer == ActivePlayerz.Miner)
                {
                    velocity.X = -(float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5) * 1.25);
                }
                else
                {
                    velocity.X = -(float)((gameTime.ElapsedGameTime.TotalMilliseconds / 5));
                }
            }                              
            else
            {
                if (jumpState == false && ladderCollision(new Rectangle((int)(gameObj.currPlayerPositionFunc.X + velocity.X), (int)(gameObj.currPlayerPositionFunc.Y + velocity.Y - yOffset), 40, 40)))
                {
                    gameObj.PlayerClass.UpdateClimb(gameTime);
                }
                else if (jumpState == false)
                {
                    if (characterFacing == CharDirection.Right)
                    {
                        gameObj.PlayerClass.UpdateStand(1);
                    }
                    else
                    {
                        gameObj.PlayerClass.UpdateStand(-1);
                    }
                }
                 velocity.X = 0f;
            }           
            if (checKeyBoardState.IsKeyDown(Keys.W))
            {

                if (ladderCollision(new Rectangle((int)(gameObj.currPlayerPositionFunc.X + velocity.X), (int)(gameObj.currPlayerPositionFunc.Y + velocity.Y - yOffset), 40, 40)))
                {
                    gameObj.PlayerClass.UpdateClimb(gameTime);
                    characterFacing = CharDirection.Up;
                    velocity.Y = -1;
                    gameObj.currPlayerPositionFunc.Y += (int)velocity.Y;
                    climbState = true;
                }          
            }
            climbState = false;
            //Checks for jump keypress
            Jump();           
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
            if(checKeyBoardState.IsKeyDown(Keys.S))
            {
                switch (playerInteractionDetection())
                {
                    case 0:
                        {                           
                            break;
                        }
                    //Swap to Eng
                    case 1:
                        {
                            if (activePlayer == ActivePlayerz.Miner)
                            {
                                gameObj.PlayerClassMiner.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassMiner.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassMiner.SwapAble = false;
                            }
                            else if (activePlayer == ActivePlayerz.Foreman)
                            {
                                gameObj.PlayerClassFore.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassFore.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassFore.SwapAble = false;
                            }
                            gameObj.PlayerClass = gameObj.PlayerClassEng;
                            gameObj.PlayerClass.SwapAble = false;
                            activePlayer = ActivePlayerz.Engineer;                    
                            break;
                        }
                    //Swap to foreman
                    case 2:
                        {
                            if (activePlayer == ActivePlayerz.Miner)
                            {
                                gameObj.PlayerClassMiner.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassMiner.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassMiner.SwapAble = false;
                            }
                            else if (activePlayer == ActivePlayerz.Engineer)
                            {
                                gameObj.PlayerClassEng.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassEng.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassEng.SwapAble = false;
                            }                           
                            gameObj.PlayerClass = gameObj.PlayerClassFore;
                            gameObj.PlayerClass.SwapAble = false;
                            activePlayer = ActivePlayerz.Foreman;
                            break;
                        }
                    //Swap to miner
                    case 3:
                        {
                            if (activePlayer == ActivePlayerz.Engineer)
                            {
                                gameObj.PlayerClassEng.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassEng.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassEng.SwapAble = false;
                            }
                            else if (activePlayer == ActivePlayerz.Foreman)
                            {
                                gameObj.PlayerClassFore.PlayerLocation = new Vector2(gameObj.PlayerClass.PlayerLocation.X, gameObj.PlayerClass.PlayerLocation.Y - 80);
                                gameObj.PlayerClassFore.ACTIVELVL = ACTIVELEVEL;
                                gameObj.PlayerClassFore.SwapAble = false;
                            }
                            gameObj.PlayerClass = gameObj.PlayerClassMiner;
                            gameObj.PlayerClass.SwapAble = false;
                            activePlayer = ActivePlayerz.Miner;
                            break;
                        }
                }
            }
            //Check if we have the mining age euipt
            if (checKeyBoardState.IsKeyDown(Keys.T) && gameObj.checkInventoryItem(0) && ACTIVELEVEL == 1)
            {
                if (TDOWN)
                {
                    TDOWN = false;
                }
                else
                {
                    TDOWN = true;
                }
                //gameObj.Pickaxe.Play();   
                throwItem();               
            }
            //Remove item from character and return to inventory bag
            if (checKeyBoardState.IsKeyDown(Keys.U))
            {
                unequiptItem();
            }
            if (checKeyBoardState.IsKeyDown(Keys.R))
            {
                if (ACTIVELEVEL < 6)
                {
                    restartLevel();
                }
            }
            //M is for minigame interaction elements
            if (checKeyBoardState.IsKeyDown(Keys.M) && interactiveDetection(gameObj.PlayerBox) && ACTIVELEVEL == 2)
            {
                CraneGameSimulation();
            }
            //Checks for item key presses, these equipt items in inventory if applicable.
            checkItemUse();
            GUIINVUpdate();
            if (ACTIVELEVEL == 6)
            {
                burstRemaining -= burstExhausted;
                burstExhausted = 0;
            }
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
                checkFlashLight();
            }
        }
        public void checkFlashLight()
        {
            int flashLightIdx =  itemIdx(1);
            int battIdx = itemIdx(2);
            if (gameObj.InventoryContainer[flashLightIdx].Equipt && gameObj.InventoryContainer[battIdx].Consumed)
            {
                flashLightView = true;
            }
            else
            {
                flashLightView = false;
            }        
        }
        public void CraneGameSimulation()
        {                   
            gameObj.GetGameState = GameState.CurrentMenu;
        }
        public void restartLevel()
        {
            healthOffset = prevHealthOffset;
            if (eventStates[ACTIVELEVEL])
            {
                gameObj.Grid[ACTIVELEVEL].generateGrid(ACTIVELEVEL + 10);
            }
            else
            {
                gameObj.Grid[ACTIVELEVEL].generateGrid(ACTIVELEVEL);
            }
            loadLevel(1);
        }
        //TODO:: INCREASE PRECISION OF WEAK POINT
        public void checkTrajectoryEvent1()
        {
            Rectangle intersectPoint2 = new Rectangle(200, 160, 40, 40);
            Rectangle intersectPoint3 = new Rectangle(240, 200, 40, 40);
            int containerIdx = 0;
            for (int y = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count; y++)
            {
                if (genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 0)
                {
                    containerIdx = y;
                }
            }     
            if (genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersectPoint2) || genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box.Intersects(intersectPoint3))
                {                                     
                    gameObj.GetGameState = GameState.DZMenu;                 
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
            foreach (OhmTile tile in genMaps[ACTIVELEVEL].OhmTile)
            {
                if (tile.Placed)
                {
                    switch (tile.Colour)
                    {
                        case 1:
                            {
                                tileCount[0] += 1;
                                resistanceGreen += tile.Ohmval;
                                break;
                            }
                        case 2:
                            {
                                tileCount[1] += 1;
                                resistanceRed += tile.Ohmval;
                                break;
                            }
                        case 3:
                            {
                                tileCount[2] += 1;
                                resistanceYellow += tile.Ohmval;
                                break;
                            }
                    }
                }
            }             
                currentGreen = (double)(240.0 / resistanceGreen);
                currentRed = (double)(480.0 / resistanceRed);
                currentYellow = (double)(600.0 / resistanceYellow);                
                resList[0] = currentGreen;
                resList[1] = currentRed;
                resList[2] = currentYellow;   
                if (currentGreen > 1.50 && currentGreen < 1.60)
                {              
                GreenState = true;
                //gameObj.Electric.Play();
                }
                else
                {
                    GreenState = false;
                }
                if (currentRed > 1.50 && currentRed < 2.00 )
                {            
                RedState = true;
                //gameObj.Electric.Play();
                }
                else
                {
                    RedState = false;
                }
                if (currentYellow > 1.50 && currentYellow < 2.00)
                {
               // gameObj.Electric.Play();
                YellowState = true;
                }
                else
                {
                    YellowState = false;
                }                
            foreach (StatusTile tile2 in genMaps[ACTIVELEVEL].StatusTile)
            {
                switch (tile2.WireColor)
                {
                    case 1:
                        {
                            if (GreenState)
                            {                               
                                tile2.updateStatus(1);
                            }
                            else
                            {
                                tile2.updateStatus(2);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (RedState)
                            {
                                tile2.updateStatus(1);
                            }
                            else
                            {
                                tile2.updateStatus(2);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (YellowState)
                            {
                                tile2.updateStatus(1);
                            }
                            else
                            {
                                tile2.updateStatus(2);
                            }
                            break;
                        }
                }
            }

            if (GreenState && RedState && YellowState)
            {
                ACTIVELEVEL = 2;
                loadLevel(1);
            }             
        }
        public int dispLocation(int wireColor)
        {
            int tempRet = 0;
            bool pos1 = false;
            bool pos2 = false;
            foreach (OhmDropTile tilez in genMaps[ACTIVELEVEL].OhmDropTile)
            {
                if (tilez.Colour == wireColor)
                {

                    if (tilez.WirePosition == 1 && tilez.HoldVal > 0)
                    {
                        tempRet++;
                        pos1 = true;
                    }
                    if (tilez.WirePosition == 2 && tilez.HoldVal > 0)
                    {
                        tempRet++;
                        pos2 = true;
                    }
                }
            }
            if (tempRet == 2)
            {
                return 2;
            }
            if (tempRet == 1 && pos2 == true)
            {
                return -1;
            }
            return tempRet;
        }
        public bool sidePrecedence(OhmDropTile tile)
        {          
            //If droping to first drop location, we dont care about verification, its valid by default
             if (tile.WirePosition == 1)
             {
                    return true;
             } 
            //If attempting to drop at right hand position on the wire, we need to ensure that the wires left hand side has been placed first.
            foreach (OhmDropTile tilez in genMaps[ACTIVELEVEL].OhmDropTile)
            {
                if (tile.WirePosition == 1)
                {
                    return true;
                } 
                //If we have a color match make sure that the first location is filled by checking its value
                else if (tilez.Colour == tile.Colour && tilez.WirePosition == 1)
                {
                    if (tilez.HoldVal > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void ohmCollisionDetection()
        {
            foreach(OhmDropTile tile in genMaps[ACTIVELEVEL].OhmDropTile)
            {
                foreach(OhmTile tile2 in genMaps[ACTIVELEVEL].OhmTile)
                {
                    if (tile.Box.Intersects(tile2.Box) && !tile.Occupied)
                    {                      
                            if (!tile2.InTransit && tile.Colour == tile2.Colour && sidePrecedence(tile))
                            {
                                tile2.Box = tile.Box;
                                tile2.Placed = true;
                                tile2.InTransit = false;
                                tile.Occupied = true;
                                tile.HoldVal = tile2.Ohmval;
                            }                       
                        }                   
                    else 
                    {
                        tile.Occupied = false;
                    }
                }
            }
        }
        public void dangerZoneSimulation()
        {
            if (splitSpikes7.Count == 0)
            {
                eventStates[1] = true;
                ACTIVELEVEL = 1;
                loadLevel(1);
            }
            //Wave 1
            foreach (SplitSpikeTile spike in splitSpikes1)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes1)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
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
            if (splitSpikes4.Count > 0)
            {
                return;
            }
            //Wave 5
            foreach (SplitSpikeTile spike in splitSpikes5)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes5)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes5);
            if (splitSpikes5.Count > 0)
            {
                return;
            }
            //Wave 6
            foreach (SplitSpikeTile spike in splitSpikes6)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes6)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes6);
            if (splitSpikes6.Count > 0)
            {
                return;
            }
            //Wave 7
            foreach (SplitSpikeTile spike in splitSpikes7)
            {
                spike.Falling = true;
            }
            foreach (SplitSpikeTile spike in splitSpikes7)
            {
                Rectangle newLoc2 = new Rectangle(spike.Box.X + spike.XVelocity, spike.Box.Y + spike.YVelocity, spike.Box.Width, spike.Box.Height);
                sprites.Draw(spike.Image, spike.Box, Color.White);
                if (spike.Falling)
                {
                    spike.Box = newLoc2;
                }
            }
            DangerZoneCollision(new Rectangle(gameObj.currPlayerPositionFunc.X, gameObj.currPlayerPositionFunc.Y - yOffset, gameObj.activePlayerFunc.Width, gameObj.activePlayerFunc.Height), splitSpikes7);
        }  
        public void dangerZoneEvent()
        {
            Console.WriteLine("Test");
            cordGen = new Random();
            int tempX;
            int push = 0;
            int tempW = 40;
            splitSpikes1 = new List<SplitSpikeTile>();
            splitSpikes2 = new List<SplitSpikeTile>();
            splitSpikes3 = new List<SplitSpikeTile>();
            splitSpikes4 = new List<SplitSpikeTile>();
            splitSpikes5 = new List<SplitSpikeTile>();
            splitSpikes6 = new List<SplitSpikeTile>();
            splitSpikes7 = new List<SplitSpikeTile>();
            splitSpikes8 = new List<SplitSpikeTile>();
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
            //Wave 5
            push = 0;
            //Bigger gap wave, but x velocity added
            for (int x = 0; x < 80; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = push;
                if (tempX < 300)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.XVelocity = -1;
                    tempVar.YVelocity = 6;
                    splitSpikes5.Add(tempVar);
                }
                if (tempX > 700)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.XVelocity = 1;
                    tempVar.YVelocity = 6;
                    splitSpikes5.Add(tempVar);
                }
                push += 10;
            }
            push = 0;
            //Bigger gap wave, but x velocity added
            for (int x = 0; x < 80; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = push;
                if (tempX > 100 && tempX < 650)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.YVelocity = 4;
                    splitSpikes6.Add(tempVar);
                }
                push += 10;
            }
            push = 0;
            //Bigger gap wave, but x velocity added
            for (int x = 0; x < 80; x++)
            {
                tempW = cordGen.Next(40, 50);
                tempX = push;
                if (tempX < 250 || tempX > 5500)
                {
                    SplitSpikeTile tempVar = new SplitSpikeTile(x, new Rectangle(tempX, 0, tempW, 40), gameObj);
                    tempVar.YVelocity = 7;
                    splitSpikes7.Add(tempVar);
                }
                push += 10;
            }
        }
        public void DangerZoneCollision(Rectangle PlayerLoc, List<SplitSpikeTile> splitSpikes)
        {
            bool soundCatch = false;
            //Collision for platform tiles require movement from top collisions           
            foreach (SplitSpikeTile tile2 in splitSpikes.ToList())
            {
                if (tile2.Box.Intersects(PlayerLoc))
                {            
                    splitSpikes.Remove(tile2);
                    deadCheck = true;
                    gameObj.GetGameState = GameState.Dead;
                }
                if (tile2.Box.Y > graphics.PreferredBackBufferHeight)
                {
                    if (!soundCatch)
                    {
                        soundCatch = true;
                        //gameObj.RockFall.Play();
                    }
                    splitSpikes.Remove(tile2);
                }
            }         
        }              
        public void throwItem()
        {
            Rectangle newBox;           
            int containerIdx = 0;
            for(int y  = 0; y < genMaps[ACTIVELEVEL].ItemTile.Count;y++)
            {
                if(genMaps[ACTIVELEVEL].ItemTile[y].ItemKey == 0)
                {
                    containerIdx = y;
                }
            }
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].MotionState = true;
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Equipt = false;
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Collected = false;
            /*
            Target position is the location of the mouse cursor, obviously before launch this will need to be updated with a decay rate for velocity of x and y
            but for now this will suffice for general demonstration
            */
            var deltaY = checkMouseState.Y - gameObj.currPlayerPositionFunc.Y - 10;
            var deltaX = checkMouseState.X - gameObj.currPlayerPositionFunc.X + (gameObj.ActivePlayer.Width + 40);
            var angle = Math.Atan2(deltaY, deltaX);
            var xFactor = Math.Cos(angle);
            var yFactor = Math.Sin(angle);
            var speed = 12;
            if (characterFacing == CharDirection.Right)
            {
                 deltaY = checkMouseState.Y - gameObj.currPlayerPositionFunc.Y - 10;
                 deltaX = checkMouseState.X - gameObj.currPlayerPositionFunc.X + (gameObj.ActivePlayer.Width + 40);
                 angle = Math.Atan2(deltaY, deltaX);
                 xFactor = Math.Cos(angle);
                 yFactor = Math.Sin(angle);
                 speed = 12;
                newBox = new Rectangle((int)gameObj.currPlayerPositionFunc.X + (gameObj.ActivePlayer.Width + 10), gameObj.currPlayerPositionFunc.Y - 60, 10, 10);
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = (float)(speed * xFactor / 1.25);
                Console.WriteLine(genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity);                  
            }
             else if (characterFacing == CharDirection.Left)
            {
                 deltaY = checkMouseState.Y - gameObj.currPlayerPositionFunc.Y - 10;
                 deltaX = gameObj.currPlayerPositionFunc.X - checkMouseState.X;
                 angle = Math.Atan2(deltaY, deltaX);
                 xFactor = Math.Cos(angle);
                 yFactor = Math.Sin(angle);
                 speed = 12;
                newBox = new Rectangle((int)gameObj.currPlayerPositionFunc.X - (gameObj.ActivePlayer.Width + 10), gameObj.currPlayerPositionFunc.Y - 60, 10, 10);
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = (float)(speed * xFactor / 1.25);
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity *= -1;
                Console.WriteLine(genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity);
            }  
            else
            {
                 deltaY = checkMouseState.Y - gameObj.currPlayerPositionFunc.Y - 10;
                 deltaX = checkMouseState.X - gameObj.currPlayerPositionFunc.X + (gameObj.ActivePlayer.Width + 40);
                 angle = Math.Atan2(deltaY, deltaX);
                 xFactor = Math.Cos(angle);
                 yFactor = Math.Sin(angle);
                 speed = 12;
                newBox = new Rectangle((int)gameObj.currPlayerPositionFunc.X + (gameObj.ActivePlayer.Width + 10), gameObj.currPlayerPositionFunc.Y - 60, 10, 10);
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = (float)(speed * xFactor / 1.25);
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].XVelocity = (float)(speed * xFactor / 1.25);

            }         
            //Fighting gravity should make a downward throw have less velocity(generally speaking)
            if (yFactor > 8)
            {
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].YVelocity = (float)(speed * yFactor * .25);
            }
            else
            {
                genMaps[ACTIVELEVEL].ItemTile[containerIdx].YVelocity = (float)(speed * yFactor * 1.5);
            }
            genMaps[ACTIVELEVEL].ItemTile[containerIdx].Box = newBox;           
        }     
        //FIN METHOD
        public void GUIINVUpdate()
        {
            List<int> activeIdx = new List<int>();
            bool tempFlag = false;
            //If our item is collected, we need to check make sure the gui displays it
            for (int x = 0; x < gameObj.InventoryContainer.Count; x++)
            {
                if (gameObj.InventoryContainer[x].Collected && !gameObj.InventoryContainer[x].Equipt && !gameObj.InventoryContainer[x].Consumed)
                {                  
                    activeIdx.Add(gameObj.InventoryContainer[x].ItemKey);
                }                                                                            
            }
            for (int y = 0; y < genMaps[ACTIVELEVEL].GuiTiles.Count; y++)
            {
                if (genMaps[ACTIVELEVEL].GuiTiles[y].GetType() == typeof(ResourceTiles) && ((ResourceTiles)genMaps[ACTIVELEVEL].GuiTiles[y]).ItemKey == 4)
                {
                    ((ResourceTiles)genMaps[ACTIVELEVEL].GuiTiles[y]).adjustResource(burstRemaining);
                }            
                tempFlag = false;
                //Make sure its a gui display tile
                if (genMaps[ACTIVELEVEL].GuiTiles[y].GetType() == typeof(InventoryDisplayTileItem))
                {
                    //Check the valid index's for collected tiles
                    for (int x = 0; x < activeIdx.Count; x++)
                    {
                        int t = ((InventoryDisplayTileItem)genMaps[ACTIVELEVEL].GuiTiles[y]).ItemKey;
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
                equiptItem(0);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D2) && !itemEquiptCheck())
            {
                //Console.WriteLine("FlashLight Equipt");
                equiptItem(1);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D3) && !itemEquiptCheck())
            {
               // Console.WriteLine("Batteries Loaded");
                equiptItem(2);
            }
            if (checKeyBoardState.IsKeyDown(Keys.D4) && !itemEquiptCheck())
            {
               // Console.WriteLine("MedKit consumed");
                equiptItem(3);
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
        public int itemIdx(int itemVal)
        {
            foreach (ItemTile tile in gameObj.InventoryContainer)
            {
                if (tile.ItemKey == itemVal)
                {
                    return gameObj.InventoryContainer.IndexOf(tile);
                }             
            }
            return -1;
        }
        //When prompted by the user, equipt an item provided it exist within our inventory container
        public void equiptItem(int itemVal)
        {
            int idx = 0;
            bool dependancy1 = false;
            foreach (ItemTile tile in gameObj.InventoryContainer)
            {
                if (tile.ItemKey == itemVal)
                {
                    idx = gameObj.InventoryContainer.IndexOf(tile);
                }
                else if (tile.ItemKey == 1)
                {
                    dependancy1 = true;
                }
            }
            switch(itemVal)
            {                
                case 0:
                    {
                        if (gameObj.InventoryContainer[idx].Collected && !gameObj.InventoryContainer[idx].Consumed)
                        {
                            Console.WriteLine("Mining Axe Equipt");
                            gameObj.InventoryContainer[idx].Equipt = true;
                            gameObj.InventoryContainer[idx].ThrowAble = true;
                        }
                        break;
                    }
                case 1:
                    {
                        if (gameObj.InventoryContainer[idx].Collected)
                        {
                            Console.WriteLine("Flash Light Equipt");
                            gameObj.InventoryContainer[idx].Equipt = true;
                        }
                        break;
                    }
                case 2:
                    {
                        if (gameObj.InventoryContainer[idx].Collected && !gameObj.InventoryContainer[idx].Consumed && dependancy1)
                        {
                            Console.WriteLine("Batteries Consumed");
                            gameObj.InventoryContainer[idx].Consumed = true;
                        }
                        break;
                    }
                case 3:
                    {
                        if (gameObj.InventoryContainer[idx].Collected && !gameObj.InventoryContainer[idx].Consumed)
                        {
                            Console.WriteLine("MedKit Equipt");
                            gameObj.InventoryContainer[idx].Equipt = true;
                        }
                        break;
                    }
            }
        }
        /*
        Used to collision detection, the sweep aspect just means it will check various points along the movement based on x and y velocity
        in the case of throwing an item, the velocity is to great to avoid "sweeping" the collision as the item will simply pass through and avoid all collision detection
        since i have capped the velocity of the throw to a given amount, creating three sweep points at 1/4, 1/2, full velocity works (for now).
         */
        public bool ItemSweepCollision(ItemTile itemCheck)
        {
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
            {
                itemCheck.MotionState = false;
                return true;
            }
            return false;
        }
        public bool ItemCollisionDetection(ItemTile itemCheck)
        {
            Rectangle newBox;
            //Collision for platform tiles require movement from top collisions
            foreach (PlatFormTile tile in genMaps[ACTIVELEVEL].PlatFormTile)
            {
                //Top
                if (itemCheck.Box.Bottom >= tile.Box.Top && itemCheck.Box.Bottom <= tile.Box.Top  && itemCheck.Box.Right >= tile.Box.Left  && itemCheck.Box.Left <= tile.Box.Right )
                {
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Top - 1, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }   
                //Bottom
                if (itemCheck.Box.Top <= tile.Box.Bottom - yOffset && itemCheck.Box.Top >= tile.Box.Bottom  && itemCheck.Box.Right >= tile.Box.Left&& itemCheck.Box.Left <= tile.Box.Right)
                {
                    newBox = new Rectangle(itemCheck.Box.X, tile.Box.Bottom + itemCheck.Box.Height, 40, 40);
                    itemCheck.Box = newBox;
                    return true;
                }                                              
                if (itemCheck.Box.Left >= tile.Box.Left && itemCheck.Box.Left <= tile.Box.Right && itemCheck.Box.Top <= tile.Box.Bottom  && itemCheck.Box.Bottom >= tile.Box.Top)
                {                  
                    newBox = new Rectangle(tile.Box.Right + 2, itemCheck.Box.Y, 40, 40);
                    itemCheck.Box = newBox;                                  
                    return true;
                }
                //Left side collisions with offsets
                if (itemCheck.Box.Right <= tile.Box.Right && itemCheck.Box.Right >= tile.Box.Left && itemCheck.Box.Top <= tile.Box.Bottom && itemCheck.Box.Bottom >= tile.Box.Top)
                {                  
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
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right - 1 && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                  //  Console.WriteLine("Right Collision");    
                    gameObj.currPlayerPositionFunc.X = tile.Box.Right + 2;
                    return true;
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left + 1 && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
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
                //Right side collison with offsets at maximum velocity
                if (checkPlayerNewState.Left >= tile.Box.Left && checkPlayerNewState.Left <= tile.Box.Right && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                        tile.Collected = true;                                                    
                }
                //Left side collisions with offsets
                if (checkPlayerNewState.Right <= tile.Box.Right && checkPlayerNewState.Right >= tile.Box.Left && checkPlayerNewState.Top <= tile.Box.Bottom - (gameObj.ActivePlayer.Height / 2 - 10) && checkPlayerNewState.Bottom >= tile.Box.Top + (gameObj.ActivePlayer.Width / 4))
                {
                   // gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Top
                if (checkPlayerNewState.Bottom >= tile.Box.Top && checkPlayerNewState.Bottom <= tile.Box.Top + (gameObj.ActivePlayer.Height / 2) + 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                   // gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
                //Bottom
                if (checkPlayerNewState.Top <= tile.Box.Bottom + gameObj.ActivePlayer.Height - YOffSet - 2 && checkPlayerNewState.Top >= tile.Box.Bottom - 1 && checkPlayerNewState.Right >= tile.Box.Left + (gameObj.ActivePlayer.Height / 10 - 1) && checkPlayerNewState.Left <= tile.Box.Right - (gameObj.ActivePlayer.Height / 10 - 1))
                {
                   // gameObj.InventoryContainer[tile.ItemKey].Collected = true;
                    tile.Collected = true;
                }
            }          
            return false;
        }
        public bool playerDetection(Rectangle player)
        {
            foreach (Tile tile in genMaps[ACTIVELEVEL].GuiTiles)
            {
                if (tile.GetType() == typeof(PlayerTile) && tile.Box.Intersects(player))
                {
                    return true;
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
                    activatePromp(1);
                    return true;
                }
            }
            deActivatePromp();        
            return false;
        }
        public int playerInteractionDetection()
        {
            if (gameObj.currPlayerPositionFunc.X >= 0)
            {
                if (activePlayer != ActivePlayerz.Engineer)
                {
                    if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X + 40, (int)gameObj.PlayerClass.PlayerLocation.X + 100).Contains((int)gameObj.PlayerClassEng.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassEng.ACTIVELVL)
                    {
                        gameObj.PlayerClassEng.UpdateStand(-1);
                        gameObj.PlayerClassEng.SwapAble = true;
                        return 1;
                    }
                    else if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X - 100, (int)gameObj.PlayerClass.PlayerLocation.X).Contains((int)gameObj.PlayerClassEng.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassEng.ACTIVELVL)
                    {
                        gameObj.PlayerClassEng.UpdateStand(1);
                        gameObj.PlayerClassEng.SwapAble = true;
                        return 1;
                    }
                    else
                    {
                        gameObj.PlayerClassEng.SwapAble = false;
                    }
                }
                if (activePlayer != ActivePlayerz.Foreman)
                {
                    if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X + 40, (int)gameObj.PlayerClass.PlayerLocation.X + 100).Contains((int)gameObj.PlayerClassFore.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassFore.ACTIVELVL)
                    {
                        gameObj.PlayerClassFore.UpdateStand(-1);
                        gameObj.PlayerClassFore.SwapAble = true;
                        return 2;
                    }
                    else if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X - 100, (int)gameObj.PlayerClass.PlayerLocation.X).Contains((int)gameObj.PlayerClassFore.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassFore.ACTIVELVL)
                    {
                        gameObj.PlayerClassFore.UpdateStand(1);
                        gameObj.PlayerClassFore.SwapAble = true;
                        return 2;
                    }
                    else
                    {
                        gameObj.PlayerClassFore.SwapAble = false;
                    }
                }
                if (activePlayer != ActivePlayerz.Miner)
                {
                    if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X + 40, (int)gameObj.PlayerClass.PlayerLocation.X + 100).Contains((int)gameObj.PlayerClassMiner.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassMiner.ACTIVELVL)
                    {
                        gameObj.PlayerClassMiner.UpdateStand(-1);
                        gameObj.PlayerClassMiner.SwapAble = true;
                        return 3;
                    }
                    else if (Enumerable.Range((int)gameObj.PlayerClass.PlayerLocation.X - 100, (int)gameObj.PlayerClass.PlayerLocation.X).Contains((int)gameObj.PlayerClassMiner.PlayerLocation.X) &&
                        gameObj.PlayerClass.ACTIVELVL == gameObj.PlayerClassMiner.ACTIVELVL)
                    {
                        gameObj.PlayerClassMiner.UpdateStand(1);
                        gameObj.PlayerClassMiner.SwapAble = true;
                        return 3;
                    }
                    else
                    {
                        gameObj.PlayerClassMiner.SwapAble = false;
                    }
                }
            }
            return 0;
        }
        public void activatePromp(int key)
        {
            foreach (Tile tile in genMaps[ACTIVELEVEL].GuiTiles)
            {
                switch (key)
                {
                    case 1:
                        {
                            if (tile.GetType() == typeof(PromptTile))
                            {
                                if (!tile.Active)
                                {                                  
                                    ((PromptTile)tile).activate();
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (tile.GetType() == typeof(PromptTile))
                            {
                                if (!tile.Active)
                                {
                                    ((PromptTile)tile).activate();
                                }
                            }
                            break;
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
