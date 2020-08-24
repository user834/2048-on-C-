using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int score = 0;
        const int tile_s = 50, grid = 4;
        int[,] field = new int[grid, grid];
        List<int> rd_list = new List<int>{ 2, 4 };
        Random random = new Random();
        int index, x_place, y_place;


        public Form1()
        {
            InitializeComponent();
            GenBox();
            GenBox();
            GenBox();
            GenBox();
            GenBox();
            GenBox();
        }

        private void GenBox()
        {
            index = random.Next(rd_list.Count);
            x_place = random.Next(0, 4);
            y_place = random.Next(0, 4);

            if (IsCellEmpty(x_place, y_place))
                field[x_place, y_place] = rd_list[index];
            else
                GenBox();
        }

        private bool IsCellEmpty(int i, int j)
        {
            if (field[i,j] == 0)
                return true;
            return false;
        }

        private void pbCanvas_Print(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.FillRectangle(Brushes.LightSlateGray, 0, 0, 225, 25);
            canvas.DrawString("Score: " + score.ToString(), this.Font, Brushes.White, new PointF(5, 5));

            for (int i = 0; i < grid; i++) 
            {
                for (int j = 0; j < grid; j++)
                {
                    if (!IsCellEmpty(i, j))
                    {
                        Font font = this.Font;
                        string cell_value = field[i, j].ToString();
                        SizeF st_size = canvas.MeasureString(cell_value, font);
                        int x_axis = j * 55 + 5, y_axis = i * 55 + 30;

                        canvas.FillRectangle(Brushes.Orange, x_axis, y_axis, tile_s, tile_s);
                        canvas.DrawString(cell_value, font, Brushes.Black, new PointF(x_axis + tile_s / 2 - st_size.Width / 2,
                                                                                      y_axis + tile_s / 2 - st_size.Height / 2));
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }
    }
}
