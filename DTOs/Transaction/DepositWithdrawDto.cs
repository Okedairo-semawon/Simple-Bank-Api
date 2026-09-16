using System.ComponentModel.DataAnnotations;

namespace BankApi.DTOs.Transaction;

public class DepositWithdrawDto
{
    [Required]
    [Range(0.01 double.MaxValue, ErrorMessage= "Amount must be greater than 0")]

    public decimal Amount {get; set; }

    public string? Description {get; set;}
}