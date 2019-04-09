using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetGUI;

namespace ConsoleTester {
    [TestClass]
    public class UnitTests {

        [TestMethod]
        public void TestMethod1() {
            SpreadsheetViewStub stub = new SpreadsheetViewStub();
            Controller controller = new Controller(stub);


            stub.FireCloseEvent();
            Assert.IsTrue(stub.CalledClosed);
            Assert.IsFalse(stub.Changed);

            stub.FireUpdateEvent(0, 0, "2");
            stub.FireCloseEvent();
            Assert.IsTrue(stub.Changed);

            Assert.AreEqual(stub.NameBox, "A1");
            Assert.AreEqual(stub.ContentBox, "2");
            Assert.AreEqual(stub.Value, "2");

            stub.FireUpdateEvent(0, 1, "Hello");
            stub.FireCloseEvent();
            Assert.IsTrue(stub.Changed);

            Assert.AreEqual(stub.NameBox, "A2");
            Assert.AreEqual(stub.ContentBox, "Hello");
            Assert.AreEqual(stub.Value, "Hello");

            stub.FireUpdateEvent(0, 1, "=Z1000");
            Assert.AreEqual(stub.NameBox, "A2");
            Assert.AreEqual(stub.ContentBox, "Hello");
            Assert.AreEqual(stub.Value, "Hello");
            Assert.IsTrue(stub.ErrorLabel.ToString() != "");


        }


    }
}
