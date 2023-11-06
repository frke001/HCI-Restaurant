using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    class ArticleTypeDAOImpl : IArticleType
    {


        public List<ArticleType> GetAll()
        {
            using (var _db = new restoranContext())
            {
                return _db.ArticleTypes.ToList();
            }
        }

        public ArticleType Add(ArticleType newArticle)
        {
            if (newArticle != null)
            {
                using (var _db = new restoranContext())
                {
                    if (_db.ArticleTypes.Any(el => el.Name == newArticle.Name))
                        return null;
                    _db.ArticleTypes.Add(newArticle);
                    _db.SaveChanges();
                }
            }

            return newArticle;
        }
        public bool Delete(ArticleType articleType) {
            bool deleted = true;
            using (var _db = new restoranContext())
            {
                try
                {
                    _db.ArticleTypes.Remove(articleType);
                    _db.SaveChanges();
                }catch (Exception ex)
                {
                    deleted = false;
                }
            }
            return deleted;
        }
        public ArticleType Update(ArticleType articleType)
        {
            if (articleType != null)
            {
                using (var _db = new restoranContext())
                {

                    ArticleType existingArticleType = _db.ArticleTypes.Find(articleType.Id);

                    if (existingArticleType != null)
                    {
                        if (_db.ArticleTypes.Any(el => el.Name == articleType.Name && el.Id != articleType.Id))
                            return null;
                        existingArticleType.Name = articleType.Name; 
                        _db.SaveChanges();
                    }
                }               
            }

            return articleType;
        }
    }
}
