
namespace TIVIT.CIPA.Api.Domain.Model.Responses
{
    public class VoterDetailResponse
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int CompanyId { get; set; }
        public int? ProfileId { get; set; }
        public string Registry { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string CorporateEmail { get; set; }
        public string ContactEmail { get; set; }
        public string CorporatePhone { get; set; }
        public string ContactPhone { get; set; }
        public string Site { get; set; }
        public string Department { get; set; }
        public string Token { get; set; }
        public bool HasVoted { get; set; }
        public string ExclusionReason { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public string ExcludedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
