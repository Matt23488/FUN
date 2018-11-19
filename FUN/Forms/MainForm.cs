using FUN.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FUN.Forms
{
    public partial class MainForm : Form, IMainView
    {
        public int FunValue
        {
            get => int.Parse(_funValueTextBox.Text);
            set => _funValueTextBox.Text = value.ToString();
        }

        public MainForm()
        {
            InitializeComponent();
            Controller.Start(this);
        }
    }
}
