namespace FirstSaturdayOrga.Contracts {
    public enum HeaderColor {
        LightGray,
        Red,
        Green,
        Blue,
        Yellow,
        Orange,
        Purple
    }

    public static class HeaderColorHelpers {
        public static HeaderColor YearToColor(int year) {
            if (year == 2023 || year == 2015)
                return HeaderColor.Red;
            else if (year == 2024 || year == 2016)
                return HeaderColor.Blue;
            else if (year == 2025 || year == 2019)
                return HeaderColor.Green;
            else if (year == 2026 || year == 2020)
                return HeaderColor.Yellow;
            else if (year == 2027)
                return HeaderColor.Orange;
            else if (year == 2028)
                return HeaderColor.Purple;
            else
                throw new ArgumentException($"Unsupported year: {year}");

        }
    }
}
