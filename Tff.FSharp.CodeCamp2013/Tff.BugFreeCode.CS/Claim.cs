using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tff.BugFreeCode.CS
{
    public class Claim
    {
        public Claim(Int32 claimId, Int32 providerId, Int32 memberId, DateTime receviedDate, Double billedAmount)
        {
            this.ClaimId = claimId;
            this.ProviderId = providerId;
            this.MemberId = memberId;
            this.ReceviedDate = receviedDate;
            this.BilledAmount = billedAmount;
        }

        public Int32 ClaimId { get; set; }
        public Int32 ProviderId { get; set; }
        public Int32 MemberId { get; set; }
        public DateTime ReceviedDate { get; set; }
        public Double BilledAmount { get; set; }
        public Boolean IsASuspectClaim
        {
            get
            {
                if (BilledAmount > 500)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
