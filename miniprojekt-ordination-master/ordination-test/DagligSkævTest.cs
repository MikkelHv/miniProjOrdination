using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test
{
    [TestClass]
    public class DagligSkævTest
    {
        [TestMethod]
        public void TC5_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC1
            // Opret en ordination med 0 dosis
            Assert.AreEqual(0, dagligSkæv.doegnDosis());
        }


        // TC6 skal laves om så vi forventer en fejl, det vil sige koden skal opdateres med regel om,
        // at der ikke kan være minus doseringer
        [TestMethod]
        public void TC6_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC2
            // Opret en ordination med -1 dosis
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, -1));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, -1));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, -1));
            Assert.AreEqual(-3, dagligSkæv.doegnDosis()); // Lav til throwsexception
                                                          //Assert.ThrowsException
        }

        [TestMethod]
        public void TC7_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC2
            // Opret en ordination med -1 dosis
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1));
            Assert.AreEqual(3, dagligSkæv.doegnDosis());
        }


        [TestMethod]
        public void TC8_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC2
            // Opret en ordination med -1 dosis
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 10));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 10));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 10));
            Assert.AreEqual(30, dagligSkæv.doegnDosis());
        }

        [TestMethod]
        public void TC9_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC2
            // Opret en ordination med -1 dosis
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 100));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 100));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 100));
            Assert.AreEqual(300, dagligSkæv.doegnDosis());
        }

        [TestMethod]
        public void TC10_DeognDosis() //test af dagligskæv dosering
        {
            // Opret en ny instans af dagligSkæv med tom liste doser-liste
            // Der er tre doseringer planlagt hver dag, disse er tomme ved oprettlsen
            DagligSkæv dagligSkæv = new DagligSkæv(new DateTime(2024, 4, 24), new DateTime(2024, 4, 27));

            // TC2
            // Opret en ordination med -1 dosis
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1000));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1000));
            dagligSkæv.doser.Add(new Dosis(DateTime.Now, 1000));
            Assert.AreEqual(3000, dagligSkæv.doegnDosis());
        }
    }
}
