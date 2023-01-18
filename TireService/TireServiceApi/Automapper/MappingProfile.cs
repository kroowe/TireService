using AutoMapper;
using TireService.Dtos.Infos.AppSettingConstant;
using TireService.Dtos.Infos.CatalogItem;
using TireService.Dtos.Infos.Category;
using TireService.Dtos.Infos.Client;
using TireService.Dtos.Infos.Order;
using TireService.Dtos.Infos.PaymentRule;
using TireService.Dtos.Infos.SalaryPaymentsToWorker;
using TireService.Dtos.Infos.ShiftWorks;
using TireService.Dtos.Infos.Warehouse;
using TireService.Dtos.Infos.Worker;
using TireService.Dtos.Views;
using TireService.Dtos.Views.AppSettingConstant;
using TireService.Dtos.Views.CatalogItem;
using TireService.Dtos.Views.Category;
using TireService.Dtos.Views.Client;
using TireService.Dtos.Views.Order;
using TireService.Dtos.Views.PaymentRule;
using TireService.Dtos.Views.Reports;
using TireService.Dtos.Views.SalaryPaymentsToWorker;
using TireService.Dtos.Views.ShiftWorkView;
using TireService.Dtos.Views.Warehouse;
using TireService.Dtos.Views.Worker;
using TireService.Infrastructure.Entities;
using TireService.Infrastructure.Entities.Settings;
using OrderStatusInfo = TireService.Dtos.Views.Order.OrderStatusInfo;

namespace TireServiceApi.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gender, GenderInfo>().ReverseMap();
            CreateMap<Client, ClientView>();
            CreateMap<WarehouseNomenclature, WarehouseNomenclatureView>();
            CreateMap<WarehouseNomenclature, WarehouseNomenclatureWithCountView>()
                .ForMember(target => target.Count, 
                    config => config.MapFrom(x => CalculateWarehouseNomenclatureFullViewCount(x.WarehouseItemHistories)));
            CreateMap<CatalogItem, CatalogItemView>();
            CreateMap<Category, CategoryView>()
                .ForMember(target => target.CategoryPath, config => config.MapFrom(x => x.CategoryPath.ToString()))
                .ForMember(target => target.LevelCategoryPath, config => config.MapFrom(x => x.CategoryPath.ToString().Split(".", StringSplitOptions.None).Length));
            CreateMap<SalaryType, SalaryTypeInfo>().ReverseMap();
            CreateMap<PaymentRule, PaymentRuleView>();
            CreateMap<Worker, WorkerView>();
            CreateMap<Worker, WorkerWithBalanceAndSalaryPaymentView>()
                .ForMember(x => x.BalanceSum, config => config.MapFrom(x => x.WorkerBalance.Balance));

            CreateMap<ClientCreateInfo, Client>();
            CreateMap<ClientUpdateInfo, Client>();
            CreateMap<WarehouseNomenclatureInfo, WarehouseNomenclature>();
            CreateMap<CatalogItemInfo, CatalogItem>();
            CreateMap<CategoryInfo, Category>()
                .ForMember(target => target.CatalogItems, config => config.Ignore());
            CreateMap<PaymentRuleInfo, PaymentRule>();
            CreateMap<WorkerCreateInfo, Worker>();
            CreateMap<WorkerUpdateInfo, Worker>();
            CreateMap<ShiftWorkCreateInfo, ShiftWork>();
            CreateMap<ShiftWork, ShiftWorkView>();
            CreateMap<ShiftWorkWorker, ShiftWorkWorkerView>()
                .ForMember(x => x.Name, config => config.MapFrom(x => x.Worker.Name))
                .ForMember(x => x.Surname, config => config.MapFrom(x => x.Worker.Surname))
                .ForMember(x => x.Patronymic, config => config.MapFrom(x => x.Worker.Patronymic))
                .ForMember(x => x.Position, config => config.MapFrom(x => x.Worker.Position));
            CreateMap<ShiftWork, ShiftWorkWithWorkersView>();
            CreateMap<AppSettingConstant, AppSettingConstantView>();
            CreateMap<AppSettingConstantCreateInfo, AppSettingConstant>();
            CreateMap<AppSettingConstantUpdateInfo, AppSettingConstant>();
            CreateMap<OrderCreateInfo, Order>()
                .ForMember(x => x.TaskOrders, config => config.Ignore());
            CreateMap<OrderUpdateInfo, Order>()
                .ForMember(x => x.TaskOrders, config => config.Ignore());
            CreateMap<OrderStatus, OrderStatusInfo>().ReverseMap();
            CreateMap<Order, OrderView>();
            CreateMap<TaskOrder, TaskOrderView>();
            CreateMap<SalaryPaymentsToWorkerInfo, SalaryPaymentsToWorker>();
            CreateMap<SalaryPaymentsToWorker, SalaryPaymentsToWorkerView>();
            
        }

        private static double CalculateWarehouseNomenclatureFullViewCount(
            ICollection<WarehouseItemHistory> warehouseItemHistories)
        {
            if (warehouseItemHistories == null)
                return 0;
            var entranceCount = warehouseItemHistories
                .Where(x => x.WarehouseItemHistoryType == WarehouseItemHistoryType.Entrance)
                .Sum(x => x.Count);
            var writeOffCount = warehouseItemHistories
                .Where(x => x.WarehouseItemHistoryType == WarehouseItemHistoryType.WriteOff)
                .Sum(x => x.Count);
            return entranceCount - writeOffCount;
        }
    }
}
