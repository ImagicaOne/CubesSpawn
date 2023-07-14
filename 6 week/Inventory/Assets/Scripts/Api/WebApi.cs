using Api.Services;
using Extensions;

namespace Api
{
    public class WebApi : Singleton<WebApi>
    {
        public string JwtToken { get; set; }
        public AuthenticationApi AuthenticationAPI { get; private set; }
        
        public InventoryApi InventoryAPI { get; private set; }
        
        public ShopApi ShopAPI { get; private set; }
        
        public MoneyApi MoneyAPI { get; private set; }
        
        protected override void Awake()
        {
            CreateApiServices();
        }

        private void CreateApiServices()
        {
            AuthenticationAPI = new AuthenticationApi();
            InventoryAPI = new InventoryApi();
            ShopAPI = new ShopApi();
            MoneyAPI = new MoneyApi();
        }
    }
}