using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetModels.Models;
public class TelesportCategory
{
    public string Name { get; set; }
    public List<TelesportBet> Tags { get; set; }
}
