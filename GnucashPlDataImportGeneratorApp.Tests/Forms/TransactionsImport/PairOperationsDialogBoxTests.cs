using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GnuCash.TransactionImportGenerator.Model;
using FakeItEasy;
using GnucashPlDataImportGeneratorApp.Forms.Common;
using System.Windows.Forms;

namespace GnucashPlDataImportGeneratorApp.Tests.Forms.TransactionsImport;

[TestClass]
public class PairOperationsDialogBoxTests
{
    [TestMethod]
    public void Should_LoadFirstOperationData_When_InitializeData()
    {
        var form = new PairOperationsDialogBox();

        var inputData = GetStandardTestData();

        form.InitializeData(inputData);

        var view = form as IPairOperationsDialogBoxView;
        CollectionAssert.AreEqual(inputData, view.PairableOperationsDataSource);
        Assert.IsFalse(view.OperationPairsDataSource.Any());
    }


    [TestMethod]
    public void Should_ReturnDialogResultOk_When_OkIsClicked()
    {
        var form = new PairOperationsDialogBox();

        var inputData = GetStandardTestData();

        form.InitializeData(inputData);

        var view = form as IPairOperationsDialogBoxView;
        CollectionAssert.AreEqual(inputData, view.PairableOperationsDataSource);
        Assert.IsFalse(view.OperationPairsDataSource.Any());

        form.Presenter.OnOkButtonClick();

        Assert.AreEqual(DialogResult.OK, view.DialogResult);
    }

    [TestMethod]
    public void Should_PairOperations_When_PairIsClicked()
    {
        var form = new PairOperationsDialogBox();

        var inputData = GetStandardTestData();
        var inputDataCopy = GetStandardTestData();

        form.InitializeData(inputData);

        var view = form as IPairOperationsDialogBoxView;
        CollectionAssert.AreEqual(inputData, view.PairableOperationsDataSource);
        Assert.IsFalse(view.OperationPairsDataSource.Any());

        view.PairableOperationsGrid.Rows[0].Selected = true;
        view.PairableOperationsGrid.Rows[1].Selected = true;
        form.Presenter.OnPairButtonClick();

        Assert.AreEqual(1, view.OperationPairsDataSource.Count);
        Assert.AreEqual(1, view.PairableOperationsDataSource.Count);
        Assert.AreEqual(inputDataCopy[1].Description, view.OperationPairsDataSource[0].Description1);
        Assert.AreEqual(inputDataCopy[0].Description, view.OperationPairsDataSource[0].Description2);
    }

    [TestMethod]
    public void Should_UnpairOperations_When_UnpairIsClicked()
    {
        var form = new PairOperationsDialogBox();

        var inputData = GetStandardTestData();
        var inputDataCopy = GetStandardTestData();

        form.InitializeData(inputData);

        var view = form as IPairOperationsDialogBoxView;
        CollectionAssert.AreEqual(inputData, view.PairableOperationsDataSource);
        Assert.IsFalse(view.OperationPairsDataSource.Any());

        view.PairableOperationsGrid.Rows[0].Selected = true;
        view.PairableOperationsGrid.Rows[1].Selected = true;
        form.Presenter.OnPairButtonClick();

        view.OperationPairsGrid.Rows[0].Selected = true;
        form.Presenter.OnUnpairButtonClick();

        Assert.AreEqual(0, view.OperationPairsDataSource.Count);
        Assert.AreEqual(3, view.PairableOperationsDataSource.Count);
    }

    [TestMethod]
    public void Should_ReturnDialogResultCancel_When_CancelIsClicked()
    {
        var form = new PairOperationsDialogBox();

        var inputData = GetStandardTestData();

        form.InitializeData(inputData);

        var view = form as IPairOperationsDialogBoxView;
        CollectionAssert.AreEqual(inputData, view.PairableOperationsDataSource);
        Assert.IsFalse(view.OperationPairsDataSource.Any());

        form.Presenter.OnCancelButtonClick();

        Assert.AreEqual(DialogResult.Cancel, view.DialogResult);
    }

    private static List<Operation> GetStandardTestData()
    {
        var data = new List<Operation>()
        {
            new Operation()
            {
                AccountCode = "ACCOUNT1",
                Amount = 100,
                Currency = "PLN",
                Date = new DateTime(2000, 1, 1),
                Description = "TEST_DESC_1"
            },
            new Operation()
            {
                AccountCode = "ACCOUNT2",
                Amount = -100,
                Currency = "PLN",
                Date = new DateTime(2000, 1, 1),
                Description = "TEST_DESC_2"
            },
            new Operation()
            {
                AccountCode = "ACCOUNT1",
                Amount = 1780,
                Currency = "PLN",
                Date = new DateTime(2000, 1, 1),
                Description = "TEST_DESC_3"
            }
        };

        return data;
    }
}
