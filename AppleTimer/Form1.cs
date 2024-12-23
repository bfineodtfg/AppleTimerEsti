﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppleTimer
{
    public partial class Form1 : Form
    {
        Timer walkTimer = new Timer();
        Timer hitTimer = new Timer();
        Timer gravity = new Timer();

        int hitCount = 0;
        int hitDirection = 1;//1: balra; 0: nyugalmi állapot; -1: jobbra
        int hitFrames = 0;
        int hitMaxFrames = 3;
        int requiredHits = 10;
        int apples = 0;
        int storageCapacity = 20;
        int priceOfBiggerStorage = 10;
        int priceOfFasterHit = 30;

        bool holdingAnApple = false;
        //bool facingLeft = true;

        public Form1()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            StartTimers();
            AddEvents();
        }
        void AddEvents()
        {
            buyBiggerStorage.Click += BuyBiggerStorage;
            buyFasterHit.Click += BuyFasterHit;
        }
        void StartTimers()
        {
            walkTimer.Interval = 16;
            hitTimer.Interval = 16;
            gravity.Interval = 16;
            walkTimer.Start();
            walkTimer.Tick += WalkEvent;
            hitTimer.Tick += HitEvent;
            gravity.Tick += GravityEvent;
        }
        void WalkEvent(object s, EventArgs e)
        {
            if (kez.Left > torzs.Right && !holdingAnApple)
            {
                fej.Left -= 3;
                test.Left -= 3;
                kez.Left -= 3;
            }
            else if (!holdingAnApple)
            {
                walkTimer.Stop();
                hitDirection = 1;
                hitTimer.Start();
            }
            else if (holdingAnApple && test.Right < kosar.Left)
            {
                fej.Left += 3;
                test.Left += 3;
                kez.Left += 3;
                alma.Left += 3;
            }
            else if (holdingAnApple && test.Right >= kosar.Left) {
                walkTimer.Stop();
                gravity.Start();
            }
        }
        void HitEvent(object s, EventArgs e)
        {
            if (hitDirection == 1)
            {
                kez.Left -= 6;
                hitFrames++;
                if (hitFrames == hitMaxFrames)
                {
                    hitDirection = -1;
                    hitFrames = 0;
                }
            }
            else if (hitDirection == -1)
            {
                kez.Left += 6;
                hitFrames++;
                if (hitFrames == hitMaxFrames)
                {
                    hitFrames = 0;
                    hitCount++;
                    this.Text = hitCount.ToString();
                    if (hitCount == requiredHits)
                    {
                        hitDirection = 0;
                        hitTimer.Stop();
                        alma.Left = kez.Left;
                        alma.Top = lomb.Bottom - alma.Height;
                        alma.Show();
                        gravity.Start();
                    }
                    else
                    {
                        hitDirection = 1;
                    }
                }
            }
        }
        void GravityEvent(object s, EventArgs e)
        {
            if (alma.Bottom < kez.Top && !holdingAnApple)
            {
                alma.Top += 3;
            }
            else if (!holdingAnApple)
            {
                holdingAnApple = true;
                gravity.Stop();
                kez.Left = test.Left + test.Width / 2;
                alma.Left = kez.Right - alma.Width;
                walkTimer.Start();
            }
            else if (holdingAnApple) {
                alma.Top += 3;
                if (alma.Top > kosar.Top)
                {
                    gravity.Stop();
                    holdingAnApple = false;
                    kez.Left = test.Left + test.Width / 2 - kez.Width;
                    hitCount = 0;
                    if (apples < storageCapacity)
                    {
                        apples++;
                    }
                    UpdateAppleLabel();
                    walkTimer.Start();
                }
            }
            
        }
        void BuyBiggerStorage(object s, EventArgs e)
        {
            if (apples >= priceOfBiggerStorage)
            {
                apples -= priceOfBiggerStorage;
                storageCapacity += 5;
                priceOfBiggerStorage += 2;
                buyBiggerStorage.Text = $"{priceOfBiggerStorage} alma";

                UpdateAppleLabel();
                UpdateStorageLabel();
            }
        }
        void BuyFasterHit(object s, EventArgs e)
        {
            if (apples >= priceOfFasterHit && requiredHits > 3)
            {
                apples -= priceOfFasterHit;
                requiredHits--;
                priceOfFasterHit += 30;
                buyFasterHit.Text = $"{priceOfFasterHit} alma";

                UpdateAppleLabel();
            }
        }
        void UpdateAppleLabel()
        {
            appleCounter.Text = $"Gyűjtött almák száma: {apples}";
        }
        void UpdateStorageLabel()
        {
            storageLabel.Text = $"Kosár teherbírása: {storageCapacity} alma";
        }
    }
}
