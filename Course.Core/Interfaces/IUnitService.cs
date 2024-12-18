using Course.Shared.DTOs;
using Course.Shared.Records;
using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Interfaces
{
    public interface IUnitService
    {
        public Task<BaseResponse<UnitDTO>> CreateUnitAsync(UnitDTO unit);
        public Task<BaseResponse<UnitDTO>> UpdateUnitAsync(UnitDTO unit);
        public Task<BaseResponse<UnitDTO>> RemoveUnitAsync(int unitId);
    }
}
