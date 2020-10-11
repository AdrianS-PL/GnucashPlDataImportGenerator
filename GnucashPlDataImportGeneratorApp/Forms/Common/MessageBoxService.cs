using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.Common
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
