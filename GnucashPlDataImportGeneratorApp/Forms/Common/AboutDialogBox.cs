using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Forms.Common;

[ExcludeFromCodeCoverage]
public partial class AboutDialogBox : Form
{
    public AboutDialogBox()
    {
        InitializeComponent();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
}
