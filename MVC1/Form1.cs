using MVC1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC1
{
    public partial class Form1 : Form
    {
        Model model;

        public Form1()
        {
            InitializeComponent();
            //создание объекта модели в форме
            model = new Model(Int32.Parse(Settings.Default["savedAC"].ToString()) % 100, Int32.Parse(Settings.Default["savedAC"].ToString()) % 100, Int32.Parse(Settings.Default["savedAC"].ToString()) / 100);
            //подключение события
            model.observers += new System.EventHandler(this.UpdateFromModel);
            //восстановление данных
            model.SetValue(Int32.Parse(Settings.Default["savedAC"].ToString()) % 100, 1);
        }

        //обновление значений в элементах
        private void UpdateFromModel(object sender, EventArgs e)
        {
            textBoxA.Text = model.GetValue(0).ToString();
            textBoxB.Text = model.GetValue(1).ToString();
            textBoxC.Text = model.GetValue(2).ToString();
            trackBarA.Value = model.GetValue(0);
            trackBarB.Value = model.GetValue(1);
            trackBarC.Value = model.GetValue(2);
            numericUpDownA.Value = Decimal.Parse(model.GetValue(0).ToString());
            numericUpDownB.Value = Decimal.Parse(model.GetValue(1).ToString());
            numericUpDownC.Value = Decimal.Parse(model.GetValue(2).ToString());
            Settings.Default["savedAC"] = model.GetValue(0) + model.GetValue(2) * 100;
            Settings.Default.Save();
        }

        //изменение в numericUpDownA
        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(numericUpDownA.Value.ToString()), 0);
        }

        //изменение в numericUpDownB
        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(numericUpDownB.Value.ToString()), 1);
        }

        //изменение в numericUpDownC
        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(numericUpDownC.Value.ToString()), 2);
        }

        //изменение в trackBarA
        private void trackBarA_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(trackBarA.Value.ToString()), 0);
        }

        //изменение в trackBarB
        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(trackBarB.Value.ToString()), 1);
        }

        //изменение в trackBarC
        private void trackBarC_ValueChanged(object sender, EventArgs e)
        {
            model.SetValue(int.Parse(trackBarC.Value.ToString()), 2);
        }

        //изменение в textBoxA
        private void textBoxA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                model.SetValue(int.Parse(textBoxA.Text), 0);
            }
        }

        //изменение в textBoxB
        private void textBoxB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                model.SetValue(int.Parse(textBoxB.Text), 1);
            }
        }

        //изменение в textBoxC
        private void textBoxC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                model.SetValue(int.Parse(textBoxC.Text), 2);
            }
        }
    }

    //класс Model для условий хранения значений
    public class Model
    {

        private int[] value = { 0, 0, 0 };

        public System.EventHandler observers;
        public Model(int A, int B, int C)
        {
            this.value[0] = A;
            this.value[1] = B;
            this.value[2] = C;
        }

        public void SetValue(int value, int index)
        {
            if (index == 0)
            {
                if (value > -1 && value < 101)
                {
                    if (value > this.value[1] && value > this.value[2])
                    {
                        this.value[0] = value;
                        this.value[1] = value;
                        this.value[2] = value;
                    }
                    else if (value > this.value[1])
                    {
                        this.value[0] = value;
                        this.value[1] = value;
                    }
                    else
                    {
                        this.value[0] = value;
                    }
                }
            }
            else if (index == 1)
            {
                if (value > this.value[0] && value < this.value[2])
                    this.value[1] = value;
            }
            else
            {
                if (value > -1 && value < 101)
                {
                    if (value < this.value[1] && value < this.value[0])
                    {
                        this.value[0] = value;
                        this.value[1] = value;
                        this.value[2] = value;
                    }
                    else if (value < this.value[1])
                    {
                        this.value[2] = value;
                        this.value[1] = value;
                    }
                    else
                    {
                        this.value[2] = value;
                    }
                }
            }
            observers.Invoke(this, null);
        }
        public int GetValue(int index)
        {
            return value[index];
        }
    }   
}
