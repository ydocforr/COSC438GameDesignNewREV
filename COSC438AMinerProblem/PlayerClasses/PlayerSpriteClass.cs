using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//Dunno Yet
namespace COSC438GameDesignNewREV
{
    public class PlayerSpriteClass
    {
        private Vector2 playerLocation;
        private bool swapAble;
        private int aCTIVELVL;
        private Texture2D sPrompt;
        public Texture2D Texture { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        private int currFrame;
        private int totalFrames;
        private int lastFrame = 0;
        private int timePerFrame = 80;
        public PlayerSpriteClass(Texture2D texture, int row, int col, int ACTIVELVL, Vector2 initLocs, Game1 game)
        {
            Texture = texture;
            Row = row;
            Col = col;
            currFrame = 0;
            totalFrames = Row * Col;
            this.aCTIVELVL = ACTIVELVL;
            this.playerLocation = initLocs;
            sPrompt = Texture2D.FromStream(game.GraphicsDevice, TitleContainer.OpenStream(@"Images/GUIImages/Sprompt.png"));
        }      
        public bool SwapAble
        {
            get
            {
                return swapAble;
            }
            set
            {
                swapAble = value;
            }
        }
        public int ACTIVELVL
        {
            get
            {
                return aCTIVELVL;
            }
            set
            {
                aCTIVELVL = value;
            }
        }
        public Vector2 PlayerLocation
        {
            get
            {
                return playerLocation;
            }
            set
            {
                playerLocation = value;
            }
        }
        public void UpdateStand(int dirKey)
        {
            if (dirKey == 1)
            {
                currFrame = 0;
            }
            else
            {
                currFrame = 1;
            }
        }
        public void UpdateClimb(GameTime gameTime)
        {
            currFrame = 4;
            lastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (lastFrame > timePerFrame)
            {
                lastFrame -= timePerFrame;
                currFrame++;
                lastFrame = 0;               
            }
        }
        public void UpdateRight(GameTime gameTime)
        {
            currFrame = 0;
            lastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (lastFrame > timePerFrame)
            {
                lastFrame -= timePerFrame;

                currFrame+= 2;
                lastFrame = 0;
            }
        }
        public void UpdateLeft(GameTime gameTime)
        {
            currFrame = 1;
            lastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (lastFrame > timePerFrame)
            {
                lastFrame -= timePerFrame;

                currFrame+= 2;
                lastFrame = 0;        
            }
        }
        public void UpdateJump(GameTime gameTime, int jumpDir)
        {
           
            if (jumpDir == 1)
            {
                currFrame = 2;
             
            }
            else if (jumpDir == 2)
            {
                currFrame = 3;         
            }                
        }
        public void UpdateUp(GameTime gameTime)
        {
            currFrame = 5;
            lastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (lastFrame > timePerFrame)
            {
                lastFrame -= timePerFrame;

                currFrame++;
                lastFrame = 0;        
            }
        }      
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Col;
            int height = Texture.Height / Row;
            int row = (int) ((float)currFrame / Col);
            int col = currFrame % Col;
            Rectangle sourceRectangle = new Rectangle(width * col, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);           
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);  
            if(swapAble)
            {
                spriteBatch.Draw(sPrompt, new Vector2(playerLocation.X, playerLocation.Y - 40), Color.White);
            }       
        }
    }
}
