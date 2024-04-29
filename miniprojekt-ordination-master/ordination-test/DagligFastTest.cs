using shared.Model;

namespace ordination_test;

[TestClass]
public class DagligFastTeeest
{
    Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");

    // TC1: Funktionalitet af døgndosis med positive doser
    [TestMethod]
    //Test DognDosis Med Positive Doser
    public void TC1_PositiveDoser()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 1, 1, 1, 1);
        double forventetDosis = 4; // 1 morgen + 1 middag + 1 aften + 1 nat
        Assert.AreEqual(forventetDosis, ordination.doegnDosis());
    }

    // TC2: Funktionalitet af døgndosis med 0 doser
    [TestMethod]
    //Test DognDosis Med Nul Doser
    public void TC2_DoegnDosisNulVærdi()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 0, 0, 0, 0);
        Assert.AreEqual(0, ordination.doegnDosis());
    }

    // TC3: Funktionalitet af døgndosis med negative doser
    [TestMethod]
    //Test DognDosis Med Negative Doser
    [ExpectedException(typeof(ArgumentException))]
    public void TC3_NegativeDoser_()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, -1, 0, 0, 0);
        ordination.doegnDosis();
    }

    // TC4: Funktionalitet af døgndosis med positive doser kun om natten
    [TestMethod]
    //Test DognDosis Med Nat Doser
    public void TC4_NatDoser()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 0, 0, 0, 7);
        Assert.AreEqual(7, ordination.doegnDosis());
    }

    // TC5: Funktionalitet af døgndosis med ekstreme negative doser om natten
    [TestMethod]
    //Test DognDosis Med Ekstreme Negative Nat Doser
    [ExpectedException(typeof(ArgumentException))]
    public void TC5__NegativeDoser_Exception()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 0, 0, 0, -10);
        ordination.doegnDosis();
    }

    // TC6: Funktionalitet af døgndosis med ekstreme positive doser om natten
    [TestMethod]
    //Test DognDosis Med Ekstreme Positive Nat Doser
    public void TC6_EkstremDoser()
    {
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 0, 0, 0, 100);
        Assert.AreEqual(100, ordination.doegnDosis());
    }
}
