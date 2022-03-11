using System;
using Netizen.Text.Json;

namespace Netizen.Text.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoPerson dd = new DemoPerson
            {
                Gender = DemoGender.Male,
                Name = "Boo Foo AAbbC".To(NamingStyle.SnakeCase),
                Age = 10,
                Birthday = DateTime.Now - TimeSpan.FromDays(365 * 10),
            };
            string ddj = dd.ToJson();
            Console.WriteLine(ddj);
            DemoPerson ddjd = ddj.JsonAs<DemoPerson>();
            Console.WriteLine($"{ddjd.Name}({ddjd.Gender}) is {ddjd.Age} age({ddjd.Birthday}).");
            Console.WriteLine("HTTP_METHOD".ToSnakeCase());
            Console.ReadLine();
        }
    }
}
