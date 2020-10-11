using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces
{
    public interface IPairOperationsDialogBoxView
    {
        DataGridViewSelectedRowCollection PairableOperationsGridSelectedRows { get; }

        DataGridViewRowCollection PairableOperationsGridRows { get; }

        DataGridViewSelectedRowCollection OperationPairsGridSelectedRows { get; }

        DataGridViewRowCollection OperationPairsGridRows { get; }

        BindingList<Operation> PairableOperationsDataSource { get; set; }

        BindingList<OperationPair> OperationPairsDataSource { get; set; }

        DialogResult DialogResult { get; set; }

        
    }
}
