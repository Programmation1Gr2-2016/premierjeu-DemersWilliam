﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject ennemy;
        GameObject projectile;
        Texture2D Background;

        bool isLaunched = false;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ApplyChanges();
            //this.graphics.ToggleFullScreen();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            //mario
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("Mario.png");
            heros.position = heros.sprite.Bounds;
            //ninja
            ennemy = new GameObject();
            ennemy.estVivant = true;
            ennemy.vitesse = 5;
            ennemy.sprite = Content.Load<Texture2D>("ennemie.png");
            ennemy.position = ennemy.sprite.Bounds;
            ennemy.position.X=925;
            ennemy.position.Y =0 ;
            //projectile
            projectile = new GameObject();
            projectile.estVivant = true;
            projectile.vitesse = 5;
            projectile.sprite = Content.Load<Texture2D>("Shuriken.png");
            projectile.position = projectile.sprite.Bounds;
            //background
            Background = Content.Load<Texture2D>("Background.jpg");


            
            

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //ennemy lance un shuriken en appuyant sur espace

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                heros.estVivant = true;
                heros.position = heros.sprite.Bounds;
            }
            UpdateProjectile();
            UpdateEnnemy();
            UpdateHeros();
            UpdateCollisions();
            base.Update(gameTime);

    }
        protected void UpdateHeros()
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.X + heros.sprite.Bounds.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Right -heros.sprite.Bounds.Width;
            }
            if (heros.position.Y + heros.sprite.Bounds.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Bounds.Height;
            }
        }
        protected void UpdateEnnemy()
        {
            ennemy.position.X += ennemy.vitesse;
            if(ennemy.position.X +ennemy.sprite.Bounds.Width >fenetre.Right)
            {
                ennemy.position.X = fenetre.Right - ennemy.sprite.Bounds.Width;
                ennemy.vitesse = -5;
                ennemy.position.X +=ennemy.vitesse;
            }
            if(ennemy.position.X <fenetre.Left)
            {
                ennemy.position.X = fenetre.Left;
                ennemy.vitesse = 5;
            }
        }
        protected void UpdateProjectile()
        {
            if(isLaunched==true)
            {
                projectile.position.Y += projectile.vitesse;
            }
            else
            {
                projectile.position = ennemy.position;
                isLaunched = true;
            }
            if(projectile.position.Y +projectile.sprite.Bounds.Height > fenetre.Bottom)
            {
                isLaunched = false;
            }
        }
        protected void UpdateCollisions()
        {
            if(heros.position.Intersects(projectile.position))
            {
                heros.estVivant = false;
            }
            if (heros.position.Intersects(ennemy.position))
            {
                heros.estVivant = false;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
            if(heros.estVivant==true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }
            spriteBatch.Draw(ennemy.sprite, ennemy.position, Color.White);
            spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
