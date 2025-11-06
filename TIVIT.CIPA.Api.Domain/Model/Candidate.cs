namespace TIVIT.CIPA.Api.Domain.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int? VoterId { get; set; }
        public int? SiteId { get; set; }
        public string CorporateId { get; set; }
        public string Name { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Area { get; set; }
        public string Department { get; set; }
        public string Site { get; set; }
        public byte[] PhotoBase64 { get; set; }
        public string PhotoURL { get; set; }
        public string PhotoMimeType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        // Navigation Properties
        public virtual Election Election { get; set; }
        public virtual Voter Voter { get; set; }
        public virtual Site SiteNavigation { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
