
using Microsoft.ML.Data;

namespace Document_Management.Model
{
   public class TestOutcomePrediction { [ColumnName("PredictedLabel")] 
   public bool PredictedPassed { get; set; } 
   public float Probability { get; set; } 
   public float Score { get; set; } } }
