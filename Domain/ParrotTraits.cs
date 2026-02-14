using System.ComponentModel.DataAnnotations;

namespace ParrotShopBackend.Domain;



public class ParrotTraits
{
    [Key]
    public long Id { get; set; }
    public long ParrotId{set;get;}
    public Size? Size {get; set;}
    public byte? NoiseLevel {set; get;}
    public byte? Sociability {set;get;}
    public byte? Trainability {set;get;}
    public byte? Talkativeness{set;get;}
    public byte? ChewingRisk {set;get;}
    public byte? CareComplexity {set;get;}
    public byte? LifespanMin{set;get;}
    public byte? LifespanMax{set;get;}
    public KidSafety? KidSafety {set;get;}

}

public enum Size
{
    Small = 1,
    Medium = 2,
    Large = 3
}

public enum KidSafety
{
    yes,
    no,
    cautious
}
