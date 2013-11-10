namespace Tff.ClaimAdjudicator
open System

type Claim(claimId: int, billedDate: DateTime, billedAmount: float) = class
    member this.ClaimId = claimId
    member this.BilledDate = billedDate
    member this.BilledAmount = billedAmount
end
 
type ClaimCalculator() =
    member this.AdjustClaim(claim: Claim) =
        claim.BilledAmount * 0.5
