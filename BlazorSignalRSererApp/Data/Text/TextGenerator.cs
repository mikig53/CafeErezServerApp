using BetModels.Interfaces;
using BetModels.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MudBlazor.CategoryTypes;
using Form = BetModels.Models.Form;

namespace BlazorSignalRSererApp.Data.Text;
public class TextGenerator : ITextService
{
   
    public async Task SaveAsTextAsync(Form form)
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var textFilename = Path.Combine(outputDirectory, "forms2.txt");

        try
        {
            DateTime lastWriteTime = default(DateTime);
            if(textFilename == null)
            {
                 lastWriteTime = DateTime.MaxValue;
            }
            else
                lastWriteTime = File.GetLastWriteTime(textFilename);
            // Compare the last write time with the current date
            if (lastWriteTime.Date < DateTime.Today)
            {
                // The file is from a previous day, so clear it
                File.WriteAllText(textFilename, string.Empty);
            }
            using (var writer = new StreamWriter(textFilename, true))
            {

                
                writer.WriteLine($"{form.ReceivedDate},{form.BetAmount},{form.TelephoneNumber},{form.FirstName},{form.LastName},{form.IsSent}");
                // Add a delimiter between forms
                writer.WriteLine("===USER DETAILS DELIMITER===");

                foreach (var bet in form.Bets)
                {
                    string formattedLine = $"{bet.BetValue},({bet.GameNumber}),{bet.Single}, {bet.SingleAmount}";
                    writer.WriteLine(formattedLine);
                }

                // Add a delimiter between forms
                writer.WriteLine("===FORM DELIMITER===");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task<List<Form>> ReadAsTextAsync()
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var csvFilename = Path.Combine(outputDirectory, "forms2.txt");
        List<BetModels.Models.Form> forms = new List<Form>();

        try
        {
            using (var reader = new StreamReader(csvFilename))
            {
                string line;
                Form currentForm = new Form();
                string details = "user";

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line == "===USER DETAILS DELIMITER===")
                    {
                        
                        details = "bets";
                        continue;
                    }

                    if (line == "===FORM DELIMITER===")
                    {
                        forms.Add(currentForm);
                        currentForm = new Form();
                        details = "user";
                        continue;
                    }
                    
                   
                    switch (details)
                    {

                        case "user":
                            string[] formsDetails = line.Split(',');//{form.ReceivedDate},{form.BetAmount},{form.TelephoneNumber},{form.FirstName},{form.LastName}{form.IsSent}");
                            currentForm.ReceivedDate = formsDetails[0];
                            Decimal.TryParse(formsDetails[1], out decimal result);
                            currentForm.BetAmount =  result;
                             currentForm.FirstName = formsDetails[3];
                            currentForm.LastName = formsDetails[4];
                            currentForm.TelephoneNumber = formsDetails[2];
                            Boolean.TryParse(formsDetails[5], out bool resultBool);
                            currentForm.IsSent = resultBool;
                            break;

                        case "bets":
                            string[] values = line.Split(',');
                            Bet bet = new Bet();
                            bet.BetValue = values[0];
                            Boolean.TryParse(values[2], out bool singleBool);
                            bet.Single = singleBool;
                            if (int.TryParse(values[3], out int singleInt))
                                bet.SingleAmount = singleInt;
                            else bet.SingleAmount = 0;
                            // Remove surrounding parentheses from GameNumber using regular expression
                            string gameNumber = values[1];
                            gameNumber = Regex.Replace(gameNumber, @"^\(|\)$", "");
                            bet.GameNumber = gameNumber;
                            currentForm.Bets.Add(bet);
                            break;
                        case "add":
                            forms.Add(currentForm);
                            details = "bets";
                            break;

                        default:
                           break;
                    }

                }

            }
                      
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return forms;
    }

    public async Task DeleteOldFormsAsync()
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var csvFilename = Path.Combine(outputDirectory, "forms2.txt");

        // Get the creation date of the file
        var creationDate = await Task.Run(() => File.GetCreationTime(csvFilename));

        // Check if the file was not created today
        if (creationDate.Date != DateTime.Today)
        {
            // Delete the file
            await Task.Run(() => File.Delete(csvFilename));
        }
    }
}
