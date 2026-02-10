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
