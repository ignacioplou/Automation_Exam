namespace Automation_Exam.EbaySite.Models
{
    public class ProductsData
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Shippment { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Price + Shippment;
            }
        }
    }
}
