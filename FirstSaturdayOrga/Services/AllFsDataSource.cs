using FirstSaturdayOrga.Data;
using System.Net.Http.Json;

namespace FirstSaturdayOrga.Services {
    public class AllFsDataSource {
        private readonly HttpClient _httpClient;

        public AllFsDataSource(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<AllFsJsonRecord>> GetAllFSDataAsync(CancellationToken ct = default) {
            return (await _httpClient.GetFromJsonAsync<List<AllFsJsonRecord>>(
                "files/allfs.json", ct))
            ?.OrderBy(p => p.Year)
            .ThenBy(p => p.Month)
            .ThenBy(p => p.City)
            .ToList()
            ?? [];
        }

    }
}
