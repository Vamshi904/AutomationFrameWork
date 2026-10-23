// using Microsoft.ML;
// using Microsoft.ML.Data;
// using System;
// using System.IO;
// using System.Threading.Tasks;

// namespace Document_Management.Model
// {
//     public class AiHelper
//     {
//         private readonly MLContext _mlContext;
//         private readonly PredictionEngine<TestOutcomeData, TestOutcomePrediction>? _predictionEngine;

//         public AiHelper()
//         {
//             _mlContext = new MLContext();

//             // Safely load model if it exists
//             if (File.Exists("model.zip"))
//             {
//                 DataViewSchema schema;
//                 ITransformer model = _mlContext.Model.Load("model.zip", out schema);
//                 _predictionEngine = _mlContext.Model.CreatePredictionEngine<TestOutcomeData, TestOutcomePrediction>(model);
//             }
//             else
//             {
//                 Console.WriteLine("⚠️ model.zip not found. AI predictions disabled.");
//                 _predictionEngine = null;
//             }
//         }

//         /// <summary>
//         /// Predicts whether the current URL is expected after login.
//         /// For now, this is a simple rule-based check, but you can later
//         /// replace it with ML model predictions.
//         /// </summary>
//         internal static Task<bool> PredictExpectedUrlAsync(string currentUrl)
//         {
//             bool isExpected = !string.IsNullOrEmpty(currentUrl) &&
//                               currentUrl.Contains("inventory.html", StringComparison.OrdinalIgnoreCase);

//             return Task.FromResult(isExpected);
//         }

//         /// <summary>
//         /// Predicts test outcome using ML model (Pass/Fail).
//         /// </summary>
//         public bool PredictOutcome(string browser, float loadTime, bool elementFound)
//         {
//             if (_predictionEngine == null)
//             {
//                 // Fallback: if model not loaded, assume pass if element found
//                 return elementFound;
//             }

//             var input = new TestOutcomeData
//             {
//                 Browser = browser,
//                 LoadTime = loadTime,
//                 ElementFound = elementFound
//             };

//             var prediction = _predictionEngine.Predict(input);
//             return prediction.PredictedPassed;
//         }

//         public static void TrainAndSaveModel()
//         {
//             var mlContext = new MLContext();

//             var dataView = mlContext.Data.LoadFromTextFile<TestOutcomeData>(
//                 path: "test_outcomes.csv", hasHeader: true, separatorChar: ',');

//             var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("Browser")
//                 .Append(mlContext.Transforms.Concatenate("Features", "Browser", "LoadTime", "ElementFound"))
//                 .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Passed"));

//             var model = pipeline.Fit(dataView);

//             mlContext.Model.Save(model, dataView.Schema, "model.zip");
//         }

//         public static void LogTestOutcome(string browser, float loadTime, bool elementFound, bool passed)
//         {
//             string filePath = "test_outcomes.csv";

//             // If file doesn't exist, create with header
//             if (!File.Exists(filePath))
//             {
//                 File.WriteAllText(filePath, "Browser,LoadTime,ElementFound,Passed\n");
//                 Console.WriteLine(filePath);
//             }

//             // Append new row
//             string newRow = $"{browser},{loadTime},{elementFound},{(passed ? 1 : 0)}\n";
//             File.AppendAllText(filePath, newRow);
//         }


//     }
// }
using Microsoft.ML;
using Microsoft.ML.Data;
namespace Document_Management.Model
{
    public class AiHelper
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<TestOutcomeData, TestOutcomePrediction>? _predictionEngine;
        private const string ModelFolder = "MLModels";
        private const string ModelFile = "model.zip";
        private const string DataFile = "test_outcomes.csv";
        private const int RetrainThreshold = 100; // retrain when dataset grows beyond this

