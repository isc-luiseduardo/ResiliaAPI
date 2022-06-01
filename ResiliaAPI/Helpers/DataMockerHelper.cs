using ResiliaAPI.Models;
using System.Text;

namespace ResiliaAPI.Helpers
{
    public static class DataMockerHelper
    {
        /// <summary>
        /// Generates a random integer to fetch a random value from the prefilled lists of this class.
        /// </summary>
        /// <param name="min">The minimum value to be generated (Integer 32-bit)</param>
        /// <param name="max">The maximum value to be generated (Integer 32-bit)</param>
        /// <returns>A random integer between the min and max values.</returns>
        private static int GetRandomNumber(int min, int max)
        {
            return Random.Shared.Next(min, max);
        }

        /// <summary>
        /// Generate a Random notification type, including its title with a tuple.
        /// </summary>
        /// <returns>A tuple with notification with structure (type, title)</returns>
        public static (string, string) GetRandomNotificationTitles()
        {
            const int localMin = 0;
            const int localMax = 7;

            //Possible values for type
            (string, string)[] types = new (string, string)[]
            {
                ("Message", "You received a message"),  //New message received within the platform
                ("Grantees", "Good news from your grantees"), //Grantees-related updates (donations, new grantee, etc) 
                ("Update", "Don't miss this one!"), //Maybe some updates related to your following preferences
                ("IRS", "Status update from your tax request form"), //Updates related to the tax-exempt process. 
                ("Reminder","Don't forget this"), //Programed remainders of certain events or schedules
                ("Event", "You have an upcomming event soon"), //Events that might interest you explicitly or implicitly
                ("Feed", "Check out the updates of your network"), //Periodical digest of your network
                ("System", "We are improving, learn more here.") //Important updates about the web platform
            };

            return types.ElementAt(GetRandomNumber(localMin, localMax));
        }

        /// <summary>
        /// Get a random full name generated for the emmiter of the notification.
        /// Applies only for certain types of notifications.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomFullName()
        {
            //15 different Names.
            string[] firstNames = new[]
            {
                "Louis", 
                "Jane", 
                "Michael", 
                "Sophie", 
                "Arthur",
                "Minerva", 
                "Mohammed", 
                "Julia", 
                "Akira", 
                "Marjorie",
                "Matt",
                "Olga", 
                "Montgomery", 
                "Helga"
            };
            //15 different last names.
            string[] lastNames = new[]
            {
                "Smith", 
                "Juarez", 
                "Johnson", 
                "Olsen", 
                "Ali", 
                "Wang", 
                "Takahashi", 
                "Nunez", 
                "McCarthy", 
                "Economos", 
                "Harper", 
                "Groening", 
                "Simpson", 
                "Smithers", 
                "Carlson"
            };
            const int localMin = 0;
            const int localMax = 14;

           return $"{firstNames.ElementAt(GetRandomNumber(localMin, localMax))} {lastNames.ElementAt(GetRandomNumber(localMin, localMax))}";
        }

        /// <summary>
        /// Generates a Lorem Picsum Photo URL randomly with the resolution parameter for sizing.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomImageUrl(int resolutionImage)
        {
            return $"https://picsum.photos/{resolutionImage}/{resolutionImage}";
        }

        /// <summary>
        /// Generates random content for the body of the notification. Lorem Ipsum variation here from an online generator.
        /// </summary>
        /// <param name="client">Gets the Http client in order to not be initialized each call.</param>
        /// <returns>A string containing the </returns>
        public static string GenerateRandomContent(HttpClient client)
        {
            const int paragraphsQuantity = 3;

            return (client.GetAsync($"https://baconipsum.com/api/?type=meat-and-filler&paras={paragraphsQuantity}&start-with-lorem=1&format=text")
                   .Result.Content.ReadAsStringAsync()) // Gets the result and read the string
                   .Result;                             // Gets the actual string after reading is done 
        }

        /// <summary>
        /// Generates a random tuple of values for Created At and Generated At date values in general format as strings.
        /// </summary>
        /// <returns>A tuple containing the two dates as string. (CreatedAt, ReceivedAt)</returns>
        public static (string, string) GetCreatedAndReceivedDates()
        {
            string created = DateTime.Now
                            .AddMinutes((Random.Shared.NextDouble() * Random.Shared.Next(0, 10000)) * -1) //Randomly remove some minutes, so it seems to be generated early.
                            .ToString("G");       //Parse as general long Date - Time UI friendly format.

            string received = DateTime.Now
                                .ToString("G");   //Parse as general long Date - Time UI friendly format.

            return (created, received);
        }

        /// <summary>
        /// Creates a simple random Guid.
        /// </summary>
        /// <returns>A new standard GUID</returns>
        public static string GenerateFakeUserID()
        {
            return Guid.NewGuid().ToString();
        }

        public static User GenerateFakeUser()
        {
            string name = GetRandomFullName();
            return new User(name, $"{name.Replace(" ", "")}@gmail.com");
        }
    }
}
