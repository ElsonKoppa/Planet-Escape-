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
using planetaryEscapeCa3;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Devices;
using System.IO;
//using System.IO.IsolatedStorage;
#endregion

namespace planetaryEscapeCa3
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont, titlefont;
        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;

        //VARIABLE INITIALIZATION - Keyboard Warrior
     //   bool musicOn = ScreenManager.GetSetting("enableMusic", true), soundOn = ScreenManager.GetSetting("enableSoundFx", true);
        
        bool gameMode = false, difficultyMode = false, tutorialMode = false, resetMode = false, menuMode = true, shopMode = false, statsMode = false;
        bool locked = false;
        int gold = 0, deaths = 0, jumps = 0, kills = 0, complete = 0, shotsFired = 0, boostersUsed = 0, weaponsPicked = 0;
        string difficulty = "";
        int booster = 1;
        int weapon = 1;
        int ammo = 10;
        int health = 5;
        int distance = 0;
        bool lose = false, win = false;
        bool jump = false;
        bool useAttack1 = false, useAttack2 = false, useAttack3 = false;
        bool usedBoost = false;
        bool pause = false;
        bool hasWeapon = false;
        bool attacked = false;

        //Background Music - Keyboard Warrior
        Song backgroundMusic;

        //Sounds - Keyboard Warrior
        SoundEffect flame, arrow, fireball, electric, died, pickup, pickedCoin, boost, jumped;

        //Sound Effect List Fix - Keyboard Warrior
        List<SoundEffectInstance> coinSounds = new List<SoundEffectInstance>();

        //Character Animation - Keyboard Warrior
        int spriteHeight;
        int spriteWidth;
        int framesPerRow = 3; //Number of frames in each row of the sprite sheet - Keyboard Warrior
        int charFramesPerSecond = 10; //Animations per second - Keyboard Warrior

        //Attack Animation Frames - Keyboard Warrior
        int attack1framesPerSecond = 10;
        int attack2framesPerSecond = 10;
        int attack3framesPerSecond = 10;

        //Monster Animation Frames - Keyboard Warrior
        int monsterFramesPerSecond = 10;

        //SPRITES INITIALIZATION - Keyboard Warrior 

        //Menu Buttons - Keyboard Warrior
        Texture2D startButton;
        Texture2D shopButton;
        Texture2D playerStatsButton;
        Texture2D resetButton;
        Texture2D menuBackground;
        Texture2D playerStats;
        Texture2D tutorial;
        Texture2D normal;
        Texture2D hard;
        Texture2D expert;
        Texture2D yes;
        Texture2D no;
        Texture2D back;

        Rectangle startRectangle;
        Rectangle shopRectangle;
        Rectangle statsRectangle;
        Rectangle resetRectangle;
        Rectangle normalButton;
        Rectangle hardButton;
        Rectangle expertButton;
        Rectangle yesButton;
        Rectangle noButton;
        Rectangle backButton;

        //Achievememts
        Texture2D TenDeaths;
        Texture2D TwoHundredFiftyKills;
        Texture2D FiveThousandGold;
        Texture2D ReadTutorial;
        Texture2D FirstCoin;
        Texture2D FirstBoost;
        Texture2D FirstLoss;
        Texture2D UnlockHard;
        Texture2D UnlockExpert;
        Texture2D CompleteExpert;
        Texture2D CompleteExpertByJumping;
        Texture2D CompleteAllDifficulties;
        Texture2D MasterAchiever;

        bool deathAchievement = false, killAchievement = false, goldAchievement = false, tutorialAchievement = false, 
             firstCoinAchievement = false, firstBoostAchievement = false, firstLossAchievement = false, 
             unlockHardAchievement = false, unlockExpertAchievement = false, completeExpertAchievement = false, 
             completeExpertWithJumpAchievement = false, completeAllDifficulties = false, masterAchievement = false;

        List<Texture2D> popUp = new List<Texture2D>();
        List<Vector2> popUpPosition = new List<Vector2>();
        List<float> popUpTimer = new List<float>();

        //Death Animation - Keyboard Warrior
        Texture2D death;
        List<Texture2D> dead = new List<Texture2D>();
        List<Vector2> deathPosition = new List<Vector2>();

        //Environment - Keyboard Warrior 
        Texture2D ground;
        Texture2D groundlv2;
        Texture2D groundlv3;
        Texture2D background;
        Texture2D backgroundlv2;
        Texture2D backgroundlv3;

        //Coins - Keyboard Warrior
        Texture2D coin;
        List<Texture2D> coins = new List<Texture2D>();
        List<Vector2> coinPosition = new List<Vector2>();

        //UI - Keyboard Warrior 
        Texture2D energyBar;
        Texture2D energy2;
        Texture2D energy3;
        Texture2D energy4;

        //Character - Keyboard Warrior 
        Texture2D character;
        Texture2D characterWep1;
        Texture2D characterWep2;
        Texture2D characterWep3;
        Texture2D superSayan;
        Texture2D superSayanWep1;
        Texture2D superSayanWep2;
        Texture2D superSayanWep3;
        Texture2D currentChar;

        //Attack Animations - Keyboard Warrior 
        Texture2D attack1;
        Texture2D attack2;
        Texture2D attack3;

        //Weapon Pickups - Keyboard Warrior 
        Texture2D weapon1;
        Texture2D weapon2;
        Texture2D weapon3;
        List<Texture2D> weapons = new List<Texture2D>();

        //Monsters - Keyboard Warrior 
        Texture2D monster1;
        Texture2D monster2;
        Texture2D monster2U;
        Texture2D monster3;
        Texture2D monster3U;
        Texture2D monster3M;
        Texture2D monster4;
        Texture2D monster4U;
        Texture2D monster4M;
        Texture2D monster5;
        Texture2D monster5U;
        Texture2D monster5M;
        Texture2D monster6;
        Texture2D monster6U;
        Texture2D monster6M;
        List<Texture2D> monsters = new List<Texture2D>();

        //VECTOR POSITION INITIALIZATION - Keyboard Warrior 
        
        //Positions
        Vector2 groundPosition = new Vector2(0, 688);
        Vector2 groundPosition2 = new Vector2(1366, 688);
        Vector2 backgroundPosition = new Vector2(0, 0);
        Vector2 energyPosition = new Vector2(635, 5);
        Vector2 charPosition = new Vector2(50, 607);
        Vector2 attackPosition1 = new Vector2(50, 608);
        Vector2 attackPosition2 = new Vector2(50, 608);
        Vector2 attackPosition3 = new Vector2(50, 608);
        Vector2 titlePosition = new Vector2(225, 20);
        Vector2 startPosition = new Vector2(265, 120);
        Vector2 shopPosition = new Vector2(265, 210);
        Vector2 statsPosition = new Vector2(265, 300);
        Vector2 resetPosition = new Vector2(550, 380);
        Vector2 screenPosition = new Vector2(5, 8);
        Vector2 statsInfoPosition = new Vector2(35, 100);
        Vector2 normalPosition = new Vector2(265, 120);
        Vector2 hardPosition = new Vector2(265, 210);
        Vector2 expertPosition = new Vector2(265, 300);
        Vector2 yesPosition = new Vector2(120, 200);
        Vector2 noPosition = new Vector2(420, 200);
        List<Vector2> monsterPosition = new List<Vector2>();
        List<Vector2> weaponPosition = new List<Vector2>();

        //Moving Speed 
        Vector2 moveSpeed = new Vector2(-200, 0);
        Vector2 attackSpeed = new Vector2(800, 0);

        //RECTANGLE INITIALIZATION - Keyboard Warrior

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
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
        /// </summary>a
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");
                titlefont = content.Load<SpriteFont>("titlefont");

                //Load Player Data
                //loadGame();

                //MUSIC AND SOUNDS - Keyboard Warrior
                    backgroundMusic = content.Load<Song>("space");


                //SPRITE LOADING - Keyboard Warrior 

                //Achievements
                TenDeaths = content.Load<Texture2D>("Achivements\\10Deaths");
                TwoHundredFiftyKills = content.Load<Texture2D>("Achivements\\250Kills");
                FiveThousandGold = content.Load<Texture2D>("Achivements\\5000Gold");
                ReadTutorial = content.Load<Texture2D>("Achivements\\ReadTutorial");
                FirstCoin = content.Load<Texture2D>("Achivements\\FirstCoin");
                FirstBoost = content.Load<Texture2D>("Achivements\\FirstBoost");
                FirstLoss = content.Load<Texture2D>("Achivements\\FirstLoss");
                UnlockHard = content.Load<Texture2D>("Achivements\\UnlockHard");
                UnlockExpert = content.Load<Texture2D>("Achivements\\UnlockExpert");
                CompleteExpert = content.Load<Texture2D>("Achivements\\CompleteExpert");
                CompleteExpertByJumping = content.Load<Texture2D>("Achivements\\CompleteExpertByJumping");
                CompleteAllDifficulties = content.Load<Texture2D>("Achivements\\CompleteAllDifficulties");
                MasterAchiever = content.Load<Texture2D>("Achivements\\MasterAchiever");

                //Menu Buttons - Keyboard Warrior
                startButton = content.Load<Texture2D>("startButton");
                shopButton = content.Load<Texture2D>("shopButton");
                playerStatsButton = content.Load<Texture2D>("playerStatsButton");
                resetButton = content.Load<Texture2D>("resetButton");
                menuBackground = content.Load<Texture2D>("menuBackground");
                playerStats = content.Load<Texture2D>("StatsScreen");
                tutorial = content.Load<Texture2D>("tutorial");
                normal = content.Load<Texture2D>("normal");
                hard = content.Load<Texture2D>("hard");
                expert = content.Load<Texture2D>("expert");
                yes = content.Load<Texture2D>("yesButton");
                no = content.Load<Texture2D>("noButton");
                back = content.Load<Texture2D>("backButton");

                startRectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y, startButton.Width, startButton.Height);
                shopRectangle = new Rectangle((int)shopPosition.X, (int)shopPosition.Y, shopButton.Width, shopButton.Height);
                statsRectangle = new Rectangle((int)statsPosition.X, (int)statsPosition.Y, playerStatsButton.Width, playerStatsButton.Height);
                resetRectangle = new Rectangle((int)resetPosition.X, (int)resetPosition.Y, resetButton.Width, resetButton.Height);
                normalButton = new Rectangle((int)normalPosition.X, (int)normalPosition.Y, normal.Width, normal.Height);
                hardButton = new Rectangle((int)hardPosition.X, (int)hardPosition.Y, hard.Width, hard.Height);
                expertButton = new Rectangle((int)expertPosition.X, (int)expertPosition.Y, expert.Width, expert.Height);
                yesButton = new Rectangle((int)yesPosition.X, (int)yesPosition.Y, yes.Width, yes.Height);
                noButton = new Rectangle((int)noPosition.X, (int)noPosition.Y, no.Width, no.Height);
                backButton = new Rectangle((int)resetPosition.X, (int)resetPosition.Y, back.Width, back.Height);

                //Death Animation - Keyboard Warrior
                death = content.Load<Texture2D>("death");

                //Environment - Keyboard Warrior
                ground = content.Load<Texture2D>("longGround2");
                groundlv2 = content.Load<Texture2D>("Lvl2LongGround2");
                groundlv3 = content.Load<Texture2D>("Lvl3LongGRound3");
                background = content.Load<Texture2D>("lvl1");
                backgroundlv2 = content.Load<Texture2D>("lvl2");
                backgroundlv3 = content.Load<Texture2D>("lvl3");

                //Sound - Keyboard Warrior
                flame = content.Load<SoundEffect>("flame");
                arrow = content.Load<SoundEffect>("arrow");
                fireball = content.Load<SoundEffect>("fireball");
                electric = content.Load<SoundEffect>("electric");
                died = content.Load<SoundEffect>("died");
                pickup = content.Load<SoundEffect>("pickup");
                pickedCoin = content.Load<SoundEffect>("pickedCoin");
                boost = content.Load<SoundEffect>("boost");
                jumped = content.Load<SoundEffect>("jump");

                for (int i = 0; i < 8; i++)
                {
                    coinSounds.Add(pickedCoin.CreateInstance());
                }
                
                //Coins - Keyboard Warrior
                coin = content.Load<Texture2D>("coin");    
                    
                //UI - Keyboard Warrior
                energyBar = content.Load<Texture2D>("energy");
                energy2 = content.Load<Texture2D>("energy2");
                energy3 = content.Load<Texture2D>("energy3");
                energy4 = content.Load<Texture2D>("energy4");

                //Weapons - Keyboard Warrior
                weapon1 = content.Load<Texture2D>("wep1");
                weapon2 = content.Load<Texture2D>("wep2");
                weapon3 = content.Load<Texture2D>("wep3");

                //Attack Animations - Keyboard Warrior
                attack1 = content.Load<Texture2D>("attack1");
                attack2 = content.Load<Texture2D>("attack2");
                attack3 = content.Load<Texture2D>("attack3");

                //Monsters - Keyboard Warrior
                monster1 = content.Load<Texture2D>("monster1");
                monster2 = content.Load<Texture2D>("monster2");
                monster2U = content.Load<Texture2D>("monster2upgrade");
                monster3 = content.Load<Texture2D>("monster3");
                monster3U = content.Load<Texture2D>("monster3upgrade");
                monster3M = content.Load<Texture2D>("monster3maxupgrade");
                monster4 = content.Load<Texture2D>("monster4");
                monster4U = content.Load<Texture2D>("monster4upgrade");
                monster4M = content.Load<Texture2D>("monster4maxupgrade");
                monster5 = content.Load<Texture2D>("monster5");
                monster5U = content.Load<Texture2D>("monster5upgrade");
                monster5M = content.Load<Texture2D>("monster5maxupgrade");
                monster6 = content.Load<Texture2D>("monster6");
                monster6U = content.Load<Texture2D>("monster6upgrade");
                monster6M = content.Load<Texture2D>("monster6maxupgrade");

                //Character Animation Loading - Keyboard Warrior
                character = content.Load<Texture2D>("char");
                characterWep1 = content.Load<Texture2D>("charWep1");
                characterWep2 = content.Load<Texture2D>("charWep2");
                characterWep3 = content.Load<Texture2D>("charWep3");
                superSayan = content.Load<Texture2D>("superSayan");
                superSayanWep1 = content.Load<Texture2D>("superSayan1");
                superSayanWep2 = content.Load<Texture2D>("superSayan2");
                superSayanWep3 = content.Load<Texture2D>("superSayan3");

                spriteHeight = character.Height / 4; //Cut the Spritesheet by the number of Rows - Keyboard Warrior
                spriteWidth = character.Width / 3; //Cut the Spritesheet by the number of Columns - Keyboard Warrior

                //Enable Gestures - Keyboard Warrior
                EnabledGestures =
                    //GestureType.Hold |
                GestureType.Tap |
                    //GestureType.DoubleTap |
                    //GestureType.FreeDrag |
                GestureType.Flick;
                    //GestureType.Pinch;

                
                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                //Thread.Sleep(1500);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }

