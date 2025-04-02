namespace ApiTP1MethodeDeDev.Dtos
{
    public class JobRequest
    {
        public string InstitutionName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Decimal MentualSalary { get; set; }
    }
}
