namespace FirstSaturdayOrga.Contracts {
    public sealed record RenderMapMarker(
        string City,
        int Year,
        double TopPct,
        double LeftPct,
        string CssClass
    );
}
