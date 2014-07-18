using System;
using System.Linq;
using System.Windows.Forms;

namespace BattleField7Namespace.NewGameDesign.UIClasses
{
    /// <summary>
    /// A small window used to get the initial game grid size input.
    /// </summary>
    public partial class PopupSizeInput : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupSizeInput"/> class.
        /// </summary>
        public PopupSizeInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public string Input
        {
             get { return textBox.Text; }
            set { textBox.Text = value; }
        }
    }
}