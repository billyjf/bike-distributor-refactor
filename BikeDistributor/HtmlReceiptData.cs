using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    partial class HtmlReceipt
    {
        private BaseReceiptData data;

        public HtmlReceipt(BaseReceiptData data)
        {
            this.data = data;
        }
    }
}
