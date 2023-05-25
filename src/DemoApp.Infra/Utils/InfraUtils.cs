namespace DemoApp.Infra.Utils
{
    public static class InfraUtils
    {
        public static readonly string DefaultConnectionName = "DemoAppConnection";

        public static readonly string DefaultConnection =
            "Server=.\\SQLEXPRESS;Database=DemoAppDB;Trusted_Connection=True;";
    }
}
