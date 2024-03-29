using GnucashPlDataImportGeneratorApp.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp;

[ExcludeFromCodeCoverage]

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var services = Startup.ConfigureServices();

        Application.Run(new MainForm(services));
    }
}
