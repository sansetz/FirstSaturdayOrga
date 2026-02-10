namespace FirstSaturdayOrga.Data {
    public sealed record AllFsJsonRecord {
        public int Year { get; set; }
        public int Month { get; set; }
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public int? AgentsEnl { get; set; }
        public int? AgentsRes { get; set; }
        public long? ApEnl { get; set; }
        public long? ApRes { get; set; }
    }
}
