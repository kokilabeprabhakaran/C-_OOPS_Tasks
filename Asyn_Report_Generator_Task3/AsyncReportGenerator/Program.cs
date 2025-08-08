using System.Reflection.Metadata;
using System.Threading.Tasks;
using AsyncReportGenerator.Models;
using AsyncReportGenerator.Services;

namespace AsyncReportGenerator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Loading student data....");
            Console.WriteLine();

            DataServices dataServices = new DataServices();
            var reportService = new ReportService();
            var notificationService = new NotificationService();

            var students = await dataServices.LoadStudentDataAsync("students.txt");

            Console.WriteLine("Generating Reports....");
            Console.WriteLine();

            // var tasks = students.Select(async student =>
            //                         {
            //                             bool success = await reportService.GenerateReportAsync(student);
            //                             if (success)
            //                             {
            //                                 await notificationService.SentNotification(student);
            //                             }
            //                             return (Student: student, Success: success);
            //                         });

            // var results = await Task.WhenAll(tasks);

            var startTasks = students.Select(student =>
                                            reportService.LogStartAsync(student));
            await Task.WhenAll(startTasks);
            Console.WriteLine();

            var resultTasks = students.Select(async student =>
            {
                bool success = await reportService.LogGeneratedAsync(student);
                Console.WriteLine();

                if (success)
                {
                    await notificationService.SentNotification(student);
                    Console.WriteLine();
                }

                return (Student: student, Success: success);
            });

            var results = await Task.WhenAll(resultTasks);

            Console.WriteLine("All reports completed");
            Console.WriteLine();

            int total = results.Length;
            int successReports = results.Count(s => s.Success);
            int failedReports = total - successReports;

            Console.WriteLine("Report Summary:");
            Console.WriteLine($"Total Students: {total}");
            Console.WriteLine($"Successful Reports: {successReports}");
            Console.WriteLine($"Failed Reports: {failedReports}");
        }
    }
}