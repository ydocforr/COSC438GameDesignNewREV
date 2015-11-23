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
        private MapContainer getMap;
        private List<ItemTile> itemTile;
        private List<PlatForm> platFormTile;
        private List<LadderTile> ladderTile;
        private List<CrackedTile> crackedTile;
        private List<WireTile> wireTile;
        private List<OhmTile> ohmTile;
        private List<OhmDropTile> ohmDropTile;
        private Game1 gameObj;
        //Getters & Setters
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
        public List<PlatForm> PlatFormTile
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
            this.itemTile = new List<ItemTile>();
            this.platFormTile = new List<PlatForm>();
            this.ladderTile = new List<LadderTile>();
            this.crackedTile = new List<CrackedTile>();
            this.wireTile = new List<WireTile>();
            this.ohmTile = new List<OhmTile>();
            this.ohmDropTile = new List<OhmDropTile>();
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
                        Console.WriteLine("Test Grid Gen 8");
                        populateGrid(getMap.CraneLevel);
                        break;
                    }
                case 9:
                    {
                        populateGrid(getMap.levelTwoPostEvent);
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
                        case -2:
                            {
                                crackedTile.Add(new CrackedTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -1:
                            {                               
                                ladderTile.Add(new LadderTile(0, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 0:
                            {                                
                                break;
                            }
                        case 1:
                            {
                                collisonTile.Add(new CollisionTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 2:
                            {
                                platFormTile.Add(new PlatForm(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case 3:
                            {
                                itemTile.Add(new ItemTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryINBAG[3], getMap.inventoryINBAG[3], gameObj));
                                break;
                            }
                        case 4:
                            {
                                itemTile.Add(new ItemTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryINBAG[4], getMap.inventoryINBAG[4], gameObj));
                                break;
                            }
                        case 7:
                            {
                                itemTile.Add(new ItemTile(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryINBAG[7], getMap.inventoryINBAG[7], gameObj));
                                break;
                            }
                        case 8:
                            {
                                itemTile.Add(new ItemTile(8, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryINBAG[8], getMap.inventoryINBAG[8], gameObj));
                                break;
                            }
                        //Drop Tiles For Scoring
                        case -80:
                            {
                                ohmDropTile.Add(new OhmDropTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -81:
                            {
                                ohmDropTile.Add(new OhmDropTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -82:
                            {
                                ohmDropTile.Add(new OhmDropTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }

                        //Wires
                        case -83:
                            {
                                wireTile.Add(new WireTile(1 , new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE),gameObj));
                                break;
                            }
                        case -84:
                            {
                                wireTile.Add(new WireTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -85:
                            {
                                wireTile.Add(new WireTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Green Resistors
                        case -101:
                            {
                                ohmTile.Add(new OhmTile(-3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -86:
                            {
                                ohmTile.Add(new OhmTile(1, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -87:
                            {
                                ohmTile.Add(new OhmTile(2, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -88:
                            {
                                ohmTile.Add(new OhmTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Red Resistors
                        case -89:
                            {
                                ohmTile.Add(new OhmTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -90:
                            {
                                ohmTile.Add(new OhmTile(5, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -91:
                            {
                                ohmTile.Add(new OhmTile(6, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -92:
                            {
                                ohmTile.Add(new OhmTile(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        //Yellow Resistors
                        case -93:
                            {
                                ohmTile.Add(new OhmTile(8, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -94:
                            {
                                ohmTile.Add(new OhmTile(9, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -95:
                            {
                                ohmTile.Add(new OhmTile(10, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                        case -96:
                            {
                                ohmTile.Add(new OhmTile(11, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), gameObj));
                                break;
                            }
                    }
                }
            }
        }
    }
}
