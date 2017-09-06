using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value) //TODO Consider refactor without 'if'
        {
            if (value != null)
            {
                return (decimal) value >= _minValue;
            }

            return true;
        }
    }
}