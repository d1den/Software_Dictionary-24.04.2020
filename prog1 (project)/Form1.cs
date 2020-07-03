using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog1
{
    public partial class Form1 : Form
    {
        List<string> vocabulary = new List<string>();

        string fileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt"; // Фильтр для файлового менеджера 
            openFileDialog1.Title = "Выберите файл txt, содержащий словарь";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
        }
        // Открыть словарь
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = ""; // Устанвливаем изначально пустое имя файла в открытом менеджере файлов
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) // Открываем менеджер. Если отмена, то выходим
                return;
            fileName = openFileDialog1.FileName; // Строка, сохраняющая имя файла
            FileToVocabulary();
        }
        // Создать словарь
        private void button4_Click(object sender, EventArgs e)
        {
            var text = richTextBox1.Text.Split('\n'); // Считываем строки из текстбокса
            vocabulary.Clear(); // Очищаем список
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Сохраните словарь в файл txt"; // Добавляем заголовок
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) // Открываем файловый менеджер. Если отмена, то выходим
                return;
            // получаем имя созданного файла
            fileName = saveFileDialog1.FileName;
            for (int i = 0; i < text.Length; i++)
                if (text[i] != "")
                    vocabulary.Add(text[i]); // Сохраняем строку в список\
            VocabularyToFile();
        }
        // Вывести слова в алфавитном порядке
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string letter = textBox1.Text; // Записываем значение в строку
                if (letter.Length > 1) // Если это не одна буква, то выводим сообщение и выходим
                {
                    MessageBox.Show("Введите одну букву!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                List<string> sortText = Vocabulary.AlphabetSort(vocabulary, letter); // создаём дополнительный список
                richTextBox1.Text = ""; // Очищаем текстбокс
                for (int i = 0; i < sortText.Count; i++)
                    richTextBox1.Text += sortText[i] + "\n"; // Выводим слова
            }
            catch(Exception bug) // Если ошибка, то откроется окно с ошибкой
            {
                MessageBox.Show(bug.Message,"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Добавить новое слово
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string word = textBox2.Text;
                for(int i=0;i<vocabulary.Count;i++)
                    if (word.ToLower() == vocabulary[i].ToLower())
                    {
                        MessageBox.Show("Ошибка, данное слово уже содержится в словаре!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                vocabulary.Add(word);
                richTextBox1.Text += word + "\n";
                VocabularyToFile();
                FileToVocabulary();
            }
            catch (Exception bug) // Если ошибка, то откроется окно с ошибкой
            {
                MessageBox.Show(bug.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Удалить слово
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string word = textBox3.Text;
                if (vocabulary.Contains(word))
                    vocabulary.Remove(word);
                VocabularyToFile();
                FileToVocabulary();
            }
            catch (Exception bug) // Если ошибка, то откроется окно с ошибкой
            {
                MessageBox.Show(bug.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public void VocabularyToFile()
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);
                for (int i = 0; i < vocabulary.Count; i++)
                    sw.WriteLine(vocabulary[i]); // И в файл
                sw.Close();// Закрываем поток
            }
            catch
            {
                MessageBox.Show("Ошибка записи в файл!", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Загрузка из открытого файла в список
        public void FileToVocabulary()
        {
            vocabulary.Clear(); // Очищаем список
            richTextBox1.Text = ""; // Очищаем текстовое окно
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName); // Создаём объект 
                while (!sr.EndOfStream) // Пока не конец файла
                {
                    string str = sr.ReadLine(); // Считываем строку
                    vocabulary.Add(str); // Сохраняем в список
                    richTextBox1.Text += str + "\n"; // И выводим в текстовое окно
                }
                sr.Close(); // Закрываем поток
            }
            catch // Если ошибка, то откроется окно с ошибкой
            {
                MessageBox.Show("Ошибка доступа к файлу!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Поиск слов, содержащих заданный слог
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string word = textBox4.Text;
                List<string> sortText = Vocabulary.FindWord(vocabulary, word);
                richTextBox1.Text = "";
                for(int i = 0; i < sortText.Count; i++)
                {
                    richTextBox1.Text += sortText[i]+"\n";
                }
                DialogResult result = MessageBox.Show(
        "Сохранить ответ в файл?",
        "Сообщение",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.FileName = "";
                    saveFileDialog1.Title = "Сохраните ответ в файл txt"; // Добавляем заголовок
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) // Открываем файловый менеджер. Если отмена, то выходим
                        return;
                    // получаем имя созданного файла
                    string filename = saveFileDialog1.FileName;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
                    for (int i = 0; i < sortText.Count; i++)
                        sw.WriteLine(sortText[i]); // И в файл
                    sw.Close();// Закрываем поток
                }
            }
            catch (Exception bug) // Если ошибка, то откроется окно с ошибкой
            {
                MessageBox.Show(bug.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
