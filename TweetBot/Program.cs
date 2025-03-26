using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using System.Net.Http;

class Program
{
    static async Task Main()
    {
        // ✅  Consumer API Key & API Secret 
        var appClient = new TwitterClient("AJcH5OVobLUX2ubjrCjoEYkc6", "anwshPXsbudaR06mYWbEe9PnF2b60TUx4Q28bo2RAtbyVYIQwG");

        // Step 1: Get request token and redirect user to authorize
        var authRequest = await appClient.Auth.RequestAuthenticationUrlAsync("https://127.0.0.1:8000");

        Console.WriteLine("👉 Open this URL clearly in your browser to authorize your app:");
        Console.WriteLine(authRequest.AuthorizationURL);

        // Step 2: User authorizes your app, then Twitter redirects back with oauth_verifier
        Console.Write("👉 Enter OAuth verifier clearly provided by Twitter: ");
        var verifierCode = Console.ReadLine();

        // Step 3: Exchange OAuth verifier for user-specific Access Tokens
        var userCredentials = await appClient.Auth.RequestCredentialsFromVerifierCodeAsync(verifierCode, authRequest);

        if (userCredentials == null)
        {
            Console.WriteLine("❌ Authentication failed. Couldn't get user credentials.");
            return;
        }

        var userClient = new TwitterClient(userCredentials);

        var user = await userClient.Users.GetAuthenticatedUserAsync();
        Console.WriteLine($"✅ Authenticated successfully as {user.Name} (@{user.ScreenName})");

        // Step 4: Tweet on behalf of authenticated user (clearly fixed!)
        var tweetResponse = await userClient.Execute.AdvanceRequestAsync(request =>
        {
            request.Query.Url = "https://api.twitter.com/2/tweets";
            request.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
            request.Query.HttpContent = new StringContent(
                "{\"text\": \"Hello World from OAuth 1.0a 3-legged flow! 🚀 #TwitterAPI #CSharp\"}",
                System.Text.Encoding.UTF8,
                "application/json"
            );
        });

        if (tweetResponse.Response.IsSuccessStatusCode)
        {
            Console.WriteLine("✅ Tweet posted successfully!");
            Console.WriteLine(tweetResponse.Content);
        }
        else
        {
            Console.WriteLine("❌ Failed to post tweet:");
            Console.WriteLine(tweetResponse.Content);
        }
    }
}