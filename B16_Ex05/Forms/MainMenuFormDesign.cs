using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex05.Forms
{
    partial class MainMenuForm
    {
        private System.Windows.Forms.NumericUpDown NumericRows;
        private System.Windows.Forms.NumericUpDown NumericCols;
        private System.Windows.Forms.CheckBox CheckboxPlayer2Human;
        private System.Windows.Forms.TextBox TextboxPlayer2Name;
        private System.Windows.Forms.TextBox TextboxPlayer1Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ButtonStart;

        private void InitializeComponent()
        {
            this.ButtonStart = new System.Windows.Forms.Button();
            this.NumericRows = new System.Windows.Forms.NumericUpDown();
            this.NumericCols = new System.Windows.Forms.NumericUpDown();
            this.CheckboxPlayer2Human = new System.Windows.Forms.CheckBox();
            this.TextboxPlayer2Name = new System.Windows.Forms.TextBox();
            this.TextboxPlayer1Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCols)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(12, 161);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(189, 23);
            this.ButtonStart.TabIndex = 0;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            // 
            // NumericRows
            // 
            this.NumericRows.Location = new System.Drawing.Point(71, 117);
            this.NumericRows.Name = "NumericRows";
            this.NumericRows.Size = new System.Drawing.Size(35, 20);
            this.NumericRows.TabIndex = 1;
            // 
            // NumericCols
            // 
            this.NumericCols.Location = new System.Drawing.Point(166, 117);
            this.NumericCols.Name = "NumericCols";
            this.NumericCols.Size = new System.Drawing.Size(35, 20);
            this.NumericCols.TabIndex = 2;
            // 
            // CheckboxPlayer2Human
            // 
            this.CheckboxPlayer2Human.AutoSize = true;
            this.CheckboxPlayer2Human.Location = new System.Drawing.Point(19, 58);
            this.CheckboxPlayer2Human.Name = "CheckboxPlayer2Human";
            this.CheckboxPlayer2Human.Size = new System.Drawing.Size(67, 17);
            this.CheckboxPlayer2Human.TabIndex = 3;
            this.CheckboxPlayer2Human.Text = "Player 2:";
            this.CheckboxPlayer2Human.UseVisualStyleBackColor = true;
            // 
            // TextboxPlayer2Name
            // 
            this.TextboxPlayer2Name.Location = new System.Drawing.Point(106, 58);
            this.TextboxPlayer2Name.Name = "TextboxPlayer2Name";
            this.TextboxPlayer2Name.Size = new System.Drawing.Size(95, 20);
            this.TextboxPlayer2Name.TabIndex = 4;
            // 
            // TextboxPlayer1Name
            // 
            this.TextboxPlayer1Name.AccessibleDescription = "";
            this.TextboxPlayer1Name.AccessibleName = "";
            this.TextboxPlayer1Name.Location = new System.Drawing.Point(106, 32);
            this.TextboxPlayer1Name.Name = "TextboxPlayer1Name";
            this.TextboxPlayer1Name.Size = new System.Drawing.Size(95, 20);
            this.TextboxPlayer1Name.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Board Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Players:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rows:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cols:";
            // 
            // MainMenuForm
            // 
            this.AcceptButton = this.ButtonStart;
            this.ClientSize = new System.Drawing.Size(213, 196);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextboxPlayer1Name);
            this.Controls.Add(this.TextboxPlayer2Name);
            this.Controls.Add(this.CheckboxPlayer2Human);
            this.Controls.Add(this.NumericCols);
            this.Controls.Add(this.NumericRows);
            this.Controls.Add(this.ButtonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game settings";
            ((System.ComponentModel.ISupportInitialize)(this.NumericRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
