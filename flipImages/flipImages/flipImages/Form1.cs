using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace flipImages
{
    public partial class Form1 : Form
    {
        private Bitmap img;
        private string imgPath;
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click_Find(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolder = folderDialog.SelectedPath;

                string[] files = Directory.GetFiles(selectedFolder.ToString());
                
                listBox1.DataSource= files;
            }
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            imgPath = listBox1.SelectedItem.ToString();
            try
            {
                img = new Bitmap(imgPath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = img;
            }
            catch
            {
                MessageBox.Show("Не вдалося відкрити зображення (можливо вибраний файл не є зображенням).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_Flip(object sender, EventArgs e)
        {
            try
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = img;
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Виберіть зображення для відзеркалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click_Save(object sender, EventArgs e)
        {
            /*if (img != null && !string.IsNullOrEmpty(imgPath))
            {
                try
                {
                    string directory = Path.GetDirectoryName(imgPath);
                    string fileName = Path.GetFileNameWithoutExtension(imgPath);
                    string saveImgPath = Path.Combine(directory, fileName);

                    img.Save($"{saveImgPath} - mirrored.gif");
                    MessageBox.Show($"Зображення збережено як: {saveImgPath} - mirrored.gif", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Не вдалося зберегти зображення (можливо відзеркалене зображення вже збережено).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ви не вибрали зображення для збереження.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/


            try
            {
                string directory = Path.GetDirectoryName(imgPath);
                string fileName = Path.GetFileNameWithoutExtension(imgPath);
                string saveImgPath = Path.Combine(directory, fileName);
                
                    img.Save($"{saveImgPath} - mirrored.gif");
                    MessageBox.Show($"Зображення збережено як: {saveImgPath} - mirrored.gif", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(ExternalException)
            {
                    MessageBox.Show($"Не вдалося зберегти зображення (можливо вже існує файл з таким ім'ям).", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Або ви не вибрали зображення, або шлях до файлу пошкоджено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
