using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGame_Mono
{
    public class Enemy
    {
        private EnemyClasses.Difficulty difficulty;
        private Texture2D skin;
        private Vector2 location;
        private int life;
        private float fireSpeed;
        private double FireCooldown;
        private int ReloadSpeed = 1300;
        private int SpecialAttackLoad = 0;
 
        public Enemy(EnemyClasses.Difficulty dif)
        {
            this.difficulty = dif;

            if (dif == EnemyClasses.Difficulty.EASY)
            {
                this.skin = Main.instance.EnemyEasyPicture;
                this.life = 2;
                this.fireSpeed = 8f;    
                this.location = new Vector2(Main.instance.rand(0, Main.instance.graphics.PreferredBackBufferWidth - 32), Main.instance.graphics.PreferredBackBufferHeight - 84);
            }
            if (dif == EnemyClasses.Difficulty.NORMAL)   
            {
                this.skin = Main.instance.EnemyNormalPicture;
                this.life = 5;
                this.fireSpeed = 8f;
                this.location = new Vector2(Main.instance.rand(0, Main.instance.graphics.PreferredBackBufferWidth - 128), Main.instance.graphics.PreferredBackBufferHeight - 84);
                
            }
            if (dif == EnemyClasses.Difficulty.HARD)
            {
                this.ReloadSpeed = 2000;
                this.skin = Main.instance.EnemyHardPicture;
                this.life = 20;
                this.fireSpeed = 7f;
                this.location = new Vector2(Main.instance.rand(0, Main.instance.graphics.PreferredBackBufferWidth - 256), Main.instance.graphics.PreferredBackBufferHeight - 84);
                
            }
            if (dif == EnemyClasses.Difficulty.HARDCORE)
            {
                this.ReloadSpeed = 500;
                this.skin = Main.instance.EnemyHardCorePicture;
                this.life = 25;
                this.fireSpeed = 15f;
                this.location = new Vector2(Main.instance.rand(0, Main.instance.graphics.PreferredBackBufferWidth - 256), Main.instance.graphics.PreferredBackBufferHeight - 84 - 128 - 32);
            }
           
        }

        public EnemyClasses.Difficulty getDifficulty()
        {
            return difficulty;
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

        public float getFireSpeed()
        {
            return this.fireSpeed;
        }

        public void setFireCooldown(double cooldown)
        {
            this.FireCooldown = cooldown;
        }

        public double getFireCooldown()
        {
            return this.FireCooldown;
        }

        public int getReloadSpeed()
        {
            return this.ReloadSpeed;
        }

        public void fire()
        {
            if (this.difficulty == EnemyClasses.Difficulty.EASY)
            {
                Projectile laser = new Projectile(fireSpeed, new Vector2(this.getX() + 8, this.getY()), Main.instance.LaserEnemyPicture);
                laser.fire();
            }

            if (this.difficulty == EnemyClasses.Difficulty.NORMAL)
            {
                Vector2 vec = this.location;
                vec.X = vec.X + 16;
                Projectile laser = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture); laser.fire();
                vec.X = vec.X + 128 - 32 - 16;
                laser = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture); laser.fire();
            }

            if (this.difficulty == EnemyClasses.Difficulty.HARD)
            {
                for (int i = 0; i < 260; i = i + 60)
                {
                    Vector2 vec = new Vector2(location.X + i, location.Y + 20);
                    Projectile laser = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture);
                    laser.fire();
                }

                for (int i = 30; i < 260; i = i + 60)
                {
                    Vector2 vec = new Vector2(location.X + i, location.Y);
                    Projectile laser = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture);
                    laser.fire();
                }

            }
            if (this.difficulty == EnemyClasses.Difficulty.HARDCORE)
            {

                Projectile laser = new Projectile(fireSpeed, new Vector2(location.X + 15, location.Y + 100), Main.instance.LaserEnemyPicture);
                laser.fire();

                laser = new Projectile(fireSpeed, new Vector2(location.X + 230, location.Y + 100), Main.instance.LaserEnemyPicture);
                laser.fire();

                if (SpecialAttackLoad == 5)
                {
                    Projectile rocket = new Projectile(4f, new Vector2(location.X + 80, location.Y + 30), Main.instance.RocketPicture);
                    rocket.fire();

                    rocket = new Projectile(4f, new Vector2(location.X + 160, location.Y + 30), Main.instance.RocketPicture);
                    rocket.fire();

                    SpecialAttackLoad = 0;

                }

                if (SpecialAttackLoad == 3)
                {
                    for (int i = 0; i < 260; i = i + 60)
                    {
                        Vector2 vec = new Vector2(location.X + i, location.Y + 20);
                        Projectile laser2 = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture);
                        laser2.fire();
                    }

                    for (int i = 30; i < 260; i = i + 60)
                    {
                        Vector2 vec = new Vector2(location.X + i, location.Y);
                        Projectile laser2 = new Projectile(fireSpeed, vec, Main.instance.LaserEnemyPicture);
                        laser2.fire();
                    }

                }

               SpecialAttackLoad++;
            }

            Main.instance.LaserLaunchSound.Play();
        }
    }
}