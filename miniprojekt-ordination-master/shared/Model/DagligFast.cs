using System;

namespace shared.Model;
using static shared.Util;

public class DagligFast : Ordination {
    public Dosis MorgenDosis { get; set; } = new Dosis();
    public Dosis MiddagDosis { get; set; } = new Dosis();
    public Dosis AftenDosis { get; set; } = new Dosis();
    public Dosis NatDosis { get; set; } = new Dosis();

    public DagligFast(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, 
        double morgenAntal, double middagAntal, double aftenAntal, double natAntal) 
        : base(laegemiddel, startDen, slutDen) {
        // Victor
        // IF-statment der sikrer at dosis ikke er negativ
        if (morgenAntal < 0 || middagAntal < 0 || aftenAntal < 0 || natAntal < 0) {
            throw new ArgumentException("Antal kan ikke være negativt");
        }

        MorgenDosis = new Dosis(CreateTimeOnly(6, 0, 0), morgenAntal);
        MiddagDosis = new Dosis(CreateTimeOnly(12, 0, 0), middagAntal);
        AftenDosis = new Dosis(CreateTimeOnly(18, 0, 0), aftenAntal);
        NatDosis = new Dosis(CreateTimeOnly(23, 59, 0), natAntal);
    }

    // Empty constructor for scenarios where no initial data is provided
    public DagligFast() : base(null!, new DateTime(), new DateTime()) {
    }

    public override double samletDosis() {
        return base.antalDage() * doegnDosis();
    }

    public override double doegnDosis() {
        double totalDosisFast = 0; 
        foreach (Dosis dosis in getDoser()) {
            totalDosisFast += dosis.antal;
        }
        return totalDosisFast;
    }

    public Dosis[] getDoser() {
        Dosis[] doser = {MorgenDosis, MiddagDosis, AftenDosis, NatDosis};
        return doser;
    }

    public override String getType() {
        return "DagligFast";
    }
}