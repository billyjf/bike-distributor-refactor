using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    partial class HtmlReceipt
    {
        private ReceiptData data;

        public HtmlReceipt(ReceiptData data)
        {
            this.data = data;
        }
    }
}
