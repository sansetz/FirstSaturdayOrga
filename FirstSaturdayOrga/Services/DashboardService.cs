using FirstSaturdayOrga.Contracts;
using System.Globalization;

namespace FirstSaturdayOrga.Services {
    public class DashboardService {
        AllFsDataSource _allFsDataSource;
        public DashboardService(AllFsDataSource allFsDataSource) {
            _allFsDataSource = allFsDataSource;
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
    }
}
