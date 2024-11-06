using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Entities;
using HastaneYonetimSistemiApp.Data.Repostories;
using HastaneYonetimSistemiApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }

       
        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetId(1);
            setting.MaintenenceMode = !setting.MaintenenceMode;
            _settingRepository.Update(setting);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Bakım modu sırasında bir hata oluştu.");
            }
            
        }
        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetId(1).MaintenenceMode;
            return maintenanceState;
        }

    }
}
