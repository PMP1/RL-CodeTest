using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL_CodeTest
{
    public class PolicyData
    {
        public string PolicyNumber { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public int Premiums { get; set; } 

        public Boolean Membership { get; set; }

        public int DiscretionaryBonus { get; set; }

        public float UpliftPercentage { get; set; }

        public string PolicyType { get; set; }

        public float MaturityValue { get; set; }

    }
}
