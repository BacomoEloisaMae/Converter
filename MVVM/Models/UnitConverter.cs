namespace Converter.MVVM.Models
{
    public static class UnitConverter
    {
        public static double Convert(string category, double value, string from, string to)
        {
            return category switch
            {
                "Length" => ConvertLength(value, from, to),
                "Mass" => ConvertMass(value, from, to),
                "Temperature" => ConvertTemp(value, from, to),
                "Current" => value,
                "Area" => ConvertArea(value, from, to),
                "Volume" => ConvertVolume(value, from, to),
                "Intensity" => value,
                _ => value,
            };
        }


        static double ConvertLength(double v, string f, string t)
        {
            double meters = f switch
            {
                "Meters" => v,
                "Centimeters" => v / 100,
                "Kilometers" => v * 1000,
                _ => v
            };


            return t switch
            {
                "Meters" => meters,
                "Centimeters" => meters * 100,
                "Kilometers" => meters / 1000,
                _ => meters
            };
        }


        static double ConvertMass(double v, string f, string t)
        {
            double kg = f switch
            {
                "Kilograms" => v,
                "Grams" => v / 1000,
                "Pounds" => v * 0.453592,
                _ => v
            };


            return t switch
            {
                "Kilograms" => kg,
                "Grams" => kg * 1000,
                "Pounds" => kg / 0.453592,
                _ => kg
            };
        }


        static double ConvertTemp(double v, string f, string t)
        {
            double c = f switch
            {
                "Celsius" => v,
                "Fahrenheit" => (v - 32) * 5 / 9,
                "Kelvin" => v - 273.15,
                _ => v
            };


            return t switch
            {
                "Celsius" => c,
                "Fahrenheit" => (c * 9 / 5) + 32,
                "Kelvin" => c + 273.15,
                _ => c
            };
        }


        static double ConvertArea(double v, string f, string t)
        {
            double sqm = f switch
            {
                "Square Meters" => v,
                "Square Centimeters" => v / 10000,
                "Square Kilometers" => v * 1_000_000,
                _ => v
            };


            return t switch
            {
                "Square Meters" => sqm,
                "Square Centimeters" => sqm * 10000,
                "Square Kilometers" => sqm / 1_000_000,
                _ => sqm
            };
        }


        static double ConvertVolume(double v, string f, string t)
        {
            double liters = f switch
            {
                "Liters" => v,
                "Milliliters" => v / 1000,
                "Cubic Meters" => v * 1000,
                _ => v
            };


            return t switch
            {
                "Liters" => liters,
                "Milliliters" => liters * 1000,
                "Cubic Meters" => liters / 1000,
                _ => liters
            };
        }
    }
}