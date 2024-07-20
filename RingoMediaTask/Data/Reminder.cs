namespace RingoMediaTask.Data
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsSent { get; set; } // To track if the reminder email has been sent
    }


}
