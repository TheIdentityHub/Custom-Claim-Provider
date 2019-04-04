using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using U2UConsult.Framework.Cryptography;
using U2UConsult.IdentityHub.ClaimProvider;
using U2UConsult.IdentityHub.Contracts.AccountProviders;
using U2UConsult.IdentityHub.Contracts.ClaimProviders;

namespace U2UConsult.DemoClaimProvider
{
    public sealed class CustomClaimProvider : ClaimProviderBase
    {
        /// <summary>
        /// Gets or sets the claim provider identifier.
        /// </summary>
        /// <value>
        /// The claim provider identifier.
        /// </value>
        public override int ClaimProviderId { get; set; }

        /// <summary>
        /// Gets or sets the claim provider manager.
        /// </summary>
        /// <value>
        /// The claim provider manager.
        /// </value>
        public override IClaimProviderManager ClaimProviderManager { get; set; }

        /// <summary>
        /// Gets the claim provider type identifier.
        /// </summary>
        /// <value>
        /// The claim provider type identifier.
        /// </value>
        /// <remarks>
        /// Must be a global unique value.
        /// </remarks>
        public override Guid ClaimProviderTypeId => CustomClaimProviderManagerFactory.CustomClaimProviderTypeId;

        /// <summary>
        /// Gets or sets the configuration settings.
        /// </summary>
        /// <value>
        /// The configuration settings.
        /// </value>
        public override ClaimProviderConfiguration ConfigurationSettings { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description shown in the administrative UI.
        /// </value>
        public override string Description => CustomClaimProviderManagerFactory.CustomClaimProviderDescription;

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name shown in the administrative UI.
        /// </value>
        public override string DisplayName => CustomClaimProviderManagerFactory.CustomClaimProviderDisplayName;

        /// <summary>
        /// Gets the name of the issuer.
        /// </summary>
        /// <value>
        /// The name of the issuer.
        /// </value>
        /// <remarks>
        /// Used to set the Original Issuer for the claims that are the result of this Claim Provider.
        /// </remarks>
        public override string IssuerName => CustomClaimProviderManager.GetIssuerName(new Identifier(ClaimProviderId).ObfuscatedValue.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Gets the provided claim types.
        /// </summary>
        /// <value>
        /// The claim types provided by this Claim Provider.
        /// </value>
        public override string[] ProvidedClaimTypes => ClaimProviderManager.ProvidedClaimTypes;

        /// <summary>
        /// Gets the list of claims based on the information provided (coming from the Account Provider).
        /// </summary>
        /// <param name="accountProvider">The account provider.</param>
        /// <param name="inputClaims">The input claims.</param>
        /// <param name="request">The request.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// List of claims.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// accountProvider
        /// or
        /// inputClaims
        /// </exception>
        public override Task<ICollection<Claim>> GetListOfClaimsAsync(IAccountProvider accountProvider, IEnumerable<Claim> inputClaims, System.Web.HttpRequestBase request, string state)
        {
            if (accountProvider == null)
            {
                throw new ArgumentNullException(nameof(accountProvider));
            }

            if (inputClaims == null)
            {
                throw new ArgumentNullException(nameof(inputClaims));
            }

            return Task.FromResult(CustomClaimProviderManager.CreateClaimSet(this, accountProvider, inputClaims, request, state));
        }
    }
}