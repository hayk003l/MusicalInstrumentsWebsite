namespace Service.Contracts
{
    public interface IServiceManager
    {
        IItemService ItemService { get; }
        IOrderService OrderService { get; }
        IShippingDetailsService ShippingDetailsService { get; }
        IDescriptionService DescriptionService { get; }
        IAuthenticationService AuthenticationService { get; }
        IUserContextService UserContextService { get; }
    }

}
