using AutoMapper;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    abstract class TrainingAndTestDataSplitStrategy<PredictionInputType, PreSplitInputType> where PredictionInputType : class where PreSplitInputType : class
    {
        IMapper _mapper;

        public TrainingAndTestDataSplitStrategy(PreSplitDataMappingProfile<PredictionInputType, PreSplitInputType> mappingProfile)
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingProfile);
            }).CreateMapper();
        }

        public abstract (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<PreSplitInputType> data);

        protected IEnumerable<PredictionInputType> Map(IEnumerable<PreSplitInputType> preSplitTypeData)
        {
            //return preSplitTypeData.Select(q => _mapper.Map<PredictionInputType>(preSplitTypeData)); // problem z linq?

            var result = new List<PredictionInputType>();

            foreach(var item in preSplitTypeData)
            {
                var mapped = _mapper.Map<PredictionInputType>(item);
                result.Add(mapped);
            }

            return result;
        }
    }
}
