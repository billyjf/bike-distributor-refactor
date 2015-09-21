using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", Bike.OneThousand);
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", Bike.TwoThousand);
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand);
        private readonly static Bike F1 = new Bike("Felt", "F1", Bike.FiveThousand);
        private readonly static Bike S5 = new Bike("Cervelo", "S5 Dura-Ace", Bike.EightThousand);

        public string IgnorePdfCreationDateAndID(string pdfBase64String)
        {
            var pdfBytes = System.Convert.FromBase64String(pdfBase64String);
            var pdfUTF8String = System.Text.Encoding.UTF8.GetString(pdfBytes);

            var exp = new Regex(@"\/CreationDate\(.*\)");
            var pdfPresentationToCheck = exp.Replace(pdfUTF8String, "");
            exp = new Regex(@"\/ID\[.*\]");
            pdfPresentationToCheck = exp.Replace(pdfPresentationToCheck, "");

            pdfBytes = System.Text.Encoding.UTF8.GetBytes(pdfPresentationToCheck);
            pdfPresentationToCheck = System.Convert.ToBase64String(pdfBytes);

            return pdfPresentationToCheck;
        }

        [TestMethod]
        public void ReceiptOneDefy()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(Defy, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new TextReceipt(new ReceiptData(company,
                                                    "$1,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$1,000.00"
                                                        }
                                                    },
                                                    "$72.50",
                                                    "$1,072.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.Text));
        }

        [TestMethod]
        public void ReceiptOneElite()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(Elite, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new TextReceipt(new ReceiptData(company,
                                                    "$2,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$2,000.00"
                                                        }
                                                    },
                                                    "$145.00",
                                                    "$2,145.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.Text));
        }

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(DuraAce, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new TextReceipt(new ReceiptData(company,
                                                    "$5,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$5,000.00"
                                                        }
                                                    },
                                                    "$362.50",
                                                    "$5,362.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.Text));
        }

        [TestMethod]
        public void ReceiptFiveF1Discount()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 5;
            var testLine = new Line(F1, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new TextReceipt(new ReceiptData(company,
                                                "$20,000.00",
                                                new TupleList<Line, string> {
                                                    {
                                                        testLine,
                                                        "$20,000.00"
                                                    }
                                                },
                                                "$1,450.00",
                                                "$21,450.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.Text));
        }

        [TestMethod]
        public void ReceiptFiveS5Discount()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 5;
            var testLine = new Line(S5, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new TextReceipt(new ReceiptData(company,
                                                "$32,000.00",
                                                new TupleList<Line, string> {
                                                    {
                                                        testLine,
                                                        "$32,000.00"
                                                    }
                                                },
                                                "$2,320.00",
                                                "$34,320.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.Text));
        }

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(Defy, quantity);
            order.AddLine(testLine);

            var check_receipt_against = 
                new HtmlReceipt(new ReceiptData(company,
                                                    "$1,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$1,000.00"
                                                        }
                                                    },
                                                    "$72.50",
                                                    "$1,072.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.HTML));
        }

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(Elite, quantity);
            order.AddLine(testLine);

            var check_receipt_against = 
                new HtmlReceipt(new ReceiptData(company,
                                                    "$2,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$2,000.00"
                                                        }
                                                    },
                                                    "$145.00",
                                                    "$2,145.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.HTML));
        }

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 1;
            var testLine = new Line(DuraAce, quantity);
            order.AddLine(testLine);

            var check_receipt_against = 
                new HtmlReceipt(new ReceiptData(company,
                                                    "$5,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            testLine,
                                                            "$5,000.00"
                                                        }
                                                    },
                                                    "$362.50",
                                                    "$5,362.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.HTML));
        }

        [TestMethod]
        public void HtmlReceiptFiveF1Discount()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 5;
            var testLine = new Line(F1, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new HtmlReceipt(new ReceiptData(company,
                                                "$20,000.00",
                                                new TupleList<Line, string> {
                                                    {
                                                        testLine,
                                                        "$20,000.00"
                                                    }
                                                },
                                                "$1,450.00",
                                                "$21,450.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.HTML));
        }

        [TestMethod]
        public void HtmlReceiptFiveS5Discount()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 5;
            var testLine = new Line(S5, quantity);
            order.AddLine(testLine);

            var check_receipt_against =
                new HtmlReceipt(new ReceiptData(company,
                                                "$32,000.00",
                                                new TupleList<Line, string> {
                                                    {
                                                        testLine,
                                                        "$32,000.00"
                                                    }
                                                },
                                                "$2,320.00",
                                                "$34,320.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt(Order.Format.HTML));
        }

        [TestMethod]
        public void PDFReceiptFiveS5Discount()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            var quantity = 5;
            var testLine = new Line(S5, quantity);
            order.AddLine(testLine);

            var checkReceiptAgainst =
                new PdfReceipt(new ReceiptData(company,
                                               "$32,000.00",
                                               new TupleList<Line, string> {
                                                   {
                                                       testLine,
                                                       "$32,000.00"
                                                   }
                                               },
                                               "$2,320.00",
                                               "$34,320.00")).TransformText();
            var orderReceipt = order.Receipt(Order.Format.PDF);
            var modifiedPresentation_checkReceiptAgainst = IgnorePdfCreationDateAndID(checkReceiptAgainst);
            var modifiedPresentation_orderReceipt = IgnorePdfCreationDateAndID(orderReceipt);

            // Easily see PDF presentation for testing
            File.WriteAllBytes("PDFTest.pdf", Convert.FromBase64String(orderReceipt));

            Assert.AreEqual(modifiedPresentation_checkReceiptAgainst, modifiedPresentation_orderReceipt);
        }
    }
}


