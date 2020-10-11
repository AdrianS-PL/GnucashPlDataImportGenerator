using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces
{
    public interface ICheckAssignedTransferAccountDialogBoxView
    {
        event EventHandler SelectedIndexChanged;

        List<TransactionImportFileRow> TransactionData { get; set; }
        List<string> AvailableTransferAccounts { get; set; }
        string TbxCountText { get; set; }
        string TbxIndexText { get; set; }
        string TbxTransactionDateText { get; set; }
        string TbxAmountText { get; set; }
        string TbxAccountText { get; set; }
        string TbxDescriptionText { get; set; }
        string TbxTransferMemoText { get; set; }
        int Index { get; set; }
        bool BtnNextEnabled { get; set; }
        bool BtnPrevEnabled { get; set; }
        string LbTransferAccountSelectedItem { get; set; }
        DialogResult DialogResult { get; set; }
    }
}
