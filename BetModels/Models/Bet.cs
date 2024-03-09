using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetModels.Models;
public class Bet
{
    
    public string GameNumber { get; set; }
    public string BetValue { get; set; }
    public bool Single { get; set; }  // if sent as single
    public int SingleAmount { get; set; }  // bet amount for single
}
