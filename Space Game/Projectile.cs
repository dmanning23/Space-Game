using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace SpaceGame_Mono
{
    public class Projectile
    {
        private float speed;
        private Vector2 location;
        private Texture2D skin;
        private int damage = 1;

        public Projectile(float speed , Vector2 startLocation, Texture2D skin)
        {
            this.speed = speed;
            this.location = startLocation;
            this.skin = skin;
        }

        public Vector2 getLocation()
        {
            return this.location;
        }

        public void setLocation(Vector2 vec)
        {
            this.location = vec;
        }

        public float getX()
        {
            return this.location.X;
        }

        public float getY()
        {
            return this.location.Y;
        }

        public void setX(float x)
        {
            location.X = x;
        }

        public void setY(float y)
        {
            location.Y = y;
        }

        public float getSpeed()
        {
            return this.speed;
        }

        public Texture2D getSkin()
        {
            return skin;
        }

        public void setSkin(Texture2D texture)
        {
            this.skin = texture;
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }
        public int getDamage()
        {
            return this.damage;
        }

        public void fire()
        {
            Main.instance.EnemyProjectileList.Add(this);
        }


    }
}
