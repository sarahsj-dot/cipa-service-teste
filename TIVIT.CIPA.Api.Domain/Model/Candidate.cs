namespace TIVIT.CIPA.Api.Domain.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public string CorporateId { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public int SiteId { get; set; }
        public byte[] PhotoBase64 { get; set; }
        public string PhotoMimeType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public Site Site { get; set; }
    }
}
