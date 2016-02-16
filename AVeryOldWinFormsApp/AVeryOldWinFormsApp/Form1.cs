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

namespace AVeryOldWinFormsApp
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
            Task<int> t1 = Task<int>.Run<int>(() =>
            {
                return Add(x, y);
            });

            t1.ContinueWith(t =>
            {
                var result = t.Result;
                // label1.Text = result.ToString();
                this.Invoke(new Action<string>(SetLabel), result.ToString());
            });
        }

        private void SetLabel(string text)
        {
            this.label1.Text = text;
        }

        public int Add(int x, int y)
        {
            Thread.Sleep(4000);
            return x + y;
        }
    }
}
