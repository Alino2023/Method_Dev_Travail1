namespace ApiTP1MethodeDeDev.Dtos
{
    public class ActifRequest
    {
        public int id { get; set; }
        public decimal Valeur { get; set; }
        public string Description { get; set; }
        public long BorroweId { get; set; }
    }
}
