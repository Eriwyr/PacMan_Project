using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PacMan_CHABRIER_REGNARD
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AnimatedObject wall;
        AnimatedObject bean;
        AnimatedObject pacMan;
        AnimatedObject ghost;
        int timer;
        int change;
        State stateDisplay;
        Game game;

        private const int VX = 31;
        private const int VY = 28;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            game = new Game();
            timer = 0;
            change = 0;
            stateDisplay = State.Nothing;
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();


            Texture2D textureWall = Content.Load<Texture2D>("mur");
            wall = new AnimatedObject(textureWall, new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new AnimatedObject(Content.Load<Texture2D>("bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            pacMan = new AnimatedObject(Content.Load<Texture2D>("pacman"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            ghost = new AnimatedObject(Content.Load<Texture2D>("ghost"), new Vector2(0f, 0f), new Vector2(20f, 20f));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            /* Vector2 pos = pacMan.getPosition();
             pos.X += 2;
             pos.Y += 2;
             pacMan.setPosition(pos);*/
            
            
            if(timer == 5)
            {
                timer = 0;
                State state = getInput();
               
                if(state != State.Nothing) // To keep in memory the last state 
                    stateDisplay = state;

                if (stateDisplay == State.Right)
                {
                    if (change % 2 == 0)
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanright"));
                    }
                    else
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanright1"));
                    }

                   
                }

                if (stateDisplay == State.Left)
                {
                    if (change % 2 == 0)
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanleft"));
                    }
                    else
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanleft1"));
                    }


                }

                if (stateDisplay == State.Up)
                {
                    if (change % 2 == 0)
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanup"));
                    }
                    else
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmanup1"));
                    }


                }

                if (stateDisplay == State.Down)
                {
                    if (change % 2 == 0)
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmandown"));
                    }
                    else
                    {
                        pacMan.setTexture(Content.Load<Texture2D>("pacmandown1"));
                    }


                }



                game.update(state);
                updateViews();
                base.Update(gameTime);

                if (change == 10) //iterator to switch between two texture => make animation
                    change = 0;
                change++;
                
            }
            timer++;
        }

        public void updateViews()
        {
            Position tmpPos = new Position(game.getPacman().getPosition().getPosX(), game.getPacman().getPosition().getPosY());

            pacMan.setPosition(new Vector2(tmpPos.getPosY()*20, tmpPos.getPosX()*20)); // We must invert x and y position beacause Vector2 has invert position.
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            Position tmppos;
            for (int x = 0; x < VX; x++)
            {
                for (int y = 0; y < VY; y++)
                {
                    tmppos = new Position(x, y);

                    if (game.getMap().checkElement(tmppos)== Element.Wall)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);

                        spriteBatch.Draw(wall.getTexture(), pos, Color.White);

                    }

                    if (game.getMap().checkElement(tmppos) == Element.Beans)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);

                        spriteBatch.Draw(bean.getTexture(), pos, Color.White);
                    }
                }
            }



            spriteBatch.Draw(pacMan.getTexture(), pacMan.getPosition(), Color.White);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        public State getInput()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                return State.Right;
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                return State.Left;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                return State.Up;
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                return State.Down;
            }

            return State.Nothing;
        }
    }
}
