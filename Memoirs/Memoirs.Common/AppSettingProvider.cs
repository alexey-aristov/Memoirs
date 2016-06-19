using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Common
{
    public class AppSettingProvider : IAppSettingsProvider
    {
        private IUnitOfWork _unitOfWork;
        public AppSettingProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string OauthGoogleClientSecret => _unitOfWork.AppSettingsRepository.Get().First(a => a.Key == "OauthGoogleClientSecret").Value;
        public string OauthGoogleClientId => _unitOfWork.AppSettingsRepository.Get().First(a => a.Key == "OauthGoogleClientId").Value;
    }
}
