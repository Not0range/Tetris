using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Leader : Form
    {
        /// <summary>
        /// Массив имён
        /// </summary>
        string[] names = new string[5];
        /// <summary>
        /// Массив очков
        /// </summary>
        public int[] scores = new int[5];
        /// <summary>
        /// Значение, записываемое в таблицу
        /// </summary>
        public int newScore = 0;
        /// <summary>
        /// Смещаемая позиция в таблице
        /// </summary>
        public int position = -1;

        /// <summary>
        /// Создание формы и загрузка данных из файла
        /// </summary>
        public Leader()
        {
            InitializeComponent();

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Tetris.lb", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (fileStream.Length == 0)
                {
                    BinaryWriter binaryWriter = null;
                    try
                    {
                        binaryWriter = new BinaryWriter(fileStream, Encoding.Unicode);
                        for (int i = 0; i < 5; i++)
                        {
                            binaryWriter.Write("-");
                            binaryWriter.Write(0);
                        }
                    }
                    finally
                    {
                        binaryWriter.Close();
                        fileStream.Close();
                    }
                    fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Tetris.lb", FileMode.Open, FileAccess.Read);
                }
                BinaryReader binaryReader = null;
                try
                {
                    binaryReader = new BinaryReader(fileStream, Encoding.Unicode);
                    for (int i = 0; i < 5; i++)
                    {
                        names[i] = binaryReader.ReadString();
                        scores[i] = binaryReader.ReadInt32();
                    }
                }
                finally
                {
                    binaryReader.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки таблицы лидеров", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                for (int i = 0; i < 5; i++)
                {
                    names[i] = "-";
                    scores[i] = 0;
                }
            }
            finally
            {
                fileStream.Close();
            }
            RefreshText();
        }

        /// <summary>
        /// Обновление текстадля отображение актуальной информации
        /// </summary>
        public void RefreshText()
        {
            for(int i = 0; i < 5; i++)
            {
                Controls.Find("Name" + (i + 1).ToString(), false)[0].Text = names[i];
                Controls.Find("Score" + (i + 1).ToString(), false)[0].Text = scores[i].ToString();
            }
        }

        /// <summary>
        /// Нажатие кнопки "Сброс"
        /// </summary>
        private void Reset_Click(object sender, EventArgs e)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Tetris.lb", FileMode.Create, FileAccess.Write);
                BinaryWriter binaryWriter = null;
                try
                {
                    binaryWriter = new BinaryWriter(fileStream, Encoding.Unicode);
                    for (int i = 0; i < 5; i++)
                    {
                        binaryWriter.Write("-");
                        binaryWriter.Write(0);
                    }
                }
                finally
                {
                    binaryWriter.Close();
                }
            }
            catch
            {
                fileStream.Close();
            }

            for (int i = 0; i < 5; i++)
            {
                names[i] = "-";
                scores[i] = 0;
            }
            RefreshText();
        }

        /// <summary>
        /// Вместо закрытия формы скрывает её
        /// </summary>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработка нажатий клавиш в текстовом поле "Новый лидер"
        /// </summary>
        private void NewLeader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveResult();
            }
        }

        /// <summary>
        /// Сохранение результата в файл
        /// </summary>
        private void SaveResult()
        {
            names[position] = NewLeader.Text;
            NewLeader.Visible = false;
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Tetris.lb", FileMode.Create, FileAccess.Write);
                if (fileStream.Length == 0)
                {
                    BinaryWriter binaryWriter = null;
                    try
                    {
                        binaryWriter = new BinaryWriter(fileStream, Encoding.Unicode);
                        for (int i = 0; i < 5; i++)
                        {
                            binaryWriter.Write(names[i]);
                            binaryWriter.Write(scores[i]);
                        }
                    }
                    finally
                    {
                        binaryWriter.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fileStream.Close();
            }
            RefreshText();
        }

        /// <summary>
        /// Проверка появления нового лидера при появлении формы
        /// </summary>
        private void Leader_Shown(object sender, EventArgs e)
        {
            if(position >= 0 && position < 5)
            {
                for(int i = 4; i > position;i--)
                {
                    names[i] = names[i - 1];
                    scores[i] = scores[i - 1];
                }
                scores[position] = newScore;
                newScore = 0;
                RefreshText();
                NewLeader.Text = "";
                NewLeader.Top = Controls.Find("Name" + (position + 1).ToString(), false)[0].Top;
                NewLeader.Visible = true;
                NewLeader.Focus();
            }
        }
    }
}
