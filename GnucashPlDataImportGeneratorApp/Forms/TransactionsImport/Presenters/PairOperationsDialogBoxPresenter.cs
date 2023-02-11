using GnuCash.TransactionImportGenerator.Model;
using GnucashPlDataImportGeneratorApp.Forms.Common;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.Presenters
{
    public class PairOperationsDialogBoxPresenter
    {
        private readonly IPairOperationsDialogBoxView _view;
        private IMessageBoxService _messageBoxService;

        public IMessageBoxService MessageBoxServiceInstance
        {
            get
            {
                _messageBoxService = _messageBoxService ?? new MessageBoxService();
                return _messageBoxService;
            }
            set
            {
                _messageBoxService = value;
            }
        }


        public PairOperationsDialogBoxPresenter(IPairOperationsDialogBoxView view)
        {
            _view = view;
        }

        public void OnPairButtonClick()
        {
            if (_view.PairableOperationsGridSelectedRows.Count != 2)
            {
                MessageBoxServiceInstance.ShowErrorMessage("Wybierz dokładnie dwa wiersze.");
                return;
            }

            var operationPair = new OperationPair()
            {
                Operation1 = _view.PairableOperationsGridSelectedRows[0].DataBoundItem as Operation,
                Operation2 = _view.PairableOperationsGridSelectedRows[1].DataBoundItem as Operation
            };

            if (operationPair.Operation1.Amount != -operationPair.Operation2.Amount)
            {
                MessageBoxServiceInstance.ShowErrorMessage("Kwoty muszą być równe!");
                return;
            }

            _view.PairableOperationsDataSource.Remove(operationPair.Operation1);
            _view.PairableOperationsDataSource.Remove(operationPair.Operation2);
            _view.OperationPairsDataSource.Add(operationPair);
        }

        public void OnUnpairButtonClick()
        {
            if (_view.OperationPairsGridSelectedRows.Count != 1)
            {
                MessageBoxServiceInstance.ShowErrorMessage("Wybierz dokładnie jeden wiersz.");
                return;
            }

            var pair = _view.OperationPairsGridSelectedRows[0].DataBoundItem as OperationPair;

            _view.OperationPairsDataSource.Remove(pair);
            _view.PairableOperationsDataSource.Add(pair.Operation1);
            _view.PairableOperationsDataSource.Add(pair.Operation2);
        }

        public void OnInterchangeButtonClick()
        {
            if (_view.OperationPairsGridSelectedRows.Count != 1)
            {
                MessageBoxServiceInstance.ShowErrorMessage("Wybierz dokładnie jeden wiersz.");
                return;
            }

            var pair = _view.OperationPairsGridSelectedRows[0].DataBoundItem as OperationPair;

            var tmp = pair.Operation1;
            pair.Operation1 = pair.Operation2;
            pair.Operation2 = tmp;

            _view.OperationPairsDataSource.ResetBindings();
        }

        public void OnOkButtonClick()
        {
            _view.DialogResult = DialogResult.OK;
        }

        public void OnCancelButtonClick()
        {
            _view.DialogResult = DialogResult.Cancel;
        }
    }
}
