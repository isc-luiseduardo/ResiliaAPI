using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Diagnostics;

namespace ResiliaAPI.Helpers
{
    public static class FirebaseHelper
    {
        /// <summary>
        /// Creates a new instance of the Firebase App and returns it, using google authentication from the json file at root directory
        /// </summary>
        /// <returns>A Firebase app instance.</returns>
        public static FirebaseApp CreateNewApp()
        {
            return FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json"))
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A boolean with the success of the action of sending the message through Firebase.</returns>
        public static async Task<string> BuildAndSendMessage(string messageTitle,
                                               string messageBody,
                                               string messageImageUrl,
                                               Dictionary<string, string> messageData,
                                               string messageToken,
                                               string topic)
        {
            try
            {
                //Create a new message structure from Firebase class.
                Message firebaseMessage = new Message()
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = messageTitle,
                        Body = messageBody,
                        ImageUrl = messageImageUrl
                    },
                    //Token = messageToken,
                    Data = messageData,
                    Topic = topic
                };

                Debug.WriteLine(firebaseMessage);  //Log the built messages

                //Send the message using Firebase default instance.
                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(firebaseMessage);

                Debug.WriteLine($"Message sent!", result);
                return result;
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
