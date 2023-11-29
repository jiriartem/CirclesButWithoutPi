using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace circunferenceDraw
{
    public partial class DrawScreen : Form
    {
        public DrawScreen()
        {
            InitializeComponent();
        }

        private void DrawScreen_Load(object sender, EventArgs e)
        {
         
        }

        private void DrawScreen_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            //Point pt1;
            //Point pt2;
            //e.Graphics.DrawEllipse(pen, 500, 500, 100, 100);


            Graphics g = e.Graphics;

            int centerX = 100; // X coordinate of the center of the circle
            int centerY = 100; // Y coordinate of the center of the circle
            int radius = 50;   // Radius of the circle

            for (int angle = 0; angle < 360; angle++)
            {
                double radians = angle * (Math.PI / 180);
                int x = (int)(centerX + radius * Math.Cos(radians));
                int y = (int)(centerY + radius * Math.Sin(radians));
                e.Graphics.DrawRectangle(pen, x, y, 1, 1);

                // Display X and Y coordinates
                g.DrawString($"X: {x}, Y: {y}", this.Font, Brushes.Black, x + 5, y);
            }
        }
    }
}