        public AiHelper()
        {
            _mlContext = new MLContext();

            string modelPath = Path.Combine(ModelFolder, ModelFile);

            // Ensure model folder exists
            if (!Directory.Exists(ModelFolder))
                Directory.CreateDirectory(ModelFolder);

            // Train if model.zip missing
            if (!File.Exists(modelPath))
            {
                Console.WriteLine(" model.zip not found. Training new model...");
                TrainAndSaveModel();
            }

            // Load model
            if (File.Exists(modelPath))
            {
                DataViewSchema schema;
                ITransformer model = _mlContext.Model.Load(modelPath, out schema);
                _predictionEngine = _mlContext.Model.CreatePredictionEngine<TestOutcomeData, TestOutcomePrediction>(model);
            }
            else
            {
                Console.WriteLine(" Could not load model. AI predictions disabled.");
                _predictionEngine = null;
            }
        }

        /// Simple rule-based check for expected URL after login.

        public static Task<bool> PredictExpectedUrlAsync(string currentUrl)
        {
            bool isExpected = !string.IsNullOrEmpty(currentUrl) &&
                              currentUrl.Contains("inventory.html", StringComparison.OrdinalIgnoreCase);

            return Task.FromResult(isExpected);
        }


        public bool PredictOutcome(string browser, float loadTime, bool elementFound)
        {
            if (_predictionEngine == null)
            {
                // Fallback: assume pass if element found
                return elementFound;
            }

            var input = new TestOutcomeData
            {
                Browser = browser,
                LoadTime = loadTime,
                ElementFound = elementFound


            };

            var prediction = _predictionEngine.Predict(input);

            Console.WriteLine($"Probability: {prediction.Probability:P2}");
            Console.WriteLine($"Score: {prediction.Score}");


            return prediction.PredictedPassed;




        }

        /// Train and save model.zip from test_outcomes.csv.
        public static void TrainAndSaveModel()
        {
            var mlContext = new MLContext();

            if (!File.Exists(DataFile))
            {
                Console.WriteLine(" No training data found. Cannot train model.");
                return;
            }

            var dataView = mlContext.Data.LoadFromTextFile<TestOutcomeData>(
                path: DataFile, hasHeader: true, separatorChar: ',');
            //working code 
            //  var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("Browser")
            // .Append(mlContext.Transforms.Conversion.ConvertType("ElementFound", outputKind: DataKind.Single))
            // .Append(mlContext.Transforms.Concatenate("Features", "Browser", "LoadTime", "ElementFound"))
            // .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Passed"));
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("Browser")
                .Append(mlContext.Transforms.Conversion.ConvertType("ElementFound", outputKind: DataKind.Single))
                .Append(mlContext.Transforms.Concatenate("Features", "Browser", "LoadTime", "ElementFound"))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Passed"));
            var model = pipeline.Fit(dataView);
            foreach (var col in dataView.Schema)
            {
                Console.WriteLine($"{col.Name} - {col.Type}");
            }


            string modelPath = Path.Combine(ModelFolder, ModelFile);
            mlContext.Model.Save(model, dataView.Schema, modelPath);

            Console.WriteLine($"Model trained and saved at {modelPath}");
        }

        /// Logs each test run outcome into test_outcomes.csv for continuous learning.
        /// Automatically retrains model when dataset grows beyond threshold.
        public static void LogTestOutcome(string browser, float loadTime, bool elementFound, bool passed)
        {
            if (!File.Exists(DataFile))
            {
                File.WriteAllText(DataFile, "Browser,LoadTime,ElementFound,Passed\n");
            }

            string newRow = $"{browser},{loadTime},{elementFound},{(passed)}\n";
            File.AppendAllText(DataFile, newRow);

            // Trigger retraining if dataset is large enough
            CheckAndRetrainModel();
        }

        /// Retrains model if dataset exceeds threshold.
        private static void CheckAndRetrainModel()
        {
            if (File.Exists(DataFile))
            {
                int rowCount = File.ReadAllLines(DataFile).Length - 1; // exclude header
                if (rowCount >= RetrainThreshold)
                {
                    Console.WriteLine($"📈 Dataset reached {rowCount} rows. Retraining model...");
                    TrainAndSaveModel();
                }
            }
        }
    }
}
