namespace Silksprite.Loch.Core
{
    public interface ILochDomainSource
    {
        void LoadDomains(ILochDomainLoader loader);
    }
}
