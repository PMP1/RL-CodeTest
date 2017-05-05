using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL_CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataReadWriter = new DataReadWriter();
            var maturityCalculator = new MaturityCalculation();

            //read the data from csv and convert to a list
            var data = dataReadWriter.ReadPolicyDate("/Data/MaturityData.csv");

            //Calculate the Maturity value fro each policy
            maturityCalculator.CalculateMaturity(data);

            //save the maturity value xml file
            dataReadWriter.WriteMaturityValue(data, "/Data/results.xml");



        }
    }
}
