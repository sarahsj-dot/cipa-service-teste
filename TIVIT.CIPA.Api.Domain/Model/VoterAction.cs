namespace TIVIT.CIPA.Api.Domain.Model
{
    public class VoterAction
    {
        public int VoterId { get; set; }
        public int ActionId { get; set; }


        public virtual Voter Voter { get; set; }
        public virtual Action Action { get; set; }
    }
}
