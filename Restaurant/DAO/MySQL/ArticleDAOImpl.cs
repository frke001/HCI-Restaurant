using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    class ArticleDAOImpl : IArticle
    {

        public bool Delete(Article article)
        {
            bool deleted  = true;
            using (var _db = new restoranContext())
            {

                if (_db.OrderItems.Any(item => item.IdArticle == article.Id))
                {

                    deleted = false;
                }
                else
                {
                    var temp = _db.Articles.Find(article.Id);
                    if (temp != null)
                    {
                        temp.Active = 0;
                        _db.SaveChanges();
                    }
                }
                

            }
            return deleted;
        }

        public List<Article> GetAll()
        {

            using (var _db = new restoranContext())
            {
                var articles = _db.Articles.ToList();
                foreach (var el in articles)
                {
                    var ArticleType = _db.ArticleTypes.Find(el.IdType);
                    el.IdTypeNavigation = ArticleType;

                }
                return articles;
            }

        }

        public Article GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Article Update(Article updatedArticle)
        {
            if (updatedArticle != null)
            {
                using (var _db = new restoranContext())
                {

                    Article existingArticle = _db.Articles.Find(updatedArticle.Id);

                    if (existingArticle != null)
                    {
                        if (_db.ArticleTypes.Any(el => el.Name == updatedArticle.Name))
                            return null;
                        existingArticle.Name = updatedArticle.Name;
                        existingArticle.Price = updatedArticle.Price;
                        existingArticle.Description = updatedArticle.Description;
                        existingArticle.IdType = updatedArticle.IdType;
                        _db.SaveChanges();
                    }
                }
            }

            return updatedArticle;
        }
        public Article Add(Article article)
        {
            using (var _db = new restoranContext())
            {
                if (_db.Articles.Any(el => el.Name == article.Name))
                    return null;
                _db.Articles.Add(article);
                _db.SaveChanges();
            }
            return article;
        }
    }
}
