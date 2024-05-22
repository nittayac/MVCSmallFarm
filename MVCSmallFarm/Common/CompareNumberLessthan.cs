using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MVCSmallFarm.Common;

public class CompareNumberLessthan : ValidationAttribute
{
    private readonly string _propToCompare;
    public CompareNumberLessthan(string propToCompare) {
        
        _propToCompare = propToCompare;
    }

    protected override ValidationResult IsValid(object value, ValidationContext vc)
    {
        if (value != null)
        { 
            decimal currentValue =Convert.ToDecimal(value);

            PropertyInfo? pinfo = vc.ObjectType.GetProperty(_propToCompare);
            object? compare = pinfo?.GetValue(vc.ObjectInstance, null);
            if(compare != null)
            {
                if (currentValue > Convert.ToDecimal(compare))
                {
                    return new ValidationResult(ErrorMessage, new[] { vc.MemberName ?? "Unknow" });
                }

            }
        
        }


        return ValidationResult.Success;
    }
}
