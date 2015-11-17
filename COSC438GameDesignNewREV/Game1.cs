using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace COSC438GameDesignNewREV
{
    public class Game1 : Game
    {
        //Grid give us our collision boxes and sprite images
        private GridLayout[] grid;
        //All statically placed sprites are defined in MapContainer.cs
        private Dictionary<int, Tuple<int, Texture2D, Rectangle>> STATICSPRITES;
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
        private const int STATICSSIZE = 50;
        private const int STATICOFFSET = 4;
        private const int eventTime = 50;
        //Timer to slow animations
        private int lastEventTime = 0;
        //sprite Container for map element sprites
        private Dictionary<int, Tuple<int, Texture2D, Rectangle>> spriteContainer;
        //Used for game state interactions and displaying inventory panel
        private Dictionary<int, ItemTile> inventoryContainer;
        //Dynamic sprites
        private Texture2D activePlayer;
        private int spriteSourceX, spriteSourceY;
        private Texture2D cursorDummy;
        private Texture2D inventory;
        private Texture2D darkbg;
        //Coordinates for keyboard and mouse
        private Point currMousePosition;
        private Point currPlayerPosition;
        //Getters & Setters
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
        public Dictionary<int, ItemTile> getINV
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
        public Dictionary<int, Tuple<int, Texture2D, Rectangle>> spriteContainerFunc
        {
            get
            {
                return spriteContainer;
            }
            set
            {
                spriteContainer = value;
            }
        }
        public void SetInv(int key, ItemTile tile)
        {
            if (inventoryContainer.ContainsKey(key))
            {
                inventoryContainer[key] = tile;
            }
            else
            {
                inventoryContainer.Add(key, tile);
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
        public Dictionary<int, Tuple<int, Texture2D, Rectangle>> STATICSPRITESFunc
        {
            get
            {
                return STATICSPRITES;
            }
            set
            {
                this.STATICSPRITES = value;
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
        //Constructor
        public Game1()
        {
            Content.RootDirectory = "Content";
            //Initialize all data structures (These are essentially Tiles/Grid Boxes/Static images). 
            spriteContainer = new Dictionary<int, Tuple<int, Texture2D, Rectangle>>();
            inventoryContainer = new Dictionary<int, ItemTile>();
            STATICSPRITES = new Dictionary<int, Tuple<int, Texture2D, Rectangle>>();
            currPlayerPosition = new Point();
            currMousePosition = new Point();
            graphics = new GraphicsDeviceManager(this);           
        }
        //TODO: Determine if we should generated levels on initialization or LoadContent
        protected override void Initialize()
        {
            //Load all levels of the game
            grid = new GridLayout[5];
            grid[0] = new GridLayout(this);
            grid[1] = new GridLayout(this);
            grid[2] = new GridLayout(this);
            grid[3] = new GridLayout(this);
            grid[4] = new GridLayout(this);
            //Generate grids based off layout matricies
            grid[0].generateGrid(0);
            grid[1].generateGrid(1);
            grid[2].generateGrid(2);
            grid[3].generateGrid(3);
            grid[4].generateGrid(4);
            base.Initialize();
        }
        protected override void LoadContent()
        {
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
            inventory = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/Inventory.png"));
            darkbg = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/DarkBG.png"));
            activePlayer = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/P1.png"));
            cursorDummy = Texture2D.FromStream(GraphicsDevice, TitleContainer.OpenStream(@"Images/cursorSprite.png"));
            //////////////////////END PRE GAME LOADING
        }
        //TODO Do we need this for our game? :/
        protected override void UnloadContent()
        {
            Content.Unload();
        }
        /* TODO
        2.) Implement Character Switching Method
        */
        protected override void Update(GameTime gameTime)
        {
            //Runs all methods relted to handling user input events.
            //ProcessInputFunctions has a chaining effect to all other detections
            physicsEngine.ProcessInputFunctions(Mouse.GetState(), Keyboard.GetState(), gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //Load sprites
            drawActiveSprites();
            drawSprites();
            //Load mouse and player sprites
            spriteBatch.Draw(activePlayer, new Rectangle(currPlayerPosition.X, currPlayerPosition.Y - ActivePlayer.Height, ActivePlayer.Width, ActivePlayer.Height), Color.White);
            spriteBatch.Draw(cursorDummy, new Rectangle(currMousePosition.X, currMousePosition.Y, 15, 15), new Rectangle(spriteSourceX, spriteSourceY, 40, 80), Color.White);
            //When inventory button is held down draw the inventory bag to the screen
            if(physicsEngine.IDOWNFunc)
            {
                loadInventory();
            }
            /*
            If a player pushes Key.T this function draws the animation
            NOTE TDOWNFunc only returns true when all conditions are met within the physics engine.
            */
            if (physicsEngine.TDOWNFunc)
            {
                drawActionEvent1(gameTime);
                physicsEngine.checkTrajectoryEvent1();
                physicsEngine.TDOWNFunc = false;
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
            foreach (PlatForm tile in grid[physicsEngine.ACTIVELEVELFunc].PlatFormTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (LadderTile tile in grid[physicsEngine.ACTIVELEVELFunc].LadderTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
            foreach (CrackedTile tile in grid[physicsEngine.ACTIVELEVELFunc].CrackedTile)
            {
                spriteBatch.Draw(tile.Image, tile.Box, Color.White);
            }
        }
        /*
        If we currently have a item in our bag, we should draw that item to the user when requestion
        */
        public void loadInventory()
        {   
            spriteBatch.Draw(inventory, new Rectangle(graphics.PreferredBackBufferWidth - inventory.Width, graphics.PreferredBackBufferHeight - inventory.Height, inventory.Width, inventory.Height), Color.White);
            for (int x = 0; x < 10; x++)
            {
                if (inventoryContainer.ContainsKey(x))
                {
                    if (inventoryContainer[x].Collected && !inventoryContainer[x].Equipt && !inventoryContainer[x].Consumed)
                    {                       
                        spriteBatch.Draw(inventoryContainer[x].BagImage, new Rectangle(grid[0].GetMap.bagLocs[x].X, grid[0].GetMap.bagLocs[x].Y, inventoryContainer[x].BagImage.Width, inventoryContainer[x].BagImage.Height), Color.White);
                    }
                }
            }
        }
        /*
        Sort through our statically placed sprites in order to determine which sprites are appropriate to draw to the screen
        */
        public void drawActiveSprites()
        {
            for (int y = 0; y < STATICSSIZE; y++)
            {
                if (STATICSPRITES.ContainsKey(y) && STATICSPRITES[y].Item1 == -1 || STATICSPRITES.ContainsKey(y) && y > 9 && y < 12)
                {
                    spriteBatch.Draw(STATICSPRITES[y].Item2, STATICSPRITES[y].Item3, Color.White);
                }
                if (STATICSPRITES.ContainsKey(y) && STATICSPRITES[y].Item1 == -2 && physicsEngine.ACTIVELEVELFunc == 2)
                {
                    spriteBatch.Draw(STATICSPRITES[y].Item2, STATICSPRITES[y].Item3, Color.White);
                }
            }
            spriteBatch.Draw(STATICSPRITES[physicsEngine.ACTIVELEVELFunc + STATICOFFSET].Item2, STATICSPRITES[physicsEngine.ACTIVELEVELFunc + STATICOFFSET].Item3, Color.White);            
        }
        /*
            Performs the animations for action event 1 taking place on level 1, each level will have its own action event and require a method similar to this.
            TODO: Fix the animation for this event so that it looks realistic.
        */
        public void drawActionEvent1(GameTime gameTime)
        {
            if (inventoryContainer.ContainsKey(3) && inventoryContainer[3].Equipt)
            {
                for (int x = 0; x < physicsEngine.AniBoxes.Count; x++)
                {                  
                        spriteBatch.Draw(inventoryContainer[3].Image, physicsEngine.AniBoxes[x], Color.White);
                        lastEventTime -= gameTime.ElapsedGameTime.Milliseconds;                
                }           
                inventoryContainer[3].Equipt = false;
            }           
        }
        public void tempAniDraw()
        {             
            spriteBatch.Draw(darkbg, new Rectangle(0,0,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);           
        }
    }
}
