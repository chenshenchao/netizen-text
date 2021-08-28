using System;

namespace Netizen.Text.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoPerson dd = new DemoPerson
            {
                Gender = DemoGender.Male,
                Name = "Boo",
                Age = 10,
                Birthday = DateTime.Now - TimeSpan.FromDays(365 * 10),
            };
            string ddj = dd.ToJson();
            Console.WriteLine(ddj);
            DemoPerson ddjd = ddj.JsonAs<DemoPerson>();
            Console.WriteLine($"{ddjd.Name}({ddjd.Gender}) is {ddjd.Age} age({ddjd.Birthday}).");
            Console.ReadLine();
        }
    }
}
