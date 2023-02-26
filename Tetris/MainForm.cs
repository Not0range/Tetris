using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace Tetris
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Время игры
        /// </summary>
        public static int time = 0;
        /// <summary>
        /// Набранные очки (заполненные строки)
        /// </summary>
        public static int score = 0;
        /// <summary>
        /// Если true, то отображается подсказка следующей фигуры, иначе не отображается
        /// </summary>
        public static bool help = true;
        /// <summary>
        /// true, если в данный момент идёт игра, иначе false
        /// </summary>
        bool game = false;
        /// <summary>
        /// Когда true, музычка играет, иначе не играет
        /// </summary>
        bool play = true;
        /// <summary>
        /// Звуковой плеер
        /// </summary>
        SoundPlayer player = new SoundPlayer("fon.wav");
        /// <summary>
        /// Форма "Правила"
        /// </summary>
        Rules rules = new Rules();
        /// <summary>
        /// Форма "Настройки"
        /// </summary>
        Settings settings = new Settings();
        /// <summary>
        /// Форма "Таблица лидеров"
        /// </summary>
        Leader leaderboard = new Leader();
        /// <summary>
        /// Сетки
        /// </summary>
        Bitmap grid, previewGrid;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузке формы...
        /// </summary>
        private void Form1_Load_1(object sender, EventArgs e)
        {
            time = 0;
            score = 0;

            player.PlayLooping();
            grid = new Bitmap(Grid.Width, Grid.Height);
            DrawGrid(10, 20,grid);

            previewGrid = new Bitmap(PreviewGrid.Width, PreviewGrid.Height);
            DrawGrid(7, 5, previewGrid);
        }

        /// <summary>
        /// Рисует сетку на Bitmap'е
        /// </summary>
        /// <param name="x">
        /// Количество столбцов
        /// </param>
        /// <param name="y">
        /// Количество строк
        /// </param>
        /// <param name="b">
        /// Bitmap, на котором нужно нарисовать сетку
        /// </param>
        private void DrawGrid(int x, int y, Bitmap b)
        {
            Graphics g = Graphics.FromImage(b);
            Pen pen = new Pen(Color.Blue, 2);
            for (int i = 0; i <= x; i++)
                g.DrawLine(pen, b.Width / x * i, 0, b.Width / x * i, b.Height);

            for (int i = 0; i <= y; i++)
                g.DrawLine(pen, 0, b.Height / y * i, Width, b.Height / y * i);
        }

        /// <summary>
        /// Кнопка "Звук"
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (play == true)
            {
                player.Stop();
                play = false;
            }
            else
            {
                player.Play();
                play = true;
            }
        }

        /// <summary>
        /// Кнопка "Правила"
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (game == true)
            {
                Counter.Stop();
                FallTick.Stop();
            }
            rules.ShowDialog();
            if (game == true)
            {
                Counter.Start();
                FallTick.Start();
            }
        }

        /// <summary>
        /// Кнопка "Настройки"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (game == true)
            {
                Counter.Stop();
                FallTick.Stop();
            }
            settings.ShowDialog();
            if (game == false || (CubeGroup.cubeCount == settings.Count && settings.NextVisible == help))
            {
                help = settings.NextVisible;
                PreviewGrid.Visible = help;
                label1.Visible = help;
                CubeGroup.cubeCount = settings.Count;
            }
            else if (MessageBox.Show("Вы изменили настройки игры." + Environment.NewLine + "Начать заново?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                help = settings.NextVisible;
                PreviewGrid.Visible = help;
                label1.Visible = help;
                CubeGroup.cubeCount = settings.Count;
                NewGame();
            }
            if (game == true)
            {
                Counter.Start();
                FallTick.Start();
            }
            Grid.Invalidate();
            PreviewGrid.Invalidate();
        }

        /// <summary>
        /// Отрисовка игрового поля
        /// </summary>
        private void Grid_Paint(object sender, PaintEventArgs e)
        {
            Score.Text = String.Format("{0, 6:D6}", score);

            if (settings.Grid == true)
                e.Graphics.DrawImage(grid, 0, 0);

            if (Cube.cubes != null)
            {
                for (int i = 0; i < 20; i++)
                    for (int j = 0; j < 10; j++)
                        if (Cube.cubes[i, j] != null)
                            e.Graphics.FillRectangle(new SolidBrush(Cube.cubes[i, j].color), grid.Width / 10 * j, grid.Height / 20 * i, grid.Width / 10, grid.Height / 20);
            }

            if (CubeGroup.Current != null)
            {
                if (CubeGroup.Current[0].I >= 0)
                    e.Graphics.FillRectangle(new SolidBrush(CubeGroup.Current[0].color), grid.Width / 10 * CubeGroup.Current[0].J, grid.Height / 20 * CubeGroup.Current[0].I, grid.Width / 10, grid.Height / 20);

                for (int i = 1; i < CubeGroup.cubeCount; i++)
                    if (CubeGroup.Current[0].I + CubeGroup.Current[i].I >= 0)
                        e.Graphics.FillRectangle(new SolidBrush(CubeGroup.Current[i].color), grid.Width / 10 * (CubeGroup.Current[0].J + CubeGroup.Current[i].J), grid.Height / 20 * (CubeGroup.Current[0].I + CubeGroup.Current[i].I), grid.Width / 10, grid.Height / 20);
            }
        }

        /// <summary>
        /// Отрисовка окна "Следующая фигура"
        /// </summary>
        private void PreviewGrid_Paint(object sender, PaintEventArgs e)
        {
            if (settings.Grid == true)
                e.Graphics.DrawImage(previewGrid, 0, 0);

            if (CubeGroup.Next != null)
            {
                int d = 2 - CubeGroup.Next[0].I;
                e.Graphics.FillRectangle(new SolidBrush(CubeGroup.Next[0].color), grid.Width / 10 * (CubeGroup.Next[0].J - 2), grid.Height / 20 * (CubeGroup.Next[0].I + d), grid.Width / 10, grid.Height / 20);

                for (int i = 1; i < CubeGroup.cubeCount; i++)
                    e.Graphics.FillRectangle(new SolidBrush(CubeGroup.Next[i].color), grid.Width / 10 * (CubeGroup.Next[0].J - 2 + CubeGroup.Next[i].J), grid.Height / 20 * (CubeGroup.Next[0].I + d + CubeGroup.Next[i].I), grid.Width / 10, grid.Height / 20);
            }
        }

        /// <summary>
        /// Обработка вижения курсора мыши по форме.
        /// Сделал для затухания кнопок меню, чтобы была возможность управлять игрой
        /// </summary>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.X > 500 && e.Y < 270)
            {
                foreach (Control c in Controls)
                    if (c is Button)
                        c.Enabled = true;
                Counter.Stop();
                FallTick.Stop();
            }
            else
            {
                foreach (Control c in Controls)
                    if (c is Button)
                        c.Enabled = false;
                Counter.Start();
                FallTick.Start();
            }
        }

        /// <summary>
        /// опускает фигуру на одну позицию один раз в определённое время, которое уменьшается по мере игры
        /// </summary>
        private void Fall_Tick(object sender, EventArgs e)
        {
            if (CubeGroup.Current.Fall() == false)
                Lose();
            Grid.Invalidate();
            PreviewGrid.Invalidate();
        }

        /// <summary>
        /// Обрабатывает нажатие клавиш
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                CubeGroup.Current.Rotate();
            else if (e.KeyCode == Keys.Down)
            {
                FallTick.Stop();
                FallTick.Start();
                if (CubeGroup.Current.Fall() == false)
                    Lose();
            }
            else if (e.KeyCode == Keys.Left)
                CubeGroup.Current.Move(-1);
            else if (e.KeyCode == Keys.Right)
                CubeGroup.Current.Move(1);
            Grid.Invalidate();
            PreviewGrid.Invalidate();
        }

        /// <summary>
        /// Счётчик времени
        /// </summary>
        private void Counter_Tick(object sender, EventArgs e)
        {
            Time.Text = String.Format("{0, 2:D2}:{1,2:D2}", (++time >= 3600) ? time / 3600 : time / 60, (time >= 3600) ? time % 3600 / 60 : time % 60);
            if(1500 - 100 * (time / 60) != FallTick.Interval)
                FallTick.Interval = (100 * (time / 60) < 1200) ? 1500 - 100 * (time / 60) : 300;
        }

        /// <summary>
        /// Кнопка "Начать игру"
        /// </summary>
        private void PlayGame_Click(object sender, EventArgs e)
        {
            if (game == false || (game == true && MessageBox.Show("Вы действительно хотите начать заново?", "", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                NewGame();
                Invalidate();
            }
        }

        /// <summary>
        /// Начинает новую игру
        /// </summary>
        private void NewGame()
        {
            score = 0;
            time = 0;
            Score.Text = "000000";
            Time.Text = "00:00";
            CubeGroup.Current = CubeGroup.Builder();
            CubeGroup.Next = CubeGroup.Builder();
            Cube.cubes = new Cube[20, 10];
            foreach (Control c in Controls)
                if (c is Button)
                    c.Enabled = false;
            if (game == false)
            {
                this.MouseMove += Form1_MouseMove;
                this.KeyDown += Form1_KeyDown;
            }
            Counter.Start();
            FallTick.Start();
            game = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LeaderButton_Click(object sender, EventArgs e)
        {
            leaderboard.Show();
        }

        /// <summary>
        /// Выполняет действия,когда игрок пригрывает (переполнение по вертикали)
        /// </summary>
        private void Lose()
        {
            FallTick.Stop();
            Counter.Stop();
            this.KeyDown -= Form1_KeyDown;
            this.MouseMove -= Form1_MouseMove;
            CubeGroup.Current = null;
            CubeGroup.Next = null;
            int i;
            for (i = 0; i < 5; i++)
                if (score > leaderboard.scores[i])
                    break;
            if(i >= 5)
                MessageBox.Show("Вы проиграли");
            else
            {
                leaderboard.newScore = score;
                leaderboard.position = i;
                leaderboard.ShowDialog();
            }
            CubeGroup.cubeCount = settings.Count;
            help = settings.NextVisible;
            PreviewGrid.Visible = help;
            label1.Visible = help;
            game = false;
            foreach(Control c in Controls)
                    if (c is Button)
                        c.Enabled = true;
        }
    }

    /// <summary>
    /// Класс описания квадратика (кубика (или кому как угодно)).
    /// Через клавишу F2 можно переименовать класс (да и вообще любую переменную, свойство и так далее) безо всяких потерь и ошибок.
    /// (Лишний текст удалить)
    /// </summary>
    class Cube
    {
        /// <summary>
        /// Массив квадратиков (больше я этого писать не буду)
        /// </summary>
        public static Cube[,] cubes;

        /// <summary>
        /// Индекс текущего квадратика (строка)
        /// </summary>
        public int I;

        /// <summary>
        /// Индекс текущего квадратика (столбец)
        /// </summary>
        public int J;

        /// <summary>
        /// Цвет квадратика
        /// </summary>
        public Color color;

        /// <summary>
        /// Конструктор класса квадратика.
        /// Проще говоря, создаёт новый квадратик с определёнными параметрами (местоположение и цвет)
        /// </summary>
        /// <param name="i">
        /// Строка, в которой находится квадратик
        /// </param>
        /// <param name="j">
        /// Столбец, в которой находится квадратик
        /// </param>
        /// <param name="c">
        /// Цвет квадратика
        /// </param>
        public Cube(int i, int j, Color c)
        {
            I = i;
            J = j;
            color = c;
        }

        /// <summary>
        /// Проверка на заполенные строки
        /// </summary>
        public static void CheckLine()
        {
            for(int i = 0; i < cubes.GetLength(0); i++)
            {
                bool full = true;
                for(int j = 0; j < cubes.GetLength(1); j++)
                {
                    if(cubes[i, j] == null)
                    {
                        full = false;
                        break;
                    }
                }

                if(full)
                {
                    for(int k = i; k > 0; k--)
                    {
                        for(int j = 0; j < cubes.GetLength(1); j++)
                        {
                            if (cubes[k - 1, j] != null)
                                cubes[k, j] = cubes[k - 1, j];
                            else
                                cubes[k, j] = null;
                        }
                    }

                    for (int j = 0; j < cubes.GetLength(1); j++)
                        cubes[0, j] = null;

                    MainForm.score += (CubeGroup.cubeCount - 3) * ((MainForm.help == true) ? 1 : 2);
                }
            }
        }
    }

    /// <summary>
    /// Класс описания группы квадратиков (кубироков (или....)).
    /// Дополнения смотреть в описании предыдущего класса.
    /// </summary>
    class CubeGroup
    {
        /// <summary>
        /// Группа квадратиков, которая в данный момент падает (нееееееет...)
        /// </summary>
        public static CubeGroup Current;

        /// <summary>
        /// Группа квадратиков, которая будет падать следующей и которая отображается в окне "Следующая фигура"
        /// </summary>
        public static CubeGroup Next;

        /// <summary>
        /// Количество квадратиков в группе
        /// </summary>
        public static int cubeCount = 4;

        /// <summary>
        /// Массив, содержащий квадратики, то есть сама группа
        /// </summary>
        Cube[] group;

        /// <summary>
        /// Конструктор класса группы.
        /// Проще говоря, создаёт новую пустую группу
        /// </summary>
        public CubeGroup()
        {
            group = new Cube[cubeCount];
        }

        /// <summary>
        /// Индексатор класса.
        /// Проще говоря, позволяет получать или задавать элементы массива, не указывая на него ссылку (если что, могу объяснить подробнее, но не здесь)
        /// </summary>
        /// <param name="i">
        /// Индекс элемента массива группы
        /// </param>
        /// <returns>
        /// Возвращает квадратик, соответствующий индексу
        /// </returns>
        public Cube this[int i]
        {
            get
            {
                return group[i];
            }
            set
            {
                group[i] = value;
            }
        }

        /// <summary>
        /// Разворот группы (только против часовой стрелки)
        /// </summary>
        public void Rotate()
        {
            #region Проверка на возможность разворота
            for (int i = 1; i < group.Length; i++)
            {
                int tempI = -group[i].J;
                int tempJ = group[i].I;

                if (tempJ + group[0].J < 0 || tempJ + group[0].J > 9 || group[0].I + tempI > 19)
                    return;

                if (group[0].I + tempI >= 0 && group[0].I + tempI < 20 && group[0].J + tempJ < 10 && group[0].J + tempJ >= 0 && Cube.cubes[group[0].I + tempI, group[0].J + tempJ] != null)
                    return;
            }
            #endregion

            for (int i = 1; i < group.Length; i++)
            {
                int temp = group[i].I;
                group[i].I = -group[i].J;
                group[i].J = temp;
            }
        }

        /// <summary>
        /// Перемещает группу (если это возможно) по горизонтали. Если d положетельно, то вправо, если отрицательно - влево
        /// </summary>
        /// <param name="d">
        /// Смещение группы по горизонтали
        /// </param>
        public void Move(int d)
        {
            if(group[0].J >= 0 && group[0].J <= 9)
            {
                for (int i = 1; i < group.Length; i++)
                    if (group[0].J + group[i].J + d < 0 || group[0].J + group[i].J + d > 9 || (group[0].I + group[i].I > 0 && Cube.cubes[group[0].I + group[i].I, group[0].J + group[i].J + d] != null))
                        return;

                group[0].J += d;
            }
        }

        /// <summary>
        /// Опускает группу на одну позицию, если это возможно, в противном случае фиксирует положение группы на поле
        /// </summary>
        /// <returns>
        /// Возвращает false, если группа была зафиксированна выше поля (то есть произошло переполнение по вертикали), в остальных случаях возвращает true
        /// </returns>
        public bool Fall()
        {
            bool fall = false;
            if (group[0].I == 19 || (group[0].I >= 0 && Cube.cubes[group[0].I + 1, group[0].J] != null))
                fall = true;
            else
            {
                for (int i = 1; i < group.Length; i++)
                {
                    if (group[0].I + group[i].I == 19 || (group[0].I + group[i].I >= 0 && Cube.cubes[group[0].I + group[i].I + 1, group[0].J + group[i].J] != null))
                    {
                        fall = true;
                        break;
                    }
                }
            }

            if (fall)
            {
                bool lose = false;
                if(group[0].I >= 0)
                    Cube.cubes[group[0].I, group[0].J] = group[0];

                for (int i = 1; i < group.Length; i++)
                {
                    if (group[0].I + group[i].I >= 0)
                        Cube.cubes[group[0].I + group[i].I, group[0].J + group[i].J] = new Cube(group[0].I + group[i].I, group[0].J + group[i].J, group[i].color);
                    else
                        lose = true;
                }
                if (lose)
                    return false;
                Cube.CheckLine();
                Current = Next;
                Next = Builder();
            }
            else
                group[0].I++;
            return true;
        }

        /// <summary>
        /// Строитель (если его можно так назвать) групп квадратиков
        /// </summary>
        /// <returns>
        /// Возвращает новую случайно сформированную (в рамках ограничений в виде предельного количества квадратиков в группе) группу
        /// </returns>
        public static CubeGroup Builder()
        {
            Random rand = new Random();
            CubeGroup cubeGroup = new CubeGroup();
            switch(cubeCount)
            {
                case 4:
                    switch(rand.Next(7))
                    {
                        case 0:
                            cubeGroup[0] = new Cube(0, 4, Color.DarkCyan);
                            cubeGroup[1] = new Cube(0, -1, Color.DarkCyan);
                            cubeGroup[2] = new Cube(0, 1, Color.DarkCyan);
                            cubeGroup[3] = new Cube(0, 2, Color.DarkCyan);
                            break;
                        case 1:
                            cubeGroup[0] = new Cube(0, 4, Color.DarkBlue);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkBlue);
                            cubeGroup[2] = new Cube(0, 1, Color.DarkBlue);
                            cubeGroup[3] = new Cube(0, 2, Color.DarkBlue);
                            break;
                        case 2:
                            cubeGroup[0] = new Cube(0, 5, Color.Orange);
                            cubeGroup[1] = new Cube(-1, 0, Color.Orange);
                            cubeGroup[2] = new Cube(0, -1, Color.Orange);
                            cubeGroup[3] = new Cube(0, -2, Color.Orange);
                            break;
                        case 3:
                            cubeGroup[0] = new Cube(0, 4, Color.Yellow);
                            cubeGroup[1] = new Cube(-1, 0, Color.Yellow);
                            cubeGroup[2] = new Cube(0, 1, Color.Yellow);
                            cubeGroup[3] = new Cube(-1, 1, Color.Yellow);
                            break;
                        case 4:
                            cubeGroup[0] = new Cube(0, 5, Color.Green);
                            cubeGroup[1] = new Cube(-1, 0, Color.Green);
                            cubeGroup[2] = new Cube(-1, 1, Color.Green);
                            cubeGroup[3] = new Cube(0, -1, Color.Green);
                            break;
                        case 5:
                            cubeGroup[0] = new Cube(0, 5, Color.DarkViolet);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkViolet);
                            cubeGroup[2] = new Cube(0, 1, Color.DarkViolet);
                            cubeGroup[3] = new Cube(0, -1, Color.DarkViolet);
                            break;
                        case 6:
                            cubeGroup[0] = new Cube(0, 4, Color.Red);
                            cubeGroup[1] = new Cube(0, 1, Color.Red);
                            cubeGroup[2] = new Cube(-1, 0, Color.Red);
                            cubeGroup[3] = new Cube(-1, -1, Color.Red);
                            break;
                    }
                    break;
                case 5:
                    switch (rand.Next(7))
                    {
                        case 0:
                            cubeGroup[0] = new Cube(0, 4, Color.DarkCyan);
                            cubeGroup[1] = new Cube(0, -1, Color.DarkCyan);
                            cubeGroup[2] = new Cube(-1, -1, Color.DarkCyan);
                            cubeGroup[3] = new Cube(0, 1, Color.DarkCyan);
                            cubeGroup[4] = new Cube(0, 2, Color.DarkCyan);
                            break;
                        case 1:
                            cubeGroup[0] = new Cube(0, 3, Color.DarkBlue);
                            cubeGroup[1] = new Cube(0, 1, Color.DarkBlue);
                            cubeGroup[2] = new Cube(0, 2, Color.DarkBlue);
                            cubeGroup[3] = new Cube(-1, 1, Color.DarkBlue);
                            cubeGroup[4] = new Cube(-1, 0, Color.DarkBlue);
                            break;
                        case 2:
                            cubeGroup[0] = new Cube(0, 5, Color.Orange);
                            cubeGroup[1] = new Cube(0, -1, Color.Orange);
                            cubeGroup[2] = new Cube(0, -2, Color.Orange);
                            cubeGroup[3] = new Cube(-1, 0, Color.Orange);
                            cubeGroup[4] = new Cube(-1, 1, Color.Orange);
                            break;
                        case 3:
                            cubeGroup[0] = new Cube(-1, 4, Color.Green);
                            cubeGroup[1] = new Cube(1, 0, Color.Green);
                            cubeGroup[2] = new Cube(-1, 0, Color.Green);
                            cubeGroup[3] = new Cube(-1, -1, Color.Green);
                            cubeGroup[4] = new Cube(-1, 1, Color.Green);
                            break;
                        case 4:
                            cubeGroup[0] = new Cube(-1, 4, Color.DarkViolet);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkViolet);
                            cubeGroup[2] = new Cube(-1, -1, Color.DarkViolet);
                            cubeGroup[3] = new Cube(0, 1, Color.DarkViolet);
                            cubeGroup[4] = new Cube(1, 1, Color.DarkViolet);
                            break;
                        case 5:
                            cubeGroup[0] = new Cube(-1, 4, Color.Red);
                            cubeGroup[1] = new Cube(-1, 0, Color.Red);
                            cubeGroup[2] = new Cube(1, 0, Color.Red);
                            cubeGroup[3] = new Cube(0, 1, Color.Red);
                            cubeGroup[4] = new Cube(0, -1, Color.Red);
                            break;
                        case 6:
                            cubeGroup[0] = new Cube(-1, 4, Color.Yellow);
                            cubeGroup[1] = new Cube(-1, 0, Color.Yellow);
                            cubeGroup[2] = new Cube(-1, 1, Color.Yellow);
                            cubeGroup[3] = new Cube(1, 0, Color.Yellow);
                            cubeGroup[4] = new Cube(1, -1, Color.Yellow);
                            break;
                    }
                    break;
                case 6:
                    switch (rand.Next(7))
                    {
                        case 0:
                            cubeGroup[0] = new Cube(-1, 4, Color.DarkCyan);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkCyan);
                            cubeGroup[2] = new Cube(-1, 1, Color.DarkCyan);
                            cubeGroup[3] = new Cube(-1, -1, Color.DarkCyan);
                            cubeGroup[4] = new Cube(0, 1, Color.DarkCyan);
                            cubeGroup[5] = new Cube(1, 1, Color.DarkCyan);
                            break;
                        case 1:
                            cubeGroup[0] = new Cube(-1, 3, Color.DarkBlue);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkBlue);
                            cubeGroup[2] = new Cube(-1, -1, Color.DarkBlue);
                            cubeGroup[3] = new Cube(0, 1, Color.DarkBlue);
                            cubeGroup[4] = new Cube(1, 1, Color.DarkBlue);
                            cubeGroup[5] = new Cube(1, 2, Color.DarkBlue);
                            break;
                        case 2:
                            cubeGroup[0] = new Cube(-1, 4, Color.Orange);
                            cubeGroup[1] = new Cube(-1, 0, Color.Orange);
                            cubeGroup[2] = new Cube(-1, 1, Color.Orange);
                            cubeGroup[3] = new Cube(0, 1, Color.Orange);
                            cubeGroup[4] = new Cube(1, 0, Color.Orange);
                            cubeGroup[5] = new Cube(1, 1, Color.Orange);
                            break;
                        case 3:
                            cubeGroup[0] = new Cube(-1, 4, Color.Yellow);
                            cubeGroup[1] = new Cube(-1, 0, Color.Yellow);
                            cubeGroup[2] = new Cube(-1, 1, Color.Yellow);
                            cubeGroup[3] = new Cube(-1, -1, Color.Yellow);
                            cubeGroup[4] = new Cube(1, 0, Color.Yellow);
                            cubeGroup[5] = new Cube(1, 1, Color.Yellow);
                            break;
                        case 4:
                            cubeGroup[0] = new Cube(0, 4, Color.Green);
                            cubeGroup[1] = new Cube(0, -1, Color.Green);
                            cubeGroup[2] = new Cube(0, -2, Color.Green);
                            cubeGroup[3] = new Cube(0, 1, Color.Green);
                            cubeGroup[4] = new Cube(0, 2, Color.Green);
                            cubeGroup[5] = new Cube(-1, 2, Color.Green);
                            break;
                        case 5:
                            cubeGroup[0] = new Cube(-1, 4, Color.DarkViolet);
                            cubeGroup[1] = new Cube(0, -1, Color.DarkViolet);
                            cubeGroup[2] = new Cube(1, -1, Color.DarkViolet);
                            cubeGroup[3] = new Cube(0, 1, Color.DarkViolet);
                            cubeGroup[4] = new Cube(0, 2, Color.DarkViolet);
                            cubeGroup[5] = new Cube(1, 2, Color.DarkViolet);
                            break;
                        case 6:
                            cubeGroup[0] = new Cube(-1, 4, Color.Red);
                            cubeGroup[1] = new Cube(0, -1, Color.Red);
                            cubeGroup[2] = new Cube(1, -1, Color.Red);
                            cubeGroup[3] = new Cube(0, 1, Color.Red);
                            cubeGroup[4] = new Cube(1, 1, Color.Red);
                            cubeGroup[5] = new Cube(0, 2, Color.Red);
                            break;
                    }
                    break;
                case 7:
                    switch (rand.Next(7))
                    {
                        case 0:
                            cubeGroup[0] = new Cube(-2, 4, Color.DarkCyan);
                            cubeGroup[1] = new Cube(0, -1, Color.DarkCyan);
                            cubeGroup[2] = new Cube(1, -1, Color.DarkCyan);
                            cubeGroup[3] = new Cube(2, -1, Color.DarkCyan);
                            cubeGroup[4] = new Cube(0, 1, Color.DarkCyan);
                            cubeGroup[5] = new Cube(1, 1, Color.DarkCyan);
                            cubeGroup[6] = new Cube(2, 1, Color.DarkCyan);
                            break;
                        case 1:
                            cubeGroup[0] = new Cube(-1, 4, Color.DarkBlue);
                            cubeGroup[1] = new Cube(0, -1, Color.DarkBlue);
                            cubeGroup[2] = new Cube(-1, 0, Color.DarkBlue);
                            cubeGroup[3] = new Cube(-1, -1, Color.DarkBlue);
                            cubeGroup[4] = new Cube(0, 1, Color.DarkBlue);
                            cubeGroup[5] = new Cube(1, 1, Color.DarkBlue);
                            cubeGroup[6] = new Cube(1, 2, Color.DarkBlue);
                            break;
                        case 2:
                            cubeGroup[0] = new Cube(-1, 4, Color.Orange);
                            cubeGroup[1] = new Cube(-1, 0, Color.Orange);
                            cubeGroup[2] = new Cube(-1, -1, Color.Orange);
                            cubeGroup[3] = new Cube(-1, 1, Color.Orange);
                            cubeGroup[4] = new Cube(0, 1, Color.Orange);
                            cubeGroup[5] = new Cube(1, 0, Color.Orange);
                            cubeGroup[6] = new Cube(1, 1, Color.Orange);
                            break;
                        case 3:
                            cubeGroup[0] = new Cube(-1, 4, Color.Yellow);
                            cubeGroup[1] = new Cube(-1, 0, Color.Yellow);
                            cubeGroup[2] = new Cube(-1, -1, Color.Yellow);
                            cubeGroup[3] = new Cube(-1, 1, Color.Yellow);
                            cubeGroup[4] = new Cube(1, 0, Color.Yellow);
                            cubeGroup[5] = new Cube(1, -1, Color.Yellow);
                            cubeGroup[6] = new Cube(1, 1, Color.Yellow);
                            break;
                        case 4:
                            cubeGroup[0] = new Cube(-1, 5, Color.Green);
                            cubeGroup[1] = new Cube(-1, 0, Color.Green);
                            cubeGroup[2] = new Cube(1, 0, Color.Green);
                            cubeGroup[3] = new Cube(0, -1, Color.Green);
                            cubeGroup[4] = new Cube(0, -2, Color.Green);
                            cubeGroup[5] = new Cube(0, 1, Color.Green);
                            cubeGroup[6] = new Cube(0, 2, Color.Green);
                            break;
                        case 5:
                            cubeGroup[0] = new Cube(-1, 4, Color.DarkViolet);
                            cubeGroup[1] = new Cube(-1, 0, Color.DarkViolet);
                            cubeGroup[2] = new Cube(0, -1, Color.DarkViolet);
                            cubeGroup[3] = new Cube(1, -1, Color.DarkViolet);
                            cubeGroup[4] = new Cube(0, 1, Color.DarkViolet);
                            cubeGroup[5] = new Cube(0, 2, Color.DarkViolet);
                            cubeGroup[6] = new Cube(1, 2, Color.DarkViolet);
                            break;
                        case 6:
                            cubeGroup[0] = new Cube(-1, 4, Color.Red);
                            cubeGroup[1] = new Cube(0, -1, Color.Red);
                            cubeGroup[2] = new Cube(1, -1, Color.Red);
                            cubeGroup[3] = new Cube(0, 1, Color.Red);
                            cubeGroup[4] = new Cube(1, 1, Color.Red);
                            cubeGroup[5] = new Cube(0, 2, Color.Red);
                            cubeGroup[6] = new Cube(-1, 2, Color.Red);
                            break;
                    }
                    break;
            }
            return cubeGroup;
        }
    }
}
