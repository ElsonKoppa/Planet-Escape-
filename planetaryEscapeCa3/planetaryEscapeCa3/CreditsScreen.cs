#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using GameStateManagement;
using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace planetaryEscapeCa3
{
    /// <summary>
    /// This screen gives credits to everyone who has contributed in any way including game asset credits.
    /// </summary>
    class CreditsScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        float pauseAlpha;
        InputAction pauseAction;
        SpriteFont creditsFont;
        int creditsPage = 0;
        Texture2D spLogo;
        Vector2 logoPosition = new Vector2(0, 50);
        Vector2 creditsPosition = new Vector2(0, 10);
        Vector2 creditsPosition2 = new Vector2(0, 550);
        Vector2 adPosition = new Vector2(170,400);
        string credits = "\nPlanetary Escape\nDeveloped by : Keyboard Warriors\nElson - Lead Artist and Designer\nLouis - Lead Programmer"
            + " \n\nBackground Art, UI Art, Menu, Coin Functions and\nCoin Spawning and most art by Elson\n\n"
            + "Signature Red & White Robot Art\nGame functionality and systems coded by Louis\n"
            + "\n\nTap to flip to next page for credits to others.";
       string credits2 = "We thank Charas Project at charas-project.net\nfor most of our Animated Sprites such as\ncharacter, pick ups, monsters."
                         + "\n\nBackground Music\nSpace Jazz - Martijn de Boer (NiGiD)\nhttp://ccmixter.org/files/NiGiD/46628"
                         + "\n\nSounds from freesound.org"
                         + "\n\nCoin Falling\nCoin Clatter - FenrirFangs\nhttps://www.freesound.org/people/FenrirFangs/"
                         + "\n\njump2.wav\nLloydEvans09"
                            + "\n\nRetro Coin Collect\nDrMinky"
                            + "\n\nsoul- death.mp3\nludist";

      string credits3 =    "Arrow Impact\nTwisted_Euphor"
                            + "\n\nJM_FX_Fireball 01.wav\nJulien Matthey"
                            + "\n\nflamewind.wav\nscarbell25"
                            + "\n\nfireball3.flac\nTimbre"
                            + "\n\nElectricuter.wav\njobro"
                         + "\n\nWe give Full Credits to these People and Organisations.\nWe DO NOT OWN ANY OF THE GAME ART OR SOUNDS ON\nTHIS PAGE AND THE PREVIOUS PAGE."
                         + "\n\nSchool of Digital Media and Infocomm Technology";

                            
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreditsScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                creditsFont = content.Load<SpriteFont>("creditsfont");
                EnabledGestures = GestureType.Tap;
                spLogo = content.Load<Texture2D>("spLogo");
                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                //Thread.Sleep(1000);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
             
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                if (creditsPage == 4)
                {
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
                  
                }
            }
           // ScreenManager.showad.Update(gameTime);
        }



        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
#if WINDOWS_PHONE
                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
#else
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
#endif
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                foreach (GestureSample gesture in input.Gestures)
                {
                    switch (gesture.GestureType)
                    {
                        case GestureType.Tap:
                            {
                                if (creditsPage < 4)
                                {
                                    creditsPage++;
                                }
                            }
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            

            spriteBatch.Begin();
            if (creditsPage == 0)
            {
                spriteBatch.DrawString(creditsFont, credits, creditsPosition, Color.White);
            }
            if (creditsPage == 1)
            {
                spriteBatch.DrawString(creditsFont, credits2, creditsPosition, Color.White);
            }
            if (creditsPage == 2)
            {
                spriteBatch.DrawString(creditsFont, credits3, creditsPosition, Color.White);
            }
            if (creditsPage == 3)
            {
                spriteBatch.Draw(spLogo, logoPosition, Color.White);
            }
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
           // ScreenManager.showad.Draw(spriteBatch, adPosition);
        }


        #endregion
    }
}
