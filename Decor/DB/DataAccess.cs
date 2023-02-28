using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.DB
{
    public class DataAccess
    {
        public delegate void AddNewItem();
        public static event AddNewItem AddNewItemEvent;

        /// <summary>
        /// Приватные поля для получения и сохранения данных
        /// </summary>
        private static DbSet<Product> _products => DecorEntities.GetContext().Products;
        private static DbSet<Order> _orders => DecorEntities.GetContext().Orders;


        /// <summary>
        /// Методы получения объектов
        /// </summary>
        public static List<Product> GetProducts() => _products.ToList();
        public static List<Order> GetOrders() => _orders.ToList();
        public static List<User> GetUsers() => DecorEntities.GetContext().Users.ToList();
        public static List<Category> GetCategories() => DecorEntities.GetContext().Categories.ToList();
        public static List<Producer> GetProducers() => DecorEntities.GetContext().Producers.ToList();
        public static List<Supplier> GetSuppliers() => DecorEntities.GetContext().Suppliers.ToList();
        public static List<Unit> GetUnits() => DecorEntities.GetContext().Units.ToList();




        /// <summary>
        /// Получение пользователя при авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Login(string login, string password) => GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);

        /// <summary>
        /// Сохранение продукта
        /// </summary>
        /// <param name="product"></param>
        public static void SaveProduct(Product product)
        {
            if (product.Id == 0)
                _products.Add(product);

            DecorEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static void DeleteProduct(Product product)
        {
            _products.Remove(product);
            DecorEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        /// <summary>
        /// Проверка артикула на уникальность
        /// </summary>
        /// <param name="product"></param>
        /// <returns>bool</returns>
        public static bool IsExistActicle(Product product) => GetProducts().Any(x => x.Id != product.Id && x.Article == product.Article);
    }
}
