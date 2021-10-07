using BossaTestDataImporter.Imports;
using BossaWebsite;
using GnuCash.CommodityPriceImportGenerator;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using GnuCash.TransactionImportGenerator;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Queries;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GnucashPlDataImportGeneratorApp.Forms.Common;

namespace GnucashPlDataImportGeneratorApp.Forms
{
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

            FilesImportResultDialogBox.ShowDialog(filesParsingResults.ParsingResults);

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

            var writer = _services.GetService<TransactionImportFileWriter>();

            fileRows = fileRows.OrderBy(q => q.Date).ToList();

            await writer.GenerateReport(fileRows);

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
                Filter = "XLS file (*.xls)|*.xls;",
                Multiselect = false,
                Title = "Importuj plik"
            };

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                button3.Enabled = true;
                return;
            }

            var bondsPriceImportGenerator = _services.GetService<IPolishTreasuryBondsPriceImportGenerator>();

            var defaultCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;


            await bondsPriceImportGenerator.GenerateImport(dialog.FileName);

            Cursor.Current = defaultCursor;
            button3.Enabled = true;
        }
    }
}
