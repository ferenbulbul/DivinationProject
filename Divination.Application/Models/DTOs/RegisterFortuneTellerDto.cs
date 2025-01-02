using System.ComponentModel.DataAnnotations;
using Divination.Domain.Entities;

public class RegisterFortuneTellerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Experience { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<int> FalCategories { get; set; }
}