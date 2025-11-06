namespace TIVIT.CIPA.Api.Domain.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string LegalName { get; set; }
        public string ProtheusCode { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Election> Elections { get; set; }
        public virtual ICollection<Voter> Voters { get; set; }
    }
}
