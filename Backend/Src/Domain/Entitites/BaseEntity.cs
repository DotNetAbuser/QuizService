namespace Domain.Entities;

public class BaseEntity<Tid> 
{
    public Tid Id { get; set;}
    public DateTime Created { get; set; } 
    public DateTime? Updated { get; set; }
}
