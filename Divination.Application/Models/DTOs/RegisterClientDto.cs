using System.ComponentModel.DataAnnotations;

public class RegisterClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DateofBirth { get; set; }
    public string Occupation { get; set; } 
    public string MaritalStatus { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}