using GnuCash.TransactionImportGenerator.Model;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.Presenters;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport
{
    public partial class CheckAssignedTransferAccountDialogBox : Form, ICheckAssignedTransferAccountDialogBoxView
    {
        public CheckAssignedTransferAccountDialogBoxPresenter Presenter { get; }


        private List<TransactionImportFileRow> _transactionData = new List<TransactionImportFileRow>();
        private int _index = 0;

        //public event EventHandler SelectedIndexChanged;

        event EventHandler ICheckAssignedTransferAccountDialogBoxView.TransactionAcceptedChanged
        {
            add => cbxTransactionChecked.CheckedChanged += value;
            remove => cbxTransactionChecked.CheckedChanged -= value;
        }

        event EventHandler ICheckAssignedTransferAccountDialogBoxView.SelectedIndexChanged
        {
            add => lbTransferAccount.SelectedIndexChanged += value;
            remove => lbTransferAccount.SelectedIndexChanged -= value;
        }

        List<TransactionImportFileRow> ICheckAssignedTransferAccountDialogBoxView.TransactionData
        {
            get
            {
                return _transactionData;
            }
            set
            {
                _transactionData = value;
            }
        }

        List<string> ICheckAssignedTransferAccountDialogBoxView.AvailableTransferAccounts
        {
            get
            {
                return lbTransferAccount.DataSource as List<string>;
            }
            set
            {
                lbTransferAccount.DataSource = value;
            }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxCountText
        {
            get { return tbxCount.Text; }
            set { tbxCount.Text = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxIndexText
        {
            get { return tbxIndex.Text; }
            set { tbxIndex.Text = value; }
        }

        int ICheckAssignedTransferAccountDialogBoxView.Index
        {
            get { return _index; }
            set { _index = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxTransactionDateText
        {
            get { return tbxTransactionDate.Text; }
            set { tbxTransactionDate.Text = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxAmountText
        {
            get { return tbxAmount.Text; }
            set { tbxAmount.Text = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxAccountText
        {
            get { return tbxAccount.Text; }
            set { tbxAccount.Text = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxDescriptionText
        {
            get { return tbxDescription.Text; }
            set { tbxDescription.Text = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.TbxTransferMemoText
        {
            get { return tbxTransferMemo.Text; }
            set { tbxTransferMemo.Text = value; }
        }

        bool ICheckAssignedTransferAccountDialogBoxView.CbxTransactionCheckedValue
        {
            get { return cbxTransactionChecked.Checked; }
            set { cbxTransactionChecked.Checked = value; }
        }

        bool ICheckAssignedTransferAccountDialogBoxView.BtnNextEnabled
        {
            get { return btnNext.Enabled; }
            set { btnNext.Enabled = value; }
        }

        bool ICheckAssignedTransferAccountDialogBoxView.BtnPrevEnabled
        {
            get { return btnPrev.Enabled; }
            set { btnPrev.Enabled = value; }
        }

        string ICheckAssignedTransferAccountDialogBoxView.LbTransferAccountSelectedItem
        {
            get { return lbTransferAccount.SelectedItem as string; }
            set { lbTransferAccount.SelectedItem = value; }
        }


        public CheckAssignedTransferAccountDialogBox()
        {
            InitializeComponent();
            Presenter = new CheckAssignedTransferAccountDialogBoxPresenter(this);
            lbTransferAccount.DataSource = new List<string>();
        }

        

        public void InitializeData(List<TransactionImportFileRow> data, List<string> availableTransferAccounts)
        {
            Presenter.OnInitializeData(data, availableTransferAccounts);            
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Presenter.OnPrevButtonClick();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Presenter.OnNextButtonClick();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Presenter.OnOkButtonClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Presenter.OnCancelButtonClick();
        }
    }
}
