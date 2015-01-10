using System;
using System.Diagnostics;
using Tweetinvi;
using Tweetinvi.Core.Interfaces.oAuth;

namespace WinPhoneTwt.BusinessLogic.Helpers
{
	public class LoginPageHelper
	{
		public static void SetAccessTokens()
		{
//			TwitterCredentials.SetCredentials(Constants.AccessToken, Constants.AccessTokenSecret, Constants.ConsumerKey,
//				Constants.ConsumerSecret);
//			TweetAsync.PublishTweet("I love tweetinvi :D");
			var authCredentials = CredentialsCreator_WithCaptcha_StepByStep(Constants.ConsumerKey, Constants.ConsumerSecret);
			TwitterCredentials.SetCredentials(authCredentials);
		}

		private static IOAuthCredentials CredentialsCreator_WithCaptcha_StepByStep(string consumerKey, string consumerSecret)
		{
			var applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(consumerKey, consumerSecret);
			var url = CredentialsCreator.GetAuthorizationURL(applicationCredentials);
			var uri = new Uri(url);
			Windows.System.Launcher.LaunchUriAsync(uri);

			var captcha = "";

			var newCredentials = CredentialsCreator.GetCredentialsFromVerifierCode(captcha, applicationCredentials);
			Debug.WriteLine("Access Token = {0}", newCredentials.AccessToken);
			Debug.WriteLine("Access Token Secret = {0}", newCredentials.AccessTokenSecret);

			return newCredentials;
		}
	}
}
