using Course.BLL.Interfaces;
using Course.DAL;
using Course.DAL.Models;
using Course.Shared;
using Course.Shared.Constants;
using Course.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public class UnitService : IUnitService
    {
        private readonly AppDbContext _db;
        public UnitService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse<UnitDTO>> CreateUnitAsync(UnitDTO unit)
        {
            try
            {
                int maxUnitId = 0;

                if (_db.Units.Any())
                    maxUnitId = await _db.Units.MaxAsync(c => c.Id);

                var entity = new Unit
                {
                    Id = maxUnitId + 1,
                    Name = unit.Name,
                    CourseId = unit.CourseId
                };

                _db.Units.Add(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<UnitDTO>(unit, Messages.AddedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UnitDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<UnitDTO>> RemoveUnitAsync(int unitId)
        {
            try
            {
                var entity = await _db.Units.FindAsync(unitId);
                if (entity == null)
                {
                    return new BaseResponse<UnitDTO>(null, Messages.NotFound, [], false);
                }

                _db.Units.Remove(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<UnitDTO>(null, Messages.RemovedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UnitDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }

        public async Task<BaseResponse<UnitDTO>> UpdateUnitAsync(UnitDTO unit)
        {
            try
            {
                var entity = await _db.Units.FindAsync(unit.Id);
                if (entity == null)
                {
                    return new BaseResponse<UnitDTO>(null, Messages.NotFound, [], false);
                }

                entity.Name = unit.Name;

                _db.Units.Update(entity);
                await _db.SaveChangesAsync();

                return new BaseResponse<UnitDTO>(unit, Messages.UpdatedSuccessfully);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UnitDTO>(null, Messages.Error, new List<string> { ex.Message }, false);
            }
        }
    }
}
