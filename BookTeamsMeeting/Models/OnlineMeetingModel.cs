namespace BookTeamsMeeting.Models
{
    public class OnlineMeetingModel
    {
        public string Subject { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string UrlMeeting { get; set; }
        public string authenCode { get; set; }
    }
}
