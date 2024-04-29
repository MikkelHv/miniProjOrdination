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

    // Exception Test PN anvend Ordination
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TC_KodenSmiderEnException_Pn_AnvendOrdination()
    {
        //Mikkel
        //
        //
        // opretter en id, der ikke findes i databasen
        int ikkeEksisterendeId = -1;
        // opretter en vilkårlg dato
        Dato testDato = new Dato { dato = DateTime.Now };
        // Test på at der ved kald efter denne id vil blive kastet en exeption
        service.AnvendOrdination(ikkeEksisterendeId, testDato);
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
    public void TC5_AnbefaletDosis()
    {
        // Finder patient og lægemiddel i databasen
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        // Opret forventet anbefalet dosis
        double foreventetAnbefaletDosis = 9.51; // udregnet fra vægten * antalEnhederPrKgPrDoegn
        // Test
        Assert.AreEqual(foreventetAnbefaletDosis, service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId));
    }
}
