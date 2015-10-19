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
                        populateGrid(getMap.levelOnePostEvent);
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
                                itemTile.Add(new ItemTile(3, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryONMAP[3], getMap.inventoryINBAG[3], gameObj));
                                break;
                            }
                        case 4:
                            {
                                itemTile.Add(new ItemTile(4, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryONMAP[4], getMap.inventoryINBAG[4], gameObj));
                                break;
                            }
                        case 7:
                            {
                                itemTile.Add(new ItemTile(7, new Rectangle(x * SIZE, y * SIZE, SIZE, SIZE), getMap.inventoryONMAP[7], getMap.inventoryINBAG[7], gameObj));
                                break;
                            }
                    }
                }
            }
        }
    }
}
