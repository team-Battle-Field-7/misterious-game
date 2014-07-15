using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using BattleField7Namespace.NewGameDesign.Interfaces;

namespace BattleField7Namespace
{
    /// <summary>
    /// A Windows Forms game interface
    /// </summary>
    public partial class WinFormsUI : Form, IUserInterface
    {
        /// <summary>
        /// Delegate used to safely access a count label (bombs or turns) from the thread of the game.
        /// </summary>
        /// <param name="count">The count to be displayed.</param>
        private delegate void ShowCountCallBack(int count);

        /// <summary>
        /// Delegate used to safely access the grid controle from the thread of the game, in order to set its sizes.
        /// </summary>
        /// <param name="size">The size.</param>
        private delegate void SetGameFieldSizeCallback(int size);

        /// <summary>
        /// Delegate used to safely access a grid cell's value from the thread of the game.
        /// </summary>
        /// <param name="paramsOfField">The parameters of field.</param>
        private delegate void SetFieldValueCallBack(string[] paramsOfField);

        /// <summary>
        /// Delegate used to safely access the AskForSizeInput() method from the thread of the game.
        /// </summary>
        /// <returns></returns>
        private delegate string GetInputCallBack(string message);

        /// <summary>
        /// Delegate used to safely access the message area of the form, from the thread of the game.
        /// </summary>
        /// <param name="message">The message.</param>
        private delegate void ShowMessageCallBack(string message);

        /// <summary>
        /// The size input window
        /// </summary>
        private PopupSizeInput sizeInputWindow = new PopupSizeInput();

        /// <summary>
        /// Needed for a check before drawing the game field,
        /// becouse the thread of the form might still not be ready with setting up the game grid's size,
        /// when the Drawer.DrawGame() method is called.
        /// </summary>
        private bool gameFieldIsInitialized = false;

        /// <summary>
        /// Needed for the active waiting for cell position input.
        /// It is false when input is not needed, or when a cell isn't yet clicked.
        /// It becomes true only for a moment, when a cell is selected during an active waiting.
        /// </summary>
        private bool gameFieldCellIsSelected = false;

        /// <summary>
        /// Needed for for a check on game grid cell selection.
        /// If there is an active waiting for input, the method will set gameFieldCellIsSelected to true.
        /// Otherwise it will ignore the input.
        /// </summary>
        private bool awaitingGameFieldCellSelection = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsUI"/> class.
        /// </summary>
        public WinFormsUI()
        {
            InitializeComponent();
            this.messagesListView.Columns[0].Width = this.messagesListView.Width-25;
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        public void DrawGame(char[,] gameField)
        {
            if (gameFieldIsInitialized)
            {
                int rows = gameField.GetLength(0);
                int cols = gameField.GetLength(1);

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        SetFieldValue(new string[] {row.ToString(), col.ToString(), gameField[row, col].ToString()});
                    }
                }
            }
            else
            {
                SetGameFieldSize(gameField.GetLength(0));
                DrawGame(gameField);
            }
        }

        /// <summary>
        /// Asks for size input.
        /// </summary>
        /// <returns>size input</returns>
        public string AskForSizeInput(string message)
        {
            if (this.gameFieldGridView.InvokeRequired)
            {
                GetInputCallBack deleg
                    = new GetInputCallBack(AskForSizeInput);
                return (string)this.Invoke(deleg, new object[] { message });
            }
            else
            {
                string result = "";
                DialogResult dialogResult = sizeInputWindow.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    result = sizeInputWindow.Input;
                }
                else
                {
                    result = AskForSizeInput(message);
                }
                return result;
            }
        }

        /// <summary>
        /// Asks for position input.
        /// </summary>
        /// <returns>position input</returns>
        public string AskForPositionInput(string message)
        {
            string resultPosition = "";
            awaitingGameFieldCellSelection = true;
            while (awaitingGameFieldCellSelection)
            {
                Thread.Sleep(50);
                if (gameFieldCellIsSelected)
                {
                    awaitingGameFieldCellSelection = false;
                    gameFieldCellIsSelected = false;

                    int col = gameFieldGridView.SelectedCells[0].ColumnIndex;
                    int row = gameFieldGridView.SelectedCells[0].RowIndex;

                    resultPosition = row + " " + col;
                    break;
                }
            }
            return resultPosition;
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            if (this.messagesListView.InvokeRequired)
            {
                ShowMessageCallBack deleg
                    = new ShowMessageCallBack(ShowMessage);
                this.Invoke(deleg, new object[] { message });
            }
            else
            {
                messagesListView.Items.Insert(
                    0,
                    "message " + messagesListView.Items.Count,
                    message,
                    0);
            }
        }

        /// <summary>
        /// Shows the congratulations message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowCongratulations(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Shows the game over message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowGameOver(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows a note message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowNote(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows the bombs count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void ShowBombsCount(int count)
        {
            if (this.bombsCountLabel.InvokeRequired)
            {
                ShowCountCallBack deleg
                    = new ShowCountCallBack(ShowBombsCount);
                this.Invoke(deleg, new object[] { count });
            }
            else
            {
                bombsCountLabel.Text = count.ToString();
            }
        }

        /// <summary>
        /// Shows the turns count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void ShowTurnsCount(int count)
        {
            if (this.turnsCountLabel.InvokeRequired)
            {
                ShowCountCallBack deleg
                    = new ShowCountCallBack(ShowTurnsCount);
                this.Invoke(deleg, new object[] { count });
            }
            else
            {
                turnsCountLabel.Text = count.ToString();
            }
        }

        /// <summary>
        /// Sets the size of the game field.
        /// </summary>
        /// <param name="size">The size.</param>
        private void SetGameFieldSize(int size)
        {
            if (this.gameFieldGridView.InvokeRequired)
            {
                SetGameFieldSizeCallback deleg
                    = new SetGameFieldSizeCallback(SetGameFieldSize);
                this.Invoke(deleg, new object[] { size });
            }
            else
            {
                this.gameFieldGridView.RowCount = size;
                this.gameFieldGridView.ColumnCount = size;
                gameFieldIsInitialized = true;

                int width = (int)(gameFieldGridView.Width / (size * 1.2));
                int height = (int)(gameFieldGridView.Height / (size * 1.2));

                for (int i = 0; i < size; i++)
                {
                    gameFieldGridView.Columns[i].Width = 
                        (int)(gameFieldGridView.Width/(size*1.2));
                    gameFieldGridView.Rows[i].Height = 
                        (int)(gameFieldGridView.Height/(size*1.2));
                }
            }
        }

        /// <summary>
        /// Sets the field value.
        /// </summary>
        /// <param name="paramsOfField">The parameters of field.</param>
        private void SetFieldValue(string[] paramsOfField) 
        {

            if (this.gameFieldGridView.InvokeRequired)
            {
                SetFieldValueCallBack deleg
                    = new SetFieldValueCallBack(SetFieldValue);
                //TODO - Find a Way to pass Multiple params
                this.Invoke(deleg, new object[] { paramsOfField });
            }
            else
            {
                int row = int.Parse(paramsOfField[0]);
                int col = int.Parse(paramsOfField[1]);
                this.gameFieldGridView[col, row].Value = paramsOfField[2];
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the WinFormsDrawer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void WinFormsDrawer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Handles the CellMouseClick event of the gameFieldGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void gameFieldGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (awaitingGameFieldCellSelection)
            {
                gameFieldCellIsSelected = true;
            }
        }
    }
}
