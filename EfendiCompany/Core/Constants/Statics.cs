using Core.Enums;

namespace Core.Constants;

public static class Statics
{
    public static bool ProductionMode { get { return true; } }
    public static int HourDifference { get { return ProductionMode ? 4 : 0; } }
    public static OSType OSType { get { return ProductionMode ? OSType.Linux : OSType.Windows; } }
    public static string BaseUrl { get { return ProductionMode ? "https://efendigroup-napi.azurewebsites.net" : "https://localhost:5001"; } }
    public static string BaseUIUrl { get { return ProductionMode ? "https://efendigroup-nui.azurewebsites.net" : "http://localhost:3000"; } }
    public static string TgBotKey { get { return ProductionMode ? "6478610429:AAGYsUwCBNcZ3uGMwHEQvcawFT_61wMpz8U" : "5407402992:AAF3Uxx5_Od3KlZw_y4gk2U7wK6a96F6F1s"; } }
    public static string ConnStr { get; set; } = Statics.ConnStrDemo;
    public static string ConnStrPattern { get { return ProductionMode ? "Data Source=./wwwroot/AppData/###.db;Mode=ReadWriteCreate;Cache=Shared;" : "Data Source=./wwwroot/AppData/AppData/###.db;Mode=ReadWriteCreate;Cache=Shared;"; } }
    //public static string ConnStrLocal { get { return "Data Source=.\\SQLEXPRESS;Initial Catalog=SteamPS;Integrated Security=True;Connect Timeout=3000;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True;"; } }
    public static string ConnStrDemo { get { return ProductionMode ? "Data Source=./wwwroot/AppData/EfendiDemo.db;Mode=ReadWriteCreate;Cache=Shared;" : "Data Source=./wwwroot/AppData/EfendiDemo.db;Mode=ReadWriteCreate;Cache=Shared;"; } }
    //public static string LogDatabaseConnStr { get { return ProductionMode ? "Data Source=./wwwroot/AppData/LogDatabase.db;Mode=ReadWriteCreate;Cache=Shared;" : "Data Source=C:\\Users\\user\\Desktop\\SteamPSProject\\SteamPS\\PlayStation\\Api\\wwwroot\\AppData\\LogDatabase.db;Mode=ReadWriteCreate;Cache=Shared;"; } }
    public static string LogDatabaseTableName { get { return "Logs"; } }
    public static string SenderEmail { get; set; } = "steamplaystation2022@gmail.com";
    public static string SenderPassword { get; set; } = "qghl xufv qpnr ocpv";

}
