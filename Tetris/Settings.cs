using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Settings : Form
    {
        /// <summary>
        /// Если true, отображает сетку, иначе не отображается
        /// </summary>
        public bool Grid = true;
        /// <summary>
        /// Количество квадратиков в группе
        /// </summary>
        public int Count = 4;
        /// <summary>
        /// Если true, отображается подсказка в виде окна "Следующая фигура", иначе не отображается
        /// </summary>
        public bool NextVisible = true;

        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переключение отображения сетки
        /// </summary>
        private void Grid_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Text == "Да")
                Grid = true;
            else
                Grid = false;
        }

        /// <summary>
        /// Переключение количества квадратиков в группе
        /// </summary>
        private void Count_CheckedChanged(object sender, EventArgs e)
        {
            Count = int.Parse((sender as RadioButton).Text);
        }

        /// <summary>
        /// Вместо закрытия формы скрывает её
        /// </summary>
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Переключение отображения подсказки
        /// </summary>
        private void Visible_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Text == "Да")
                NextVisible = true;
            else
                NextVisible = false;
        }
    }
}
