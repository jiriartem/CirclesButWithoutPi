using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class CircleDrawing : Form
{
    private TextBox txtBoxCoord;
    private List<Point> circlePoints = new List<Point>();

    public CircleDrawing()
    {
        Load();
        //CalculateCirclePoints();
        //CalculateCirclePointsBresenham();
        CalculateCirclePointsBresenhamReduxed();
        this.Paint += new PaintEventHandler(this.CircleDrawing_Paint);
    }

    private void CalculateCirclePoints()
    {
        int centerX = 100; // X coordinate of the center of the circle
        int centerY = 100; // Y coordinate of the center of the circle
        int radius = 100;  // Radius of the circle

        const double piIsThree = Math.PI;

        for (int angle = 0; angle < 90; angle++)
        {
            double radians = angle * (piIsThree / 180);
            int x = (int)(centerX + radius * MiCoseno(radians));
            int y = (int)(centerY + radius * MiSeno(radians));
            circlePoints.Add(new Point(x, y));
        }
    }


    private void CalculateCirclePointsBresenham() {
        {
            int x = 100;
            int y = 100;
            int r = 100;
            
            int x1 = 0;
            int y1 = r;
            //double d = 3 - 2 * r;
            //double d = Math.PI - 2 * r; FUNCIONA IGUAL CON PI pero Bresenham... supo que da lo mismo con un 3.
            //double d = 4 - 2 * r; // oh, sorpresa... funciona con 4
            //double d = 2 - 2 * r; // oh, sorpresa... y funciona con 2???
            //double d = 1 - 2 * r; // oh, sorpresa... y funciona con 1???
            //double d = 0 - 2 * r; // oh, sorpresa... y funciona con 0???
            //double d = 2 * r; // con 2*r vale...???
            //double d = r; // con r vale...
            //double d = -1; // con -1 también...???
            double d = 0; // con -100 también...??? tiene pinta de que esto en el fondo sobra...

            while (x1 <= y1)
            {
                circlePoints.Add(new Point(x + x1, y + y1));
                circlePoints.Add(new Point(x - x1, y + y1));
                circlePoints.Add(new Point(x + x1, y - y1));
                circlePoints.Add(new Point(x - x1, y - y1));
                circlePoints.Add(new Point(x + y1, y + x1));
                circlePoints.Add(new Point(x - y1, y + x1));
                circlePoints.Add(new Point(x + y1, y - x1));
                circlePoints.Add(new Point(x - y1, y - x1));


                /////d += (d > 0) ? (d -= y1--) + x1++ : x1++;//esto es fallo, pero queda divertido

                if (d < 0)
                {
                    d += 4 * x1 + 6;
                }
                else
                {
                    d += 4 * (x1 - y1) + 10;
                    y1--;
                }
                x1++;
            }
        }
    }

    private void CalculateCirclePointsBresenhamReduxed()
    {
        {
            int x = 200;
            int y = 200;
            int r = 200;

            int x1 = 0;
            int y1 = r;

            bool signX;
            bool signY = true;

            int d = 0;
            //d = 3 - 2 * r; //No hace falta ni Pi ni nada que se le parezca.

            int c = 0;

            while (x1 <= y1)
            {
                c++;
                int j = 0;
                for (int i = 0; i <= 8; i++)
                {
                    signX = i % 2 == 0; //par : impar
                    signY = j < 2 ? signY : !signY; //Dos sí : Dos no
                    j = j++ > 1 ? 0 : j++;

                    circlePoints.Add(new Point(
                                genericPoint(x, x1, y1, i, signX),
                                genericPoint(y, y1, x1, i, signY))
                      );


                    //circlePoints.Add(new Point(
                    //        signX ?
                    //              genericPointX1Y1plus(x, x1, y1, i)
                    //            : genericPointX1Y1minus(x, x1, y1, i),
                    //        signY ?
                    //              genericPointY1X1plus(y, x1, y1, i)
                    //            : genericPointY1X1minus(y, x1, y1, i))
                    //    );

                    //if (i < 4)
                    //{
                    //    circlePoints.Add(new Point(signX ? (x + x1) : (x - x1), signY ? (y + y1) : (y - y1)));
                    //}
                    //else {
                    //    circlePoints.Add(new Point(signX ? (x + y1) : (x - y1), signY ? (y + x1) : (y - x1)));
                    //}
                }

                //Lo de arriba es una forma breve de hacer esto:
                //circlePoints.Add(new Point(x + x1, y + y1));
                //circlePoints.Add(new Point(x - x1, y + y1));
                //circlePoints.Add(new Point(x + x1, y - y1));
                //circlePoints.Add(new Point(x - x1, y - y1));
                //circlePoints.Add(new Point(x + y1, y + x1));
                //circlePoints.Add(new Point(x - y1, y + x1));
                //circlePoints.Add(new Point(x + y1, y - x1));
                //circlePoints.Add(new Point(x - y1, y - x1));

                if (d > 0) {
                    d -= y1;
                    y1--;
                }
                
                d += x1;

                x1++;
            }

            MessageBox.Show(c.ToString());
        }
    }

    private static int genericPoint(int i, int j, int k, int h, bool oppMinus)
    {
        int calc = (h < 4) ? j : k;
        calc = oppMinus ? -1 * calc : calc;
        return i + calc;
    }

    private static int genericPointPlus(int p, int p1, int p2, int i)
    {
        return (p + ((i < 4) ? p2 : p1));
    }

    private static int genericPointY1X1minus(int p, int p1, int p2, int i)
    {
        return (p - ((i < 4) ? p2 : p1));
    }

    private static int genericPointY1X1plus(int p, int p1, int p2, int i)
    {
        return (p + ((i < 4) ? p2 : p1));
    }

    private static int genericPointX1Y1minus(int p, int p1, int p2, int i)
    {
        return (p - ((i < 4) ? p1 : p2));
    }

    private static int genericPointX1Y1plus(int p, int p1, int p2, int i)
    {
        return (p + ((i < 4) ? p1 : p2));
    }

    private double MiSeno(double radians)
    {
        return Math.Sin(radians);
        //return Sin(radians);
    }

    private double MiCoseno(double radians)
    {
        //return Math.Cos(radians);
        return Cos(radians);
    }

    private void CircleDrawing_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Graphics gtwo = e.Graphics;
        Pen pen = new Pen(Color.Black);

        // Draw the circle using stored points
        foreach (Point point in circlePoints)
        {
            g.DrawRectangle(pen, point.X, point.Y, 1, 1);
            this.txtBoxCoord.Text = $"X: {point.X}, Y: {point.Y}";
        }
    }

    private int Factorial(int n)
    {
        if (n <= 1)
            return 1;
        else
            return n * Factorial(n - 1);
    }

    private double Cos(double x)
    {
        const int terms = 10; // Number of terms in the Taylor series
        double result = 0;
        for (int i = 0; i < terms; i++)
        {
            int exponent = 2 * i;
            double term = Math.Pow(-1, i) * Math.Pow(x, exponent) / Factorial(exponent);
            result += term;
        }
        return result;
    }

    private double Sin(double cosAngle)
    {
        return Math.Sqrt(1 - cosAngle * cosAngle);
    }

    private new void Load()
    {
        this.txtBoxCoord = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // txtBoxCoord
        // 
        this.txtBoxCoord.Location = new System.Drawing.Point(374, 341);
        this.txtBoxCoord.Name = "txtBoxCoord";
        this.txtBoxCoord.Size = new System.Drawing.Size(100, 22);
        this.txtBoxCoord.TabIndex = 0;
        // 
        // CircleDrawing
        // 
        this.ClientSize = new System.Drawing.Size(936, 871);
        this.Controls.Add(this.txtBoxCoord);
        this.Name = "CircleDrawing";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // CircleDrawing
            // 
            this.ClientSize = new System.Drawing.Size(1264, 735);
            this.Name = "CircleDrawing";
            this.ResumeLayout(false);

    }
}
