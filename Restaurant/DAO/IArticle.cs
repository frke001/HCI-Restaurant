using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Restaurant.DAO
{
    interface IArticle
    {
        List<Article> GetAll();
        Article GetById(int id);
        Article Update(Article updatedArticle);
        void Delete(Article article);
        Article Add(Article article);
    }
}
