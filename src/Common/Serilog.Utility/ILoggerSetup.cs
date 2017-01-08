namespace Serilog.Utility
{
    public interface ILoggerSetup
    {
        void CustomInformation(string user = "", string request = "", string response = "", string enviornment = "", string host = "",
            string informationMessage = "");

        void CustomWarning(string user = "", string request = "", string response = "", string enviornment = "", string host = "",
            string warningMessage = "");

        void CustomError(string user = "", string request = "", string response = "", string enviornment = "", string host = "",
            string errorMessage = "");

        void CustomFatal(string user = "", string request = "", string response = "", string enviornment = "", string host = "",
            string fatalMessage = "");

        void CloseAndFlush();
    }
}