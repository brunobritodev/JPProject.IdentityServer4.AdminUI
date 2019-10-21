namespace Jp.Application.EventSourcedNormalizers
{
    public class EventHistoryData
    {
        public string Category { get; set; }
        public string Action { get; set; }
        public string Id { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
        public string Details { get; set; }
    }
}