using AsyncReportGenerator.Models;

namespace AsyncReportGenerator.Services
{
    public class DataServices
    {
        public async Task<List<Student>> LoadStudentDataAsync(string filepath)
        {
            var students = new List<Student>();

            using (var reader = new StreamReader(filepath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var parts = line.Split(",");
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id))
                    {
                        students.Add(new Student
                        {
                            Id = id,
                            Name = parts[1].Trim(),
                            Department = parts[2].Trim()
                        });
                    }
                }
            }
            return students;
        }
    }
}