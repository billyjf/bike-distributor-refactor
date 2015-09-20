using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    public partial class TextReceipt
    {
        private BaseReceiptData data;

        public TextReceipt(BaseReceiptData data)
        {
            this.data = data;
        }
    }
}
