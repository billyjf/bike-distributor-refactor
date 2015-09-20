using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", Bike.OneThousand);
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", Bike.TwoThousand);
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand);

        [TestMethod]
        public void ReceiptOneDefy()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(Defy, 1));

            var check_receipt_against =
                new TextReceipt(new BaseReceiptData(company,
                                                    "$1,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            new Line(Defy, 1),
                                                            "$1,000.00"
                                                        }
                                                    },
                                                    "$72.50",
                                                    "$1,072.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt());
        }

        [TestMethod]
        public void ReceiptOneElite()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(Elite, 1));

            var check_receipt_against =
                new TextReceipt(new BaseReceiptData(company,
                                                    "$2,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            new Line(Elite, 1),
                                                            "$2,000.00"
                                                        }
                                                    },
                                                    "$145.00",
                                                    "$2,145.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt());
        }

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(DuraAce, 1));

            var check_receipt_against =
                new TextReceipt(new BaseReceiptData(company,
                                                    "$5,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            new Line(DuraAce, 1),
                                                            "$5,000.00"
                                                        }
                                                    },
                                                    "$362.50",
                                                    "$5,362.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.Receipt());
        }

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(Defy, 1));

            var check_receipt_against = 
                new HtmlReceipt(new BaseReceiptData(company,
                                                    "$1,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            new Line(Defy, 1),
                                                            "$1,000.00"
                                                        }
                                                    },
                                                    "$72.50",
                                                    "$1,072.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.HtmlReceipt());
        }

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(Elite, 1));

            var check_receipt_against = 
                new HtmlReceipt(new BaseReceiptData(company,
                                                    "$2,000.00",
                                                    new TupleList<Line, string> {
                                                        {
                                                            new Line(Elite, 1),
                                                            "$2,000.00"
                                                        }
                                                    },
                                                    "$145.00",
                                                    "$2,145.00")).TransformText();

            Assert.AreEqual(check_receipt_against, order.HtmlReceipt());
        }

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var company = "Anywhere Bike Shop";
            var order = new Order(company);
            order.AddLine(new Line(DuraAce, 1));

            var check_receipt_against = 
                new HtmlReceipt(new BaseReceiptData(company,
                                                    "$5,000.00",
                                                    new TupleList<Line, string> {
                                                                    {
                                                                        new Line(DuraAce, 1),
                                                                        "$5,000.00"
                                                                    }
                                                    },
                                                    "$362.50",
                                                    "$5,362.50")).TransformText();

            Assert.AreEqual(check_receipt_against, order.HtmlReceipt());
        }
    }
}


