namespace PedaloWebApp.Core.Interfaces.Data
{
    using PedaloWebApp.Core.Domain;

    public interface IDbContextFactory
    {
        PedaloContext CreateContext();
        PedaloContext CreateReadOnlyContext();
    }
}
