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
    enum GameState { playing, relaunch }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AnimatedObject wall;
        AnimatedObject bean;
        AnimatedObject bigBean;
        AnimatedObject pacMan;
        AnimatedObject ghost;
        BufferInput buffer;
        KeyboardState oldState;
        GameState gameState;
        int timer;
        int change;
        int changeDead;
        int ghostDefensiveTime;
        bool pacManIsDead;
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
            changeDead = 0;
            ghostDefensiveTime = 0;
            stateDisplay = State.Nothing;
            buffer = new BufferInput();
            gameState = GameState.playing;
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
            bigBean = new AnimatedObject(Content.Load<Texture2D>("gros_bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
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

            // TODO: Add your update logic hereS
            State state = getInput();

            if(state == State.Quit)
            {
                this.Exit();
            }

            if(state != State.Nothing)
                buffer.push(state);

            if (gameState == GameState.relaunch)// If we relaunch the game after pacman was dead
            {
                if (timer == 12)//We play animation more slower
                {
                    timer = 0;

                    game.getGhost().getPosition().setPosXY(15, 15); //We send ghost to the spawn
                    switch (changeDead) // We play the dead animation of pacman
                    {
                        case 0:
                            pacMan.setTexture(Content.Load<Texture2D>("Mort0"));
                            changeDead++;
                            break;
                        case 1:
                            pacMan.setTexture(Content.Load<Texture2D>("Mort1"));
                            changeDead++;
                            break;
                        case 2:
                            pacMan.setTexture(Content.Load<Texture2D>("Mort2"));
                            changeDead++;
                            break;
                        case 3:
                            pacMan.setTexture(Content.Load<Texture2D>("Mort3"));
                            changeDead = 0;
                            game.getPacman().getPosition().setPosXY(17, 14);// When we are done we send pacman to his spawn
                            gameState = GameState.playing; // We launch the game again
                            break;

                    }
                }
            }
            else // if pacman is not dead
            {
                if (timer == 6)
                {
                    timer = 0;
                    State currentState = game.getPacman().getState();
                    if (currentState != State.Nothing) // To keep in memory the last state 
                        stateDisplay = currentState;

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

                    game.computeGhosts();
                    if (game.isTouched())// If pacman and ghost enter in colision 
                    {
                        gameState = GameState.relaunch;// We change the game state
                    }
                    if (buffer.getCount() > 0)
                    {
                        if (game.checkPacman(buffer.getHead()))
                        {
                            game.pacmanMovement(buffer.pop()); 
                            buffer.clear();
                             
                        }
                        else
                        {
                            game.pacmanMovement(State.Nothing);
                        }
                    }
                    else
                    {
                        game.pacmanMovement(State.Nothing);
                    }
                    Console.WriteLine(game.getMap().checkElement(game.getPacman().getPosition()));
                    if (game.getMap().checkElement(game.getPacman().getPosition()) == Element.BigBeans) //We check if the element is a bigBean
                    {
                        Console.WriteLine("OI¨QJSFS");
                        game.getGhost().setDefensive(); // we set ghost to vulnerable state
                    }

                    if(game.getGhost().getAggressivity() == Aggressivity.defensive) // If pacman eat bigBean
                    {
                        if(ghostDefensiveTime == 15)//We make ghost vulnerable during a time
                        {
                            ghostDefensiveTime = 0;
                            game.getGhost().setAgresive();
                            ghost.setTexture(Content.Load<Texture2D>("ghost"));//we reset orginial texture
                        }
                        else
                        {// if ghost is vulnerabel, we set his sprite to afraidGhost
                            if(ghostDefensiveTime % 2 == 0)
                            {
                                ghost.setTexture(Content.Load<Texture2D>("FamtomePeur0"));
                            }
                            else
                            {
                                ghost.setTexture(Content.Load<Texture2D>("FamtomePeur1"));
                            }

                            ghostDefensiveTime++;//We increment the time
                        }
                    }
                    if (game.isTouched())// If pacman and ghost enter in colision 
                    {
                        gameState = GameState.relaunch;// We change the game state
                    }
                    game.ghostMovement(); // We move the ghost

                    if (game.isTouched())// If pacman and ghost enter in colision 
                    {
                        gameState = GameState.relaunch;// We change the game state
                    }

                    if(!(game.getPacman().getState() == State.Nothing))
                    {
                        if (change == 10) //iterator to switch between two texture => make animation
                            change = 0;
                        change++;
                    }

                }

                updateViews();
                base.Update(gameTime);
                
            }
            

            timer++; 
        }

        public void updateViews()
        {
            Position tmpPos = new Position(game.getPacman().getPosition().getPosX(), game.getPacman().getPosition().getPosY());

            pacMan.setPosition(new Vector2(tmpPos.getPosY()*20, tmpPos.getPosX()*20)); // We must invert x and y position beacause Vector2 has invert position.
            tmpPos = new Position(game.getGhost().getPosition().getPosX(), game.getGhost().getPosition().getPosY());
            ghost.setPosition(new Vector2(tmpPos.getPosY() * 20, tmpPos.getPosX() * 20));
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
            int x = 0;
            int y = 0;
            for (x = 0; x < VX; x++)
            {
                for (y = 0; y < VY; y++)
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

                    if (game.getMap().checkElement(tmppos) == Element.BigBeans)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);

                        spriteBatch.Draw(bigBean.getTexture(), pos, Color.White);
                    }
                }
            }


            
            spriteBatch.Draw(pacMan.getTexture(), pacMan.getPos(), Color.White);

            if (gameState == GameState.playing) // If pacman is dead we dont display ghosts during this time 
            {
                spriteBatch.Draw(ghost.getTexture(), ghost.getPos(), Color.White);
            }
            
            base.Draw(gameTime);
            spriteBatch.End();
        }

        public State getInput()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape) )
            {
                return State.Quit;
            }

            if (keyboard.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right))
            {
                return State.Right;
            }

            if (keyboard.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left))
            {
                return State.Left;
            }

            if (keyboard.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up))
            { 
                return State.Up;
            }

            if (keyboard.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down))
            {
                return State.Down;
            }
            oldState = keyboard;
            return State.Nothing;
        }

        public void Quit()
        {
            this.Exit();
        }
    }
}
