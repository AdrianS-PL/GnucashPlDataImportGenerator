using BossaTestDataImporter.Imports.ViewModels;
using GnuCash.DataModel.Dtos;
using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.TransactionsImport
{
    public partial class FilesImportResultDialogBox : Form
    {
        private List<FileImportResultGridRowViewModel> _importResults;

        public FilesImportResultDialogBox()
        {
            InitializeComponent();
        }

        public List<FileImportResultGridRowViewModel> ImportResults
        {
            get
            {
                return _importResults;
            }
            set
            {
                _importResults = value;
                bsFileImportResults.DataSource = _importResults;
                bsFileImportResults.ResetBindings(false);
            }
        }

        public static void ShowDialog(List<FileImportResultBase> importResults)
        {
            var dialog = new FilesImportResultDialogBox();
            dialog.ImportResults = importResults.Select(q => new FileImportResultGridRowViewModel
            {
                Filename = Path.GetFileName(q.Pathname),
                IsError = q.IsError,
                Message = q.Message
            }).ToList();
            dialog.ShowDialog();
        }

        private void FilesImportResultDialogBox_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
