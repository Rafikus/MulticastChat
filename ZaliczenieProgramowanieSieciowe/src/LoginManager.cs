using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Threading;

namespace ZaliczenieProgramowanieSieciowe
{
    static class LoginManager
    {
        public static bool Flag = true;
        public static bool VerifyUsername(string username)
        {
            Flag = true;
            var thread = new Thread(() =>
            {
                ChatManager.Sender.Send($"NICK {username}"); 
                Thread.Sleep(1000);
            });
            thread.Start();
            thread.Join();
            return Flag;
        }
    }
}
