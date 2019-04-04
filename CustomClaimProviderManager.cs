using System;
using System.Collections.Generic;
using System.Security.Claims;
using U2UConsult.IdentityHub.ClaimProvider;
using U2UConsult.IdentityHub.Contracts.AccountProviders;

namespace U2UConsult.DemoClaimProvider
{
    internal sealed class CustomClaimProviderManager : ClaimProviderManager<CustomClaimProviderConfiguration, CustomClaimProvider>
    {
        /// <summary>
        /// The issuer name prefix.
        /// </summary>
        internal const string IssuerNamePrefix = "http://customclaimprovider/";

        /// <summary>
        /// Gets the display name of the claim provider.
        /// </summary>
        /// <value>
        /// The display name of the claim provider.
        /// </value>
        public override string ClaimProviderDisplayName => CustomClaimProviderManagerFactory.CustomClaimProviderDisplayName;

        /// <summary>
        /// Gets a value indicating whether the claim provider is static.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the claim provider is static; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Always true for now. For future use.
        /// </remarks>
        public override bool ClaimProviderIsStatic => true;

        /// <summary>
        /// Gets a value indicating whether the claim provider is unique.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the claim provider is unique; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Always true for now. For future use.
        /// </remarks>
        public override bool ClaimProviderIsUnique => true;

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
        /// Gets the provided claim types.
        /// </summary>
        /// <value>
        /// The provided claim types.
        /// </value>
        /// <value>
        /// The claim types provided by this Claim Provider.
        /// </value>
        public override string[] ProvidedClaimTypes => new string[2] { IssuerNamePrefix + "customclaim", ClaimTypes.Role };

        /// <summary>
        /// Gets the claim provider configuration.
        /// </summary>
        /// <param name="claimProviderId">The claim provider identifier.</param>
        /// <returns>
        /// The configuration.
        /// </returns>
        /// <remarks>
        /// Use this method to modify the configuration coming from the database, before it is used.
        /// </remarks>
        public override CustomClaimProviderConfiguration GetClaimProviderConfiguration(CustomClaimProviderConfiguration storedConfiguration)
        {
            return base.GetClaimProviderConfiguration(storedConfiguration);
        }

        /// <summary>
        /// Validates the claim provider configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns><see cref="System.Array"/> of <see cref="System.String"/> containing validation errors.</returns>
        /// <remarks>
        /// This method can be used to validate and modify the configuration before it is stored in the database.
        /// This method is called when a new Claim Provider instance is created.
        /// </remarks>
        public override string[] ValidateCreateClaimProviderConfiguration(CustomClaimProviderConfiguration configuration)
        {
            return new string[0];
        }

        /// <summary>
        /// Validates the delete claim provider configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns><see cref="System.Array"/> of <see cref="System.String"/> containing validation errors.</returns>
        /// <remarks>
        /// This method can be used to validate the deletion (or perform custom clean up) of a Claim Povider instance before the deletion is executed in the database.
        /// This method is called when a Claim Provider instance is being deleted.
        /// </remarks>
        public override string[] ValidateDeleteClaimProviderConfiguration(CustomClaimProviderConfiguration configuration)
        {
            return new string[0];
        }

        /// <summary>
        /// Validates the claim provider configuration.
        /// </summary>
        /// <param name="oldConfiguration">The old configuration.</param>
        /// <param name="newConfiguration">The new configuration.</param>
        /// <returns><see cref="System.Array"/> of <see cref="System.String"/> containing validation errors.</returns>
        /// <returns><see cref="System.Array"/> of <see cref="System.String"/> containing validation errors.</returns>
        /// This method can be used to validate and modify the configuration before it is stored in the database.
        /// This method is called when a Claim Provider instance is updated.
        /// </remarks>
        public override string[] ValidateUpdateClaimProviderConfiguration(CustomClaimProviderConfiguration oldConfiguration, CustomClaimProviderConfiguration newConfiguration)
        {
            return new string[0];
        }

        /// <summary>
        /// Creates the claim set.
        /// </summary>
        /// <param name="claimProvider">The claim provider.</param>
        /// <param name="accountProvider">The account provider.</param>
        /// <param name="inputClaims">The input claims.</param>
        /// <param name="request">The request.</param>
        /// <param name="state">The state.</param>
        /// <returns>
        /// The claims for the user based on the provided information.
        /// </returns>
        internal static ICollection<Claim> CreateClaimSet(CustomClaimProvider claimProvider, IAccountProvider accountProvider, IEnumerable<Claim> inputClaims, System.Web.HttpRequestBase request, string state)
        {
            var claims = new List<Claim>(1)
            {
                CreateClaimProviderClaim(IssuerNamePrefix + "customclaim", "custom claim value", claimProvider.IssuerName),
                CreateClaimProviderClaim(ClaimTypes.Role, "TopGroupOne", claimProvider.IssuerName),
                CreateClaimProviderClaim(ClaimTypes.Role, "NonExisting", claimProvider.IssuerName),
                CreateClaimProviderClaim(ClaimTypes.Role, "Domain Users", claimProvider.IssuerName)
            };

            for (int i = 0; i < 10; i++)
            {
                claims.Add(CreateClaimProviderClaim(IssuerNamePrefix + "customclaim", "custom claim value " + i.ToString(), claimProvider.IssuerName));
            }

            return claims;
        }

        /// <summary>
        /// Gets the name of the issuer.
        /// </summary>
        /// <param name="claimProviderId">The claim provider identifier.</param>
        /// <returns>
        /// The name of the issuer.
        /// </returns>
        internal static string GetIssuerName(string claimProviderId)
        {
            return IssuerNamePrefix + claimProviderId;
        }
    }
}