
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WebCrawler;
using HtmlAgilityPack;
using System.Net.Http;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

await startCrawlerasync();

static async Task startCrawlerasync()
{
    var url = "https://www.19kala.com/%DA%AF%D9%88%D8%B4%DB%8C-%D9%85%D9%88%D8%A8%D8%A7%DB%8C%D9%84-%D8%B3%D8%A7%D9%85%D8%B3%D9%88%D9%86%DA%AF-samsung";
    
    var httpClient = new HttpClient();
    var html = await httpClient.GetStringAsync(url);
    
    var htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);

    var products = ParseHtml(htmlDocument);

    await SaveProductsAsync(products);
}

static List<Product> ParseHtml(HtmlDocument htmlDocument)
{
    var products = new List<Product>();

    var divs = htmlDocument.DocumentNode
        .Descendants("div")
        .Where(n => n.HasClass("product-list-item"))
        .ToList();

    foreach (var div in divs)
    {
        var product = new Product
        {
            Title = div.Descendants("h4").FirstOrDefault().InnerText,
            Link = div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value,
            OldPrice = div.Descendants("span").Where(n => n.HasClass("price-old")).FirstOrDefault().InnerText,
            NewPrice = div.Descendants("span").Where(n => n.HasClass("price-new")).FirstOrDefault().InnerText,
            Thumb = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value
        };

        products.Add(product);

        // Get only 3 items for demo purposes
        if (products.Count == 3)
            break;
    }

    return products;
}

static async Task SaveProductsAsync(List<Product> products)
{
    using (var context = new ApplicationDbContext())
    {
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}