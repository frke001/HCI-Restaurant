﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class ArticleType
{
    public ArticleType() { }
    public ArticleType( string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}