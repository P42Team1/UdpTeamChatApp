using ChatLibrary.Data;

namespace UdpTeamChatApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ChatContextFactory chatContextFactory = new ChatContextFactory();
            ChatContext context = chatContextFactory.CreateDbContext(Array.Empty<string>());

            Service service = new Service(context);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(service));
        }
    }
}