using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL_CodeTest
{
    public class MaturityCalculation
    {

        /// <summary>
        /// calculate the Maturity Value of a Policy
        /// 
        /// Calculated as:
        /// ((premiums – management fee) +discretionary bonus if qualifying) *uplift
        /// </summary>
        /// <param name="data"></param>
        public void CalculateMaturity(List<PolicyData> data)
        {
            foreach(var row in data)
            {
                var fee = CalculateManagementFee(row);
                var bonus = CalculateBonus(row);
                row.MaturityValue = ((row.Premiums - fee) + bonus) * (1 + (row.UpliftPercentage/100));
            }

        }

        /// <summary>
        /// Calculate the Management Fee for a particular Policy
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float CalculateManagementFee(PolicyData data)
        {
            switch (data.PolicyType)
            {
                case "A":
                    return data.Premiums * 0.03f;
                case "B":
                    return data.Premiums * 0.05f;
                default:
                    return data.Premiums * 0.07f;
            }
        }

        /// <summary>
        /// Calculate the Management Fee for a particular Policy
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float CalculateBonus(PolicyData data)
        {
            switch (data.PolicyType)
            {
                case "A":
                    return CalculateBonusA(data);
                case "B":
                    return CalculateBonusB(data);
                default:
                    return CalculateBonusC(data);
            }
        }


        /// <summary>
        /// Bonus for Policy Type A
        /// Criteria: Policy was taken out before 01/01/1990
        /// </summary>
        private float CalculateBonusA(PolicyData data)
        {
            if (data.PolicyStartDate < new DateTime(1990, 1, 1))
            {
                return data.DiscretionaryBonus;
            }

            return 0;
        }

        /// <summary>
        /// Bonus for Policy Type B
        /// Criteria: Policy has membership rights
        /// </summary>
        private float CalculateBonusB(PolicyData data)
        {
            if (data.Membership)
            {
                return data.DiscretionaryBonus;
            }

            return 0;
        }

        /// <summary>
        /// Bonus for Policy Type C
        /// Criteria: Policy was taken out before 01/01/1990 Policy has membership rights
        /// </summary>
        private float CalculateBonusC(PolicyData data)
        {
            if (data.PolicyStartDate >= new DateTime(1990, 1, 1) && data.Membership)
            {
                return data.DiscretionaryBonus;
            }

            return 0;
        }


    }
}
