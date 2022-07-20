namespace CreativPage.Models
{
    public class Portfolio : Base
    {
        public string PhotoURL { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }

    }
}
