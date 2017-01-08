using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Serilog.Utility
{
    public class LoggerSetup : ILoggerSetup
    {
        // http://blachniet.com/blog/serilog-good-habits/
        public LoggerSetup()
        {
            var connectionString = ConfigurationManager.AppSettings["MasterDB"];
            var tableName = ConfigurationManager.AppSettings["TableName"];

            var columnOptions = ColumnOptions();

            columnOptions.Store.Add(StandardColumn.LogEvent);

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                                                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                                   .MinimumLevel.Override("System", LogEventLevel.Error)
                                                   .WriteTo.MSSqlServer(connectionString, tableName, LogEventLevel.Information,
                                                   columnOptions: columnOptions).CreateLogger();
        }

        private static ColumnOptions ColumnOptions()
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof(string), ColumnName = CustomColumn.User.ToString()},
                    new DataColumn {DataType = typeof(string), ColumnName = CustomColumn.Host.ToString()},
                    new DataColumn {DataType = typeof(string), ColumnName = CustomColumn.Enviornment.ToString()},
                    new DataColumn {DataType = typeof(string), ColumnName = CustomColumn.Request.ToString()},
                    new DataColumn {DataType = typeof(string), ColumnName = CustomColumn.Response.ToString()},
                }
            };
            return columnOptions;
        }

        public void CustomInformation(string user = "", string request = "", string response = "", string enviornment = "", string host = "", string informationMessage = "")
        {
            BaseLog(user, request, response, enviornment, host).Information(informationMessage);
        }

        public void CustomWarning(string user = "", string request = "", string response = "", string enviornment = "", string host = "", string warningMessage = "")
        {
            BaseLog(user, request, response, enviornment, host).Warning(warningMessage);
        }

        public void CustomError(string user = "", string request = "", string response = "", string enviornment = "", string host = "", string errorMessage = "")
        {
            BaseLog(user, request, response, enviornment, host).Error(errorMessage);
        }

        public void CustomFatal(string user = "", string request = "", string response = "", string enviornment = "", string host = "", string fatalMessage = "")
        {
            BaseLog(user, request, response, enviornment, host).Fatal(fatalMessage);
        }

        private static ILogger BaseLog(string user, string request, string response, string enviornment, string host)
        {
            return Log.Logger.ForContext(CustomColumn.User.ToString(), user)
                .ForContext(CustomColumn.Request.ToString(), request)
                 .ForContext(CustomColumn.Response.ToString(), response)
                .ForContext(CustomColumn.Enviornment.ToString(), enviornment)
                .ForContext(CustomColumn.Host.ToString(), host);

            
        }

        public void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }
    }
}