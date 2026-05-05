using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    internal class MlNetPreSplitPredictionInputRowSimpleMapper : SimpleMapper<MlNetPredictionInputRow, MlNetPreSplitPredictionInputRow>
    {
        public override MlNetPredictionInputRow Map(MlNetPreSplitPredictionInputRow input)
        {
            var result = new MlNetPredictionInputRow(input.Description, input.TransferAccountId);
            return result;
        }
    }
}
