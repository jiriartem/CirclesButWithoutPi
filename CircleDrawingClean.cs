using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class CircleDrawingClean : Form
{
    private TextBox txtBoxCoord;
    private List<Point> circlePoints = new List<Point>();

    public CircleDrawingClean()
    {
        Load();
        CalculateCirclePointsBresenhamReduxed();
        this.Paint += new PaintEventHandler(this.CircleDrawing_Paint);
    }

    private void CalculateCirclePointsBresenhamReduxed()
    {
        {
            int x = 200; // Simplemente la posición inicial x
            int y = 200; // Simplemente la posición inicial y
            int r = 200; // RADIO El único valor clave... para definir la circunferencia.

            int x1 = 0;
            int y1 = r; // La parte y1 empieza a la derecha en función del radio

            bool signX; 
            bool signY = true;

            int d = 0; //Toma valores -r a + r para dibujar left, rigth, up, down
            //d = 3 - 2 * r; //No hace falta ni Pi ni nada que se le parezca.

            //Este bucle representa cada punto.
            while (x1 <= y1)
            {
                int j = 0; //En el punto y (cada dos cambia de signo) ver algoritmo original.
                //este otro
                for (int i = 0; i <= 8; i++)
                {
                    signX = i % 2 == 0; //El signo de sumar o restar en X cambia entre par : impar
                    signY = j < 2 ? signY : !signY; //El signo de sumar o restar cambia para Y pero en este caso Dos sí : Dos no, se lleva la cuenta en j
                    j = j++ > 1 ? 0 : j++;

                    circlePoints.Add(new Point(
                                genericPoint(x, x1, y1, i, signX),
                                genericPoint(y, y1, x1, i, signY))
                      );
                }

                if (d > 0)
                {
                    d -= y1--;
                }

                d += x1++;
            }
        }
    }

    private static int genericPoint(int i, int j, int k, int h, bool oppMinus)
    {
        //Se hacen 4 de un modo y otras 4 de otro, son 8 movientos, desde left, rigth, up or down
        int calc = (h < 4) ? j : k;
        calc = oppMinus ? -1 * calc : calc; //esto es básicamente que sume o reste.
        return i + calc;
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

}
