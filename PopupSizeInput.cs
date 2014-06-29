using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BattleField7Namespace
{
    public partial class PopupSizeInput : Form
    {
        public PopupSizeInput()
        {
            InitializeComponent();
        }
        public string Input
        {
             get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}