using FirstSaturdayOrga.Data;
using System.Net.Http.Json;

namespace FirstSaturdayOrga.Services.Data {
    public class AllMapMarkersDataSource {
        private readonly HttpClient _httpClient;

        public AllMapMarkersDataSource(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<MapMarkerJsonRecord>> GetAllMapMarkersDataAsync(CancellationToken ct = default) {
            return (await _httpClient.GetFromJsonAsync<List<MapMarkerJsonRecord>>(
                "files/mapmarkers.json", ct))
            ?.OrderBy(p => p.Name)
            .ToList()
            ?? [];
        }
    }
}
