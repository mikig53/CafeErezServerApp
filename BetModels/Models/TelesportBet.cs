using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetModels.Models;
public class TelesportBet
{
    public string Number { get; set; }// bet number in the form
    public string Status { get; set; }// finished/current game time/will start game time
    public string Description { get; set; }
    public string Type { get; set; }// S-single, D-duble
    public string Bet1 { get; set; }// bate rate for 1
    public string BetX { get; set; }// bate rate for X
    public string Bet2 { get; set; }// bate rate for 2
    public string Score { get; set; }// score of the game
    public string WinBet { get; set; } // 2|x|1
    public bool EligibleBet { get; set; } // The game not started
    public string HomeTeam { get; set; } = string.Empty;
    public string GuestTeam { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
}
