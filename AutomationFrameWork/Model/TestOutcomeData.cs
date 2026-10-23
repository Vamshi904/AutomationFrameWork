using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
namespace Document_Management.Model
 {
public class TestOutcomeData
{
    [LoadColumn(0)]
    public string Browser { get; set; }

    [LoadColumn(1)]
    public float LoadTime { get; set; }

    [LoadColumn(2)]
    public bool ElementFound { get; set; }

    [LoadColumn(3)]
    public bool Passed { get; set; }
}




}
