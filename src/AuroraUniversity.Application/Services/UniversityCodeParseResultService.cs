using System.Text;
using System.Text.RegularExpressions;

namespace AuroraUniversity.Application.Validators;

public static class UniversityCodeParserService
{
    public static UniversityCodeParseResult CheckValidCode(string input)
    {
        var result = new UniversityCodeParseResult();

        if (string.IsNullOrWhiteSpace(input))
        {
            result.IsValid = false;
            result.ErrorMessage = "Input cannot be empty.";
            return result;
        }

        input = input.Trim().ToUpper();
        var parts = input.Split('-');

        if (parts.Length != 6)
        {
            result.IsValid = false;
            result.ErrorMessage = "Code must have 6 parts separated by hyphens (AU-TYPE-YEARTERM-CODE-NNNN-CC).";
            return result;
        }

        if (parts[0] != "AU")
        {
            result.IsValid = false;
            result.ErrorMessage = "Invalid institution prefix. Must be 'AU'.";
            return result;
        }

        var type = parts[1];
        if (!new[] { "STU", "MOD", "STF" }.Contains(type))
        {
            result.IsValid = false;
            result.ErrorMessage = "Invalid type. Must be STU, MOD, or STF.";
            return result;
        }
        result.Type = type;

        var yearTermPart = parts[2];

        if (yearTermPart.Length != 5)
        {
            result.IsValid = false;
            result.ErrorMessage = "YearTerm part must be exactly 5 digits (YYYYT).";
            return result;
        }

        var yearString = yearTermPart.Substring(0, 4);
        if (yearString.Length != 4 || !int.TryParse(yearString, out int year))
        {
            result.IsValid = false;
            result.ErrorMessage = "Year must be 4 digits.";
            return result;
        }
        result.Year = year;

        var termString = yearTermPart.Substring(4, 1);
        if (!int.TryParse(termString, out int term) || term < 1 || term > 3)
        {
            result.IsValid = false;
            result.ErrorMessage = "Term must be 1, 2, or 3.";
            return result;
        }
        result.Term = term;


        var code = parts[3];
        if (!Regex.IsMatch(code, "^[A-Z]{3,6}$"))
        {
            result.IsValid = false;
            result.ErrorMessage = "Code must be 3-6 uppercase letters.";
            return result;
        }
        result.Code = code;

        var serialPart = parts[4];
        if (serialPart.Length != 4 || !int.TryParse(serialPart, out int serial))
        {
            result.IsValid = false;
            result.ErrorMessage = "Serial must be 4 digits.";
            return result;
        }
        result.Serial = serialPart;

        var checksumPart = parts[5];
        if (checksumPart.Length != 2 || !int.TryParse(checksumPart, out int checksum))
        {
            result.IsValid = false;
            result.ErrorMessage = "Checksum must be 2 digits.";
            return result;
        }
        result.Checksum = checksum;


        var combined = string.Concat(parts[0], parts[1], parts[2], parts[3], parts[4]);

        StringBuilder digitString = new();
        foreach (var ch in combined)
        {
            if (char.IsDigit(ch))
                digitString.Append(ch - '0');
            else if (char.IsLetter(ch))
                digitString.Append(ch - 'A' + 10);
        }

        long sum = 0;
        foreach (var digitChar in digitString.ToString())
        {
            sum += (long)(digitChar - '0');
        }

        if (sum % 97 != checksum)
        {
            result.IsValid = false;
            result.ErrorMessage = "Checksum mismatch.";
            return result;
        }

        result.IsValid = true;
        return result;
    }
}

public class UniversityCodeParseResult
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Term { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;
    public int Checksum { get; set; }
}
