namespace API_Business.Request
{
    public class Apps
    {
        public int appid { get; set; }
        public string? name { get; set; }
    }
    public class Applist
    {
        public IList<Apps>? apps { get; set; }

    }
    public class Application
    {
        public Applist? applist { get; set; }
    }

}
