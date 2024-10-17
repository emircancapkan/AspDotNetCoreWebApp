/*namespace WebApplication2.Models
{
    public class ProductRepo
    {
        private static List<Product> _products = new List<Product>()
        {
            new () { Id = 1, Name = "Audi", Price = 30000, Stock = 200 },
            new () { Id = 2, Name = "Mercedes", Price = 20200, Stock = 300 },
            new () { Id = 3, Name = "Volkswagen", Price = 15000, Stock = 400 },
            new () { Id = 4, Name = "BMW", Price = 50000, Stock = 500 }
        };

        public List<Product> GetAll() => _products;

        public void Add(Product newProduct)=> _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct= _products.FirstOrDefault(p=> p.Id == id);

            if (hasProduct == null)
            {
                throw new Exception($"there is no id {id} product");
            }

            else
            {
                _products.Remove(hasProduct);
            }
        }

        public void Update(Product updateProdoct)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == updateProdoct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"there is no id {updateProdoct.Id} product");
            }

            else
            {
                hasProduct.Name = updateProdoct.Name;
                hasProduct.Price = updateProdoct.Price;
                hasProduct.Stock = updateProdoct.Stock;
            }

            var index=_products.FindIndex(p=>p.Id == updateProdoct.Id);

            _products[index] = hasProduct;
        }

    }
}
*/