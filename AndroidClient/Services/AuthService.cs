using Android.App;
using Client.Models;
using Common;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service
    {
        public AuthService(
            Activity activity
            ) : base(activity,"api/auth")
        {
        }
        
        public async Task<HttpResult<bool>> Login(Login model)
        {
            return await Post(model,"/login");
        }
    }
}