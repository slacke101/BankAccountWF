namespace BankAccountAppBasic
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            AmountLabel = new Label();
            OwnerBox = new TextBox();
            AmountNum = new NumericUpDown();
            BankAccountsGrid = new DataGridView();
            depositBtn = new Button();
            withdrawBtn = new Button();
            AccountCreateButton = new Button();
            ((System.ComponentModel.ISupportInitialize)AmountNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BankAccountsGrid).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Tan;
            label1.Location = new Point(901, 9);
            label1.Name = "label1";
            label1.Size = new Size(161, 44);
            label1.TabIndex = 0;
            label1.Text = "SunCal";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Tan;
            label2.Location = new Point(23, 80);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 1;
            label2.Text = "Owner:";
            // 
            // AmountLabel
            // 
            AmountLabel.AutoSize = true;
            AmountLabel.ForeColor = Color.Tan;
            AmountLabel.Location = new Point(23, 471);
            AmountLabel.Name = "AmountLabel";
            AmountLabel.Size = new Size(54, 15);
            AmountLabel.TabIndex = 2;
            AmountLabel.Text = "Amount:";
            // 
            // OwnerBox
            // 
            OwnerBox.Location = new Point(74, 78);
            OwnerBox.Name = "OwnerBox";
            OwnerBox.Size = new Size(162, 23);
            OwnerBox.TabIndex = 3;
            // 
            // AmountNum
            // 
            AmountNum.Location = new Point(80, 469);
            AmountNum.Name = "AmountNum";
            AmountNum.Size = new Size(156, 23);
            AmountNum.TabIndex = 4;
            // 
            // BankAccountsGrid
            // 
            BankAccountsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BankAccountsGrid.BackgroundColor = Color.White;
            BankAccountsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            BankAccountsGrid.Location = new Point(531, 79);
            BankAccountsGrid.Name = "BankAccountsGrid";
            BankAccountsGrid.RowTemplate.Height = 25;
            BankAccountsGrid.Size = new Size(852, 381);
            BankAccountsGrid.TabIndex = 5;
            // 
            // depositBtn
            // 
            depositBtn.Location = new Point(669, 500);
            depositBtn.Name = "depositBtn";
            depositBtn.Size = new Size(193, 43);
            depositBtn.TabIndex = 6;
            depositBtn.Text = "Deposit";
            depositBtn.UseVisualStyleBackColor = true;
            // 
            // withdrawBtn
            // 
            withdrawBtn.Location = new Point(1032, 500);
            withdrawBtn.Name = "withdrawBtn";
            withdrawBtn.Size = new Size(193, 43);
            withdrawBtn.TabIndex = 7;
            withdrawBtn.Text = "Withdraw";
            withdrawBtn.UseVisualStyleBackColor = true;
            // 
            // AccountCreateButton
            // 
            AccountCreateButton.Location = new Point(80, 120);
            AccountCreateButton.Name = "AccountCreateButton";
            AccountCreateButton.Size = new Size(156, 32);
            AccountCreateButton.TabIndex = 8;
            AccountCreateButton.Text = "Create Account";
            AccountCreateButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Sienna;
            ClientSize = new Size(1420, 567);
            Controls.Add(AccountCreateButton);
            Controls.Add(withdrawBtn);
            Controls.Add(depositBtn);
            Controls.Add(BankAccountsGrid);
            Controls.Add(AmountNum);
            Controls.Add(OwnerBox);
            Controls.Add(AmountLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)AmountNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)BankAccountsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label AmountLabel;
        private TextBox OwnerBox;
        private NumericUpDown AmountNum;
        private DataGridView BankAccountsGrid;
        private Button depositBtn;
        private Button withdrawBtn;
        private Button AccountCreateButton;
    }
}