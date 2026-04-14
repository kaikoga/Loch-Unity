#if LOCH_NDMF_SUPPORT

using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.localization;
using Silksprite.Loch.Extensions;
using UnityEngine;

namespace Silksprite.Loch.Utils
{
    class WrappedError : SimpleError
    {
        public override ErrorSeverity Severity { get; }

        readonly LocalizedContent _loc;
        readonly ObjectReference? _context;

            #region unused ndmf API
        public override Localizer? Localizer => null;

        public override string? TitleKey => null;

        public override string[]? TitleSubst => null;
        public override string[]? DetailsSubst => null;
        public override string[]? HintSubst => null;
            #endregion

        public WrappedError(ErrorSeverity errorSeverity, LocalizedContent loc, Object? context)
        {
            Severity = errorSeverity;
            _loc = loc;
            AddReference(ObjectRegistry.GetReference(context));
        }

        public override string? FormatTitle()
        {
            return _loc.Tr.SplitCompat("\n").FirstOrDefault();
        }

        public override string FormatDetails()
        {
            return _loc.Tr;
        }

        public override string? FormatHint()
        {
            return null;
        }
    }
}

#endif