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
        // Test af om 
        //opret et lægemiddel
        Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");

        [TestMethod]
        public void TC1()
        {
            // Edens
            // Mikkel
            //
            PN ordinationPn = new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
            //PN pn1 = new PN(DateTime.Now, DateTime.Now.AddDays(5), 2, laegemiddel); //Dette er jo oprettelsen af en ny ordnidation og ikke en ny anvendelse
            Dato dato = new Dato {dato = DateTime.Now};
            
            //Første anvendelse af ordniation
            ordinationPn.givDosis(dato);
            //Test på at der kun er 1 anvendelse
            double forventetAnvendelser = 1;
            // Test på at der kun er 1 anvendelse
            Assert.AreEqual(forventetAnvendelser, ordinationPn.dates.Count());

            //Anden anvendelse af ordination
            ordinationPn.givDosis(dato);
            //Sætter forventetAnvendelser = 2
            forventetAnvendelser = 2;
            // Test på at der nu er 2 anvendelser
            Assert.AreEqual(forventetAnvendelser, ordinationPn.dates.Count());
        }
        
    }
    
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
