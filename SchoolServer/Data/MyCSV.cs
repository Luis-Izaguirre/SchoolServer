namespace SchoolServer.Data
{
    public class MyCSV
    {
        public string studentName { get; set; } = null!;
        public required string studentEmail { get; set; }  = null!;
        public string major { get; set; } = null!;
        public int studentYear { get; set; }
        public string courseName { get; set; } = null!;
        public string description { get; set; } = null!;
        public string instructorName { get; set; } = null!;
        public decimal? population { get; set; }
        public int id { get; set; }
    }
}
