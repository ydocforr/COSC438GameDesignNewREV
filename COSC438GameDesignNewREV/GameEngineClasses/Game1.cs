using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace COSC438GameDesignNewREV
{ 
    public enum GameState
    {
        StartMenu,
        ControlMenu,
        DZMenu,
        CurrentMenu,
        MedicLvLMenu,
        Loading,
        Playing,
        Dead,
        LVL0,
        LVL1,
        LVL2,
        LVL3,
        LVL4,
        LVL5,
        Paused
    }
    public class Game1 : Game
    {
        /*
        http://www.spikie.be/blog/page/Building-a-main-menu-and-loading-screens-in-XNA.aspx
        //Load Screen,Pause,Main Screen code pretty much straight from a tutorial
        */
        //Screen Images
        private Texture2D pauseScreen;
        private Texture2D loadScreen;
        private Texture2D interactionPrompt;
        private Texture2D startScreen;
        private Texture2D controlScreen;
        private Texture2D DZMenu;
        private Texture2D CurrentMenu;
        private Texture2D MedicMenu;
        private Texture2D DeadMenu;
        private Texture2D LVL0Screen;
        private Texture2D LVL1Screen;
        private Texture2D LVL2Screen;
        private Texture2D LVL3Screen;
        private Texture2D LVL4Screen;
        private Texture2D LVL5Screen;
        //Screen locations
        private Vector2 sButtonPos;
        private Vector2 eButtonPos;
        private Vector2 rButtonPos;
        private Vector2 controlButtonPos;    
        //Dimensions
        private const float oWidth = 50f;
        private const float oHeight = 50f;
        private Thread backgroundThread;
        private bool isLoading = false;
        private bool restart = false;
        private MouseState mouseState;
        private MouseState prevMouseState;
        private KeyboardState keyState;
        private Rectangle playerBox;
        private Rectangle sButtonBox;
        private Rectangle eButtonBox;
        private Rectangle rButtonBox;
        private Rectangle infoButtonBox;
        private Rectangle controlButtonBox;
        public GameState GetGameState
        {
            get
            {
                return gameState;
            }
            set
            {
                gameState = value;
            }
        }
        public Rectangle PlayerBox
        {
            get
            {
                return playerBox;
            }
            set
            {
                playerBox = value;
            }
        }
        public Thread BackgroundThread
        {
            get
            {
                return backgroundThread;
            }
            set
            {
                backgroundThread = value;
            }
        }
        public MouseState MouseState
        {
            get
            {
                return mouseState;
            }
            set
            {
                mouseState = value;
            }
        }
        public MouseState PrevMouseState
        {
            get
            {
                return prevMouseState;
            }
            set
            {
                prevMouseState = value;
            }
        }
        public void LoadGame()
        {      
            keyState = new KeyboardState();
            //interactionPrompt = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/interactionPrompt.png"));
            if (prevGameState == GameState.Dead)
            {
                Console.WriteLine("TEST LOADGAME");
                prevGameState = GameState.Playing;
                replay();
                gameState = GameState.Playing;
                isLoading = false;
            }
            else if (!restart)
            {
                Thread.Sleep(1000);
                gameState = GameState.Playing;
                isLoading = false;
            }
        }
        public void replay()
        {
            restart = true;
            Initialize();
            LoadContent();
        }
        public GameState findGameState()
        {
            switch(physicsEngine.ACTIVELEVELFunc)
            {
                case 0:
                    {
                        return GameState.LVL0;               
                    }
                case 1:
                    {
                        return GameState.LVL1;                  
                    }
                case 2:
                    {
                        return GameState.LVL2;
                    }
                case 3:
                    {
                        return GameState.LVL3;
                    }
                case 4:
                    {
                        return GameState.LVL4;
                    }
                case 5:
                    {
                        return GameState.LVL5;
                    }
            }
            return GameState.LVL1;
        }
        public void MouseClicked(int x, int y)
        {           
            Rectangle mouseClickBox = new Rectangle(x, y, 10, 10);
            if(gameState == GameState.StartMenu)
            {            
                prevGameState = GameState.StartMenu;
                if(mouseClickBox.Intersects(sButtonBox))
                {               
                    gameState = GameState.Loading;
                    isLoading = false;                   
                }
                if(mouseClickBox.Intersects(eButtonBox))
                {
                    Exit();
                }
                if (mouseClickBox.Intersects(controlButtonBox))
                {
                    gameState = GameState.ControlMenu;
                }
            }  
            if(gameState == GameState.Playing)
            {
                if(mouseClickBox.Intersects(infoButtonBox))
                {
                    gameState = findGameState();
                }
            }         
            if(gameState == GameState.Paused)
            {
                prevGameState = GameState.Paused;
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if(mouseClickBox.Intersects(rButtonRec))
                {
                    
                    gameState = GameState.Playing;
                }
                if (mouseClickBox.Intersects(eButtonBox))
                {
                    Exit();
                }
                if(mouseClickBox.Intersects(controlButtonBox))
                {
                   
                    gameState = GameState.ControlMenu;
                }
            }
            if (gameState == GameState.ControlMenu)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    prevGameState = GameState.ControlMenu;
                    gameState = GameState.Playing;
                }              
            }
            if (gameState == GameState.DZMenu)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    prevGameState = GameState.DZMenu;
                    gameState = GameState.Playing;
                }
            }
            if (gameState == GameState.CurrentMenu)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    prevGameState = GameState.CurrentMenu;
                    gameState = GameState.Playing;
                }
            }
            if (gameState == GameState.MedicLvLMenu)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    prevGameState = GameState.MedicLvLMenu;
                    gameState = GameState.Playing;
                }
            }
            if (gameState == GameState.Dead)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    Console.WriteLine("replay gamestate dead");
                    prevGameState = GameState.Dead;
                    gameState = GameState.Loading;
                    isLoading = false;                                   
                }
            }
            if (gameState == GameState.LVL0 || gameState == GameState.LVL1 || gameState == GameState.LVL2 || gameState == GameState.LVL3
                || gameState == GameState.LVL4 || gameState == GameState.LVL5)
            {
                Rectangle rButtonRec = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 100, 20);
                if (mouseClickBox.Intersects(rButtonRec))
                {
                    gameState = GameState.Playing;
                }
            }           
        }
        //Grid give us our collision boxes and sprite images
        private GridLayout[] grid;
        private GridLayout[] shadeGrid;
        //All statically placed sprites are defined in MapContainer.cs
        //Point class defines our characters position at all times
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        //MonoStuff
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //Phyics Engine handles all movement, interaction, and collision
        private Physics physicsEngine;
        //MISC Constants. These constants are soley used for dictonary bounds
        private const int MAPSIZE = 240;
        private const int STATICSSIZE = 100;
        private const int STATICOFFSET = 4;
        private const int eventTime = 50;
        //sprite Container for map element sprites
        private Dictionary<int, Tuple<int, Texture2D, Rectangle>> spriteContainer;
        //Used for game state interactions and displaying inventory panel
        private List<ItemTile> inventoryContainer;
        //Dynamic sprites
        private Texture2D activePlayer;
        private Texture2D cursorDummyPassive;
        //Coordinates for keyboard and mouse
        private Point currMousePosition;
        private Point currPlayerPosition;
        private GameState gameState;
        private GameState prevGameState;
        //Getters & Setters
        public Texture2D CursorDummyPassive
        {
            get
            {
                return cursorDummyPassive;
            }
            set
            {
                cursorDummyPassive = value;
            }
        }
        public SpriteBatch SpriteBatch
        {
            get
            {
                return spriteBatch;
            }
            set
            {
                spriteBatch = value;
            }
        }
        public Texture2D ActivePlayer
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
        public GraphicsDeviceManager graphicsFunc
        {
            get
            {
                return graphics;
            }
        }
        public Texture2D activePlayerFunc
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
        public Point currPlayerPositionFunc
        {
            get
            {
                return currPlayerPosition;
            }
        }
        public Point currMousePositionFunc
        {
            get
            {
                return currMousePosition;
            }
        }
        public GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
            set
            {
                graphics = value;
            }
        }
        public GridLayout[] Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }
        public GridLayout[] GenMaps
        {
            get
            {
                return grid;
            }
        }
        public GridLayout[] ShadeGrid
        {
            get
            {
                return shadeGrid;
            }
            set
            {
                shadeGrid = value;
            }
        }
        public Physics PhysicsEngine
        {
            get
            {
                return physicsEngine;
            }
        }
        public List<ItemTile> InventoryContainer
        {
            get
            {
                return inventoryContainer;
            }
            set
            {
                inventoryContainer = value;
            }
        }
        public int getInvItem(int key)
        {
            for(int x = 0; x < inventoryContainer.Count; x++)
            {
                if(inventoryContainer[x].ItemKey.Equals(key))
                {
                    return x;
                }
            }
            return -1;
        }
        //Constructor
        public Game1()
        {
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
            //Initialize all data structures (These are essentially Tiles/Grid Boxes/Static images). 
            spriteContainer = new Dictionary<int, Tuple<int, Texture2D, Rectangle>>();
            inventoryContainer = new List<ItemTile>();         
            currPlayerPosition = new Point();
            currMousePosition = new Point();
            graphics = new GraphicsDeviceManager(this);
        }
        //TODO: Determine if we should generated levels on initialization or LoadContent
        protected override void Initialize()
        {                  
            //Bounding Box Of GUI Buttons       
            sButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 150);
            rButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 150);
            eButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);
            controlButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            //Bounding Box Of GUI Buttons
            sButtonBox = new Rectangle((int)sButtonPos.X, (int)sButtonPos.Y, 150, 30);
            rButtonBox = new Rectangle((int)rButtonPos.X, (int)rButtonPos.Y, 150, 30);
            eButtonBox = new Rectangle((int)eButtonPos.X, (int)eButtonPos.Y, 150, 30);
            infoButtonBox = new Rectangle(760, 40, 40, 40);
            controlButtonBox = new Rectangle((int)controlButtonPos.X, (int)controlButtonPos.Y, 150, 30);
            LoadGame();
            if (restart)
            {
                gameState = GameState.Loading;
                prevGameState = GameState.Loading;
                restart = false;
            }
            else
            {
                gameState = GameState.StartMenu;
                prevGameState = GameState.StartMenu;
            }
            //Load all levels of the game
            grid = new GridLayout[10];
            shadeGrid = new GridLayout[10];
            for (int x = 0; x < 10; x++)
            {
                grid[x] = new GridLayout(this);
                shadeGrid[x] = new GridLayout(this);
                //Generate grids based off layout matricies
                grid[x].generateGrid(x);
                foreach (ItemTile tile in grid[x].ItemTile)
                {
                    inventoryContainer.Add(tile);
                }           
                shadeGrid[x].generateGrid(-199);
            }           
            mouseState = Mouse.GetState();
            prevMouseState = mouseState;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            //sButton = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/start.jpg"));
            //eButton = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/exit.jpg"));
            loadScreen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/LoadingScreen.gif"));
            startScreen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/StartMenu.gif"));
            pauseScreen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/PauseMenu.png"));
            controlScreen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/ControlsMenu.png"));
            CurrentMenu = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/CurrentlyOutofOrderMenu.png"));
            DZMenu = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/DangerZoneMenu.png"));
            DeadMenu = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/DeathScreen.png"));
            //LVL0Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel1.png"));
            LVL1Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel1.png"));
            LVL2Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel2.png"));
            //LVL3Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel1.png"));
            //LVL4Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel1.png"));
            //LVL5Screen = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/InfoScreenLevel1.png"));
            //////////////////////PRE GAME LOADING           
            currPlayerPosition.X = 10;
            currPlayerPosition.Y = graphics.PreferredBackBufferHeight;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            /*
            Generate the initial physicsEngine, this engine processes all movement and collision detection for each map based on the collisionBox for that specificed map
            All events are handled within the physicsEngine, these events update container objects from this class, so be careful with what you modify.
            */
            physicsEngine = new Physics(this, 1, spriteBatch, graphics);
            physicsEngine.loadLevel(1);
            //Dynamic sprites that are annoying to handle within container element
            activePlayer = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/P1.png"));            
            //cursorDummyPassive = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/cursorSprite.png"));
            //cursorDummyActive = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/cursorDummyActive.png"));
            //////////////////////END PRE GAME LOADING
        }
        //TODO Do we need this for our game? :/
        protected override void UnloadContent()
        {
        }
        /* TODO
        2.) Implement Character Switching Method
        */
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            if (gameState == GameState.Loading && !isLoading)
            {
                backgroundThread = new Thread(LoadGame);
                isLoading = true;
                backgroundThread.Start();
            }
            mouseState = Mouse.GetState();           
            if (prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {              
                MouseClicked(mouseState.X, mouseState.Y);
            }
            prevMouseState = mouseState;
            if(gameState == GameState.Playing && isLoading)
            {
                LoadGame();
                isLoading = false;                           
            }            
            if (gameState == GameState.Playing)
            {
                if (keyState.IsKeyDown(Keys.P))
                {
                    prevGameState = GameState.Playing;
                    gameState = GameState.Paused;
                }
                //Runs all methods relted to handling user input events.
                //ProcessInputFunctions has a chaining effect to all other detections
                playerBox = new Rectangle(currPlayerPosition.X, currPlayerPosition.Y, activePlayer.Width, activePlayer.Height);
                physicsEngine.ProcessInputFunctions(Mouse.GetState(), Keyboard.GetState(), gameTime);
            }                       
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if(gameState == GameState.StartMenu)
            {
                spriteBatch.Draw(startScreen, new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),Color.White);            
            }
            if(gameState == GameState.Loading && physicsEngine.ACTIVELEVELFunc == 6)
            {
                spriteBatch.Draw(loadScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (loadScreen.Height / 2)), Color.White);
            }
            if (gameState == GameState.Loading)
            {
                spriteBatch.Draw(loadScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (loadScreen.Height / 2)), Color.White);
            }
            if (gameState == GameState.Playing)
            {
                //Load sprites
                drawSprites();
               // drawActiveSprites();            
                //Load mouse and player sprites
                spriteBatch.Draw(activePlayer, new Rectangle(currPlayerPosition.X, currPlayerPosition.Y - ActivePlayer.Height, ActivePlayer.Width, ActivePlayer.Height), Color.White);
                //spriteBatch.Draw(cursorDummyPassive, new Rectangle(currMousePosition.X, currMousePosition.Y, 15, 15), Color.White);
                //When inventory button is held down draw the inventory bag to the screen
                /*
                If a player pushes Key.T this function draws the animation
                NOTE TDOWNFunc only returns true when all conditions are met within the physics engine.
                */
                if (physicsEngine.TDOWNFunc && !physicsEngine.EventStates[1])
                {
                    physicsEngine.checkTrajectoryEvent1();
                }
                if (physicsEngine.ACTIVELEVELFunc == 6)
                {
                    physicsEngine.dangerZoneSimulation();
                }
                if(physicsEngine.InterActiveProximity && !physicsEngine.EventStates[2])
                {
                    spriteBatch.Draw(interactionPrompt, new Rectangle(160, graphics.PreferredBackBufferHeight - (160 + interactionPrompt.Height), interactionPrompt.Width, interactionPrompt.Height),Color.White);
                }
                if(physicsEngine.ACTIVELEVELFunc == 8)
                {
                    physicsEngine.ohmCollisionDetection();
                    physicsEngine.scoreWires();
                }
            }  
            if (gameState == GameState.Paused)
            {         
                spriteBatch.Draw(pauseScreen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.ControlMenu)
            {
                spriteBatch.Draw(controlScreen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.DZMenu)
            {
                spriteBatch.Draw(DZMenu, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.CurrentMenu)
            {
                spriteBatch.Draw(CurrentMenu, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.Dead)
            {
                spriteBatch.Draw(DeadMenu, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL0)
            {
                spriteBatch.Draw(LVL0Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL1)
            {
                spriteBatch.Draw(LVL1Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL2)
            {
                spriteBatch.Draw(LVL2Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL3)
            {
                spriteBatch.Draw(LVL3Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL4)
            {
                spriteBatch.Draw(LVL4Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            if (gameState == GameState.LVL5)
            {
                spriteBatch.Draw(LVL5Screen, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /*
        Draw all tile based sprites to the screen, since our map is a static background with tile overlay, this method is relatively efficient, if switched to a full tile map
        this function becomes costly.
        TODO: Determine if its worth going with a full tile map.
        */
        public void drawSprites()
        {
            foreach (BackGroundTile tile in grid[physicsEngine.ACTIVELEVELFunc].BackGroundTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }            
            foreach (CaveTopTile tile in grid[physicsEngine.ACTIVELEVELFunc].CaveTopTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (CraneTile tile in grid[physicsEngine.ACTIVELEVELFunc].CraneTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (CollisionTile tile in grid[physicsEngine.ACTIVELEVELFunc].CollisionTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (ItemTile tile in grid[physicsEngine.ACTIVELEVELFunc].ItemTile)
            {
                if (!tile.Collected)
                {
                    spriteBatch.Draw(tile.Image, tile.Box, Color.White);
                }
            }
            foreach (PlatFormTile tile in grid[physicsEngine.ACTIVELEVELFunc].PlatFormTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (LadderTile tile in grid[physicsEngine.ACTIVELEVELFunc].LadderTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (SlipperyTile tile in grid[physicsEngine.ACTIVELEVELFunc].SlipperyTiles)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (CrackedTile tile in grid[physicsEngine.ACTIVELEVELFunc].CrackedTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (OhmDropTile tile in grid[physicsEngine.ACTIVELEVELFunc].OhmDropTile)
            {

                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (WireTile tile in grid[physicsEngine.ACTIVELEVELFunc].WireTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (OhmTile tile in grid[physicsEngine.ACTIVELEVELFunc].OhmTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            /*
            if (physicsEngine.EventStates[1] == true && physicsEngine.ACTIVELEVELFunc >= 0 && physicsEngine.ACTIVELEVELFunc <= 5)
            {
                DrawShaders();
            }
            */
            foreach (SpikeTile tile in grid[physicsEngine.ACTIVELEVELFunc].SpikeTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (Tile tile in grid[physicsEngine.ACTIVELEVELFunc].GuiTiles)
            {
                if (tile.Active)
                {
                    spriteBatch.Draw(tile.Image, tile.Box, Color.White);
                }
            }
        }
        public void DrawShaders()
        {
            foreach(ShadedTile tile in shadeGrid[physicsEngine.ACTIVELEVELFunc].ShadeTile)
            {
                Rectangle playerShadeBox = new Rectangle(currPlayerPosition.X - (activePlayer.Width / 2) * 6, currPlayerPosition.Y  - ( (activePlayer.Height / 2) * 3) - physicsEngine.YOffSet, activePlayer.Height * 3, activePlayer.Width * 6);
                Rectangle playerShadeBox2 = new Rectangle(currPlayerPosition.X - (activePlayer.Width / 2) * 4, currPlayerPosition.Y - ((activePlayer.Height / 2) * 2) - physicsEngine.YOffSet, activePlayer.Height * 2, activePlayer.Width * 4);
                Rectangle playerShadeBox3 = new Rectangle(currPlayerPosition.X - (activePlayer.Width / 2) * 2, currPlayerPosition.Y - ((activePlayer.Height / 2) * 1) - physicsEngine.YOffSet, activePlayer.Height * 1, activePlayer.Width * 2);
                Rectangle playerFlashLightBox = new Rectangle(currPlayerPosition.X - (activePlayer.Width / 2) * 6, currPlayerPosition.Y - ((activePlayer.Height / 2) * 1) - physicsEngine.YOffSet, activePlayer.Height * 1, activePlayer.Width * 6);
                if (physicsEngine.FlashLightView)
                {
                    spriteBatch.Draw(tile.Image, tile.Box, Color.White * .10f);
                }
                else
                {
                    if (playerShadeBox3.Intersects(tile.Box))
                    {
                        spriteBatch.Draw(tile.Image, tile.Box, Color.White * .25f);
                    }
                    else if (playerShadeBox2.Intersects(tile.Box))
                    {
                        spriteBatch.Draw(tile.Image, tile.Box, Color.White * .50f);
                    }
                    else if (playerShadeBox.Intersects(tile.Box))
                    {
                        spriteBatch.Draw(tile.Image, tile.Box, Color.White * .75f);
                    }
                    else
                    {
                        spriteBatch.Draw(tile.Image, tile.Box, Color.White);
                    }
                }
            }
        }
        public bool checkInventoryItem(int itemNum)
        {
            foreach(ItemTile tile in inventoryContainer)
            {
                if(tile.ItemKey.Equals(itemNum) && tile.Collected && !tile.Consumed)
                {
                    return true;
                }
            }
            return false;
        }
        public void overloadedDraw(List<SpikeTile> temp)
        {
            for(int x = 0; x < temp.Count; x++)
            {                          
                spriteBatch.Draw(temp[x].Image, temp[x].Box, Color.White);
            }
        }
    }
}
