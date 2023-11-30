using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Business.Models
{
    public class SalesRecord
    {
        public int id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Amounth { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord() { }
       
        public SalesRecord(int id, DateTime date, double amounth, SaleStatus status, Seller seller)
        {
            this.id = id;
            Date = date;
            Amounth = amounth;
            Status = status;
            Seller = seller;
        }
    }
}
