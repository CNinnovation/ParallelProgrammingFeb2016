using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox1.Text);
            int y = int.Parse(textBox2.Text);

            backgroundWorker1.RunWorkerAsync(Tuple.Create(x, y));
        }

        private void OnCalculation(object sender, DoWorkEventArgs e)
        {
            // background thread
            Tuple<int, int> input = (Tuple<int, int>)e.Argument;
            Thread.Sleep(1000);
            int result = input.Item1 + input.Item2;
            e.Result = result;
        }

        private void OnCalculationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int result  = (int)e.Result;
            label1.Text = result.ToString();

        }
    }
}
