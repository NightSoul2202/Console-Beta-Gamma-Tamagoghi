using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;

namespace ConsoleApp17
{
    internal class Program
    {
        class TamoEventArgs : EventArgs
        {
            public int No_Col { get; set; }

            public bool Death { get; set; }

        }

        class TaskForTamo
        {

            public void Pokormit(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show($"Тамо хоче поїсти. Покормити?", "Тамо щось захотів", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Console.WriteLine("Тамо наїся!");
                }
                else if (res == DialogResult.No)
                {
                    Console.WriteLine("Тамо не нагодований.");
                    t.No_Col++;
                }
            }
            public void GoSleep(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show("Тамо хоче спати. Укласти?", "Тамо щось захотів", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Console.WriteLine("Тамо виспався!");
                }
                else if (res == DialogResult.No)
                {
                    Console.WriteLine("Тамо тепер буде сонним.");
                    t.No_Col++;
                }
            }
            public void Pogulyat(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show("Тамо хоче погуляти. Вигуляти?", "Тамо щось захотів", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Console.WriteLine("Тамо нагулявся!");
                }
                else if (res == DialogResult.No)
                {
                    Console.WriteLine("Тамо тепер сумний, бо не погуляв.");
                    t.No_Col++;
                }
            }
            public void Polechit(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show("Тамо хворіє. Вилікувати?", "ТАМО ХВОРІЄ!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    Console.WriteLine("Тамо вилікувався, і його життю нічого більше не загрожує");
                }
                else if (res == DialogResult.No)
                {
                    Console.WriteLine("Тамо стало гірше!");
                    t.Death = true;
                }
            }
            public void Poigrat(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show("Тамо хоче погратися. Погратися?", "Тамо щось захотів", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Console.WriteLine("Тамо вдоволь награвся!");
                }
                else if (res == DialogResult.No)
                {
                    Console.WriteLine("Тамо тепер сумний, бо не погуляв.");
                    t.No_Col++;
                }
            }
            public void Smert(object sender, TamoEventArgs t)
            {
                DialogResult res = MessageBox.Show("Тамо більше нічого не хоче...", "ТАМО ВМЕР!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    t.Death = true;
                }
            }
        }
        class Tamagochi
        {
            public event EventHandler<TamoEventArgs> task;

            public void SetTask(TamoEventArgs t)
            {
                if (task != null)
                {
                    task(this, t);
                }
            }
        }



        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            //Реалізувати роботу з таймером я не зміг.

            Tamagochi tamagochi = new Tamagochi();
            TaskForTamo tft = new TaskForTamo();
            int s, s_temp = 0;
            Random random = new Random();
            TamoEventArgs tamoEventArgs = new TamoEventArgs { No_Col = 0, Death = false };

            while (tamoEventArgs.Death != true)
            {
                s = random.Next(1, 5);
                if (s == s_temp)
                {
                    while (s == s_temp)
                        s = random.Next(1, 5);
                }
                if (tamoEventArgs.No_Col == 3)
                {
                    Console.WriteLine("Tamo bolen");
                    tamagochi.task += tft.Polechit;
                    tamagochi.SetTask(tamoEventArgs);
                    tamagochi.task -= tft.Polechit;
                    tamoEventArgs.No_Col = 0;
                    if (tamoEventArgs.Death == true)
                    {
                        tamagochi.task += tft.Smert;
                        tamagochi.SetTask(tamoEventArgs);
                    }
                }
                else
                {
                    switch (s)
                    {
                        case 1: tamagochi.task += tft.Pokormit; tamagochi.SetTask(tamoEventArgs); tamagochi.task -= tft.Pokormit; break;
                        case 2: tamagochi.task += tft.GoSleep; tamagochi.SetTask(tamoEventArgs); tamagochi.task -= tft.GoSleep; break;
                        case 3: tamagochi.task += tft.Pogulyat; tamagochi.SetTask(tamoEventArgs); tamagochi.task -= tft.Pogulyat; break;
                        case 4: tamagochi.task += tft.Poigrat; tamagochi.SetTask(tamoEventArgs); tamagochi.task -= tft.Poigrat; break;
                    }
                }
                s_temp = s;
            }
        }
    }
}
