using System.ComponentModel.DataAnnotations;
using BankApi.Models;

namespace BankApi.DTOs.Account;

public class CreateAcountDto
{
    // [Required]
    // public string AccountNumber { get; set; } = string.Empty;

    [Required]
    public AccountType Type { get; set;}
}