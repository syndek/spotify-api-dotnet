using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.Web.Authorization.Flows;

namespace Spotify.Web.Authorization.Tests
{
    [TestClass]
    public class AuthorizationCodeFlowTests : Object
    {
        [TestMethod]
        public void CreateAuthorizationUrl_NoScopes()
        {
            const String AuthorizationUrl = "https://accounts.spotify.com/authorize" +
                "?response_type=code" +
                "&client_id=CLIENT_ID" +
                "&redirect_uri=REDIRECT_URI";

            Assert.AreEqual(
                expected: new(AuthorizationUrl),
                actual: AuthorizationCodeFlow.CreateAuthorizationUrl("CLIENT_ID", "REDIRECT_URI"));
        }

        [TestMethod]
        public void CreateAuthorizationUrl_SingleScope()
        {
            const String AuthorizationUrl = "https://accounts.spotify.com/authorize" +
                "?response_type=code" +
                "&client_id=CLIENT_ID" +
                "&redirect_uri=REDIRECT_URI" +
                "&scope=ugc-image-upload";

            Assert.AreEqual(
                expected: new(AuthorizationUrl),
                actual: AuthorizationCodeFlow.CreateAuthorizationUrl(
                    "CLIENT_ID",
                    "REDIRECT_URI",
                    scopes: AuthorizationScopes.UgcImageUpload));
        }

        [TestMethod]
        public void CreateAuthorizationUrl_MultipleScopes()
        {
            const String AuthorizationUrl = "https://accounts.spotify.com/authorize" +
                "?response_type=code" +
                "&client_id=CLIENT_ID" +
                "&redirect_uri=REDIRECT_URI" +
                "&scope=ugc-image-upload%20streaming%20playlist-read-collaborative";

            Assert.AreEqual(
                expected: new(AuthorizationUrl),
                actual: AuthorizationCodeFlow.CreateAuthorizationUrl(
                    "CLIENT_ID",
                    "REDIRECT_URI",
                    scopes: AuthorizationScopes.UgcImageUpload | AuthorizationScopes.Streaming | AuthorizationScopes.PlaylistReadCollaborative));
        }
    }
}