using GnuCash.TransactionImportGenerator.Model;
using GnucashPlDataImportGeneratorApp.Forms.Common;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.Presenters
{
    public class CheckAssignedTransferAccountDialogBoxPresenter
    {
        private readonly ICheckAssignedTransferAccountDialogBoxView _view;

        public CheckAssignedTransferAccountDialogBoxPresenter(ICheckAssignedTransferAccountDialogBoxView view)
        {
            _view = view;
        }

        public void OnInitializeData(List<TransactionImportFileRow> data, List<string> availableTransferAccounts)
        {
            data = data ?? throw new NullReferenceException($"{nameof(data)} cannot be null!");
            availableTransferAccounts = availableTransferAccounts 
                ?? throw new NullReferenceException($"{nameof(availableTransferAccounts)} cannot be null!");

            _view.SelectedIndexChanged -= OnLbTransferAccountSelectedIndexChange;
            _view.AvailableTransferAccounts = availableTransferAccounts;
            _view.TransactionData = data;
            _view.TbxCountText = _view.TransactionData.Count.ToString();
            if(data.Count > 0 && availableTransferAccounts.Count > 0)
            {
                UpdateDisplayedTransaction(0);
            }
            else
            {
                ClearDisplay();
            }
            _view.SelectedIndexChanged += OnLbTransferAccountSelectedIndexChange;
        }

        public void OnPrevButtonClick()
        {
            _view.SelectedIndexChanged -= OnLbTransferAccountSelectedIndexChange;
            UpdateDisplayedTransaction(_view.Index - 1);
            _view.SelectedIndexChanged += OnLbTransferAccountSelectedIndexChange;
        }

        public void OnNextButtonClick()
        {
            _view.SelectedIndexChanged -= OnLbTransferAccountSelectedIndexChange;
            UpdateDisplayedTransaction(_view.Index + 1);
            _view.SelectedIndexChanged += OnLbTransferAccountSelectedIndexChange;
        }

        public void OnOkButtonClick()
        {
            _view.DialogResult = DialogResult.OK;
        }

        public void OnCancelButtonClick()
        {
            _view.DialogResult = DialogResult.Cancel;
        }

        public void OnLbTransferAccountSelectedIndexChange(object sender, EventArgs e)
        {
            TransactionImportFileRow transaction = _view.TransactionData[_view.Index];
            transaction.TransferAccount = _view.LbTransferAccountSelectedItem;
        }

        private void ClearDisplay()
        {
            _view.TbxIndexText = "";
            _view.TbxCountText = "";
            _view.TbxAccountText = "";
            _view.TbxAmountText = "";
            _view.TbxDescriptionText = "";
            _view.TbxTransactionDateText = "";
            _view.TbxTransferMemoText = "";
            _view.LbTransferAccountSelectedItem = null;
            _view.BtnNextEnabled = false;
            _view.BtnPrevEnabled = false;
        }

        private void UpdateIndex(int newIndex)
        {
            _view.Index = newIndex;
            _view.TbxIndexText = (newIndex + 1).ToString();
        }

        private void UpdateDisplayedTransaction(int newIndex)
        {  
            TransactionImportFileRow transaction = _view.TransactionData[newIndex];
            UpdateIndex(newIndex);
            _view.TbxAccountText = transaction.Account;
            _view.TbxAmountText = transaction.Deposit.ToString();
            _view.TbxDescriptionText = transaction.Description;
            _view.TbxTransactionDateText = transaction.Date.ToString();
            _view.TbxTransferMemoText = transaction.TransferMemo;
            if (_view.AvailableTransferAccounts.Contains(transaction.TransferAccount))
                _view.LbTransferAccountSelectedItem = transaction.TransferAccount;
            else
                _view.LbTransferAccountSelectedItem = _view.AvailableTransferAccounts.First();

            UpdatePrevAndNextButtonEnabled();
        }

        private void UpdatePrevAndNextButtonEnabled()
        {
            _view.BtnNextEnabled = _view.Index < (_view.TransactionData.Count - 1);
            _view.BtnPrevEnabled = _view.Index > 0;
        }
    }
}
