using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Cors;
using ResiliaAPI.Helpers;
using ResiliaAPI.Models;

namespace ResiliaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IConfiguration _configuration;
        private User currentUser;
        private string CloudMessagingToken = "";

        public NotificationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [EnableCors("AllowAll")]
        [HttpPost(Name = "Initialize")]
        public ActionResult Post([FromBody]string firebaseToken)
        {
            //Setup the name on the Service and start sending 
            try
            {
                currentUser = DataMockerHelper.GenerateFakeUser();
                CloudMessagingToken = firebaseToken;
                StartPushNotifications();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        private async Task StartPushNotifications()
        {
            HttpClient client = new HttpClient();

            try
            {
                //Create new Firebase app with login with Google
                FirebaseApp? defaultApp = FirebaseHelper.CreateNewApp();
                Debug.WriteLine($"App Logged: [{defaultApp.Name}]");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login to Firebase failed!", ex);
            }


            //Iterations of push messages to be sent / Notifications sent
            int pushQty = Random.Shared.Next(10, 20);

            for (int x = 0; x < pushQty; x++)
            {
                //Wait for a while, for not sending all notifications at same time.
                GeneralHelpers.WaitInterval();

                (string, string) notificationTitles = DataMockerHelper.GetRandomNotificationTitles();
                (string, string) notificationDates = DataMockerHelper.GetCreatedAndReceivedDates();

                //Generate structure and send notifications
                var result = await FirebaseHelper.BuildAndSendMessage(notificationTitles.Item2,
                                                   DataMockerHelper.GenerateRandomContent(client),
                                                   DataMockerHelper.GetRandomImageUrl(200),
                                                   new Dictionary<string, string>()
                                                   {
                                                        { "FullName", DataMockerHelper.GetRandomFullName() },
                                                        { "UserID", DataMockerHelper.GenerateFakeUserID() },
                                                        { "CreatedAt", notificationDates.Item1 },
                                                        { "ReceivedAt", notificationDates.Item2 },
                                                        { "RecipentName", currentUser.Name },
                                                        { "RecipentEmail", currentUser.Email },
                                                        { "Type", notificationTitles.Item1 }
                                                   },
                                                   CloudMessagingToken,
                                                   "");
            }
        }
    }
}
