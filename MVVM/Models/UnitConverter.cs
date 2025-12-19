using UnitsNet;
using UnitsNet.Units;

namespace Converter.MVVM.Models
{
    public static class UnitConverter
    {
        public static double Convert(string category, double value, string fromUnit, string toUnit)
        {
            try
            {
                return category switch
                {
                    "Length" => Length.From(value, ParseUnit<LengthUnit>(fromUnit))
                                       .As(ParseUnit<LengthUnit>(toUnit)),

                    "Mass" => Mass.From(value, ParseUnit<MassUnit>(fromUnit))
                                   .As(ParseUnit<MassUnit>(toUnit)),

                    "Area" => Area.From(value, ParseUnit<AreaUnit>(fromUnit))
                                   .As(ParseUnit<AreaUnit>(toUnit)),

                    "Volume" => Volume.From(value, ParseUnit<VolumeUnit>(fromUnit))
                                     .As(ParseUnit<VolumeUnit>(toUnit)),

                    "Temperature" => Temperature.From(value, ParseUnit<TemperatureUnit>(fromUnit))
                                                 .As(ParseUnit<TemperatureUnit>(toUnit)),

                    "Energy" => Energy.From(value, ParseUnit<EnergyUnit>(fromUnit))
                                       .As(ParseUnit<EnergyUnit>(toUnit)),

                    "Speed" => Speed.From(value, ParseUnit<SpeedUnit>(fromUnit))
                                     .As(ParseUnit<SpeedUnit>(toUnit)),

                    "Duration" => Duration.From(value, ParseUnit<DurationUnit>(fromUnit))
                                           .As(ParseUnit<DurationUnit>(toUnit)),

                    "Information" => (double)Information.From(value, ParseUnit<InformationUnit>(fromUnit))
                                                .As(ParseUnit<InformationUnit>(toUnit)),

                    _ => throw new ArgumentException($"Category '{category}' is not supported.")
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot convert {value} from {fromUnit} to {toUnit} in '{category}'. {ex.Message}");
            }
        }

        private static TUnit ParseUnit<TUnit>(string unitName) where TUnit : Enum
        {
            if (!Enum.TryParse(typeof(TUnit), unitName, ignoreCase: false, out var result))
                throw new ArgumentException($"Unit '{unitName}' is invalid for {typeof(TUnit).Name}");
            return (TUnit)result;
        }
    }
}
