using System.Linq;
using JetBrains.Annotations;
using Silksprite.Loch.Extensions;
using UnityEngine;

#if LOCH_NDMF_SUPPORT
using nadena.dev.ndmf;
using nadena.dev.ndmf.localization;
#endif

namespace Silksprite.Loch.Utils
{
    [PublicAPI]
    public static class ErrorReportWrapper
    {
        public static void LogWarningFormat(LocalizedContent loc, Substitution substitution)
        {
#if LOCH_NDMF_SUPPORT
            ErrorReport.ReportError(new WrappedError(ErrorSeverity.NonFatal, loc, null, substitution));
#else
            Debug.LogWarningFormat(substitution.Format(loc.Tr), null);
#endif
        }

        public static void LogWarningFormat(LocalizedContent loc, Object target, Substitution substitution)
        {
#if LOCH_NDMF_SUPPORT
            ErrorReport.ReportError(new WrappedError(ErrorSeverity.NonFatal, loc, target, substitution));
#else
            Debug.LogWarningFormat(substitution.Format(loc.Tr), target);
#endif
        }
        
#if LOCH_NDMF_SUPPORT
        class WrappedError : SimpleError
        {
            public override ErrorSeverity Severity { get; }

            readonly LocalizedContent _loc;
            readonly ObjectReference? _context;
            readonly Substitution _substitution;

            #region unused ndmf API
            public override Localizer? Localizer => null;

            public override string? TitleKey => null;

            public override string[]? TitleSubst => null;
            public override string[]? DetailsSubst => null;
            public override string[]? HintSubst => null;
            #endregion

            public WrappedError(ErrorSeverity errorSeverity, LocalizedContent loc, Object? context, Substitution substitution)
            {
                Severity = errorSeverity;
                _loc = loc;
                _substitution = substitution;
                AddReference(ObjectRegistry.GetReference(context));
            }

            public override string? FormatTitle()
            {
                return _loc.Format(_substitution).Tr.SplitCompat("\n").FirstOrDefault();
            }

            public override string FormatDetails()
            {
                return _loc.Format(_substitution).Tr;
            }

            public override string? FormatHint()
            {
                return null;
            }
        }
#endif
    }
}