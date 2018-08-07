#region File Description
//-----------------------------------------------------------------------------
// PhoneMainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using planetaryEscapeCa3;
using Microsoft.Xna.Framework;

namespace planetaryEscapeCa3
{
    class PhoneMainMenuScreen : PhoneMenuScreen
    {
        public PhoneMainMenuScreen()
            : base("  Planetary \n  Escape")
        {
            // Create a button to start the game
            Button playButton = new Button("Enter");
            playButton.Tapped += playButton_Tapped;
            MenuButtons.Add(playButton);

            // Create two buttons to toggle 
          //  effects and music. This sample just shows one way
            // of making and using these buttons; it doesn't actually have sound effects or music
            //BooleanButton sfxButton = new BooleanButton("SoundFx", ScreenManager.GetSetting("enableSoundFx", true));
            BooleanButton sfxButton = new BooleanButton("SoundFx", true);
            sfxButton.Tapped += sfxButton_Tapped;
            MenuButtons.Add(sfxButton);

            //BooleanButton musicButton = new BooleanButton("Music", ScreenManager.GetSetting("enableMusic", true));
            BooleanButton musicButton = new BooleanButton("Music",true);
            musicButton.Tapped += musicButton_Tapped;
            MenuButtons.Add(musicButton);

            Button creditButton = new Button("Credits");
            creditButton.Tapped += creditButton_Tapped;
            MenuButtons.Add(creditButton);

            //BooleanButton vibrateButton = new BooleanButton("Vibration", true);
            //vibrateButton.Tapped += vibrateButton_Tapped;
            //MenuButtons.Add(vibrateButton);

        }

        void creditButton_Tapped(object sender, EventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new CreditsScreen());
        }

        void playButton_Tapped(object sender, EventArgs e)
        {
            // When the "Play" button is tapped, we load the GameplayScreen
            LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new GameplayScreen());
        }

        void sfxButton_Tapped(object sender, EventArgs e)
        {
            BooleanButton button = sender as BooleanButton;

            //if (GameplayScreen.soundOn)
            //    GameplayScreen.soundOn = false;
            //else
            //    GameplayScreen.soundOn = true;

            ScreenManager.EnableSoundFx = !ScreenManager.EnableSoundFx;

            // In a real game, you'd want to store away the value of 
            // the button to turn off sounds here. :)
        }

        void musicButton_Tapped(object sender, EventArgs e)
        {
            BooleanButton button = sender as BooleanButton;

            //if (GameplayScreen.musicOn)
            //    GameplayScreen.musicOn = false;
            //else
            //    GameplayScreen.musicOn = true;
            ScreenManager.EnableMusic = !ScreenManager.EnableMusic;

            // In a real game, you'd want to store away the value of 
            // the button to turn off music here. :)
        }
        //void vibrateButton_Tapped(object sender, EventArgs e)
        //{
        //    BooleanButton button = sender as BooleanButton;
        //}
        protected override void OnCancel()
        {
            ScreenManager.Game.Exit();
            base.OnCancel();
        }


    }
}
