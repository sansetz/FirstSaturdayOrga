using FirstSaturdayOrga.Contracts;
using FirstSaturdayOrga.Services.Data;
using System.Globalization;

namespace FirstSaturdayOrga.Services.App {
    public class DashboardService {
        AllFsDataSource _allFsDataSource;
        AllMapMarkersDataSource _allMapMarkersDataSource;

        public DashboardService(AllFsDataSource allFsDataSource, AllMapMarkersDataSource allMapMarkersDataSource) {
            _allFsDataSource = allFsDataSource;
            _allMapMarkersDataSource = allMapMarkersDataSource;
        }

        public async Task<List<FSEventListItem>> GetAllEventsForYearAsync(int year, CancellationToken ct = default) {
            var allFsData = await _allFsDataSource.GetAllFSDataAsync(ct);

            return allFsData.Where(p => p.Year == year)
                .Select(e => new FSEventListItem(
                    GetMonthNameByNr(e.Month),
                    e.City,
                    e.Province
                )).ToList();
        }
        public async Task<List<FSEventNumbersListItem>> GetAllPostCovidEventsAsync(CancellationToken ct = default) {
            var allFsData = await _allFsDataSource.GetAllFSDataAsync(ct);

            return allFsData.Where(p => p.Year > 2020)
                .Select(e => new FSEventNumbersListItem(
                    e.Year,
                    GetMonthNameByNr(e.Month),
                    e.City,
                    e.AgentsEnl,
                    e.AgentsRes,
                    e.AgentsEnl + e.AgentsRes,
                    e.AgentsEnl > e.AgentsRes ? "ENL" : e.AgentsRes > e.AgentsEnl ? "RES" : "-",
                    e.ApEnl,
                    e.ApRes,
                    e.ApEnl + e.ApRes,
                    e.ApEnl > e.ApRes ? "ENL" : e.ApRes > e.ApEnl ? "RES" : "-"
                )).ToList();
        }

        public async Task<List<RenderMapMarker>> GetAllEventMapMarkersAsync(bool isPreCovid, CancellationToken ct = default) {
            var allFsData = await _allFsDataSource.GetAllFSDataAsync(ct);
            var filteredData = isPreCovid ? allFsData.Where(p => p.Year < 2021) : allFsData.Where(p => p.Year > 2021);

            var allMapMarkers = await _allMapMarkersDataSource.GetAllMapMarkersDataAsync(ct);
            var markerByCity = allMapMarkers.ToDictionary(m => m.Name);

            var renderMarkers =
                filteredData
                    .Where(e => markerByCity.ContainsKey(e.City)) // skip onbekende plaatsen
                    .GroupBy(e => e.City)
                    .SelectMany(g => {
                        var baseMarker = markerByCity[g.Key];

                        // Belangrijk: sorteren zodat index (en dus jitter) stabiel blijft
                        var ordered = g
                            .OrderBy(e => e.Year)
                            .ThenBy(e => e.Month) // als je die hebt; anders weglaten
                            .ToList();

                        var count = ordered.Count;

                        return ordered.Select((e, index) => {
                            var (top, left) = ApplyRadialJitter(
                                baseTopPct: baseMarker.TopPct,
                                baseLeftPct: baseMarker.LeftPct,
                                index: index,
                                count: count
                            );

                            return new RenderMapMarker(
                                City: e.City,
                                Year: e.Year,
                                TopPct: top,
                                LeftPct: left,
                                CssClass: $"marker marker--y{e.Year}"
                            );
                        });
                    })
                    .ToList();

            return renderMarkers;
        }

        private static (double top, double left) ApplyRadialJitter(
            double baseTopPct,
            double baseLeftPct,
            int index,
            int count,
            double baseRadiusPct = 0.9,   // “standaard” spreiding in %
            double extraRadiusPct = 0.18, // extra radius per extra marker
            double maxRadiusPct = 3.0     // max spreiding
        ) {
            if (count <= 1) return (baseTopPct, baseLeftPct);

            // Golden angle zorgt voor een mooie, niet-rijtjesachtige verdeling
            double goldenAngle = Math.PI * (3 - Math.Sqrt(5)); // ~2.39996

            // Radius groeit mee met het aantal, maar begrensd
            var radius = Math.Min(baseRadiusPct + (count - 1) * extraRadiusPct, maxRadiusPct);

            // Spreid punten rondom een cirkel (met golden angle)
            var angle = index * goldenAngle;

            var top = baseTopPct + Math.Sin(angle) * radius;
            var left = baseLeftPct + Math.Cos(angle) * radius;

            // Clamp naar 0..100 zodat het altijd op de kaart blijft
            top = Math.Clamp(top, 0, 100);
            left = Math.Clamp(left, 0, 100);

            return (top, left);
        }

        public async Task<List<FSEventNumbersListItem>> GetAllPostCovidEventsForYearAsync(int year, CancellationToken ct = default) {
            var allFsData = await _allFsDataSource.GetAllFSDataAsync(ct);

            return allFsData.Where(p => p.Year == year)
                .Select(e => new FSEventNumbersListItem(
                    e.Year,
                    GetMonthNameByNr(e.Month),
                    e.City,
                    e.AgentsEnl,
                    e.AgentsRes,
                    e.AgentsEnl + e.AgentsRes,
                    e.AgentsEnl > e.AgentsRes ? "ENL" : e.AgentsRes > e.AgentsEnl ? "RES" : "-",
                    e.ApEnl,
                    e.ApRes,
                    e.ApEnl + e.ApRes,
                    e.ApEnl > e.ApRes ? "ENL" : e.ApRes > e.ApEnl ? "RES" : "-"
                )).ToList();
        }

        private string GetMonthNameByNr(int monthNr) {
            if (monthNr < 1 || monthNr > 12) {
                throw new ArgumentOutOfRangeException(nameof(monthNr), "Month number must be between 1 and 12.");
            }

            var culture = new CultureInfo("nl-NL");
            var monthName = culture.DateTimeFormat.GetMonthName(monthNr);
            return culture.TextInfo.ToTitleCase(monthName);
        }

        public async Task<List<FSEventListItem>> GetAllMapMarkersPreCovidAsync(int year, CancellationToken ct = default) {
            var allFsData = await _allFsDataSource.GetAllFSDataAsync(ct);

            return allFsData.Where(p => p.Year < 2021)
                .Select(e => new FSEventListItem(
                    GetMonthNameByNr(e.Month),
                    e.City,
                    e.Province
                )).ToList();
        }

    }
}
