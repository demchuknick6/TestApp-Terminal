namespace TestApp.Terminal.Services.Interfaces
{
    public interface IScanningService : IService
    {
        void Scan(string productCode);
    }
}
