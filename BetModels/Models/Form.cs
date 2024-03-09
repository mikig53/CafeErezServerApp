using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetModels.Models;
public class Form : User
{
         
    public List<Bet>  Bets { get; set; } = new List<Bet>();
    
    public decimal BetAmount { get; set; }
    public string ReceivedDate { get; set; } = string.Empty;
    public bool IsSent { get; set; } = false;
}
