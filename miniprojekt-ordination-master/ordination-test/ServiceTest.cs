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
}