using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test
{
    [TestClass]
    public class PnTest
    {
        
        //opret et lægemiddel
        Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");
        [TestMethod]
        public void TC1()
        {
            PN ordination = new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
            ordination.givDosis;

            double forventetDosis = 2;

            Assert.AreEqual(forventetDosis, ordination.doegnDosis());
        }
        
    }
}
