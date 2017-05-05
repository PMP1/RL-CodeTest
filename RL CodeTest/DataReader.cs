using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RL_CodeTest
{
    public class DataReadWriter
    {
        /// <summary>
        /// OPen a file and read the data
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<PolicyData> ReadPolicyDate(string location)
        {

            if (!File.Exists(location))
            {
                throw new Exception("File Not found");
            }

            List<PolicyData> results = new List<PolicyData>();

            var firstLine = false;

            using (var fs = File.OpenRead(location))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    //skip first line
                    if (!firstLine)
                    {
                        reader.ReadLine();
                        firstLine = true;
                    }

                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //convert the data to an object
                    var result = ConvertStringArrayToPolicyData(values);

                    results.Add(result);
                }
            }
            return results;
        }

        /// <summary>
        /// Write the Policy Number and Maturity value to an xml file
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savePath"></param>
        public void WriteMaturityValue(List<PolicyData> data, string savePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("policies");
            xmlDoc.AppendChild(rootNode);

            foreach (var policy in data)
            {
                XmlNode policyNode = xmlDoc.CreateElement("policy");

                XmlNode policyNumberNode = xmlDoc.CreateElement("policy-number");
                policyNumberNode.InnerText = policy.PolicyNumber;

                XmlNode maturityValueNode = xmlDoc.CreateElement("maturity-value");
                maturityValueNode.InnerText = policy.MaturityValue.ToString();

                policyNode.AppendChild(policyNumberNode);
                policyNode.AppendChild(maturityValueNode);
                rootNode.AppendChild(policyNode);
            }

            xmlDoc.Save(savePath);
        }

        /// <summary>
        /// Convert a string array to a PolicyData object. Will check that the data is also valid and throw an exception if not
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private PolicyData ConvertStringArrayToPolicyData(string[] data)
        {
            if (string.IsNullOrWhiteSpace(data[0]))
            {
                throw new Exception("Invalid Data - Policy Number");
            }

            if (!DateTime.TryParse(data[1], out DateTime startDate))
            {
                throw new Exception("Invalid Data - Policy Start Date");
            }

            if (!int.TryParse(data[2], out int premiums))
            {
                throw new Exception("Invalid Data - Premiums data");
            }

            if (!int.TryParse(data[4], out int discretionaryBonus))
            {
                throw new Exception("Invalid Data - Discretionary Bonus");
            }

            if (!float.TryParse(data[5], out float upliftPercentage))
            {
                throw new Exception("Invalid Data - Uplift Percentage");
            }

            var result = new PolicyData()
            {
                PolicyNumber = data[0],
                PolicyStartDate = startDate,
                Premiums = premiums,
                Membership = ConvertMembershipToBool(data[3]),
                DiscretionaryBonus = discretionaryBonus,
                UpliftPercentage = upliftPercentage,
                PolicyType = GetPolicyType(data[0])
            };

            return result;
        }

        /// <summary>
        /// Converts  Y or  N to a boolean value
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        private bool ConvertMembershipToBool(string membership)
        {
            if (membership == "Y")
                return true;
            if (membership == "N")
                return false;

            throw new Exception("Invalid Data - Membership");
        }

        /// <summary>
        /// Gets the policy Type based off the policy number
        /// </summary>
        /// <param name="policyNumber"></param>
        /// <returns></returns>
        private string GetPolicyType(string policyNumber)
        {
            var result = policyNumber.Substring(0, 1);

            if (result != "A" && result != "B" && result != "C")
            {
                throw new Exception("Invalid Data - PolicyNumber");
            }

            return result;
        }
    }
}
