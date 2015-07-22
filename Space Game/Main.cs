using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceGame_Mono
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        public static Main instance;
        EnemyManager em = new EnemyManager();

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private FrameCounter _frameCounter = new FrameCounter();

        public double LaserCooldown = 0;
        public double EnemySpawnCooldown = 0;
        public double RocketCooldoown = 0;

        //Content
        public Texture2D PlayerNormalPicture;
        public Texture2D LaserNormalPicture;

        public Texture2D EnemyNormalPicture;
        public Texture2D EnemyEasyPicture;
        public Texture2D EnemyHardPicture;
        public Texture2D EnemyHardCorePicture;
        public Texture2D LaserEnemyPicture;
        public Texture2D RocketPicture;

        public static Texture2D Background;

        SpriteFont NormalFont;
        SpriteFont SmallFont;
        SpriteFont HealthFont;
        SpriteFont BigFont;

        public SoundEffect LaserLaunchSound;
        public SoundEffect RocketLaunchSound;
        public SoundEffect RocketExplodeSound;

        Rectangle mouse;
        MenuButton PlayButton = new MenuButton("Play", new Rectangle(500, 200, 120, 30));

        public SoundEffectInstance RocketLaunchSoundInstance;

        public Boolean Active = false;
        public Boolean Paused = false;

        public Player p = new Player("P1");

        public List<Projectile> ProjectileList = new List<Projectile>();
        public List<Enemy> EnemyList = new List<Enemy>();
        public List<Projectile> EnemyProjectileList = new List<Projectile>();

        public int kills = 0;

        MouseState OldMouseState = Mouse.GetState();

        public Main(): base()
        {
            instance = this;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;

            this.Window.Title = "Space-Game by fipso (Beta 0.5)";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Content
            PlayerNormalPicture = this.Content.Load<Texture2D>("PlayerNormal");
            LaserNormalPicture = this.Content.Load<Texture2D>("LaserNormal");
            LaserEnemyPicture = this.Content.Load<Texture2D>("LaserEnemy");
            EnemyNormalPicture = this.Content.Load<Texture2D>("EnemyNormal");
            EnemyEasyPicture = this.Content.Load<Texture2D>("EnemyEasy");
            EnemyHardPicture = this.Content.Load<Texture2D>("EnemyHard");
            EnemyHardCorePicture = this.Content.Load<Texture2D>("EnemyHardCore");
            RocketPicture = this.Content.Load<Texture2D>("Rocket");

            p.setSkin(PlayerNormalPicture);

            NormalFont = this.Content.Load<SpriteFont>("SpriteFont1");
            SmallFont = this.Content.Load<SpriteFont>("SpriteFont2");
            HealthFont = this.Content.Load<SpriteFont>("SpriteFont3");
            BigFont = this.Content.Load<SpriteFont>("SpriteFont4");


            Background = this.Content.Load<Texture2D>("Background");

            LaserLaunchSound = this.Content.Load<SoundEffect>("LaserLaunch");
            RocketLaunchSound = this.Content.Load<SoundEffect>("RocketLaunch");
            RocketExplodeSound = this.Content.Load<SoundEffect>("RocketExplode");

        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Wenn gestartet
            if (Active == true)
            {
                KeyboardState currentKeyboard = Keyboard.GetState();

                if (currentKeyboard.IsKeyDown(Keys.A) && p.getX() >= 5)
                {
                    p.setX(p.getX() - 7.0f);
                }

                if (currentKeyboard.IsKeyDown(Keys.D) && p.getX() <= graphics.PreferredBackBufferWidth - 64)
                {
                    p.setX(p.getX() + 7.0f);
                }

                if (currentKeyboard.IsKeyDown(Keys.Space))
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds - LaserCooldown > 250 - ((p.getFireSpeed() - 7) * 10))
                    {
                        Vector2 vec = p.getLocation();
                        vec.X = p.getX() + 18f;
                        vec.Y = p.getY() + 64f;
                        ProjectileList.Add(new Projectile(p.getFireSpeed(), vec, LaserNormalPicture));
                        LaserCooldown = gameTime.TotalGameTime.TotalMilliseconds;
                        LaserLaunchSound.Play();

                    }

                }

                if (currentKeyboard.IsKeyDown(Keys.Enter))
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds - RocketCooldoown > 10000 - ((p.getRocketSpeed() - 4) * 1500))
                    {
                        Vector2 vec = p.getLocation();
                        vec.X = p.getX() + 24f;
                        vec.Y = p.getY() + 64f;
                        Projectile rocket = new Projectile(p.getRocketSpeed(), vec, RocketPicture);
                        rocket.setDamage(10);
                        rocket.setSkin(RocketPicture);
                        ProjectileList.Add(rocket);

                        SoundEffectInstance fx = RocketLaunchSound.CreateInstance();
                        fx.Play();
                        RocketLaunchSoundInstance = fx;

                        RocketCooldoown = gameTime.TotalGameTime.TotalMilliseconds;
                    }


                }
                if (currentKeyboard.IsKeyDown(Keys.P) || currentKeyboard.IsKeyDown(Keys.Escape))
                {
                    Active = false;
                    Paused = true;
                }


                if (gameTime.TotalGameTime.TotalMilliseconds - EnemySpawnCooldown > 5000)
                {
                    int random = rand(1, 4);

                    if (random == 1)
                    {
                        Enemy enemy = new Enemy(EnemyClasses.Difficulty.EASY);
                        em.spawnEnemy(enemy);
                        EnemySpawnCooldown = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    else if (random == 2)
                    {
                        Enemy enemy = new Enemy(EnemyClasses.Difficulty.NORMAL);
                        em.spawnEnemy(enemy);
                        EnemySpawnCooldown = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    else if (random == 3)
                    {
                        Enemy enemy = new Enemy(EnemyClasses.Difficulty.HARD);
                        em.spawnEnemy(enemy);
                        EnemySpawnCooldown = gameTime.TotalGameTime.TotalMilliseconds;

                    }

                    int HardCoreRandom = rand(0, 10);

                    if (kills >= 15 && HardCoreRandom <= 5)
                    {
                        Enemy enemy = new Enemy(EnemyClasses.Difficulty.HARDCORE);
                        em.spawnEnemy(enemy);
                        EnemySpawnCooldown = gameTime.TotalGameTime.TotalMilliseconds;
                    }

                }

                for (int i = ProjectileList.Count - 1; i >= 0; i--)
                {
                    Projectile projectile = ProjectileList[i];
                    projectile.setY(projectile.getY() + projectile.getSpeed());
                    if (projectile.getY() > graphics.PreferredBackBufferWidth)
                    {
                        ProjectileList.Remove(projectile);
                    }

                    //Kollisions abfrage bei treff von feind
                    for (int j = 0; j < EnemyList.Count; j++)
                    {
                        Enemy enemy = EnemyList[j];
                        if (HitManager.KollisionSprite(projectile.getSkin(), projectile.getLocation(), enemy.getSkin(), enemy.getLocation()))
                        {
                            enemy.setLife(enemy.getLife() - projectile.getDamage());
                            if (enemy.getLife() <= 0)
                            {
                                EnemyList.Remove(enemy);
                                kills++;
                                //Mehr leben bei kill von feind auf : HARD
                                //Und schneller ballern
                                if (enemy.getDifficulty() == EnemyClasses.Difficulty.HARD)
                                {
                                    p.setLife(p.getLife() + 1);
                                    p.setFireSpeed(p.getFireSpeed() + 0.5f);
                                }
                                //Und schneller raketen nachladen
                                //bei HARD CORE
                                if (enemy.getDifficulty() == EnemyClasses.Difficulty.HARDCORE)
                                {
                                    p.setRocketSpeed(p.getRocketSpeed() + 1);
                                    p.setLife(p.getLife() + 2);
                                    p.setFireSpeed(p.getFireSpeed() + 2);
                                }
                            }
                            ProjectileList.Remove(projectile);

                            //Soud bei racketet treffer
                            if (projectile.getSkin() == RocketPicture)
                            {
                                RocketLaunchSoundInstance.Stop();
                                RocketExplodeSound.Play();
                            }
                        }

                    }

                }

                //Alle feinde feuern
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    Enemy enemy = EnemyList[j];
                    if (gameTime.TotalGameTime.TotalMilliseconds - enemy.getFireCooldown() > enemy.getReloadSpeed())
                    {
                        enemy.fire();
                        enemy.setFireCooldown(gameTime.TotalGameTime.TotalMilliseconds);
                    }

                }

                //Feind trifft Spieler
                for (int i = EnemyProjectileList.Count - 1; i >= 0; i--)
                {
                    Projectile projectile = EnemyProjectileList[i];
                    projectile.setY(projectile.getY() - projectile.getSpeed());
                    if (projectile.getY() == graphics.PreferredBackBufferHeight)
                    {
                        EnemyProjectileList.Remove(projectile);
                    }
                    if (HitManager.KollisionSprite(projectile.getSkin(), projectile.getLocation(), p.getSkin(), p.getLocation()))
                    {
                        p.setLife(p.getLife() - projectile.getDamage());
                        if (p.getLife() <= 0)
                        {
                            Exit();
                        }
                        EnemyProjectileList.Remove(projectile);
                    }
                }

                //Spieler Prijectile triff auf gegner projectile
                for (int i = ProjectileList.Count - 1; i >= 0; i--)
                {
                    Projectile projectile = ProjectileList[i];

                    for (int j = EnemyProjectileList.Count - 1; j >= 0; j--)
                    {
                        Projectile eProjectile = EnemyProjectileList[j];
                        if (HitManager.KollisionSprite(projectile.getSkin(), projectile.getLocation(), eProjectile.getSkin(), eProjectile.getLocation()))
                        {
                            EnemyProjectileList.Remove(eProjectile);
                            ProjectileList.Remove(projectile);

                            if (projectile.getSkin() == RocketPicture || eProjectile.getSkin() == RocketPicture)
                            {
                                if (RocketLaunchSoundInstance != null)
                                {
                                    if (RocketLaunchSoundInstance.State == SoundState.Playing) RocketLaunchSoundInstance.Stop();
                                }
                            }
                        }

                    }

                }
            }
            //Wenn das spiel nicht Läuft
            else
            {
                mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);
                if (HitManager.KollisionRechteck(mouse, PlayButton.getRectangle()))
                {
                    if (Mouse.GetState().LeftButton != OldMouseState.LeftButton)
                    {
                        Active = true;
                        Paused = false;
                    }
                }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(p.getSkin(), p.getLocation(), Color.White);

            if (Active == true)
            {
                int v = 0;
                for (int i = 1; i <= p.getLife(); i++)
                {
                    spriteBatch.DrawString(HealthFont, "■ ", new Vector2(p.getX() + v, p.getY() - 22), Color.LightGreen);
                    v = v + 11;
                }
                foreach (Projectile projectile in ProjectileList)
                {
                    spriteBatch.Draw(projectile.getSkin(), projectile.getLocation(), Color.White);
                }

                foreach (Projectile projectile in EnemyProjectileList)
                {
                    if (projectile.getSkin() == LaserEnemyPicture)
                        spriteBatch.Draw(projectile.getSkin(), projectile.getLocation(), Color.White);
                    if (projectile.getSkin() == RocketPicture)
                        spriteBatch.Draw(projectile.getSkin(), projectile.getLocation(), null, Color.White, (float)Math.PI, new Vector2(projectile.getSkin().Width, projectile.getSkin().Height), 1, SpriteEffects.None, 0);

                }
                foreach (Enemy enemy in EnemyList)
                {
                    spriteBatch.Draw(enemy.getSkin(), enemy.getLocation(), Color.White);
                    int ö = 0;
                    for (int i = 1; i <= enemy.getLife(); i++)
                    {
                        spriteBatch.DrawString(HealthFont, "■ ", new Vector2(enemy.getX() + ö, enemy.getY() - 22), Color.Red);
                        ö = ö + 11;
                    }

                }


                spriteBatch.DrawString(NormalFont, "Kills: " + kills.ToString(), new Vector2(1, 10), Color.Red);
                spriteBatch.DrawString(SmallFont, "Fire Speed: " + p.getFireSpeed().ToString(), new Vector2(1, 30), Color.Red);

                //Next Rocket
                if (gameTime.TotalGameTime.TotalMilliseconds - RocketCooldoown - 10000 + ((p.getRocketSpeed() - 4) * 1500) < 0)
                    spriteBatch.DrawString(SmallFont, "Next Rocket in: " + Convert.ToString(gameTime.TotalGameTime.TotalMilliseconds - RocketCooldoown - 10000 + ((p.getRocketSpeed() - 4) * 1500)), new Vector2(1, 42), Color.Red);
                else spriteBatch.DrawString(SmallFont, "Next Rocket in: NOW", new Vector2(1, 42), Color.Red);
                spriteBatch.DrawString(SmallFont, "Rocket Speed: " + p.getRocketSpeed().ToString(), new Vector2(1, 52), Color.Red);

                //Fps Counter

                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _frameCounter.Update(deltaTime);
                var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
                spriteBatch.DrawString(SmallFont, fps, new Vector2(1, 1), Color.Red);
            }
            //Wenn nicht Aktiv / Läuft
            else
            {
                //Male menü
                Color color = Color.Red;

                mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);
                if (HitManager.KollisionRechteck(mouse, PlayButton.getRectangle()))
                {
                    color = Color.Green;
                }

                String ButtonText = PlayButton.getText();
                if (Paused == true)
                {
                    ButtonText = "Resume";
                }
                spriteBatch.DrawString(BigFont, ButtonText, new Vector2(500f, 200f), color);
            }

            MouseState ms = Mouse.GetState();
            float mouseX = ms.X;
            float mouseY = ms.Y;
            Vector2 mouseVec = new Vector2(mouseX, mouseY);
            spriteBatch.DrawString(NormalFont, "X", mouseVec, Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int rand(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);

        }

    }
}
