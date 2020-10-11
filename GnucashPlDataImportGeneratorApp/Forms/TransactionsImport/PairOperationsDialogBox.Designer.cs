namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport
{
    partial class PairOperationsDialogBox
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInterchange = new System.Windows.Forms.Button();
            this.btnUnpair = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPair = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colAccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.colAccountCode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccountCode2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrency1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrency2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperation1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperation2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1119, 453);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInterchange);
            this.panel1.Controls.Add(this.btnUnpair);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1113, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnInterchange
            // 
            this.btnInterchange.Location = new System.Drawing.Point(136, 0);
            this.btnInterchange.Name = "btnInterchange";
            this.btnInterchange.Size = new System.Drawing.Size(130, 34);
            this.btnInterchange.TabIndex = 3;
            this.btnInterchange.Text = "Zamień";
            this.btnInterchange.UseVisualStyleBackColor = true;
            this.btnInterchange.Click += new System.EventHandler(this.btnInterchange_Click);
            // 
            // btnUnpair
            // 
            this.btnUnpair.Location = new System.Drawing.Point(0, 0);
            this.btnUnpair.Name = "btnUnpair";
            this.btnUnpair.Size = new System.Drawing.Size(130, 34);
            this.btnUnpair.TabIndex = 2;
            this.btnUnpair.Text = "Rozparuj";
            this.btnUnpair.UseVisualStyleBackColor = true;
            this.btnUnpair.Click += new System.EventHandler(this.btnUnpair_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1013, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Anuluj";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(907, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 34);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPair);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 189);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1113, 34);
            this.panel2.TabIndex = 1;
            // 
            // btnPair
            // 
            this.btnPair.Location = new System.Drawing.Point(0, 0);
            this.btnPair.Name = "btnPair";
            this.btnPair.Size = new System.Drawing.Size(130, 34);
            this.btnPair.TabIndex = 0;
            this.btnPair.Text = "Paruj";
            this.btnPair.UseVisualStyleBackColor = true;
            this.btnPair.Click += new System.EventHandler(this.btnPair_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAccountCode,
            this.colDate,
            this.colAmount,
            this.colCurrency,
            this.colDescription});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1113, 180);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Text = "dataGridView1";
            // 
            // colAccountCode
            // 
            this.colAccountCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAccountCode.DataPropertyName = "AccountCode";
            this.colAccountCode.HeaderText = "Kod konta";
            this.colAccountCode.MinimumWidth = 6;
            this.colAccountCode.Name = "colAccountCode";
            this.colAccountCode.ReadOnly = true;
            this.colAccountCode.Width = 106;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDate.DataPropertyName = "Date";
            this.colDate.HeaderText = "Data";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 70;
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAmount.DataPropertyName = "Amount";
            this.colAmount.HeaderText = "Kwota";
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 79;
            // 
            // colCurrency
            // 
            this.colCurrency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCurrency.DataPropertyName = "Currency";
            this.colCurrency.HeaderText = "Waluta";
            this.colCurrency.MinimumWidth = 6;
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.ReadOnly = true;
            this.colCurrency.Width = 84;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Opis";
            this.colDescription.MinimumWidth = 6;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 68;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAccountCode1,
            this.colAccountCode2,
            this.colDate1,
            this.colDate2,
            this.colAmount1,
            this.colAmount2,
            this.colCurrency1,
            this.colCurrency2,
            this.colDescription1,
            this.colDescription2,
            this.colOperation1,
            this.colOperation2});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 229);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1113, 180);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.Text = "dataGridView2";
            // 
            // colAccountCode1
            // 
            this.colAccountCode1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAccountCode1.DataPropertyName = "AccountCode1";
            this.colAccountCode1.HeaderText = "Kod konta 1";
            this.colAccountCode1.MinimumWidth = 6;
            this.colAccountCode1.Name = "colAccountCode1";
            this.colAccountCode1.ReadOnly = true;
            this.colAccountCode1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAccountCode1.Width = 118;
            // 
            // colAccountCode2
            // 
            this.colAccountCode2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAccountCode2.DataPropertyName = "AccountCode2";
            this.colAccountCode2.HeaderText = "Kod konta 2";
            this.colAccountCode2.MinimumWidth = 6;
            this.colAccountCode2.Name = "colAccountCode2";
            this.colAccountCode2.ReadOnly = true;
            this.colAccountCode2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAccountCode2.Width = 118;
            // 
            // colDate1
            // 
            this.colDate1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDate1.DataPropertyName = "Date1";
            this.colDate1.HeaderText = "Data 1";
            this.colDate1.MinimumWidth = 6;
            this.colDate1.Name = "colDate1";
            this.colDate1.ReadOnly = true;
            this.colDate1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDate1.Width = 82;
            // 
            // colDate2
            // 
            this.colDate2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDate2.DataPropertyName = "Date2";
            this.colDate2.HeaderText = "Data 2";
            this.colDate2.MinimumWidth = 6;
            this.colDate2.Name = "colDate2";
            this.colDate2.ReadOnly = true;
            this.colDate2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDate2.Width = 82;
            // 
            // colAmount1
            // 
            this.colAmount1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAmount1.DataPropertyName = "Amount1";
            this.colAmount1.HeaderText = "Kwota 1";
            this.colAmount1.MinimumWidth = 6;
            this.colAmount1.Name = "colAmount1";
            this.colAmount1.ReadOnly = true;
            this.colAmount1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAmount1.Width = 91;
            // 
            // colAmount2
            // 
            this.colAmount2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAmount2.DataPropertyName = "Amount2";
            this.colAmount2.HeaderText = "Kwota 2";
            this.colAmount2.MinimumWidth = 6;
            this.colAmount2.Name = "colAmount2";
            this.colAmount2.ReadOnly = true;
            this.colAmount2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAmount2.Width = 91;
            // 
            // colCurrency1
            // 
            this.colCurrency1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCurrency1.DataPropertyName = "Currency1";
            this.colCurrency1.HeaderText = "Waluta 1";
            this.colCurrency1.MinimumWidth = 6;
            this.colCurrency1.Name = "colCurrency1";
            this.colCurrency1.ReadOnly = true;
            this.colCurrency1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCurrency1.Width = 96;
            // 
            // colCurrency2
            // 
            this.colCurrency2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCurrency2.DataPropertyName = "Currency2";
            this.colCurrency2.HeaderText = "Waluta 2";
            this.colCurrency2.MinimumWidth = 6;
            this.colCurrency2.Name = "colCurrency2";
            this.colCurrency2.ReadOnly = true;
            this.colCurrency2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCurrency2.Width = 96;
            // 
            // colDescription1
            // 
            this.colDescription1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescription1.DataPropertyName = "Description1";
            this.colDescription1.HeaderText = "Opis 1";
            this.colDescription1.MinimumWidth = 6;
            this.colDescription1.Name = "colDescription1";
            this.colDescription1.ReadOnly = true;
            this.colDescription1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescription1.Width = 80;
            // 
            // colDescription2
            // 
            this.colDescription2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDescription2.DataPropertyName = "Description2";
            this.colDescription2.HeaderText = "Opis 2";
            this.colDescription2.MinimumWidth = 6;
            this.colDescription2.Name = "colDescription2";
            this.colDescription2.ReadOnly = true;
            this.colDescription2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescription2.Width = 80;
            // 
            // colOperation1
            // 
            this.colOperation1.DataPropertyName = "Operation1";
            this.colOperation1.HeaderText = "colOperation1";
            this.colOperation1.MinimumWidth = 6;
            this.colOperation1.Name = "colOperation1";
            this.colOperation1.ReadOnly = true;
            this.colOperation1.Visible = false;
            this.colOperation1.Width = 125;
            // 
            // colOperation2
            // 
            this.colOperation2.DataPropertyName = "Operation2";
            this.colOperation2.HeaderText = "colOperation2";
            this.colOperation2.MinimumWidth = 6;
            this.colOperation2.Name = "colOperation2";
            this.colOperation2.ReadOnly = true;
            this.colOperation2.Visible = false;
            this.colOperation2.Width = 125;
            // 
            // PairOperationsDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "PairOperationsDialogBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paruj operacje";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUnpair;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPair;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrency;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Button btnInterchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountCode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountCode2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrency1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrency2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperation2;
    }
}

