// Address.cs
// Defines a structure to store address information
namespace StudentManagementSystem.Models
{
    public struct Address
    {
        public string Street;
        public string City;
        public string GetFullAddress()
        {
            return $"{Street}, {City}";
        }
    }
}