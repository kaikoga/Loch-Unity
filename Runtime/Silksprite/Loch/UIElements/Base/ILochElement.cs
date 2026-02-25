using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Silksprite.Loch.UIElements.Base
{
    public interface ILochElement
    {
        void LochRefresh();
    }

    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ILocElement : ILochElement
    {
        LocalizedContent? loc { get; set; }
    }
    
    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ILocField : ILocElement
    {
        string? label { get; set; }
    }
    
    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface ILocTextElement : ILocElement
    {
        string? text { get; set; }
    }
}
