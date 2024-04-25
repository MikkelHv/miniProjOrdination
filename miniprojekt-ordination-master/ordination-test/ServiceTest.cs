namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count()); //Tester , Vi ved der er 1 dagligfast i databasen, så denne er automatisk true

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId, //Opretter en ny ordination i dagligfast
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count()); // Tester om den nye dagligfaste er oprettet, derfor tester den ,2.
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException() // Der skal smides en exception fra anvend ordination - Hvis pnID == null
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }

    [TestMethod]
    public void TC1_OpretDagligFastStartMindreEndSlut()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDate = new DateTime(2024, 4, 25);
        DateTime endDate = new DateTime(2024, 4, 22);

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, startDate, endDate);

        var createdDagligFast = service.GetDagligFaste().Last();

        Assert.IsTrue(createdDagligFast.startDen >= createdDagligFast.slutDen);
    }

    [TestMethod]
    public void TC2_OpretDagligFastSammeDag()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDate = new DateTime(2024, 4, 24);
        DateTime endDate = new DateTime(2024, 4, 24);

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, startDate, endDate);

        var createdDagligFast = service.GetDagligFaste().Last();

        Assert.IsTrue(createdDagligFast.startDen <= createdDagligFast.slutDen);
    }

    [TestMethod]
    public void TC3_OpretDagligFastEtÅrFraNu()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDate = new DateTime(2024, 4, 24);
        DateTime endDate = new DateTime(2025, 4, 24);

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, startDate, endDate);

        var createdDagligFast = service.GetDagligFaste().Last();

        Assert.IsTrue(createdDagligFast.startDen <= createdDagligFast.slutDen);
    }

    [TestMethod]
    public void TC4_OpretDagligFastEtÅrogendagmindreFraNu()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDate = new DateTime(2024, 4, 24);
        DateTime endDate = new DateTime(2025, 4, 23);

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, startDate, endDate);

        var createdDagligFast = service.GetDagligFaste().Last();

        Assert.IsTrue(createdDagligFast.startDen <= createdDagligFast.slutDen);
    }

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
