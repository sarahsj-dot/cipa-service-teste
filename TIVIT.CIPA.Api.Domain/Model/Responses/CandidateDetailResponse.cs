namespace TIVIT.CIPA.Api.Domain.Model.Responses
{
    public class CandidateDetailResponse
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
        public string PhotoBase64 { get; set; }
        public string PhotoMimeType { get; set; }
        public bool IsActive { get; set; }
        public SiteResponse? SiteNavigation { get; set; }
    }
}
