using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceGame_Mono
{
    public class Player
    {
        private String name;
        private Vector2 location = new Vector2(600.0f - 64, 30.0f);
        private Texture2D skin;
        private int life = 5;
        private float firespeed = 7;
        public float rocketspeed = 4;

        public Player(String name)
        {
            this.name = name;
        }

        public String getName(){
            return name;
        }

        public Vector2 getLocation()
        {
            return location;
        }

        public float getX()
        {
            return location.X;
        }

        public float getY()
        {
            return location.Y;
        }

        public void setX(float x)
        {
            location.X = x;
        }

        public void setY(float y)
        {
            location.Y = y;
        }

        public void setLocation(Vector2 vec)
        {
            location = vec;
        }

        public Texture2D getSkin()
        {
            return skin;
        }

        public void setSkin(Texture2D texture)
        {
            this.skin = texture;
        }

        public int getLife()
        {
            return this.life;
        }

        public void setLife(int life)
        {
            this.life = life;
        }

        public void setFireSpeed(float speed)
        {
            this.firespeed = speed;
        }
        public float getFireSpeed()
        {
            return this.firespeed;
        }

        public void setRocketSpeed(float speed)
        {
            this.rocketspeed = speed;
        }
        public float getRocketSpeed()
        {
            return this.rocketspeed;
        }


        
    }
}
