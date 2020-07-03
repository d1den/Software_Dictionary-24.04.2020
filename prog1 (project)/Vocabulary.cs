using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog1
{
    class Vocabulary
    {
        // Функция вывода слов по алфавиту, начиная с какой-то буквы
        static public List<string> AlphabetSort(List<string> text, string letter) // На вход список слов и буква в виде строки
        {
            List<string> suitableText = new List<string>(); // Создаём список подходящих элементов
            text.Sort(); // Сортируем изначальный список
            for(int i = 0; i < text.Count; i++) // Начинаем перебирать элементы
            {
                if(text[i][0]-Convert.ToChar(letter.ToLower())>=0|| text[i][0] - Convert.ToChar(letter.ToUpper())>=0) // Если первый символ слова больше по номеру прописной или заглавной букве
                {
                    for (int j = i; j < text.Count; j++)// То начиная с этого слова заносим слова с список подходящих
                        suitableText.Add(text[j]);
                    break; // выходим из цикла
                }
            }
            return suitableText; // Возвращаем список
        }

        static public List<string> FindWord(List<string> text, string word)
        {
            List<string> suitableText = new List<string>(); // Создаём список подходящих элементов
            for(int i = 0; i < text.Count; i++)
            {
                if (text[i].ToLower().Contains(word.ToLower()))
                    suitableText.Add(text[i]);
            }
            return suitableText;
        }
    }
}
