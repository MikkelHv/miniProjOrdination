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
        public void TC1_AnvendOrdination_Og_DoegnDosis()
        {
            // Edens
            // Mikkel
            //
            PN ordinationPn = new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
            //PN pn1 = new PN(DateTime.Now, DateTime.Now.AddDays(5), 2, laegemiddel); //Dette er jo oprettelsen af en ny ordnidation og ikke en ny anvendelse
            Dato datoNu = new Dato { dato = DateTime.Now };
            Dato datoOmSyvDage = new Dato { dato = DateTime.Now.AddDays(1)};


            //Første anvendelse af ordniation
            ordinationPn.givDosis(datoNu);
            //Test på at der kun er 1 anvendelse
            double forventetAnvendelser = 1;
            // Test på at der kun er 1 anvendelse
            Assert.AreEqual(forventetAnvendelser, ordinationPn.dates.Count());

            //Anden anvendelse af ordination
            ordinationPn.givDosis(datoNu);
            //Sætter forventetAnvendelser = 2
            forventetAnvendelser = 2;
            // Test på at der nu er 2 anvendelser
            Assert.AreEqual(forventetAnvendelser, ordinationPn.dates.Count());

            //Test på doegnDosis
            // Der bliver givet 2 dosis per givDosis
            double forventetDoegnDosis = 4; 
            Assert.AreEqual(forventetDoegnDosis, ordinationPn.doegnDosis());

            //Testgennemsnit over flere dage DoegnDosis
            
            ordinationPn.givDosis(datoOmSyvDage);
            forventetDoegnDosis = 3;
            Assert.AreEqual(forventetDoegnDosis, ordinationPn.doegnDosis());

        }

    }
}
