using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OrderSystem.Core.Entities
{
    /// <summary>
    /// Persistente Basisklasse
    /// </summary>
    public abstract class PersistentEntityBase
    {
        public override string ToString()
        {
            return $"{GetType().Name}_{Id.ToString()}";
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        [ColumnName("Created At")]
        public DateTime CreatedAt { get; set; }

        [ColumnName("Updated At")]
        public DateTime UpdatedAt { get; set; }

        [HideInListView]
        public bool IsDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public Result SoftValidate()
        {
            List<string> errors = ValidateCore();
            if (errors.Count == 0)
            {
                return Result.Ok();
            }

            string message = string.Join("\r\n", errors);
            return Result.Fail(message);
        }

        public void ValidateOrThrow()
        {
            List<string> errors = ValidateCore();
            if (errors.Count > 0)
            {
                string message = string.Join("\r\n", errors);
                Type entityType = GetType();
                Type exceptionType = typeof(ValidationException<>).MakeGenericType(entityType);
                throw (Exception)Activator.CreateInstance(exceptionType, this, message)!;
            }
        }

        private List<string> ValidateCore()
        {
            List<string> output = [];

            Type type = GetType();
            PropertyInfo[] propsToCheck = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo prop in propsToCheck)
            {
                object? value = prop.GetValue(this);

                if (value is IEnumerable enumerable && prop.PropertyType != typeof(string))
                {
                    IEnumerator enumerator = enumerable.GetEnumerator();

                    AtLeastOneAttribute? atLeastOne = prop.GetCustomAttribute<AtLeastOneAttribute>();
                    if (atLeastOne != null)
                    {
                        if (!enumerator.MoveNext())
                        {
                            output.Add($"{type.Name}.{prop.Name} must contain at least one valid element.");
                        }
                    }

                    ((IDisposable)enumerator).Dispose();
                }

                if (prop.GetCustomAttribute<RequiredAttribute>() is not null)
                {
                    if (value is null)
                    {
                        output.Add($"{type.Name}.{prop.Name} cannot be null.");
                    }

                    if (value is string s && string.IsNullOrWhiteSpace(s))
                    {
                        output.Add($"{type.Name}.{prop.Name} cannot be empty.");
                    }
                }

                ClampLengthAttribute? len = prop.GetCustomAttribute<ClampLengthAttribute>();
                if (len is not null && value is string str)
                {
                    if (len.Min > 0 && str.Length < len.Min)
                    {
                        output.Add($"{type.Name}.{prop.Name} is too short (min. {len.Min}).");
                    }

                    if (len.Max > 0 && str.Length > len.Max)
                    {
                        output.Add($"{type.Name}.{prop.Name} is too long (max. {len.Max}).");
                    }
                }

                ClampValueAttribute? clamp = prop.GetCustomAttribute<ClampValueAttribute>();
                if (clamp is not null && value is not null)
                {
                    if (value is IConvertible)
                    {
                        double d = Convert.ToDouble(value);
                        if (clamp.Min != 0 && d < clamp.Min)
                        {
                            output.Add($"{type.Name}.{prop.Name} is too low (min. {clamp.Min}).");
                        }

                        if (clamp.Max != 0 && d > clamp.Max)
                        {
                            output.Add($"{type.Name}.{prop.Name} is too high (max. {clamp.Max})");
                        }
                    }
                }

                ClampDecimalValueAttribute? clampDec = prop.GetCustomAttribute<ClampDecimalValueAttribute>();
                if (clampDec is not null && value is decimal dec)
                {
                    if (clampDec.Min != 0 && dec < (decimal)clampDec.Min)
                    {
                        output.Add($"{type.Name}.{prop.Name} is too low (min. {clampDec.Min}).");
                    }

                    if (clampDec.Max != 0 && dec > (decimal)clampDec.Max)
                    {
                        output.Add($"{type.Name}.{prop.Name} is too high (max. {clampDec.Max})");
                    }
                }
            }

            return output;
        }

        public string GetIdentifier()
        {
            PropertyInfo? identProp = GetType()
                                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .FirstOrDefault(x => x.GetCustomAttribute<IdentifierAttribute>() != null);

            object? identValue = identProp?.GetValue(this);
            if (identValue is string s && !string.IsNullOrEmpty(s))
            {
                return s;
            }

            return ToString();
        }
    }

    #region Attributes

    /// <summary>
    /// Decorating a property with this attribute will make the caption of the respecting column the name
    /// </summary>
    /// <param name="name"></param>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute(string name) : Attribute
    {
        public string Name { get; set; } = name;
    }

    /// <summary>
    /// Decorate a property with this attribute so it does not show up in listviews
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HideInListViewAttribute : Attribute;

    /// <summary>
    /// Decorate a non-nullable string property with this attribute so it will be used as a view header
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentifierAttribute : Attribute;

    /// <summary>
    /// Decorate a property with this attribute so it has to be not null while trying to save
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute;

    /// <summary>
    /// Decorate a string property with this property so it has to be between min and max in length before saving
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    [AttributeUsage(AttributeTargets.Property)]
    public class ClampLengthAttribute(int min = 0, int max = 0) : Attribute
    {
        public int Min { get; set; } = min;

        public int Max { get; set; } = max;
    }

    /// <summary>
    /// A property decorated with this attribute will have to be between min and max before saving
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ClampValueAttribute(float min = 0, float max = 0) : Attribute
    {
        public float Min { get; set; } = min;

        public float Max { get; set; } = max;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ClampDecimalValueAttribute(double min = 0, double max = 0) : Attribute
    {
        public double Min { get; set; } = min;

        public double Max { get; set; } = max;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class AtLeastOneAttribute : Attribute;

    #endregion Attributes
}