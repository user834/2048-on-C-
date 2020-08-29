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
        bool action, gameover;
        int[,] field;
        Random random;
        List<int> rd_list;
        Dictionary<string, Brush> color_dict;
        int score, index, x_place, y_place, tile_s, grid;


        public Form1()
        {
            InitializeComponent();
            BrushesInit();
            StartGame();

            GameTimer.Interval = 1000 / 20;
            GameTimer.Start();
        }

        private void StartGame()
        {
            random = new Random();
            rd_list = new List<int> { 2, 4 };
            tile_s = 50;
            score = 0;
            grid = 4;

            action = false;
            gameover = false;
            field = new int[grid, grid];
            GenBox();
            GenBox();
        }

        private bool IsGameOver()
        {
            foreach (int cell in field)
                if (cell == 2048)
                    return true;

            foreach (int cell in field)
                if (cell == 0)
                    return false;

            int temp;
            for (int i = 0; i < grid; i++)
            {
                temp = 0;
                for(int j = 0; j < grid; j++)
                {
                    if (field[i, j] == temp)
                        return false;
                    temp = field[i, j];
                }
            }
            for (int i = 0; i < grid; i++)
            {
                temp = 0;
                for (int j = 0; j < grid; j++)
                {
                    if (field[j, i] == temp)
                        return false;
                    temp = field[j, i];
                }
            }
            return true;

        }

        private void BrushesInit()
        {
            color_dict = new Dictionary<string, Brush>
            {
                {"2", Brushes.LightSkyBlue},
                {"4", Brushes.SkyBlue},
                {"8", Brushes.DeepSkyBlue},
                {"16", Brushes.DarkTurquoise},
                {"32", Brushes.SlateBlue},
                {"64", Brushes.MediumPurple},
                {"128", Brushes.MediumOrchid},
                {"256", Brushes.Orchid},
                {"512", Brushes.Violet},
                {"1024", Brushes.Plum},
                {"2048", Brushes.Pink}
            };
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

        private void Update(object sender, EventArgs e)
        {
            int[,] field_cp = (int[,])field.Clone();

            if (gameover)
            {
                if (Input.Pressed(Keys.Enter))
                    StartGame();
            }  
            else
            {
                if (Input.Pressed(Keys.Up))
                    MoveUp();

                if (Input.Pressed(Keys.Down))
                    MoveDown();

                if (Input.Pressed(Keys.Right))
                    MoveRight();

                if (Input.Pressed(Keys.Left))
                    MoveLeft();
            }


            if (action)
            {
                if (!IsEqual(field, field_cp))
                    GenBox();
                action = false;
            }

            if (IsGameOver())
                gameover = true;

            pbCanvas.Invalidate();
        }

        private bool IsEqual(int[,] arr1, int[,] arr2)
        {
            for (int i = 0; i < grid; i++)
                for (int j = 0; j < grid; j++)
                    if (arr1[i, j] != arr2[i, j])
                        return false;
            return true;
        }

        private void MoveDown()
        {
            int shift, temp;
            for (int i = grid - 1; i >= 0; i--)
            {
                temp = 0;
                shift = 0;
                for (int j = grid - 1; j >= 0; j--)
                {
                    if (field[j, i] == 0)
                        shift++;
                    else
                    {
                        if (temp != 0 && temp == field[j,i])
                        {
                            score += field[j + shift + 1, i];
                            field[j + shift + 1, i] *= 2;
                            field[j, i] = 0;
                            temp = 0;
                            shift++;
                        }
                        else
                        {
                            temp = field[j, i];
                            field[j + shift, i] = field[j, i];
                            if (shift != 0)
                                field[j, i] = 0;
                        }
                    }
                }
            }
            action = true;
        }

        private void MoveUp()
        {
            int shift, temp;
            for (int i = 0; i < grid; i++)
            {
                temp = 0;
                shift = 0;
                for (int j = 0; j < grid; j++)
                {
                    if (field[j, i] == 0)
                        shift++;
                    else
                    {
                        if (temp != 0 && temp == field[j, i])
                        {
                            score += field[j - shift - 1, i];
                            field[j - shift - 1, i] *= 2;
                            field[j, i] = 0;
                            temp = 0;
                            shift++;
                        }
                        else
                        {
                            temp = field[j, i];
                            field[j - shift, i] = field[j, i];
                            if (shift != 0)
                                field[j, i] = 0;
                        }
                    }
                }
            }
            action = true;
        }

        private void MoveRight()
        {
            int shift, temp;
            for (int i = grid - 1; i >= 0; i--)
            {
                temp = 0;
                shift = 0;
                for (int j = grid - 1; j >= 0; j--)
                {
                    if (field[i, j] == 0)
                        shift++;
                    else
                    {
                        if (temp != 0 && temp == field[i, j])
                        {
                            score += field[i, j + shift + 1];
                            field[i, j + shift + 1] *= 2;
                            field[i, j] = 0;
                            temp = 0;
                            shift++;
                        }
                        else
                        {
                            temp = field[i, j];
                            field[i, j + shift] = field[i, j];
                            if (shift != 0)
                                field[i, j] = 0;
                        }
                    }
                }
            }
            action = true;
        }

        private void MoveLeft()
        {
            int shift, temp;
            for (int i = 0; i < grid; i++)
            {
                temp = 0;
                shift = 0;
                for (int j = 0; j < grid; j++)
                {
                    if (field[i, j] == 0)
                        shift++;
                    else
                    {
                        if (temp != 0 && temp == field[i, j])
                        {
                            score += field[i, j - shift - 1];
                            field[i, j - shift - 1] *= 2;
                            field[i, j] = 0;
                            temp = 0;
                            shift++;
                        }
                        else
                        {
                            temp = field[i, j];
                            field[i, j - shift] = field[i, j];
                            if (shift != 0)
                                field[i, j] = 0;
                        }
                    }
                }
            }
            action = true;
        }

        private void pbCanvas_Print(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Font score_font = new Font(SystemFonts.DefaultFont.FontFamily, 11, FontStyle.Regular);
            canvas.FillRectangle(Brushes.LightSlateGray, 0, 0, 225, 25);
            canvas.DrawString("Score: " + score.ToString(), score_font, Brushes.White, new PointF(3, 2));

            for (int i = 0; i < grid; i++) 
            {
                for (int j = 0; j < grid; j++)
                {
                    if (!IsCellEmpty(i, j))
                    {
                        Font font = new Font(SystemFonts.DefaultFont.FontFamily, 13, FontStyle.Bold);
                        string cell_value = field[i, j].ToString();
                        SizeF st_size = canvas.MeasureString(cell_value, font);
                        int x_axis = j * 55 + 5, y_axis = i * 55 + 30;

                        canvas.FillRectangle(color_dict[cell_value], x_axis, y_axis, tile_s, tile_s);
                        canvas.DrawString(cell_value, font, Brushes.Black, new PointF(x_axis + tile_s / 2 - st_size.Width / 2,
                                                                                      y_axis + tile_s / 2 - st_size.Height / 2));
                    }
                }
            }
            if (gameover)
            {
                int msg_x = 25, msg_y = 87, width = 175, height = 100;
                Font font = new Font(SystemFonts.DefaultFont.FontFamily, 20, FontStyle.Bold);
                string gameover_st = "Game over";
                string press_enter_st = "Press ENTER to start over";
                SizeF msg_size = canvas.MeasureString(gameover_st, font);
                canvas.FillRectangle(Brushes.WhiteSmoke, msg_x, msg_y, width, height);
                canvas.DrawRectangle(new Pen(Brushes.Black), msg_x, msg_y, width, height);
                canvas.DrawString(gameover_st, font, Brushes.Black, new PointF(msg_x + width / 2 - msg_size.Width / 2,
                                                                               msg_y + height / 2 - msg_size.Height / 2 - 15));
                font = new Font(SystemFonts.DefaultFont.FontFamily, 9, FontStyle.Bold);
                msg_size = canvas.MeasureString(press_enter_st, font);
                canvas.DrawString(press_enter_st, font, Brushes.Black, new PointF(msg_x + width / 2 - msg_size.Width / 2,
                                                                                  msg_y + height / 2 - msg_size.Height / 2 + 30));

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
