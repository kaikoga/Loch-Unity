using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.Base
{
    public static class LochElement
    {
#if !UNITY_2023_2_OR_NEWER
        static readonly Assembly DefaultAssembly = typeof(LocalizedContent).Assembly;

        public class UxmlTraits<TTraits> : UxmlTraits
            where TTraits : UxmlTraits, new()
        {
            readonly UxmlStringAttributeDescription _loc = new UxmlStringAttributeDescription { name = "loc" };
            readonly UxmlStringAttributeDescription _text = new UxmlStringAttributeDescription { name = "text" };
            readonly UxmlStringAttributeDescription _label = new UxmlStringAttributeDescription { name = "label" };

            readonly TTraits _traits = new TTraits();

            public override IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription => _traits.uxmlAttributesDescription;
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription =>  _traits.uxmlChildElementsDescription;

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                _traits.Init(ve, bag, cc);

                if (ve is ILocElement locElement)
                {
                    if (ve is ILocTextElement locTextElement)
                    {
                        var text = "";
                        if (_text.TryGetValueFromBag(bag, cc, ref text))
                        {
                            locTextElement.text = text;
                        }
                    }

                    if (ve is ILocField locField)
                    {
                        var label = "";
                        if (_label.TryGetValueFromBag(bag, cc, ref label))
                        {
                            locField.label = label;
                        }
                    }

                    {
                        var locValue = "";
                        if (_loc.TryGetValueFromBag(bag, cc, ref locValue))
                        {
                            locElement.loc = new LocalizedContent(locValue, DefaultAssembly);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"{ve.GetType()} is not a {nameof(ILocElement)}");
                }

            }
        }
#endif
    }
}
