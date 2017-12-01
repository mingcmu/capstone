using System;
using Microsoft.VisualBasic.FileIO;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class BatchRegisterModels
    {
        public void FileParser()
        {
            var path = @"/Users/terrycao/DownloadsQ/user.csv"; 
            
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.

                    Candidates candiate = new Candidates();
                    string[] fields = csvParser.ReadFields();
                    candiate.FirstName = fields[0];
                    candiate.LastName = fields[1];
                    candiate.UserName = fields[2];
                    candiate.Email = fields[3];
                    candiate.Password = fields[4];
                    candiate.ProjectId = fields[5];

                }
            }
        }
        
    }
}