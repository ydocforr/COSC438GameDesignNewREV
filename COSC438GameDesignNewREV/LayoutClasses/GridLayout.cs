using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace COSC438GameDesignNewREV
{
    public class GridLayout
    {
        private const int SIZE = 40;
        //All Levels Standardized to 800W, 400H
        //Each grid will be 40x40 AKA 20Cols,12Rows
        private List<CollisionTile> collisonTile;
        private List<SpikeTile> spikeTile;
        private List<CraneTile> craneTile;
        private List<Tile> guiTiles;
        private List<SlipperyTile> slipperyTiles;
        private List<ResourceTiles> resourceTile;
        private List<BackGroundTile> backGroundTile;
        private List<ScoreTiles> scoreTiles;
        private List<CaveTopTile> caveTopTile;
        private MapContainer getMap;
        private List<ItemTile> itemTile;
        private List<PlatFormTile> platFormTile;
        private List<LadderTile> ladderTile;
        private List<CrackedTile> crackedTile;
        private List<WireTile> wireTile;
        private List<OhmTile> ohmTile;
        private List<OhmDropTile> ohmDropTile;
        private List<ShadedTile> shadeTile;
        private Game1 gameObj;
        //Getters & Setters
        public List<SlipperyTile> SlipperyTiles
        {
            get
            {
                return slipperyTiles;
            }
            set
            {
                slipperyTiles = value;
            }
        }
        public List<SpikeTile> SpikeTile
        {
            get
            {
                return spikeTile;
            }
            set
            {
                spikeTile = value;
            }
        }
        public List<CraneTile> CraneTile
        {
            get
            {
                return craneTile;
            }
            set
            {
                craneTile = value;
            }
        }
        public List<ScoreTiles> ScoreTiles
        {
            get
            {
                return scoreTiles;
            }
            set
            {
                scoreTiles = value;
            }
        }
        public List<ResourceTiles> ResourceTiles
        {
            get
            {
                return resourceTile;
            }
            set
            {
                resourceTile = value;
            }
        }            
        public MapContainer GetMap
        {
            get
            {
                return getMap;
            }
            set
            {
                getMap = value;
            }
        }
        public List<Tile> GuiTiles
        {
            get
            {
                return guiTiles;
            }
            set
            {
                guiTiles = value;
            }
        }
        public List<ShadedTile> ShadeTile
        {
            get
            {
                return shadeTile;
            }
            set
            {
                shadeTile = value;
            }
        }
        public List<BackGroundTile> BackGroundTile
        {
            get
            {
                return backGroundTile;
            }
            set
            {
                backGroundTile = value;
            }
        }
        public List<CaveTopTile> CaveTopTile
        {
            get
            {
                return caveTopTile;
            }
            set
            {
                caveTopTile = value;
            }
        }
        public List<CollisionTile> CollisionTile
        {
            get
            {
                return collisonTile;
            }
            set
            {
                collisonTile = value;
            }
        }
        public List<WireTile> WireTile
        {
            get
            {
                return wireTile;
            }
            set
            {
                wireTile = value;
            }
        }
        public List<OhmTile> OhmTile
        {
            get
            {
                return ohmTile;
            }
            set
            {
                ohmTile = value;
            }
        }
        public List<OhmDropTile> OhmDropTile
        {
            get
            {
                return ohmDropTile;
            }
            set
            {
                ohmDropTile = value;
            }
        }
        public List<ItemTile> ItemTile
        {
            get
            {
                return itemTile;
            }
            set
            {
                itemTile = value;
            }
        }
        public List<PlatFormTile> PlatFormTile
        {
            get
            {
                return platFormTile;
            }
            set
            {
                platFormTile = value;
            }
        }
        public List<LadderTile> LadderTile
        {
            get
            {
                return ladderTile;
            }
            set
            {
                ladderTile = value;
            }
        }
        public List<CrackedTile> CrackedTile
        {
            get
            {
                return crackedTile;
            }
            set
            {
                crackedTile = value;
            }
        }
        public void ClearTiles()
        {
            collisonTile.Clear();
            itemTile.Clear();
            platFormTile.Clear();
        }
        public GridLayout(Game1 gameObj)
        {
            this.gameObj = gameObj;
        }
        //Used to create the maps for each level, allocates list of all relevant tiles
        public void generateGrid(int levelNum)
        {         
            getMap = new MapContainer(gameObj);           
            this.collisonTile = new List<CollisionTile>();
            this.spikeTile = new List<SpikeTile>();
            this.craneTile = new List<CraneTile>();
            this.scoreTiles = new List<ScoreTiles>();
            this.slipperyTiles = new List<SlipperyTile>();
            this.resourceTile = new List<ResourceTiles>();
            this.guiTiles = new List<Tile>();
            this.caveTopTile = new List<CaveTopTile>();
            this.backGroundTile = new List<BackGroundTile>();
            this.itemTile = new List<ItemTile>();
            this.platFormTile = new List<PlatFormTile>();
            this.ladderTile = new List<LadderTile>();
            this.crackedTile = new List<CrackedTile>();
            this.wireTile = new List<WireTile>();
            this.ohmTile = new List<OhmTile>();
            this.ohmDropTile = new List<OhmDropTile>();
            this.shadeTile = new List<ShadedTile>();
            switch (levelNum)
            {
                case 0:
                    {
                        populateGrid(getMap.levelZero);
                        break;
                    }
                case 1:
                    {
                        populateGrid(getMap.levelOne);
                        break;
                    }
                case 2:
                    {
                        populateGrid(getMap.levelTwo);
                        break;
                    }
                case 3:
                    {
                        populateGrid(getMap.levelThree);
                        break;
                    }
                case 4:
                    {
                        populateGrid(getMap.levelFour);
                        break;
                    }
                case 5:
                    {
                        populateGrid(getMap.levelFive);
                        break;
                    }
                case 6:
                    {
                        populateGrid(getMap.dangerZone);
                        break;
                    }
                case 7:
                    {
                        populateGrid(getMap.levelOnePostEvent);
                        break;
                    }
                case 8:
                    {
                        populateGrid(getMap.CraneLevel);
                        break;
                    }
                case 9:
                    {
                        populateGrid(getMap.levelTwoPostEvent);
                        break;
                    }
                case -199:
                    {
                        populateGrid(getMap.shaderMap);
                        break;
                    }                   
            }
        }
        //Reads matrix and creates appropriate tiles.
        public void populateGrid(int[,] tempArr)
        {         
            int x, y;
            for (x = 0; x < tempArr.GetLength(1); x++)
            {
                for (y = 0; y < tempArr.GetLength(0); y++)
                {
                    switch (tempArr[y, x])
                    {
                        //NOTE: Adding a background tile underneath every other tile type is not an ideal solution, but since some tiles are going to be removed later on
                        //its simpler to just have a background tile incase, since background tiles are always drawn first, this isnt noticable in any way.
                        case -38:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                slipperyTiles.Add(new SlipperyTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -37:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                spikeTile.Add(new SpikeTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -36:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                spikeTile.Add(new SpikeTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -35:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                spikeTile.Add(new SpikeTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -34:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new PromptTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -33:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(8, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -32:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -31:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(6, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -30:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -29:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -28:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -27:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -26:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                craneTile.Add(new CraneTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -25:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -24:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -23:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -22:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -21:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -20:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -19:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new InfoTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -18:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ResourceTiles(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -17:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ResourceTiles(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -16:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ResourceTiles(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));                         
                                break;
                            }
                        case -15:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ScoreTiles(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new ScoreTiles(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -14:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ScoreTiles(6, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new ScoreTiles(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -13:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ScoreTiles(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new ScoreTiles(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -12:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                //These are health bar ties, they occupy the same grid space, one is just not always visible depending on HP
                                guiTiles.Add(new ScoreTiles(8, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new ScoreTiles(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -11:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -10:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -9:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -8:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -7:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -6:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new InventoryDisplayTileItem(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -5:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new DownTop(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -4:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                guiTiles.Add(new DownBot(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -3:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                caveTopTile.Add(new CaveTopTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -2:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                crackedTile.Add(new CrackedTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -1:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ladderTile.Add(new LadderTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 0:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 1:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                collisonTile.Add(new CollisionTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 2:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                platFormTile.Add(new PlatFormTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 3:
                            {
                                //Mining Axe
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                itemTile.Add(new ItemTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.Item1,  gameObj));
                                break;
                            }
                        case 4:
                            {
                                //FlashLight
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                itemTile.Add(new ItemTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.Item2, gameObj));
                                break;
                            }
                        case 7:
                            {
                                //Batteries
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                itemTile.Add(new ItemTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.Item3,  gameObj));
                                break;
                            }
                        case 8:
                            {
                                //MedKit
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                itemTile.Add(new ItemTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.Item4,  gameObj));
                                break;
                            }
                        case 9:
                            {
                                //Engineer ToolKit
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                itemTile.Add(new ItemTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.Item5,  gameObj));
                                break;
                            }
                        //Drop Tiles For Scoring
                        case -79:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmDropTile.Add(new OhmDropTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Drop Tiles For Scoring
                        case -80:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                slipperyTiles.Add(new SlipperyTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -81:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmDropTile.Add(new OhmDropTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -82:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmDropTile.Add(new OhmDropTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }

                        //Wires
                        case -83:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                wireTile.Add(new WireTile(1 , new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE),gameObj));
                                break;
                            }
                        case -84:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                wireTile.Add(new WireTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -85:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                wireTile.Add(new WireTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Green Resistors
                        case -101:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(-3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -86:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -87:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -88:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Red Resistors
                        case -89:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -90:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -91:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(6, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -92:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Yellow Resistors
                        case -93:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(8, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -94:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(9, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -95:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(10, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -96:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                ohmTile.Add(new OhmTile(11, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -199:
                            {
                                backGroundTile.Add(new BackGroundTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                shadeTile.Add(new ShadedTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                    }
                }
            }
        }
    }
}
