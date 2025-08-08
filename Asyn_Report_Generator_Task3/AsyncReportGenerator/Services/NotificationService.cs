using AsyncReportGenerator.Models;

namespace AsyncReportGenerator.Services
{
    public class NotificationService
    {
        public async Task SentNotification(Student student)
        {
            await Task.Delay(1000);
            Console.WriteLine($"[{student.Name}] Notification Sent");
        }
    }
}