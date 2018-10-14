using System;

namespace Ripple.Training
{
    using Nest;
    using Serilog;
    using Serilog.Core;

    public class Student
    {
        
        public int RollNo { get; set; }
        [Text]
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            Logger Log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            Log.Information("My First Log");

            var settings = new ConnectionSettings(new Uri("http://localhost:9200/"))
                .DefaultIndex("dump_index").DefaultMappingFor<Student>(m=>m.IndexName("student_data_index"));

            var client = new ElasticClient(settings);

            var i = 0;
            while(i<100)
            {
                i++;

                var student = new Student
                {
                    RollNo = i,
                    Name = "Name_" + i.ToString()
                };
            var indexPositon = client.IndexDocument(student);
                Log.Information(indexPositon.Result.ToString());

            }

            Console.Read();
        }
    }
}
