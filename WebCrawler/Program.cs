
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WebCrawler;
using HtmlAgilityPack;
using System.Net.Http;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;


await startCrawlerasync();

const string baseAddress = "https://meghdadit.com/";


static async Task startCrawlerasync()
{
    //var url = "https://www.technolife.ir/product/list/164_163_130/%D8%AA%D9%85%D8%A7%D9%85%DB%8C-%DA%A9%D8%A7%D9%85%D9%BE%DB%8C%D9%88%D8%AA%D8%B1%D9%87%D8%A7-%D9%88-%D9%84%D9%BE-%D8%AA%D8%A7%D9%BE-%D9%87%D8%A7";
    
    var productListUrl = baseAddress + "productlist/laptop/";
    
    var httpClient = new HttpClient();
    var html = await httpClient.GetAsync(productListUrl);
    
    var htmlDocument = new HtmlDocument();
    var htmlContent = await html.Content.ReadAsStringAsync();
    htmlDocument.LoadHtml(htmlContent);

    var products = await ParseHtml(htmlDocument);

    await SaveProductsAsync(products);
}

static async Task<List<Product>> ParseHtml(HtmlDocument htmlDocument)
{
    var products = new List<Product>();

    //var lis = htmlDocument.DocumentNode
    //    .Descendants("li")
    //    .Where(n => n.HasClass("ProductPrlist_product__PdoZm"))
    //    .ToList();

    var lis = htmlDocument.DocumentNode
        .SelectNodes("//ul[@id='SharedMessage_ContentPlaceHolder1_divThumbnailView']/li")
        .ToList();

    var liIndex = 0;

    foreach (var li in lis)
    {
        var productDetailAddress = li.Descendants("a").FirstOrDefault().Attributes["href"].Value;

        var httpClient = new HttpClient();
        var detaildPageHtml = await httpClient.GetAsync(baseAddress + productDetailAddress);

        var detaildPageHtmlDocument = new HtmlDocument();
        var htmlContent = await detaildPageHtml.Content.ReadAsStringAsync();
        detaildPageHtmlDocument.LoadHtml(htmlContent);

        var descriptionDiv = detaildPageHtmlDocument.DocumentNode
            .SelectNodes("//div[contains(@class, 'product-item-description')]")
            //.SelectNodes("//div[@class='product-item-description']")
            .FirstOrDefault();

        var product = new Product
        {
            Name = descriptionDiv.SelectSingleNode(".//span[@id='SharedMessage_ContentPlaceHolder1_lblItemTitle']").InnerText,
            ImageLink = detaildPageHtmlDocument.DocumentNode.SelectSingleNode(".//img[@id='SharedMessage_ContentPlaceHolder1_imgItem']").Attributes["src"].Value,
            Price = li.SelectSingleNode($".//span[@id='SharedMessage_ContentPlaceHolder1_rptList_lblLowestPrice_{liIndex}']").InnerText?.Replace("تومان", "")?.Trim(),
            OriginalPrice = li.SelectSingleNode($".//span[@id='SharedMessage_ContentPlaceHolder1_rptList_lblOriginalPrice_{liIndex}']").InnerText?.Replace("تومان", "")?.Trim(),
            Dimention = detaildPageHtmlDocument.DocumentNode.SelectSingleNode(".//span[@id='SharedMessage_ContentPlaceHolder1_rptAttributeSet_rptAttribute_0_lblAttributeValue_0']").InnerText,
            Weight = detaildPageHtmlDocument.DocumentNode.SelectSingleNode(".//span[@id='SharedMessage_ContentPlaceHolder1_rptAttributeSet_rptAttribute_0_lblAttributeValue_1']").InnerText,
        };

        products.Add(product);

        // Get only 3 items for demo purposes
        //if (products.Count == 3)
        //    break;

        liIndex++;
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