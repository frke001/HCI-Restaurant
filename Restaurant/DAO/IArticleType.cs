using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO
{
    interface IArticleType
    {
        List<ArticleType> GetAll();
        ArticleType Add(ArticleType articleType);
        void Delete(ArticleType articleType);
        ArticleType Update(ArticleType articleType);
    }
}
