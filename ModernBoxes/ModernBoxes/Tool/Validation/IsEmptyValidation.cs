using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModernBoxes.Tool.Validation
{
    public class IsEmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == String.Empty)
            {
                return new ValidationResult(false, "不能为空");
            }
            return new ValidationResult(true, "");
        }
    }
}