using System.Diagnostics;

namespace ResiliaAPI.Helpers
{
    public static class GeneralHelpers
    {
        /// <summary>
        /// Simply generates a wait for simulating time lapses between push notificationss
        /// </summary>
        public static void WaitInterval()
        {
            const int minWait = 3;
            const int maxWait = 10;
            //Interval time between notifications
            int sleepTime = Random.Shared.Next(minWait, maxWait);
            Debug.WriteLine($"Waiting for {sleepTime} seconds...");
            Thread.Sleep(sleepTime * 1000);
            return;
        }
    }
}
