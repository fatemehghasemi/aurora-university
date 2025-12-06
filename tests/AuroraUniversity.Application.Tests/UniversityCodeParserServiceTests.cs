using AuroraUniversity.Application.Validators;

namespace AuroraUniversity.Tests.Validators
{
    public class UniversityCodeParserServiceTests
    {
        [Fact]
        public void CheckValidCode_ShouldReturnValidResult_WhenInputIsValid()
        {

            var input = "AU-STF-20253-PSYCH-5555-01";

            var result = UniversityCodeParserService.CheckValidCode(input);

            Assert.True(result.IsValid);
            Assert.Equal("STF", result.Type);
            Assert.Equal(2025, result.Year);
            Assert.Equal(3, result.Term);
            Assert.Equal("PSYCH", result.Code);
            Assert.Equal("5555", result.Serial);
            Assert.Equal(1, result.Checksum);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void CheckValidCode_ShouldReturnInvalidResult_WhenPrefixIsWrong()
        {
            var result = UniversityCodeParserService.CheckValidCode("AB-STU-20231-COM-1234-68");
            Assert.False(result.IsValid);
            Assert.Equal("Invalid institution prefix. Must be 'AU'.", result.ErrorMessage);
        }
        [Fact]
        public void Should_Fail_When_Not_Six_Parts()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-20231-COM-1234");
            Assert.False(result.IsValid);
            Assert.Equal("Code must have 6 parts separated by hyphens (AU-TYPE-YEARTERM-CODE-NNNN-CC).", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Invalid_Prefix()
        {
            var result = UniversityCodeParserService.CheckValidCode("AB-STU-20231-COM-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("Invalid institution prefix. Must be 'AU'.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Invalid_Type()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-XYZ-20231-COM-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("Invalid type. Must be STU, MOD, or STF.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_YearTerm_Length_Is_Wrong()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-2023-COM-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("YearTerm part must be exactly 5 digits (YYYYT).", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Year_Is_Not_Numeric()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-ABCD1-COM-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("Year must be 4 digits.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Term_Is_Invalid()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-20234-COM-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("Term must be 1, 2, or 3.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Code_Format_Is_Invalid()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-20231-Co1-1234-12");
            Assert.False(result.IsValid);
            Assert.Equal("Code must be 3-6 uppercase letters.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Serial_Is_Not_Four_Digits()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-20231-COM-12A-12");
            Assert.False(result.IsValid);
            Assert.Equal("Serial must be 4 digits.", result.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Checksum_Is_Not_Numeric()
        {
            var result = UniversityCodeParserService.CheckValidCode("AU-STU-20231-COM-1234-AB");
            Assert.False(result.IsValid);
            Assert.Equal("Checksum must be 2 digits.", result.ErrorMessage);
        }
    }
}
