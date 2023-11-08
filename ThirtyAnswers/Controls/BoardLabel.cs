﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirtyAnswers.Controls
{
    public class BoardLabel : Label
    {
        protected int _xOffset = 5;
        protected int _yOffset = 5;

        public BoardLabel() : base() => InitializeComponent();

        /// <summary>Specifies the solid-colour value of the shadow. No alpha information from this setting is used.</summary>
        /// <remarks>Alpha blending is handled programmatically via the <i>Alpha</i> accessor value.</remarks>
        /// <seealso cref="Alpha"/>
        public Color ShadowColor { get; set; } = Color.Black;

        public int xOffset
        {
            get => this._xOffset;
            set => this._xOffset = (value < 0) ? Math.Max(value, -25) : Math.Min(25, value);
        }


        public int yOffset
        {
            get => this._yOffset;
            set => this._yOffset = (value < 0) ? Math.Max(value, -25) : Math.Min(25, value);
        }

        /// <summary>Specifies the starting Alpha value of the shadow (how solid is it).</summary>
        /// <remarks>The shadow is made more transparent as it deepens, from this value to zero.</remarks>
        public byte Alpha { get; set; } = 255;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int xStart = Math.Min(this.Location.X, this.Location.X + xOffset),
                xEnd = Math.Max(this.Location.X, this.Location.X + xOffset),
                yStart = Math.Min(this.Location.Y, this.Location.Y + yOffset),
                yEnd = Math.Max(this.Location.Y, this.Location.Y + yOffset),
                steps, xIncrement, yIncrement, alphaIncrement;

            steps = Math.Max(xEnd - xStart, yEnd - yStart);
            xIncrement = (xOffset < 0 ? -1 : 1) * (int)Math.Floor((xEnd - xStart) / (float)steps);
            yIncrement = (yOffset < 0 ? -1 : 1) * (int)Math.Floor((yEnd - yStart) / (float)steps);
            alphaIncrement = (int)Math.Floor(Alpha / (float)steps);

            if (steps > 0)
            {
                for (int i = steps; i > 0; i--)
                    g.DrawString(
                        this.Text,
                        this.Font,
                        new SolidBrush(
                                Color.FromArgb(
                                    this.Alpha - (alphaIncrement * i),
                                    ShadowColor.R,
                                    ShadowColor.G,
                                    ShadowColor.B
                                )
                            ),
                            new PointF()
                            {
                                X = (xIncrement * i), // this.Location.X + (xIncrement * i), 
                                Y = (yIncrement * i)  // this.Location.Y + (yIncrement * i) 
                            }
                        );

                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new PointF(0f, 0f));
            }
            else base.OnPaint(e);
        }


        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() =>
            components = new System.ComponentModel.Container();
    }
}