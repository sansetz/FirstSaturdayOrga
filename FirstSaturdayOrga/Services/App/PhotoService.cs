using FirstSaturdayOrga.Services.Data;

namespace FirstSaturdayOrga.Services.App {
    public class PhotoService {
        AllPhotosDataSource _allPhotosDataSource;

        public PhotoService(AllPhotosDataSource allPhotosDataSource) {
            _allPhotosDataSource = allPhotosDataSource;
        }

        public async Task<List<string>> GetAllPhotosAsync(CancellationToken ct = default) {
            return await _allPhotosDataSource.GetAllPhotosAsync(ct);
        }
    }
}
