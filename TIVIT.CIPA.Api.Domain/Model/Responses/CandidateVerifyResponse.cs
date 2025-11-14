using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIVIT.CIPA.Api.Domain.Model.Responses
{
    public class CandidateVerifyResponse
    {
        public int VoterId { get; set; }
        public string CorporateId { get; set; }
        public string Name { get; set; }
        public string SiteName { get; set; }
        public string Department { get; set; }
        public string CorporateEmail { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
