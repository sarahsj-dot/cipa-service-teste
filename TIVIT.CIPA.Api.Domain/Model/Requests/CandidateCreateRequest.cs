namespace TIVIT.CIPA.Api.Domain.Model.Requests
{
    public class CandidateCreateRequest
    {
        public int ElectionId { get; set; }
        public string CorporateId { get; set; }
        public string Name { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Area { get; set; }
        public string Department { get; set; }
        public int? SiteId { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
