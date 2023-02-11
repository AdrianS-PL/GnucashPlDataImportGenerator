using GnuCash.TransactionImportGenerator.Model;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.Presenters;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport
{
    public partial class PairOperationsDialogBox : Form, IPairOperationsDialogBoxView
    {
        public PairOperationsDialogBoxPresenter Presenter { get; }

        BindingList<Operation> IPairOperationsDialogBoxView.PairableOperationsDataSource
        {
            get => dataGridView1.DataSource as BindingList<Operation>;
            set
            {
                dataGridView1.DataSource = value;
            }
        }

        DataGridViewSelectedRowCollection IPairOperationsDialogBoxView.PairableOperationsGridSelectedRows { get => dataGridView1.SelectedRows; }
        DataGridViewRowCollection IPairOperationsDialogBoxView.PairableOperationsGridRows { get => dataGridView1.Rows; }
        DataGridView IPairOperationsDialogBoxView.PairableOperationsGrid { get => dataGridView1; }

        public BindingList<OperationPair> OperationPairsDataSource { get; private set; } = new BindingList<OperationPair>();

        DataGridViewSelectedRowCollection IPairOperationsDialogBoxView.OperationPairsGridSelectedRows => dataGridView2.SelectedRows;

        DataGridViewRowCollection IPairOperationsDialogBoxView.OperationPairsGridRows => dataGridView2.Rows;
        DataGridView IPairOperationsDialogBoxView.OperationPairsGrid { get => dataGridView2; }

        BindingList<OperationPair> IPairOperationsDialogBoxView.OperationPairsDataSource
        {
            get => dataGridView2.DataSource as BindingList<OperationPair>;
            set
            {
                dataGridView2.DataSource = value;
            }
        }

        public PairOperationsDialogBox()
        {
            InitializeComponent();
            dataGridView1.DataSource = new BindingList<Operation>();
            dataGridView2.DataSource = new BindingList<OperationPair>();
            Presenter = new PairOperationsDialogBoxPresenter(this);
        }

        public void InitializeData(IList<Operation> pairableOperations)
        {
            dataGridView1.DataSource = new BindingList<Operation>(pairableOperations);
        }

        public List<OperationPair> GetResults()
        {
            return (this as IPairOperationsDialogBoxView).OperationPairsDataSource.ToList();
        }

        private void btnPair_Click(object sender, EventArgs e)
        {
            Presenter.OnPairButtonClick();
        }

        private void btnUnpair_Click(object sender, EventArgs e)
        {
            Presenter.OnUnpairButtonClick();
        }

        private void btnInterchange_Click(object sender, EventArgs e)
        {
            Presenter.OnInterchangeButtonClick();
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