#if WINDOWS_PHONE
            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("PlayerPosition"))
            {
                //playerPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"];
                //enemyPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"];
            }
#endif
        }


        public override void Deactivate()
        {
#if WINDOWS_PHONE
            //Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"] = playerPosition;
            //Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"] = enemyPosition;
#endif

            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();

#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("PlayerPosition");
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("EnemyPosition");
#endif
        }


        #endregion

        #region Update and Draw

        //Function written to Save Game Data
        //void saveGame()
        //{
        //    IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
        //    isf.CreateDirectory("Data");
        //    StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream("Data\\playerStats.txt", FileMode.Create, isf));
        //    sw.WriteLine(gold);
        //    sw.WriteLine(jumps);
        //    sw.WriteLine(kills);
        //    sw.WriteLine(deaths);
        //    sw.WriteLine(complete);
        //    sw.WriteLine(shotsFired);
        //    sw.WriteLine(boostersUsed);
        //    sw.WriteLine(weaponsPicked);
        //    sw.WriteLine(deathAchievement);
        //    sw.WriteLine(killAchievement);
        //    sw.WriteLine(goldAchievement);
        //    sw.WriteLine(tutorialAchievement);
        //    sw.WriteLine(unlockHardAchievement);
        //    sw.WriteLine(unlockExpertAchievement);
        //    sw.WriteLine(completeExpertAchievement);
        //    sw.WriteLine(completeExpertWithJumpAchievement);
        //    sw.WriteLine(completeAllDifficulties);
        //    sw.WriteLine(masterAchievement);
        //    sw.Close();
        //}

        ////Function written to Load Game Data
        //void loadGame()
        //{
        //    IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
        //    StreamReader sr = null;

        //    try
        //    {
        //        sr = new StreamReader(new IsolatedStorageFileStream("Data\\playerStats.txt", FileMode.Open, isf));
        //        gold = Convert.ToInt32(sr.ReadLine());
        //        jumps = Convert.ToInt32(sr.ReadLine());
        //        kills = Convert.ToInt32(sr.ReadLine());
        //        deaths = Convert.ToInt32(sr.ReadLine());
        //        complete = Convert.ToInt32(sr.ReadLine());
        //        shotsFired = Convert.ToInt32(sr.ReadLine());
        //        boostersUsed = Convert.ToInt32(sr.ReadLine());
        //        weaponsPicked = Convert.ToInt32(sr.ReadLine());
        //        deathAchievement = Convert.ToBoolean(sr.ReadLine());
        //        killAchievement = Convert.ToBoolean(sr.ReadLine());
        //        goldAchievement = Convert.ToBoolean(sr.ReadLine());
        //        tutorialAchievement = Convert.ToBoolean(sr.ReadLine());
        //        firstCoinAchievement = Convert.ToBoolean(sr.ReadLine());
        //        firstBoostAchievement = Convert.ToBoolean(sr.ReadLine());
        //        firstLossAchievement = Convert.ToBoolean(sr.ReadLine());
        //        unlockHardAchievement = Convert.ToBoolean(sr.ReadLine());
        //        unlockExpertAchievement = Convert.ToBoolean(sr.ReadLine());
        //        completeExpertAchievement = Convert.ToBoolean(sr.ReadLine());
        //        completeExpertWithJumpAchievement = Convert.ToBoolean(sr.ReadLine());
        //        completeAllDifficulties = Convert.ToBoolean(sr.ReadLine());
        //        masterAchievement = Convert.ToBoolean(sr.ReadLine());
        //        sr.Close();
        //    }

        //    catch
        //    {
        //    }
        //}

        ////Function written to Reset Game Data
        //void ResetData()
        //{
        //    IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
        //    isf.CreateDirectory("Data");
        //    StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream("Data\\playerStats.txt", FileMode.Create, isf));
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("0");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.WriteLine("false");
        //    sw.Close();
        //    loadGame();
        //}

        //Function written to check for Collision - Keyboard Warrior (Louis)
        //Returns a True or False value
        //Enter the First Sprite and its Position, 
        //Then enter the Second Sprite and its Position.
        bool CheckForCollision(Texture2D spriteOne, Vector2 positionOne, Texture2D spriteTwo, Vector2 positionTwo)
        {
            int width, height;
            //if (spriteOne == character)
            //{
            //    width = spriteOne.Width / 6;
            //    height = spriteOne.Height / 8;
            //}

            //else
            //{
            width = spriteOne.Width / 2;
            height = spriteOne.Height / 2;
            //}

            BoundingBox characterBox = new BoundingBox(
                new Vector3(positionOne.X - (width), positionOne.Y - (height), 0),
                new Vector3(positionOne.X + (width), positionOne.Y + (height), 0));

            BoundingBox objectBox = new BoundingBox(
                new Vector3(positionTwo.X - (spriteTwo.Width / 2), positionTwo.Y - (spriteTwo.Height / 2), 0),
                new Vector3(positionTwo.X + (spriteTwo.Width / 2), positionTwo.Y + (spriteTwo.Height / 2), 0));

            if (characterBox.Intersects(objectBox))
                return true;
            else return false;
        }

        //Function written to spawn coins - Keyboard Warrior (Elson)
        void spawnCoin(char shape)
        {
            if (shape == 'c')
            {
                for (int i = 0; i < 9; i++)
                {
                    coins.Add(coin);
                }

                coinPosition.Add( new Vector2(1340, 384));
                coinPosition.Add(new Vector2(1340,  404));
                coinPosition.Add( new Vector2(1340, 424));
                coinPosition.Add(new Vector2(1340,  444));
                coinPosition.Add(new Vector2(1340,  464));
                 //Top ->
                coinPosition.Add(new Vector2(1360, 384));
                coinPosition.Add(new Vector2(1380, 384));
                //Bottom ->
                coinPosition.Add(new Vector2(1360, 464));
                coinPosition.Add(new Vector2(1380, 464));
            }

            if (shape == 'o')
            {
                for (int i = 0; i < 14; i++)
                {
                    coins.Add(coin);
                }

                //Left ->
                coinPosition.Add(new Vector2(1420, 384)); //200
                coinPosition.Add(new Vector2(1420, 404)); //220
                coinPosition.Add(new Vector2(1420, 424)); //240
                coinPosition.Add(new Vector2(1420, 444)); //260
                coinPosition.Add(new Vector2(1420, 464)); //280
                //Right ->
                coinPosition.Add(new Vector2(1480, 404));
                coinPosition.Add(new Vector2(1480, 424));
                coinPosition.Add(new Vector2(1480, 444));
                //Top ->
                coinPosition.Add(new Vector2(1440, 384));
                coinPosition.Add(new Vector2(1460, 384));
                coinPosition.Add(new Vector2(1480, 384));
                //Bottom ->
                coinPosition.Add(new Vector2(1440, 464));
                coinPosition.Add(new Vector2(1460, 464));
                coinPosition.Add(new Vector2(1480, 464));
            }

            if (shape == 'i')
            {
                for (int i = 0; i < 5; i++)
                {
                    coins.Add(coin);  
                }

                coinPosition.Add(new Vector2(1520, 384));
                coinPosition.Add(new Vector2(1520, 404));
                coinPosition.Add(new Vector2(1520, 424));
                coinPosition.Add(new Vector2(1520, 444));
                coinPosition.Add(new Vector2(1520, 464));
            }

            if(shape == 'n')
            {
                for (int i = 0; i < 12; i++)
                {
                    coins.Add(coin);
                }

                coinPosition.Add(new Vector2(1560, 384));//1120
                coinPosition.Add(new Vector2(1560, 404));
                coinPosition.Add(new Vector2(1560, 424));
                coinPosition.Add(new Vector2(1560, 444));
                coinPosition.Add(new Vector2(1560, 464));
                //Right
                coinPosition.Add(new Vector2(1620, 384));
                coinPosition.Add(new Vector2(1620, 404));
                coinPosition.Add(new Vector2(1620, 424));
                coinPosition.Add(new Vector2(1620, 444));
                coinPosition.Add(new Vector2(1620, 464));
                //Top 
                coinPosition.Add(new Vector2(1580, 384));
                coinPosition.Add(new Vector2(1600, 384));
            }
        }

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
            {
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
                pause = true;
            }

            else
            {
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
                pause = false;
            }

            if (IsActive)
            {

                //Achievement Tracking
                if (gold >= 5000 && !goldAchievement)
                {
                    goldAchievement = true;
                    popUp.Add(FiveThousandGold);
                    popUpPosition.Add(new Vector2(15, -100));
                    popUpTimer.Add(0);
                }

                //MASTER ACHIEVEMENT - All Achievements Unlocked
                if (deathAchievement && killAchievement && goldAchievement && tutorialAchievement &&
                     firstCoinAchievement && firstBoostAchievement && firstLossAchievement &&
                     unlockHardAchievement && unlockExpertAchievement && completeExpertAchievement &&
                     completeExpertWithJumpAchievement && completeAllDifficulties && !masterAchievement)
                {
                    masterAchievement = true;
                    popUp.Add(MasterAchiever);
                    popUpPosition.Add(new Vector2(15, -100));
                    popUpTimer.Add(0);
                }

                
                //Achievement Popups
                for (int i = 0; i < popUp.Count; i++)
                {
                    if (popUpPosition[i].Y < 0 && popUpTimer[i] < 5)
                    {
                        popUpPosition[i] += new Vector2(0, 5);
                    }

                    if (popUpPosition[i].Y >= 0)
                    {
                        popUpTimer[i] += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    if (popUpTimer[i] >= 5)
                    {
                        popUpPosition[i] += new Vector2(0, -5);
                        if (popUpPosition[i].Y == -100)
                        {
                            popUp.RemoveAt(i);
                            popUpPosition.RemoveAt(i);
                            popUpTimer.RemoveAt(i);
                            i--;
                        }
                    }
                }

                //While Game Is Active, - Keyboard Warrior
                if (gameMode)
                {
                    //Positive Health Update
                    if (health < 0)
                    {
                        health = 0;
                    }

                    //Win & Lose Conditions - Keyboard Warrior
                    if (distance > 50000 && !win)
                    {
                    
                        distance = 99999;
                        win = true;

                        if (difficulty == "normal" && !unlockHardAchievement)
                        {
                            unlockHardAchievement = true;
                            popUp.Add(UnlockHard);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                        if (difficulty == "hard" && !unlockExpertAchievement)
                        {
                            unlockExpertAchievement = true;
                            popUp.Add(UnlockExpert);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                        if (difficulty == "expert" && !completeExpertAchievement)
                        {
                            completeExpertAchievement = true;
                            popUp.Add(CompleteExpert);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                            completeAllDifficulties = true;
                            popUp.Add(CompleteAllDifficulties);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                        if (difficulty == "expert" && !completeExpertWithJumpAchievement && !attacked)
                        {
                            completeExpertWithJumpAchievement = true;
                            popUp.Add(CompleteExpertByJumping);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                        complete++;
                        //saveGame();
                        charFramesPerSecond = 0;
                        attack1framesPerSecond = 0;
                        attack2framesPerSecond = 0;
                        attack3framesPerSecond = 0;
                        monsterFramesPerSecond = 0;
                    }
                    if (health == 0)
                    {
                        health = 100;
                 

                        lose = true;
                        deaths++;

                        //Achievement Tracking
                        if (!firstLossAchievement)
                        {
                            firstLossAchievement = true;
                            popUp.Add(FirstLoss);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                        if (deaths >= 10 && !deathAchievement)
                        {
                            deathAchievement = true;
                            popUp.Add(TenDeaths);
                            popUpPosition.Add(new Vector2(15, -100));
                            popUpTimer.Add(0);
                        }

                      //  saveGame();
                        charFramesPerSecond = 0;
                        attack1framesPerSecond = 0;
                        attack2framesPerSecond = 0;
                        attack3framesPerSecond = 0;
                        monsterFramesPerSecond = 0;
                    }

                    if (!(win || lose))
                    {
                        //Frame Animation Update
                        if (moveSpeed.X == -200)
                        {
                            charFramesPerSecond = 10;
                        }
                        if (moveSpeed.X == -400)
                        {
                            charFramesPerSecond = 20;
                        }
                        if (moveSpeed.X == -600)
                        {
                            charFramesPerSecond = 30;
                        }
                        if (moveSpeed.X == -800)
                        {
                            charFramesPerSecond = 40;
                        }

                        attack1framesPerSecond = 10;
                        attack2framesPerSecond = 10;
                        attack3framesPerSecond = 10;
                        monsterFramesPerSecond = 10;

                        //Distance Travelled - Keyboard Warrior
                        distance += (-1) * (int)(moveSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (!(distance > 32010 && backgroundPosition.X < -200))
                        {
                            backgroundPosition += ((moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) / 80);
                        }

                        //Weapon Spawning - Keyboard Warrior
                        int spawn = random.Next(1, 1000);
                        if (spawn == 999)
                        {
                            weapons.Add(weapon1);
                            weaponPosition.Add(new Vector2(1380, 657));
                        }
                        if (spawn == 1)
                        {
                            weapons.Add(weapon2);
                            weaponPosition.Add(new Vector2(1380, 660));
                        }
                        if (spawn == 500)
                        {
                            weapons.Add(weapon3);
                            weaponPosition.Add(new Vector2(1380, 678));
                        }

                        //Weapon out of Screen
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            if (weaponPosition[i].X < -100)
                            {
                                weapons.RemoveAt(i);
                                weaponPosition.RemoveAt(i);
                                i--;
                            }
                        }
                        //Monster Spawning - Keyboard Warrior
                        bool dontSpawn = false;

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (monsterPosition[i].X > 780)
                            {
                                dontSpawn = true;
                            }
                        }

                        if (!dontSpawn)
                        {
                            if (distance < 16000 && moveSpeed.X != 0)
                            {
                                int r = random.Next(1, 100);
                                if (r == 99)
                                {
                                    monsters.Add(monster1);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }

                                if (r == 98)
                                {
                                    monsters.Add(monster2);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }
                            }

                            if (distance > 16000 && distance < 32000)
                            {
                                int r = random.Next(1, 100);
                                if (r == 99)
                                {
                                    monsters.Add(monster3);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }

                                if (r == 98)
                                {
                                    monsters.Add(monster4);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }
                            }

                            if (distance > 32000)
                            {
                                int r = random.Next(1, 100);
                                if (r == 99)
                                {
                                    monsters.Add(monster5);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }

                                if (r == 98)
                                {
                                    monsters.Add(monster6);
                                    monsterPosition.Add(new Vector2(1380, 622));
                                }
                            }
                        }

                        //Monster Movement and Collision - Keyboard Warrior
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            monsterPosition[i] += (moveSpeed + new Vector2(-100, 0)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (monsterPosition[i].X > charPosition.X)
                            {
                                if (CheckForCollision(character, charPosition, monsters[i], (monsterPosition[i] + new Vector2(195, 215)))) //Offset Sprite Position
                                {
                                 
                                       electric.Play();
                                    monsters.RemoveAt(i);
                                    monsterPosition.RemoveAt(i);
                                    i--;
                                    health -= 1;
                                    moveSpeed.X = -200;
                                    charFramesPerSecond = 10;
                                }
                            }

                            else
                            {
                                if (CheckForCollision(character, charPosition, monsters[i], (monsterPosition[i] + new Vector2(-195, 215)))) //Offset Sprite Position
                                {
                                        electric.Play();
                                    monsters.RemoveAt(i);
                                    monsterPosition.RemoveAt(i);
                                    i--;
                                    health -= 1;
                                    moveSpeed.X = -200;
                                    charFramesPerSecond = 10;
                                }
                            }
                        }

                        //Monster has left the screen - Keyboard Warriors

                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (monsterPosition[i].X < -100)
                            {
                                monsters.RemoveAt(i);
                                monsterPosition.RemoveAt(i);
                                i--;
                            }
                        }

                        //Random Coin Spawning - Keyboard Warrior
                        //int x = random.Next(1, 101);
                        //if (x > 98)
                        //{
                        //    spawnCoin('c');
                        //    spawnCoin('o');
                        //    spawnCoin('i');
                        //    spawnCoin('n');
                        //}

                        //Coin Movement - Keyboard Warrior
                        for (int i = 0; i < coins.Count; i++)
                        {
                            coinPosition[i] += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (CheckForCollision(character, charPosition + new Vector2(-35, 230), coins[i], coinPosition[i]))
                            {
        
                                    for (int z = 0; z < coinSounds.Count; z++)
                                    {
                                        if (coinSounds[z].State == SoundState.Stopped)
                                        {
                                            coinSounds[z].Play();
                                        }
                                    }
                                
                                coins.RemoveAt(i);
                                coinPosition.RemoveAt(i);
                                i--;
                                gold++;
                                if (!firstCoinAchievement)
                                {
                                    firstCoinAchievement = true;
                                    popUp.Add(FirstCoin);
                                    popUpPosition.Add(new Vector2(15, -100));
                                    popUpTimer.Add(0);
                                }
                            }
                        }

                        //Coin out of Screen - Keyboard Warrior
                        for (int i = 0; i < coins.Count; i++)
                        {
                            if (coinPosition[i].X < -50)
                            {
                                coins.RemoveAt(i);
                                coinPosition.RemoveAt(i);
                                i--;
                            }
                        }

                        //Attack Animation Movement and Collision- Keyboard Warrior
                        if (useAttack1)
                        {
                            attackPosition1 += attackSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            attacked = true;
                            for (int i = 0; i < monsters.Count; i++)
                            {
                                if (CheckForCollision(attack1, (attackPosition1 + new Vector2(-80, -215)), monsters[i], (monsterPosition[i] + new Vector2(150, 0))))
                                {
                               
                                        died.Play();
                                    
                                    kills++;

                                    if (kills >= 250 && !killAchievement)
                                    {
                                        killAchievement = true;
                                        popUp.Add(TwoHundredFiftyKills);
                                        popUpPosition.Add(new Vector2(15, -100));
                                        popUpTimer.Add(0);
                                    }

                                    dead.Add(death);
                                    deathPosition.Add(new Vector2(monsterPosition[i].X, 622)); //Offset Position
                                    monsters.RemoveAt(i);
                                    monsterPosition.RemoveAt(i);
                                    i--;
                                    useAttack1 = false;
                                    attackPosition1.X = 50;
                                }
                            }
                        }

                        if (useAttack2)
                        {
                            attackPosition2 += attackSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            attacked = true;
                            for (int i = 0; i < monsters.Count; i++)
                            {
                                if (CheckForCollision(attack2, (attackPosition2 + new Vector2(-80, -215)), monsters[i], (monsterPosition[i] + new Vector2(150, 0))))
                                {
                                        died.Play();
                                    kills++;
                                    dead.Add(death);
                                    deathPosition.Add(new Vector2(monsterPosition[i].X, 335)); //Offset Position
                                    monsters.RemoveAt(i);
                                    monsterPosition.RemoveAt(i);
                                    i--;
                                    useAttack2 = false;
                                    attackPosition2.X = 50;
                                }
                            }
                        }

                        if (useAttack3)
                        {
                            attackPosition3 += attackSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            attacked = true;
                            for (int i = 0; i < monsters.Count; i++)
                            {
                                if (CheckForCollision(attack3, (attackPosition3 + new Vector2(-80, -215)), monsters[i], (monsterPosition[i] + new Vector2(150, 0))))
                                {
                                        died.Play();
                                    kills++;
                                    dead.Add(death);
                                    deathPosition.Add(new Vector2(monsterPosition[i].X, 335)); //Offset Position
                                    monsters.RemoveAt(i);
                                    monsterPosition.RemoveAt(i);
                                    i--;
                                    useAttack3 = false;
                                    attackPosition3.X = 50;
                                }
                            }
                        }

                        //Ammo Check - Keyboard Warrior
                        if (ammo == 0)
                        {
                            weapon = 0;
                            if (moveSpeed.X != 800)
                            {
                                character = content.Load<Texture2D>("char");
                            }
                            else
                            {
                                character = content.Load<Texture2D>("supersayan");
                            }
                        }

                        //ACTIONS - Keyboard Warrior

                        //Character Jumping
                        if (jump && charPosition.Y != 120)
                        {
                            if (charPosition.Y == 820)
                            {
                                jumped.Play();
                                jumps++;
                            }
                          //  charPosition.Y -= 10;
                        }

                        else
                        {
                            jump = false;
                        }

                        if (!jump && charPosition.Y != 320)
                        {
                           // charPosition.Y += 10;
                        }

                        //CHECK FOR COLLISION - Keyboard Warrior

                        //Increase Original Weapon X Position for Realistic Pick up - Keyboard Warrior

                        //Weapon Pickup Collision - Keyboard Warrior
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            if (CheckForCollision(character, charPosition, weapons[i], (weaponPosition[i] + new Vector2(100, 120))))
                            {
                                if (weapons[i] == weapon1)
                                {
                                    weaponsPicked++;
                                    ammo = 10;
                                    weapon = 1;
                                    hasWeapon = true;
                                    pickup.Play();
                                    booster += 1;
                                    weapons.RemoveAt(i);
                                    weaponPosition.RemoveAt(i);
                                    i--;
                                }

                                else if (weapons[i] == weapon2)
                                {
                                    weaponsPicked++;
                                    ammo = 10;
                                    weapon = 2;
                                    hasWeapon = true;
                                    pickup.Play();
                                    booster += 1;
                                    weapons.RemoveAt(i);
                                    weaponPosition.RemoveAt(i);
                                    i--;
                                }

                                else if (weapons[i] == weapon3)
                                {
                                    weaponsPicked++;
                                    ammo = 10;
                                    weapon = 3;
                                    hasWeapon = true;
                                    pickup.Play();
                                    booster += 1;
                                    weapons.RemoveAt(i);
                                    weaponPosition.RemoveAt(i);
                                    i--;
                                }
                            }
                        }

                        //Make the Ground Move from Right to Left - Keyboard Warrior
                        groundPosition += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        groundPosition2 += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (groundPosition.X < -1366)
                            groundPosition.X = 1366;
                        if (groundPosition2.X < -1366)
                            groundPosition2.X = 1366;


                        //Weapon Movement - Keyboard Warrior
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            weaponPosition[i] += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }

                        //Dead Movement and Out of Screen - Keyboard Warrior
                        for (int i = 0; i < dead.Count; i++)
                        {
                            deathPosition[i] += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }

                        for (int i = 0; i < dead.Count; i++)
                        {
                            if (deathPosition[i].X < -100)
                            {
                                dead.RemoveAt(i);
                                deathPosition.RemoveAt(i);
                                i--;
                            }
                        }

                        //Accelerometer - Keyboard Warrior
                        Vector3 acceleration = Accelerometer.GetState().Acceleration;

                        if (acceleration.Z > -0.5)
                        {
                            usedBoost = false;
                        }

                        if (acceleration.Z < -0.75)
                        {
                            if (booster > 0)
                            {
                                if (moveSpeed.X == 0 && !usedBoost)
                                {

                                        boost.Play();
                                    
                                    boostersUsed++;
                                    charFramesPerSecond += 10;
                                    moveSpeed.X = -200;
                                    booster--;
                                    usedBoost = true;
                                }

                                else if (moveSpeed.X == -200 && !usedBoost)
                                {
               
                                        boost.Play();
                        

                             

                                    if (!firstBoostAchievement)
                                    {
                                        firstBoostAchievement = true;
                                        popUp.Add(FirstBoost);
                                        popUpPosition.Add(new Vector2(15, -100));
                                        popUpTimer.Add(0);
                                    }

                                    boostersUsed++;
                                    moveSpeed.X = -400;
                                    charFramesPerSecond += 10;
                                    booster--;
                                    usedBoost = true;
                                }

                                else if (moveSpeed.X == -400 && !usedBoost)
                                {
                                    
                                        boost.Play();
                                    

                       
                                    boostersUsed++;
                                    moveSpeed.X = -600;
                                    charFramesPerSecond += 10;
                                    booster--;
                                    usedBoost = true;
                                }

                                else if (moveSpeed.X == -600 && !usedBoost)
                                {
                                    
                                        boost.Play();
                                    
                                    boostersUsed++;
                                    moveSpeed.X = -800;
                                    charFramesPerSecond += 10;
                                    booster--;
                                    usedBoost = true;
                                }

                                else
                                {
                                }
                            }
                        }
                    }
                }
            }
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

                if (gameMode)
                {
                    charFramesPerSecond = 0;
                    attack1framesPerSecond = 0;
                    attack2framesPerSecond = 0;
                    attack3framesPerSecond = 0;
                    monsterFramesPerSecond = 0;
                    saveGame();
                }
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

                //Gesture Inputs - Keyboard Warrior
                foreach (GestureSample gesture in input.Gestures)
                {
                    switch (gesture.GestureType)
                    {
                     
                                
                        case GestureType.Tap:
                            {
                               //Handle boost
                                usedBoost = false;
                                if (booster > 0)
                                {
                                    if (moveSpeed.X == 0 && !usedBoost)
                                    {

                                        boost.Play();

                                        boostersUsed++;
                                        charFramesPerSecond += 10;
                                        moveSpeed.X = -200;
                                        booster--;
                                        usedBoost = true;
                                    }

                                    else if (moveSpeed.X == -200 && !usedBoost)
                                    {

                                        boost.Play();




                                        if (!firstBoostAchievement)
                                        {
                                            firstBoostAchievement = true;
                                            popUp.Add(FirstBoost);
                                            popUpPosition.Add(new Vector2(15, -100));
                                            popUpTimer.Add(0);
                                        }

                                        boostersUsed++;
                                        moveSpeed.X = -400;
                                        charFramesPerSecond += 10;
                                        booster--;
                                        usedBoost = true;
                                    }

                                    else if (moveSpeed.X == -400 && !usedBoost)
                                    {

                                        boost.Play();



                                        boostersUsed++;
                                        moveSpeed.X = -600;
                                        charFramesPerSecond += 10;
                                        booster--;
                                        usedBoost = true;
                                    }

                                    else if (moveSpeed.X == -600 && !usedBoost)
                                    {

                                        boost.Play();

                                        boostersUsed++;
                                        moveSpeed.X = -800;
                                        charFramesPerSecond += 10;
                                        booster--;
                                        usedBoost = true;
                                    }


                                }
                                //Boost complete
                                if (tutorialMode)
                                {
                                    tutorialMode = false;
                                    gameMode = true;
                                    if (!tutorialAchievement)
                                    {
                                        tutorialAchievement = true;
                                        popUp.Add(ReadTutorial);
                                        popUpPosition.Add(new Vector2(15, -100));
                                        popUpTimer.Add(0);
                                    }
                                }

                                if (difficultyMode)
                                {
                                    if (normalButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        difficulty = "normal";
                                        difficultyMode = false;
                                        tutorialMode = true;
                                        locked = false;
                                    }

                                    if (hardButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        if (unlockHardAchievement)
                                        {
                                            difficulty = "hard";
                                            difficultyMode = false;
                                            tutorialMode = true;
                                            locked = false;
                                        }

                                        else
                                        {
                                            locked = true;
                                        }
                                    }

                                    if (expertButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        if (unlockExpertAchievement)
                                        {
                                            difficulty = "expert";
                                            difficultyMode = false;
                                            tutorialMode = true;
                                            locked = false;
                                        }

                                        else
                                        {
                                            locked = true;
                                        }
                                    }
                                }

                                if (menuMode)
                                {
                                    if (startRectangle.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        menuMode = false;
                                        difficultyMode = true;
                                    }

                                    if (shopRectangle.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {

                                    }

                                    if (statsRectangle.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        menuMode = false;
                                        statsMode = true;
                                    }

                                    if (resetRectangle.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        menuMode = false;
                                        resetMode = true;
                                    }
                                }

                                if (statsMode)
                                {
                                    if (backButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        statsMode = false;
                                        menuMode = true;
                                    }
                                }

                                if (gameMode)
                                {
                                    if (!(win || lose))
                                    {
                                        //Tap Gesture for Jumping Motion - Keyboard Warrior
                                        if (!jump && charPosition.Y == 320)
                                        {
                                            jump = true;
                                        }
                                    }

                                    else
                                    {
                                        gameMode = false;
                                        menuMode = true;
                                        win = false;
                                        lose = false;
                                        booster = 1;
                                        weapon = 1;
                                        ammo = 10;
                                        health = 5;
                                        distance = 0;
                                        jump = false;
                                        useAttack1 = false;
                                        useAttack2 = false;
                                        useAttack3 = false;
                                        usedBoost = false;
                                        pause = false;
                                        hasWeapon = false;
                                        attacked = false;
                                        difficulty = "";
                                        dead.Clear();
                                        deathPosition.Clear();
                                        coins.Clear();
                                        coinPosition.Clear();
                                        weapons.Clear();
                                        monsters.Clear();
                                        monsterPosition.Clear();
                                        weaponPosition.Clear();
                                    }
                                }

                                if (resetMode)
                                {
                                    if (yesButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        resetMode = false;
                                        menuMode = true;
                                        //ResetData();
                                    }

                                    if (noButton.Contains((int)gesture.Position.X, (int)gesture.Position.Y))
                                    {
                                        resetMode = false;
                                        menuMode = true;
                                    }
                                }
                            }
                            break;
                     
                        case GestureType.Flick:
                            {
                                if (gameMode)
                                {
                                    //Swipe Gesture for Attacking - Keyboard Warrior
                                    if (!(win || lose))
                                    {
                                        if (ammo != 0 && gesture.Delta.X > 0)
                                        {
                                            if (weapon == 1 && !useAttack1)
                                            {
                                               
                                                    flame.Play();
                                                
                                                shotsFired++;
                                                ammo -= 1;
                                                attackPosition1.Y = charPosition.Y + 20;
                                                useAttack1 = true;
                                            }

                                            if (weapon == 2 && !useAttack2)
                                            {
                                                
                                                    arrow.Play();
                                                
                                                shotsFired++;
                                                ammo -= 1;
                                                attackPosition2.Y = charPosition.Y - 5;
                                                useAttack2 = true;
                                            }

                                            if (weapon == 3 && !useAttack3)
                                            {
                                                
                                                    fireball.Play();
                                                
                                                shotsFired++;
                                                ammo -= 1;
                                                attackPosition3.Y = charPosition.Y + 15;
                                                useAttack3 = true;
                                            }
                                        }
                                    }
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
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            //Scale Animations to Processor Speed - Keyboard Warrior
            int charFrame = (int)(gameTime.TotalGameTime.TotalSeconds * charFramesPerSecond) % framesPerRow;
            int attackFrame1 = (int)(gameTime.TotalGameTime.TotalSeconds * attack1framesPerSecond) % framesPerRow;
            int attackFrame2 = (int)(gameTime.TotalGameTime.TotalSeconds * attack2framesPerSecond) % framesPerRow;
            int attackFrame3 = (int)(gameTime.TotalGameTime.TotalSeconds * attack3framesPerSecond) % framesPerRow;
            int monsterFrame = (int)(gameTime.TotalGameTime.TotalSeconds * monsterFramesPerSecond) % framesPerRow;

            spriteBatch.Begin();

            if (resetMode)
            {
                spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(gameFont, "Are you sure you want\n to reset your data?", new Vector2(140, 100), Color.White);
                spriteBatch.Draw(yes, yesPosition, Color.White);
                spriteBatch.Draw(no, noPosition, Color.White);
            }

            if (tutorialMode)
            {
                spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(tutorial, new Vector2(0,0), Color.White);
            }

            if (menuMode)
            {
                spriteBatch.Draw(menuBackground, new Vector2(0,0), Color.White);
                spriteBatch.DrawString(titlefont, "Planetary\n   Escape", titlePosition, Color.White);
                spriteBatch.Draw(startButton, startPosition, Color.White);
                spriteBatch.Draw(shopButton, shopPosition, Color.White);
                spriteBatch.Draw(playerStatsButton, statsPosition, Color.White);
                spriteBatch.Draw(resetButton, resetPosition, Color.White);
            }

            if (difficultyMode)
            {
                spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(titlefont, "Difficulty", titlePosition + new Vector2(0, 30), Color.White);
                spriteBatch.Draw(normal, normalPosition, Color.White);
                spriteBatch.Draw(hard, hardPosition, Color.White);
                spriteBatch.Draw(expert, expertPosition, Color.White);

                if (locked)
                {
                    spriteBatch.DrawString(gameFont, "                    Locked!\nComplete Easier Mode first!", new Vector2(85, 400), Color.White);
                }
            }

            if (statsMode)
            {
                spriteBatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(playerStats, screenPosition, Color.White);
                spriteBatch.DrawString(gameFont, "Total Kills : " + kills + "\nTotal Deaths : " + deaths
                                                + "\nTotal Jumps : " + jumps 
                                                + "\nWeapons Picked : " + weaponsPicked + "\nShots Fired : " + shotsFired 
                                                + "\nBoosters Used : " + boostersUsed 
                                                + "\nGames completed : " + complete
                                                + "\nGold : " + gold, statsInfoPosition, Color.White);
                spriteBatch.Draw(back, resetPosition, Color.White);
            }

            if (gameMode)
            {
                //Draw the Background and the Ground - Keyboard Warrior 
                if (distance < 16000)
                {
                    spriteBatch.Draw(background, backgroundPosition, Color.White);
                    spriteBatch.Draw(ground, groundPosition, Color.White);
                    spriteBatch.Draw(ground, groundPosition2, Color.White);
                }

                else if (distance < 32000)
                {
                    if (distance < 32005)
                    {
                        backgroundPosition.X = 0;
                    }

                    spriteBatch.Draw(backgroundlv2, backgroundPosition, Color.White);
                    spriteBatch.Draw(groundlv2, groundPosition, Color.White);
                    spriteBatch.Draw(groundlv2, groundPosition2, Color.White);
                }

                else
                {
                    if (distance < 48005)
                    {
                        backgroundPosition.X = 0;
                    }
                    spriteBatch.Draw(backgroundlv3, backgroundPosition, Color.White);
                    spriteBatch.Draw(groundlv3, groundPosition, Color.White);
                    spriteBatch.Draw(groundlv3, groundPosition2, Color.White);
                }

                //Death Animation - Keyboard Warrior
                for (int i = 0; i < dead.Count; i++)
                {
                    spriteBatch.Draw(dead[i], deathPosition[i], Color.White);
                }

                //Draw the Energy Bar - Keyboard Warrior
                if (!pause)
                {
                    if (health == 5)
                    {
                        spriteBatch.Draw(energyBar, energyPosition, Color.White);
                    }

                    if (health == 4)
                    {
                        spriteBatch.Draw(energy2, energyPosition, Color.White);
                    }

                    if (health == 3)
                    {
                        spriteBatch.Draw(energy3, energyPosition, Color.White);
                    }

                    if (health == 2)
                    {
                        spriteBatch.Draw(energy4, energyPosition, Color.White);
                    }
                }

                //Draw the Coins - Keyboard Warrior
                if (coins.Count == 0)
                {
                    spawnCoin('c');
                    spawnCoin('o');
                    spawnCoin('i');
                    spawnCoin('n');
                }

                for (int i = 0; i < coins.Count; i++)
                {
                    spriteBatch.Draw(coins[i], coinPosition[i], Color.White);
                }

                ////Draw the Character Animation - Keyboard Warrior
                //Check current state of Character before Drawing
                if (weapon == 0 && moveSpeed.X != -800)
                {
                    currentChar = character;
                }

                if (weapon == 1 && moveSpeed.X != -800)
                {
                    currentChar = characterWep1;
                }

                if (weapon == 2 && moveSpeed.X != -800)
                {
                    currentChar = characterWep2;
                }

                if (weapon == 3 && moveSpeed.X != -800)
                {
                    currentChar = characterWep3;
                }

                if (weapon == 0 && moveSpeed.X == -800)
                {
                    currentChar = superSayan;
                }

                if (weapon == 1 && moveSpeed.X == -800)
                {
                    currentChar = superSayanWep1;
                }

                if (weapon == 2 && moveSpeed.X == -800)
                {
                    currentChar = superSayanWep2;
                }

                if (weapon == 3 && moveSpeed.X == -800)
                {
                    currentChar = superSayanWep3;
                }

                //Draw Character

                spriteBatch.Draw(currentChar, charPosition,
                new Rectangle(charFrame * spriteWidth, spriteHeight, spriteWidth, spriteHeight), Color.White);

                //Draw the Monsters - Keyboard Warrior
                for (int i = 0; i < monsters.Count; i++)
                {
                    spriteBatch.Draw(monsters[i], monsterPosition[i],
                        new Rectangle(monsterFrame * spriteWidth, spriteHeight, spriteWidth, spriteHeight), Color.White);
                }

                //Draw the Weapons - Keyboard Warrior
                for (int i = 0; i < weapons.Count; i++)
                {
                    spriteBatch.Draw(weapons[i], weaponPosition[i], Color.White);
                }

                //Draw the Attack Animation - Keyboard Warrior
                if (useAttack1)
                {
                    spriteBatch.Draw(attack1, attackPosition1,
                        new Rectangle(attackFrame1 * spriteWidth, spriteHeight, spriteWidth, spriteHeight), Color.White);
                }

                if (useAttack2)
                {
                    spriteBatch.Draw(attack2, attackPosition2,
                        new Rectangle(attackFrame2 * spriteWidth, spriteHeight, spriteWidth, spriteHeight), Color.White);
                }

                if (useAttack3)
                {
                    spriteBatch.Draw(attack3, attackPosition3,
                        new Rectangle(attackFrame3 * spriteWidth, spriteHeight, spriteWidth, spriteHeight), Color.White);
                }

                if (attackPosition1.X > 800)
                {
                    attackPosition1.X = 50;
                    useAttack1 = false;
                }

                if (attackPosition2.X > 800)
                {
                    attackPosition2.X = 50;
                    useAttack2 = false;
                }

                if (attackPosition3.X > 800)
                {
                    attackPosition3.X = 50;
                    useAttack3 = false;
                }

                if (win)
                {
                    spriteBatch.DrawString(gameFont, "You've Won! Congratulations!!", new Vector2(130, 350), Color.White);
                }

                if (lose)
                {
                    spriteBatch.DrawString(gameFont, "You've Lost! Try Again!", new Vector2(145, 350), Color.White);
                }

                //DrawStrings for Developer Testing - Keyboard Warrior
                //if (pause)
                //{
                //    spriteBatch.DrawString(gameFont, "Attack - Swipe\nJump - Tap\nBoost -\nPhone Forward", new Vector2(200, 200), Color.Red);
                //}

                if (!pause)
                {
                    spriteBatch.DrawString(gameFont, "\nBoosters : " + booster
                                           + "\nAmmo : " + ammo + "\nGold : " + gold, new Vector2(energyPosition.X - 150, energyPosition.Y + 60), Color.White);
                    spriteBatch.DrawString(gameFont, "ENERGY", new Vector2(energyPosition.X - 170, energyPosition.Y + 27), Color.White);
                }
            }

            //Achievement Tracking
            for (int i = 0; i < popUp.Count; i++)
            {
                spriteBatch.Draw(popUp[i], popUpPosition[i], Color.White);
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
        #endregion
    }
}
