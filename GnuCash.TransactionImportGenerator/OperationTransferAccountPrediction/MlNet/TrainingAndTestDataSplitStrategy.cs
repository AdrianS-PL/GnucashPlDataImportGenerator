using AutoMapper;
using Microsoft.ML;
using System.Collections.Generic;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    abstract class TrainingAndTestDataSplitStrategy<PredictionInputType, PreSplitInputType> where PredictionInputType : class where PreSplitInputType : class
    {
        private readonly IMapper _mapper;

        protected TrainingAndTestDataSplitStrategy(PreSplitDataMappingProfile<PredictionInputType, PreSplitInputType> mappingProfile)
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingProfile);
            }).CreateMapper();
        }

        public abstract (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<PreSplitInputType> data);

        protected IEnumerable<PredictionInputType> Map(IEnumerable<PreSplitInputType> preSplitTypeData)
        {
            var result = new List<PredictionInputType>();

            foreach (var item in preSplitTypeData)
            {
                var mapped = _mapper.Map<PredictionInputType>(item);
                result.Add(mapped);
            }

            return result;
        }
    }
}
