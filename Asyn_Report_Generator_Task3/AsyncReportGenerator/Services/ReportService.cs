using AsyncReportGenerator.Models;

namespace AsyncReportGenerator.Services
{
    public class ReportService
    {
        // public async Task<bool> GenerateReportAsync(Student student)
        // {
        //     Console.WriteLine($"[{student.Name}] Report generation started");
        //     await Task.Delay(1000);
        //     Console.WriteLine($"[{student.Name}] Report generated");
        //     Console.WriteLine();

        //     return student.Name != "Karan";
        // }

        public Task LogStartAsync(Student student)
        {
            Console.WriteLine($"[{student.Name}] Report generation started");
            return Task.CompletedTask;
        }

        public async Task<bool> LogGeneratedAsync(Student student)
        {
            await Task.Delay(1000);
            Console.WriteLine($"[{student.Name}] Report generated");
            return student.Name != "Karan";
        }
    }
}