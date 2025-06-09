namespace WebShopNovi.Models
{
    public class ViewModel
    {
        public class ProductsListViewModel
        {
            public List<Product> Products { get; set; }
            public List<string> Categories { get; set; }
            public string SelectedCategory { get; set; }
            public string SortOrder { get; set; }
            public string SortDirection { get; set; }
        }
    }

}
