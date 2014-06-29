﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BattleField7Namespace
{
    /// <summary>
    /// A Windows Forms game interface
    /// </summary>
    public partial class WinFormsDrawer : Form, IDrawer
    {
        private delegate void ShowCountCallBack(int count);
        private delegate void SetGameFieldSizeCallback(int size);
        private delegate void SetFieldValueCallBack(string[] paramsOfField);
        private delegate string GetInputCallBack();
        private delegate void ShowMessageCallBack(string message);

        private PopupSizeInput inputWindow = new PopupSizeInput();

        private bool gameFieldIsInitialized = false;
        private bool gameFieldCellIsSelected = false;
        private bool awaitingGameFieldCellSelection = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsDrawer"/> class.
        /// </summary>
        public WinFormsDrawer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        public void DrawGame(Field[,] gameField)
        {
            if (gameFieldIsInitialized)
            {
                int rows = gameField.GetLength(0);
                int cols = gameField.GetLength(1);

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        string valueOfCell = "";
                        switch (gameField[r, c].Condition)
                        {
                            case Condition.Empty:
                                valueOfCell = "-";
                                break;
                            case Condition.Bomb:
                                valueOfCell = gameField[r, c].ExplosivePower.ToString();
                                break;
                            case Condition.BlownUp:
                                valueOfCell = "X";
                                break;
                            default:
                                break;
                        }
                        SetFieldValue(new string[] {r.ToString(), c.ToString(), valueOfCell});
                    }
                }
            }
            else
            {
                SetGameFieldSize(gameField.GetLength(0));
                DrawGame(gameField);
            }
        }

        public string AskForSizeInput()
        {
            if (this.gameFieldGridView.InvokeRequired)
            {
                GetInputCallBack deleg
                    = new GetInputCallBack(AskForSizeInput);
                return (string)this.Invoke(deleg);
            }
            else
            {
                string result = "";
                DialogResult dialogResult = inputWindow.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    result = inputWindow.Input;
                }
                else
                {
                    result = AskForSizeInput();
                }
                return result;
            }
        }

        public string AskForPositionInput()
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

        public void ShowCongratulations(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowGameOver(string message)
        {
            ShowMessage(message);
        }

        public void ShowNote(string message)
        {
            ShowMessage(message);
        }

        public void ShowAskInput(string message)
        {
            // Do nothing
        }

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

        private void WinFormsDrawer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void gameFieldGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (awaitingGameFieldCellSelection)
            {
                gameFieldCellIsSelected = true;
            }
        }

        private void WinFormsDrawer_Load(object sender, EventArgs e)
        {

        }
    }
}