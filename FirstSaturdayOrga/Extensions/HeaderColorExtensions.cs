using FirstSaturdayOrga.Contracts;

namespace FirstSaturdayOrga.Extensions {
    public static class HeaderColorExtensions {
        public static string ToCssClass(this HeaderColor color) =>
        color switch {
            HeaderColor.Red => "header--red",
            HeaderColor.Blue => "header--blue",
            HeaderColor.Green => "header--green",
            HeaderColor.Yellow => "header--yellow",
            HeaderColor.Orange => "header--orange",
            _ => "header--red"
        };
    }
}
