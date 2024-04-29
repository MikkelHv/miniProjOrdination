namespace shared.Model;

public class DagligSkæv : Ordination {
    public List<Dosis> doser { get; set; } = new List<Dosis>();

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
	}

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, Dosis[] doser) : base(laegemiddel, startDen, slutDen) {
        this.doser = doser.ToList();
    }    

    public DagligSkæv() : base(null!, new DateTime(), new DateTime()) {
    }

	public void opretDosis(DateTime tid, double antal) {
        doser.Add(new Dosis(tid, antal));
    }

	public override double samletDosis() {
		return base.antalDage() * doegnDosis();
	}

	public override double doegnDosis() {
		//Mikkel
		
		double døgnDosisSkæv = 0;
		foreach (Dosis dosis in doser) 
		{
			døgnDosisSkæv += dosis.antal;
		}

        return døgnDosisSkæv;
	}

	public override String getType() {
		return "DagligSkæv";
	}

	// Ny konstruktør for to DateTime argumenter
	// Til at køre test som designet
    public DagligSkæv(DateTime startDen, DateTime slutDen) : base(null!, startDen, slutDen) {
    }
}
