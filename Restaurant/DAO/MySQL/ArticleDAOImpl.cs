using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    class ArticleDAOImpl : IArticle
    {
        private readonly restoranContext _db;

        public ArticleDAOImpl()
        {
            _db = new restoranContext();
        }
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetAll()
        {
            return _db.Articles.ToList();
        }

        public Article GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Article Update(int id, Article updatedArticle)
        {
            throw new NotImplementedException();
        }
    }
}
