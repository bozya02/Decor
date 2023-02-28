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
        public event AddNewItem AddNewItemEvent;

        /// <summary>
        /// Приватные поля для получения и сохранения данных
        /// </summary>
        private static DbSet<Product> _products => DecorEntities.GetContext().Products;


        /// <summary>
        /// Методы получения объектов
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetProducts() => _products.ToList();
        public static List<User> GetUsers() => DecorEntities.GetContext().Users.ToList();




        /// <summary>
        /// Получение пользователя при авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Login(string login, string password) => GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
    }
}
