namespace API_Business.Request
{
    public class GameInfo
    {
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public int playtime_deck_forever { get; set; }
        public long rtime_last_played { get; set; }
        public int playtime_disconnected { get; set; }
    }

    public class Response
    {
        public int game_count { get; set; }
        public List<GameInfo> games { get; set; }
    }

    public class Root
    {
        public Response response { get; set; }
    }
}
