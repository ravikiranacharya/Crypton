using Crypton.Models.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.DataHelpers
{
    public abstract class Data
    {
        public Provider provider { get; set; }
        public Data()
        {

        }

        public Data(Provider provider)
        {
            this.provider = provider;
        }
        public List<object> data { get; set; }
    }
}
