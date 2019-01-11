using Common.DTO;
using Data_Access_Layer;

namespace WebApiServer.Mappers
{
    public class DetailsMapper : Mapper
    {
        public void Map(IBaseEntity entity, Common.DTO.BaseEntity dto)
        {
            dto.CreatedBy = entity.CreatedById.HasValue ? base.Map(entity.CreatedBy) : null;
            dto.LastModifiedBy = entity.LastModifiedById.HasValue ? base.Map(entity.LastModifiedBy) : null;
        }

        public override Common.DTO.City Map(Data_Access_Layer.City entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Attribute Map(Data_Access_Layer.Attribute entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Counterparty Map(Data_Access_Layer.Counterparty entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Invoice Map(Data_Access_Layer.Invoice entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Product Map(Data_Access_Layer.Product entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.User Map(Data_Access_Layer.User entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Role Map(Data_Access_Layer.Role entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.Location Map(Data_Access_Layer.Location entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.GoodsReceivedNote Map(Data_Access_Layer.GoodsReceivedNote entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }

        public override Common.DTO.GoodsDispatchedNote Map(Data_Access_Layer.GoodsDispatchedNote entity)
        {
            var dto = base.Map(entity);
            Map(entity, dto);
            return dto;
        }
    }
}
