namespace MyAspNetCoreApp.Web.Models
{
    //CRUD işlemlerinin kodlandığı bölümdür.
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>() { 


            new() { Id = 1, Name = "Kalem1", Price = 15, Stok = 200 },
            new () { Id = 2, Name = "Kalem2", Price = 45, Stok = 120 },
            new () { Id = 3, Name = "Kalem3", Price = 65, Stok = 80}
    
    };

        public List<Product> GetAllProducts() => _products;
        public void Add(Product newProduct) => _products.Add(newProduct); 

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);

            _products.Remove(hasProduct);

            if(hasProduct==null)
            {
                throw new Exception($"Bu id{id} ye sahip ürün bulunmamaktadır.");
            }
        }

        public void Uptade(Product updateProduct)
        {
           var hasProduct= _products.FirstOrDefault(x => x.Id == updateProduct.Id);

            hasProduct.Name = updateProduct.Name;
            hasProduct.Price=updateProduct.Price;
            hasProduct.Stok=updateProduct.Stok;

          var index=  _products.FindIndex(x => x.Id == updateProduct.Id);
            //_products.Insert(index, updateProduct);
            _products[index] = hasProduct;

        }

    }
}
