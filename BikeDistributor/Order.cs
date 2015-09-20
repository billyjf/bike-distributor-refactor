using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    public class Order
    {
        public enum Format { Text, HTML };

        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; private set; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        private double CalculateAmountPlusDiscount(Bike bike, int quantity)
        {
            var amountWithDiscount = 0d;

            switch (bike.Price)
            {
                case Bike.OneThousand:
                    if (quantity >= 20)
                        amountWithDiscount += quantity * bike.Price * .9d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
                case Bike.TwoThousand:
                    if (quantity >= 10)
                        amountWithDiscount += quantity * bike.Price * .8d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
                case Bike.FiveThousand:
                    if (quantity >= 5)
                        amountWithDiscount += quantity * bike.Price * .8d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
                case Bike.EightThousand:
                    if (quantity >= 4)
                        amountWithDiscount += quantity * bike.Price * .8d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
            }

            return amountWithDiscount;
        }

        public string Receipt(Format format)
        {
            var totalAmount = 0d;
            var reportLines = new TupleList<Line, string>();
            foreach (var line in _lines)
            {
                var thisAmount = 0d;
                thisAmount += CalculateAmountPlusDiscount(line.Bike, line.Quantity);
                reportLines.Add(line, thisAmount.ToString("C"));
                totalAmount += thisAmount;
            }
            var tax = totalAmount * TaxRate;

            var data = new ReceiptData(Company,
                                       totalAmount.ToString("C"),
                                       reportLines,
                                       tax.ToString("C"),
                                       (totalAmount + tax).ToString("C"));
            if (format == Format.Text)
                return new TextReceipt(data).TransformText();
            else if (format == Format.HTML)
                return new HtmlReceipt(data).TransformText();
            else
                throw new Exception("Unsupported format type!");
        }

    }
}
