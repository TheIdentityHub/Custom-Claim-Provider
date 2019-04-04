using System;
using U2UConsult.IdentityHub.Contracts.ClaimProviders;

namespace U2UConsult.DemoClaimProvider
{
    /// <summary>
    /// Custom claim provider manager factory.
    /// </summary>
    /// <seealso cref="U2UConsult.IdentityHub.Contracts.ClaimProviders.IClaimProviderManagerFactory" />
    [ClaimProviderManagerFactory]
    internal sealed class CustomClaimProviderManagerFactory : IClaimProviderManagerFactory
    {
        /// <summary>
        /// The custom claim provider description.
        /// </summary>
        internal static readonly string CustomClaimProviderDescription = "Custom Claim Provider Description";

        /// <summary>
        /// The custom claim provider display name.
        /// </summary>
        internal static readonly string CustomClaimProviderDisplayName = "Custom Claim Provider Display Name";

        /// <summary>
        /// The claim provider type identifier.
        /// </summary>
        /// <remarks>
        /// Change this value and use a new GUID.
        /// </remarks>
        internal static readonly Guid CustomClaimProviderTypeId = new Guid("{BA79A153-7BCA-48AE-8349-D45A1686ACAA}");

        /// <summary>
        /// Gets the claim provider type identifier.
        /// </summary>
        /// <value>
        /// The claim provider type identifier.
        /// </value>
        public Guid ClaimProviderTypeId => CustomClaimProviderTypeId;

        /// <summary>
        /// Gets the claim provider manager.
        /// </summary>
        /// <returns>A new instance of the claim provider manager.</returns>
        public IClaimProviderManager GetClaimProviderManager()
        {
            return new CustomClaimProviderManager();
        }
    }
}