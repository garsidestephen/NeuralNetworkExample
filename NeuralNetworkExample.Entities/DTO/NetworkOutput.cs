using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkExample.Entities.DTO
{
    public class NetworkOutput
    {
        public int Number { get; set; }

        public string Description { get; set; }

        public double ExpectedOutput { get; set; }
    }
}
