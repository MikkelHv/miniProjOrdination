using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test
{
    /*
    [TestClass]
    public class PnTest
    {
        
        //opret et lægemiddel
        Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");
        [TestMethod]
        public void TC1()
        {
            PN ordination = new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
            ordination.dates.Add

            double forventetDosis = 2;

            //Assert.AreEqual(forventetDosis, ordination.doegnDosis());
            Assert.AreEqual(forventetDosis, ordination.dates.Count()); //Jeg rammer ikke den rigtige db så den retunere 0, selvom der er 1 dato i db
        }
        
    }
    */
    /*
     * chat løsning, virker heller ikke
    [TestClass]
    public class PnTest
    {
        private PN opretPNOrdination()
        {
            // Opret en PN ordination med testdata
            Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");
            return new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
        }

        [TestMethod]
        public void AnvendOrdination_PNOrdinationAnvendt_DosisTilfoejet()
        {
            // Arrange
            PN ordination = opretPNOrdination();
            Dato testDato = new Dato(DateTime.Now); // Opret en testdato

            // Act
            ordination.givDosis(testDato); // Anvend ordinationen

            // Assert
            Assert.AreEqual(1, ordination.dates.Count); // Forventet resultat: en dato blev tilføjet
        }
    }
    */

}
