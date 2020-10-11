using GnuCash.TransactionImportGenerator.Model;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport;
using GnucashPlDataImportGeneratorApp.Forms.TransactionsImport.ViewInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnucashPlDataImportGeneratorApp.Tests.Forms.TransactionsImport
{
    [TestClass]
    public class CheckAssignedTransferAccountDialogBoxTests
    {
        
        [TestMethod]
        public void Should_LoadFirstTransactionData_When_InitializeData()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(inputData, transferAccounts);

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.AreEqual(inputData.Count.ToString(), view.TbxCountText);
            Assert.AreEqual("1", view.TbxIndexText);
            CollectionAssert.AreEqual(transferAccounts, view.AvailableTransferAccounts);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);
            //Assert.AreEqual()
        }

        [TestMethod]
        public void Should_CleanDisplay_When_TransactionDataIsEmpty()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(new List<TransactionImportFileRow>(), transferAccounts);

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.AreEqual("", view.TbxCountText);
            Assert.AreEqual("", view.TbxIndexText);
            Assert.AreEqual("", view.TbxAccountText);
            Assert.AreEqual("", view.TbxAmountText);
            Assert.AreEqual("", view.TbxDescriptionText);
            Assert.AreEqual("", view.TbxTransactionDateText);
            Assert.AreEqual("", view.TbxTransferMemoText);
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsFalse(view.BtnNextEnabled);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Should_ThrowNullReferenceException_When_TransactionDataIsNull()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(null, transferAccounts);
        }

        [TestMethod]
        public void Should_CleanDisplay_When_AvailableTransferAccountsIsEmpty()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData();
            form.InitializeData(inputData, new List<string>());

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.AreEqual("", view.TbxCountText);
            Assert.AreEqual("", view.TbxIndexText);
            Assert.AreEqual("", view.TbxAccountText);
            Assert.AreEqual("", view.TbxAmountText);
            Assert.AreEqual("", view.TbxDescriptionText);
            Assert.AreEqual("", view.TbxTransactionDateText);
            Assert.AreEqual("", view.TbxTransferMemoText);
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsFalse(view.BtnNextEnabled);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Should_ThrowNullReferenceException_When_AvailableTransferAccountsIsNull()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData();
            form.InitializeData(inputData, null);
        }

        [TestMethod]
        public void Should_PrevAndNextButtonEnableStatusActCorrectly_When_DataHas3Records()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(inputData, transferAccounts);

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("1", view.TbxIndexText);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnNextButtonClick();
            Assert.IsTrue(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("2", view.TbxIndexText);
            Assert.AreEqual(inputData[1].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[1].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[1].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[1].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[1].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnNextButtonClick();
            Assert.IsTrue(view.BtnPrevEnabled);
            Assert.IsFalse(view.BtnNextEnabled);
            Assert.AreEqual("3", view.TbxIndexText);
            Assert.AreEqual(inputData[2].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[2].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[2].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[2].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[2].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnPrevButtonClick();
            Assert.IsTrue(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("2", view.TbxIndexText);
            Assert.AreEqual(inputData[1].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[1].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[1].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[1].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[1].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnPrevButtonClick();
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("1", view.TbxIndexText);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);
        }

        [TestMethod]
        public void Should_PrevAndNextButtonEnableStatusActCorrectly_When_DataHas2Records()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData().Take(2).ToList();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(inputData, transferAccounts);

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("1", view.TbxIndexText);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnNextButtonClick();
            Assert.IsTrue(view.BtnPrevEnabled);
            Assert.IsFalse(view.BtnNextEnabled);
            Assert.AreEqual("2", view.TbxIndexText);
            Assert.AreEqual(inputData[1].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[1].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[1].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[1].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[1].TransferMemo, view.TbxTransferMemoText);

            form.Presenter.OnPrevButtonClick();
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsTrue(view.BtnNextEnabled);
            Assert.AreEqual("1", view.TbxIndexText);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);
        }

        [TestMethod]
        public void Should_PrevAndNextButtonEnableStatusActCorrectly_When_DataHas1Record()
        {
            var form = new CheckAssignedTransferAccountDialogBox();
            var inputData = GetStandardTestData().Take(1).ToList();
            var transferAccounts = GetStandardTransferAccountList();
            form.InitializeData(inputData, transferAccounts);

            var view = form as ICheckAssignedTransferAccountDialogBoxView;
            Assert.IsFalse(view.BtnPrevEnabled);
            Assert.IsFalse(view.BtnNextEnabled);
            Assert.AreEqual("1", view.TbxIndexText);
            Assert.AreEqual(inputData[0].Account, view.TbxAccountText);
            Assert.AreEqual(inputData[0].Deposit.ToString(), view.TbxAmountText);
            Assert.AreEqual(inputData[0].Description, view.TbxDescriptionText);
            Assert.AreEqual(inputData[0].Date.ToString(), view.TbxTransactionDateText);
            Assert.AreEqual(inputData[0].TransferMemo, view.TbxTransferMemoText);
        }

        private List<TransactionImportFileRow> GetStandardTestData()
        {
            return new List<TransactionImportFileRow>
            {
                new TransactionImportFileRow
                {
                    Account = "Aktywa:ROR Bank 1",
                    Date = new DateTime(2000,1,1),
                    Deposit = 123.54m,
                    Description = "test description 1",
                    Memo = "test memo 1",
                    TransferAccount = "Wydatki:Pies",
                    TransferMemo = " test transfer memo 1"
                },
                new TransactionImportFileRow
                {
                    Account = "Aktywa:ROR Bank 2",
                    Date = new DateTime(2000,1,2),
                    Deposit = 678.98m,
                    Description = "test description 2",
                    Memo = "test memo 2",
                    TransferAccount = "Wydatki:Kot",
                    TransferMemo = " test transfer memo 2"
                },
                new TransactionImportFileRow
                {
                    Account = "Aktywa:ROR Bank 3",
                    Date = new DateTime(2000,1,3),
                    Deposit = 1678.98m,
                    Description = "test description 3",
                    Memo = "test memo 3",
                    TransferAccount = "Wydatki:Chomik",
                    TransferMemo = " test transfer memo 3"
                }
            };
        }

        private List<string> GetStandardTransferAccountList()
        {
            return new List<string> { "Aktywa:ROR Bank 1", "Aktywa:ROR Bank 2", "Aktywa:ROR Bank 3", "Wydatki:Pies", "Wydatki:Kot", "Wydatki:Chomik" };
        }
    }
}
