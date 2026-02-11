using System.Net.Http.Json;

namespace FirstSaturdayOrga.Services.Data {
    public class AllPhotosDataSource {
        private readonly HttpClient _httpClient;

        public AllPhotosDataSource(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<string>> GetAllPhotosAsync(CancellationToken ct = default) {
            return (await _httpClient.GetFromJsonAsync<List<string>>(
            "files/fsphotos.json", ct))?
             .OrderByDescending(p => p)
             .ToList()
             ?? [];
        }
    }
}
