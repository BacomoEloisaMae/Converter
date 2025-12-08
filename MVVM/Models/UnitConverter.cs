using UnitsNet;
using UnitsNet.Units;

namespace Converter.MVVM.Models
{
    public static class UnitConverter
    {
        public static double Convert(string category, double value, string fromUnit, string toUnit)
        {
            return category switch
            {
                "Length" => Length.From(value, ParseUnit<LengthUnit>(fromUnit)).As(ParseUnit<LengthUnit>(toUnit)),
                "Mass" => Mass.From(value, ParseUnit<MassUnit>(fromUnit)).As(ParseUnit<MassUnit>(toUnit)),
                "Area" => Area.From(value, ParseUnit<AreaUnit>(fromUnit)).As(ParseUnit<AreaUnit>(toUnit)),
                "Volume" => Volume.From(value, ParseUnit<VolumeUnit>(fromUnit)).As(ParseUnit<VolumeUnit>(toUnit)),
                "Temperature" => Temperature.From(value, ParseUnit<TemperatureUnit>(fromUnit)).As(ParseUnit<TemperatureUnit>(toUnit)),

                _ => value
            };
        }

        private static TUnit ParseUnit<TUnit>(string unitName) where TUnit : Enum
        {
            return (TUnit)Enum.Parse(typeof(TUnit), unitName.Replace(" ", ""));
        }
    }
}
