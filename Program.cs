using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

namespace Task16
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/Юлёсик/Desktop/Автоматизация BIM проектирования/ОСНОВЫ ПРОГРАММИРОВАНИЯ НА ЯЗЫКЕ C#/16_Задание 16/Products.json";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            Product [] products= new Product[5];
            Console.WriteLine("Введите код, название и цену товара");
           
            for (int i = 0; i < 5; i++)            
            {                
                products[i] = new Product(Convert.ToInt16(Console.ReadLine()), Console.ReadLine(), Convert.ToDouble(Console.ReadLine()));                
            }
            
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            string jsonString = JsonSerializer.Serialize(products, options);
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(jsonString);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                string jsontext = sr.ReadToEnd();
                Product [] products1 = JsonSerializer.Deserialize<Product[]>(jsontext);                
                
                double maxPrice = 0;
                for (int i = 0; i < products1.Length; i++)
                {
                    if (products1[i].productPrice> maxPrice)
                    {
                        maxPrice = products1[i].productPrice;
                        Console.WriteLine("Самый дорогой товар:{0} - {1}", products1[i].productCode, products1[i].productName);
                    }
                    
                }
            }
            
            Console.ReadKey();

        }
    }
    public class Product
    {
        public int productCode { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
        public Product(int code, string name, double price)
        {
            productCode = code;
            productName = name;
            productPrice = price;
        }
    }
}
