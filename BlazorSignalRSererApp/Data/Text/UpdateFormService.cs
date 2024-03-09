using BetModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSignalRSererApp.Data.Text;
public class UpdateFormService
{
    public async Task UpdateIsSentAsync(string receivedDate, string firstName, string lastName)
    {
        var outputDirectory = AppDomain.CurrentDomain.BaseDirectory; ;
        var textFilename = Path.Combine(outputDirectory, "forms2.txt");
        // Read all lines from the CSV file
        var lines = await File.ReadAllLinesAsync(textFilename);

        // Search for the line that contains the desired form
        var formLineIndex = -1;
        for (var i = 0; i < lines.Length; i++)
        {
            var formValues = lines[i].Split(',');

            // Check if the form matches the search criteria
            if (DateTime.TryParse(formValues[0], out var formReceivedDate) &&
                formValues[0] == receivedDate &&
                formValues[3] == firstName &&
                formValues[4] == lastName)
               
            {
                formLineIndex = i;
                break;
            }
        }

        if (formLineIndex >= 0)
        {
            // Split the line using comma as the delimiter
            var formValues = lines[formLineIndex].Split(',');

            // Update the IsSent property
            var isSent = formValues[5].ToLower() == "true";
            formValues[5] = (!isSent).ToString();

            // Combine the updated values and write back to the line
            lines[formLineIndex] = string.Join(',', formValues);

            // Write the modified lines back to the CSV file
            await File.WriteAllLinesAsync(textFilename, lines);
        }
        else
        {
            throw new ArgumentException("Form not found in the CSV file.", nameof(receivedDate));
        }
    }
}
