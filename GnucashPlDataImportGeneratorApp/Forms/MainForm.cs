using GnuCash.CommodityPriceImportGenerator;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Dtos;
using GnuCash.DataModel.Queries;
using GnuCash.TransactionImportGenerator;
using GnucashPlDataImportGeneratorApp.Forms.Common;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms
{
    [ExcludeFromCodeCoverage]
    public partial class MainForm : Form
    {
        IServiceProvider _services;

        public MainForm(IServiceProvider services)
        {
            _services = services;
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var generator = _services.GetService<IPriceImportGenerator>();
            await generator.GenerateImport();

            Cursor.Current = defaultCursor;
            button1.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "XML and CSV files (*.xml, *.csv, *.mt940)|*.xml;*.csv;*.mt940|All files (*.*)|*.*;",
                Multiselect = true,
                Title = "Importuj pliki"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                button2.Enabled = true;
                return;
            }

            var defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var operationsFilesParser = _services.GetService<OperationFilesParser>();
            var filesParsingResults = await operationsFilesParser.ParseOperationsFiles(dialog.FileNames);

            Cursor.Current = defaultCursor;

            FilesImportResultDialogBox.ShowDialog(filesParsingResults.ParsingResults.Select(q => q as FileImportResultBase).ToList());

            var pairOperationsDialogBox = new PairOperationsDialogBox();
            pairOperationsDialogBox.InitializeData(filesParsingResults.PairableOperations.OrderBy(q => q.Date).ToList());
            result = pairOperationsDialogBox.ShowDialog();

            if (result != DialogResult.OK)
            {
                button2.Enabled = true;
                return;
            }

            defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var transactionImportFileDataGenerator = _services.GetService<TransactionImportFileDataGenerator>();
            var fileRows = await transactionImportFileDataGenerator.GenerateImportFileData(filesParsingResults.LoadedOperations, pairOperationsDialogBox.GetResults());

            Cursor.Current = defaultCursor;

            var checkAssignedTransferAccountDialogBox = new CheckAssignedTransferAccountDialogBox();

            var context = _services.GetService<GnuCashContext>();
            checkAssignedTransferAccountDialogBox.InitializeData(fileRows, (await context.Set<Account>().GetAccountFullNamesAsync()).OrderBy(q => q.FullName).Select(q => q.FullName).ToList());

            result = checkAssignedTransferAccountDialogBox.ShowDialog();

            if (result != DialogResult.OK)
            {
                button2.Enabled = true;
                return;
            }

            fileRows = fileRows.OrderBy(q => q.Date).ToList();

            await TransactionImportFileWriter.GenerateReport(fileRows);

            button2.Enabled = true;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var aboutDialogBox = new AboutDialogBox();
            aboutDialogBox.ShowDialog();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "XLS files (*.xls)|*.xls;",
                Multiselect = true,
                Title = "Importuj pliki"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                button3.Enabled = true;
                return;
            }

            var defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var filesParser = _services.GetService<PolishTreasuryBondsAccountStateFilesParser>();
            var filesParsingResults = await filesParser.ParseOperationsFiles(dialog.FileNames);

            Cursor.Current = defaultCursor;

            FilesImportResultDialogBox.ShowDialog(filesParsingResults.ParsingResults.Select(q => q as FileImportResultBase).ToList());

            var bondsPriceImportFileWriter = _services.GetService<PolishTreasuryBondsPricesImportFileWriter>();

            defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            await bondsPriceImportFileWriter.WriteImportFile(filesParsingResults.LoadedFileRecords, DateTime.Today);

            Cursor.Current = defaultCursor;
            button3.Enabled = true;
        }
    }
}
