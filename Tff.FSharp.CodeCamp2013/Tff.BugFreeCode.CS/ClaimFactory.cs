using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tff.BugFreeCode.CS
{
    public class ClaimFactory
    {
        List<Claim> _claims = null;

        public ClaimFactory(List<Claim> claims)
        {
            _claims = claims;
        }

        public List<Claim> ProcessSmallClaims()
        {
            List<Claim> goodClaims = _claims;
            goodClaims.RemoveAll(claim => claim.IsASuspectClaim);
            return goodClaims;
        }

        public List<Claim> ProcessOutlyerClaims()
        {
            return null;
        }
    }
}
