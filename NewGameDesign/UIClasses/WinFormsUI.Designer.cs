namespace BattleField7Namespace.NewGameDesign.UIClasses
{
    partial class WinFormsUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormsUI));
            this.turnsCountLabel = new System.Windows.Forms.Label();
            this.gameFieldGridView = new System.Windows.Forms.DataGridView();
            this.bombsCountLabel = new System.Windows.Forms.Label();
            this.messagesListView = new System.Windows.Forms.ListView();
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.turnsLabel = new System.Windows.Forms.Label();
            this.bombsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // turnsCountLabel
            // 
            this.turnsCountLabel.AutoSize = true;
            this.turnsCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.turnsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.turnsCountLabel.Location = new System.Drawing.Point(105, 378);
            this.turnsCountLabel.Name = "turnsCountLabel";
            this.turnsCountLabel.Size = new System.Drawing.Size(18, 20);
            this.turnsCountLabel.TabIndex = 10;
            this.turnsCountLabel.Text = "0";
            // 
            // gameFieldGridView
            // 
            this.gameFieldGridView.BackgroundColor = System.Drawing.Color.Silver;
            this.gameFieldGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameFieldGridView.Location = new System.Drawing.Point(67, 12);
            this.gameFieldGridView.MultiSelect = false;
            this.gameFieldGridView.Name = "gameFieldGridView";
            this.gameFieldGridView.Size = new System.Drawing.Size(267, 290);
            this.gameFieldGridView.TabIndex = 5;
            this.gameFieldGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GameFieldGridViewCellMouseClick);
            // 
            // bombsCountLabel
            // 
            this.bombsCountLabel.AutoSize = true;
            this.bombsCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.bombsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.bombsCountLabel.Location = new System.Drawing.Point(105, 348);
            this.bombsCountLabel.Name = "bombsCountLabel";
            this.bombsCountLabel.Size = new System.Drawing.Size(18, 20);
            this.bombsCountLabel.TabIndex = 9;
            this.bombsCountLabel.Text = "0";
            // 
            // messagesListView
            // 
            this.messagesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1});
            this.messagesListView.Location = new System.Drawing.Point(151, 308);
            this.messagesListView.Name = "messagesListView";
            this.messagesListView.Size = new System.Drawing.Size(246, 121);
            this.messagesListView.TabIndex = 8;
            this.messagesListView.UseCompatibleStateImageBehavior = false;
            this.messagesListView.View = System.Windows.Forms.View.Details;
            // 
            // col1
            // 
            this.col1.Text = "";
            // 
            // turnsLabel
            // 
            this.turnsLabel.AutoSize = true;
            this.turnsLabel.BackColor = System.Drawing.Color.Transparent;
            this.turnsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.turnsLabel.Location = new System.Drawing.Point(32, 378);
            this.turnsLabel.Name = "turnsLabel";
            this.turnsLabel.Size = new System.Drawing.Size(57, 20);
            this.turnsLabel.TabIndex = 7;
            this.turnsLabel.Text = "Turns:";
            // 
            // bombsLabel
            // 
            this.bombsLabel.AutoSize = true;
            this.bombsLabel.BackColor = System.Drawing.Color.Transparent;
            this.bombsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.bombsLabel.Location = new System.Drawing.Point(32, 348);
            this.bombsLabel.Name = "bombsLabel";
            this.bombsLabel.Size = new System.Drawing.Size(67, 20);
            this.bombsLabel.TabIndex = 6;
            this.bombsLabel.Text = "Bombs:";
            // 
            // WinFormsDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(422, 442);
            this.Controls.Add(this.turnsCountLabel);
            this.Controls.Add(this.gameFieldGridView);
            this.Controls.Add(this.bombsCountLabel);
            this.Controls.Add(this.messagesListView);
            this.Controls.Add(this.turnsLabel);
            this.Controls.Add(this.bombsLabel);
            this.Name = "WinFormsDrawer";
            this.Text = "WinFormsGameUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFormsDrawer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label turnsCountLabel;
        internal System.Windows.Forms.DataGridView gameFieldGridView;
        internal System.Windows.Forms.Label bombsCountLabel;
        private System.Windows.Forms.ListView messagesListView;
        private System.Windows.Forms.Label turnsLabel;
        private System.Windows.Forms.Label bombsLabel;
        private System.Windows.Forms.ColumnHeader col1;
    }
}