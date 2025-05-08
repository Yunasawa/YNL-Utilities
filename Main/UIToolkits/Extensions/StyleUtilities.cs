using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;

namespace YNL.Utilities.UIToolkits
{
    public static class EStyle
    {
        #region ✦ Style - Extension

        #region ▶ Element Style - Containing

        /// <summary>
        /// Add elements to a container element.
        /// </summary>
        public static T AddElements<T>(this T container, params VisualElement[] elements) where T : VisualElement
        {
            foreach (var element in elements) container.Add(element);
            return container;
        }

        /// <summary>
        /// Insert elements to a container element.
        /// </summary>
        public static T InsertElements<T>(this T container, int index = 0, params VisualElement[] elements) where T : VisualElement
        {
            foreach (var element in elements) container.Insert(index, element);
            return container;
        }

        /// <summary>
        /// Remove all child elements from a container element.
        /// </summary>
        public static T RemoveAllElements<T>(this T container) where T : VisualElement
        {
            foreach (var element in container.Children().ToList()) element.RemoveFromHierarchy();
            return container;
        }

        #endregion
        #region ▶ Element Style - Class

        /// <summary>
        /// Add classes to a visual element.
        /// </summary>
        public static T AddClass<T>(this T element, params string[] classes) where T : VisualElement
        {
            foreach (var className in classes)
            {
                if (!element.ClassListContains(className)) element.AddToClassList(className);
            }
            return element;
        }

        /// <summary>
        /// Add classes to a visual element.
        /// </summary>
        public static T EnableClass<T>(this T element, bool enable, params string[] classes) where T : VisualElement
        {
            foreach (var className in classes) element.EnableInClassList(className, enable);
            return element;
        }
        public static T EnableClass<T>(this T element, params string[] classes) where T : VisualElement
            => element.EnableClass(true, classes);
        public static T DisableClass<T>(this T element, params string[] classes) where T : VisualElement
            => element.EnableClass(false, classes);

        /// <summary>
        /// Remove classes from a visual element.
        /// </summary>
        public static T RemoveClass<T>(this T element, params string[] classes) where T : VisualElement
        {
            foreach (var className in classes)
            {
                if (element.ClassListContains(className)) element.RemoveFromClassList(className);
            }
            return element;
        }

        #endregion
        #region ▶ Element Style - Style Sheet

        public static T AddStyle<T>(this T element, params StyleSheet[] styles) where T : VisualElement
        {
            foreach (var style in styles)
            {
                if (!element.styleSheets.Contains(style)) element.styleSheets.Add(style);
            }
            return element;
        }

        public static T AddStyle<T>(this T element, params string[] stylePaths) where T : VisualElement
        {
#if UNITY_EDITOR
            foreach (var stylePath in stylePaths) element.AddStyle(Resources.Load<StyleSheet>(stylePath));
#endif
            return element;
        }

        public static T RemoveStyle<T>(this T element, params StyleSheet[] styles) where T : VisualElement
        {
            foreach (var style in styles)
            {
                if (element.styleSheets.Contains(style)) element.styleSheets.Remove(style);
            }
            return element;
        }

        public static T RemoveStyle<T>(this T element, params string[] stylePaths) where T : VisualElement
        {
#if UNITY_EDITOR
            foreach (var stylePath in stylePaths)
            {
                StyleSheet style = Resources.Load<StyleSheet>(stylePath);
                for (int i = 0; i < element.styleSheets.count; i++)
                {
                    if (element.styleSheets[i].name == style.name) element.styleSheets.Remove(element.styleSheets[i]);
                }
            }
#endif
            return element;
        }

        #endregion
        #region ▶ Element Style - Space

        public static T AddSpace<T>(this T target, float width, float height) where T : VisualElement =>
            target.AddElements(new VisualElement().SetName("Space").SetSize(width, height));

        public static T AddHSpace<T>(this T target, float height) where T : VisualElement =>
            target.AddElements(new VisualElement().SetName("HSpace").SetWidth(height));

        public static T AddVSpace<T>(this T target, float width) where T : VisualElement =>
            target.AddElements(new VisualElement().SetName("VSpace").SetHeight(width));

        #endregion

        #endregion
        #region ✦ Style - Attribute

        #region ▶ Element Style - Name

        /// <summary>
        /// Set name of a visual element.
        /// </summary>
        public static T SetName<T>(this T element, string nane) where T : VisualElement
        {
            element.name = nane;
            return element;
        }

        /// <summary>
        /// Get name of a visual element.
        /// </summary>
        public static string GetName<T>(this T element) where T : VisualElement
            => element.name;

        #endregion
        #region ▶ Element Style - Picking Mode
        public static T SetPickingMode<T>(this T element, PickingMode mode) where T : VisualElement
        {
            element.pickingMode = mode;
            return element;
        }

        public static PickingMode GetPickingMode<T>(this T element) where T : VisualElement
        {
            return element.pickingMode;
        }
        #endregion
        #region ▶ Element Style - Tooltip
        public static T SetTooltip<T>(this T element, string text) where T : VisualElement
        {
            element.tooltip = text;
            return element;
        }
        #endregion
        public static T SetText<T>(this T element, string text) where T : UnityEngine.UIElements.TextElement
        {
            element.text = text;
            return element;
        }
        #endregion
        #region ✦ Style - Display

        #region ▶ Element Style - Opacity
        public static T SetOpacity<T>(this T element, float value = 100) where T : VisualElement
        {
            element.style.opacity = value;
            return element;
        }

        public static float GetOpacity<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.opacity;
        }
        #endregion
        #region ▶ Element Style - Display
        public static T SetDisplay<T>(this T element, DisplayStyle style) where T : VisualElement
        {
            element.style.display = style;
            return element;
        }

        public static DisplayStyle GetDisplay<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.display;
        }
        #endregion
        #region ▶ Element Style - Visibility
        public static T SetVisibility<T>(this T element, Visibility visibility) where T : VisualElement
        {
            element.style.visibility = visibility;
            return element;
        }

        public static Visibility GetVisibility<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.visibility;
        }
        #endregion
        #region ▶ Element Style - Overflow
        public static T SetOverflow<T>(this T element, Overflow value) where T : VisualElement
        {
            element.style.overflow = value;
            return element;
        }

        public static Overflow GetOverflow<T>(this T element) where T : VisualElement
        {
            return element.style.overflow.value;
        }
        #endregion

        #endregion
        #region ✦ Style - Position

        #region ▶ Element Style - Position
        public static T SetTop<T>(this T target, float value) where T : VisualElement
        {
            target.style.top = value;
            return target;
        }

        public static T SetLeft<T>(this T target, float value) where T : VisualElement
        {
            target.style.left = value;
            return target;
        }

        public static T SetRight<T>(this T target, float value) where T : VisualElement
        {
            target.style.right = value;
            return target;
        }

        public static T SetBottom<T>(this T target, float value) where T : VisualElement
        {
            target.style.bottom = value;
            return target;
        }

        public static T SetPosition<T>(this T target, float top, float right, float bottom, float left) where T : VisualElement
        {
            target.SetTop(top).SetRight(right).SetBottom(bottom).SetLeft(left);
            return target;
        }
        #endregion
        #region ▶ Element Style - Position Mode
        public static T SetPositionMode<T>(this T element, Position position) where T : VisualElement
        {
            element.style.position = position;
            return element;
        }

        public static Position GetPositionMode<T>(this T element) where T : VisualElement
        {
            return element.style.position.value;
        }
        #endregion

        #endregion
        #region ✦ Style - Flex

        #region ▶ Element Style - Flex Basis
        public static T SetFlexBasis<T>(this T element, Length value) where T : VisualElement
        {
            element.style.flexBasis = value;
            return element;
        }

        public static Length GetFlexBasis<T>(this T element) where T : VisualElement
        {
            return element.style.flexBasis.value;
        }
        #endregion
        #region ▶ Element Style - Flex Grow
        public static T SetFlexGrow<T>(this T element, float value) where T : VisualElement
        {
            element.style.flexGrow = value;
            return element;
        }

        public static float GetFlexGrow<T>(this T element) where T : VisualElement
        {
            return element.style.flexGrow.value;
        }
        #endregion
        #region ▶ Element Style - Flex Shrink
        public static T SetFlexShrink<T>(this T element, float value) where T : VisualElement
        {
            element.style.flexShrink = value;
            return element;
        }

        public static float GetFlexShrink<T>(this T element) where T : VisualElement
        {
            return element.style.flexShrink.value;
        }
        #endregion
        #region ▶ Element Style - Flex Direction

        /// <summary> 
        /// Set the direction of the main axis to layout children in a container
        /// </summary>
        public static T SetFlexDirection<T>(this T element, FlexDirection value) where T : VisualElement
        {
            element.style.flexDirection = new StyleEnum<FlexDirection>(value);
            return element;
        }

        /// <summary> 
        /// Get the direction of the main axis to layout children in a container
        /// </summary>
        public static FlexDirection GetFlexDirection<T>(this T element) where T : VisualElement
            => element.style.flexDirection.value;

        #endregion
        #region ▶ Element Style - Flex Wrap
        public static T SetFlexWrap<T>(this T element, Wrap value) where T : VisualElement
        {
            element.style.flexWrap = value;
            return element;
        }

        public static Wrap GetFlexWrap<T>(this T element) where T : VisualElement
        {
            return element.style.flexWrap.value;
        }
        #endregion

        #endregion
        #region ✦ Style - Align

        #region ▶ Element Style - Align Self

        /// <summary>
        /// Align this element itself.
        /// </summary>
        public static T SetAlignSelf<T>(this T target, Align value) where T : VisualElement
        {
            target.style.alignSelf = new StyleEnum<Align>(value);
            return target;
        }

        /// <summary>
        /// Get align-self value of this element.
        /// <para/> style.alignSelf
        public static Align GetAlignSelf<T>(this T target) where T : VisualElement =>
            target.style.alignSelf.value;

        #endregion
        #region ▶ Element Style - Align Items
        public static T SetAlignItems<T>(this T element, Align value) where T : VisualElement
        {
            element.style.alignItems = value;
            return element;
        }

        public static Align GetAlignItems<T>(this T element) where T : VisualElement
        {
            return element.style.alignItems.value;
        }
        #endregion
        #region ▶ Element Style - Align Content
        public static T SetAlignContent<T>(this T element, Align value) where T : VisualElement
        {
            element.style.alignContent = value;
            return element;
        }

        public static Align GetAlignContent<T>(this T element) where T : VisualElement
        {
            return element.style.alignContent.value;
        }
        #endregion
        #region ▶ Element Style - Justify Content
        public static T SetJustifyContent<T>(this T element, Justify value) where T : VisualElement
        {
            element.style.justifyContent = value;
            return element;
        }

        public static Justify GetJustifyContent<T>(this T element) where T : VisualElement
        {
            return element.style.justifyContent.value;
        }
        #endregion

        #endregion
        #region ✦ Style - Size

        #region ▶ Element Style - Height

        #region ▶ Element Style - Height
        public static T SetHeight<T>(this T element, float height, bool percent = false) where T : VisualElement
        {
            if (percent) element.style.height = new Length(height, LengthUnit.Percent);
            else element.style.height = height;
            return element;
        }
        public static T SetHeight<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.height = new StyleLength(styleKeyword);
            return target;
        }
        public static T ResetHeight<T>(this T target) where T : VisualElement
            => target.SetHeight(StyleKeyword.Auto);
        public static float GetHeight<T>(this T target) where T : VisualElement
            => target.style.height.value.value;
        #endregion
        #region ▶ Element Style - Min Height
        public static T SetMinHeight<T>(this T element, float Height) where T : VisualElement
        {
            element.style.minHeight = Height;
            return element;
        }
        public static T SetMinHeight<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.minHeight = new StyleLength(styleKeyword);
            return target;
        }
        public static T ResetMinHeight<T>(this T target) where T : VisualElement
            => target.SetHeight(StyleKeyword.Auto);
        public static float GetMinHeight<T>(this T target) where T : VisualElement
            => target.style.minHeight.value.value;
        #endregion
        #region ▶ Element Style - Max Height
        public static T SetMaxHeight<T>(this T element, float Height) where T : VisualElement
        {
            element.style.maxHeight = Height;
            return element;
        }
        public static T SetMaxHeight<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.maxHeight = new StyleLength(styleKeyword);
            return target;
        }
        public static T ResetMaxHeight<T>(this T target) where T : VisualElement
            => target.SetHeight(StyleKeyword.Auto);
        public static float GetMaxHeight<T>(this T target) where T : VisualElement
            => target.style.maxHeight.value.value;
        #endregion

        #endregion
        #region ▶ Element Style - Width

        #region ▶ Element Style - Width
        public static T SetWidth<T>(this T element, float width, bool percent = false) where T : VisualElement
        {
            if (percent) element.style.width = new Length(width, LengthUnit.Percent);
            else element.style.width = width;
            return element;
        }
        public static T SetWidth<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.width = new StyleLength(styleKeyword);
            return target;
        }

        public static T ResetWidth<T>(this T target) where T : VisualElement
            => target.SetWidth(StyleKeyword.Auto);
        public static float GetWidth<T>(this T target) where T : VisualElement
            => target.style.width.value.value;
        #endregion
        #region ▶ Element Style - Min Width
        public static T SetMinWidth<T>(this T element, float width, bool percent = false) where T : VisualElement
        {
            if (percent) element.style.minWidth = new Length(width, LengthUnit.Percent);
            else element.style.minWidth = width;
            return element;
        }
        public static T SetMinWidth<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.minWidth = new StyleLength(styleKeyword);
            return target;
        }
        public static T ResetMinWidth<T>(this T target) where T : VisualElement
            => target.SetWidth(StyleKeyword.Auto);
        public static float GetMinWidth<T>(this T target) where T : VisualElement
            => target.style.minWidth.value.value;
        #endregion
        #region ▶ Element Style - Max Width
        public static T SetMaxWidth<T>(this T element, float width, bool percent = false) where T : VisualElement
        {
            if (!percent) element.style.maxWidth = new Length(width, LengthUnit.Percent);
            else element.style.maxWidth = width;
            return element;
        }
        public static T SetMaxWidth<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.style.maxWidth = new StyleLength(styleKeyword);
            return target;
        }
        public static T ResetMaxWidth<T>(this T target) where T : VisualElement
            => target.SetWidth(StyleKeyword.Auto);
        public static float GetMaxWidth<T>(this T target) where T : VisualElement
            => target.style.maxWidth.value.value;
        #endregion

        #endregion
        #region ▶ Element Style - Size

        #region ▶ Element Style - Size

        /// <summary>
        /// Set the fixed values for the width and height of an element for the layout
        /// </summary>
        public static T SetSize<T>(this T target, float width, float height) where T : VisualElement =>
            target.SetWidth(width).SetHeight(height);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetSize<T>(this T target, float value) where T : VisualElement =>
            target.SetSize(value, value);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetSize<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.SetWidth(styleKeyword).SetHeight(styleKeyword);
            return target;
        }

        /// <summary>
        /// Set the width and height of an element to auto
        /// </summary>
        public static T ResetSize<T>(this T target) where T : VisualElement =>
            target.SetSize(StyleKeyword.Auto);

        #endregion
        #region ▶ Element Style - Min Size

        /// <summary>
        /// Set the fixed values for the width and height of an element for the layout
        /// </summary>
        public static T SetMinSize<T>(this T target, float width, float height) where T : VisualElement =>
            target.SetMinWidth(width).SetMinHeight(height);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetMinSize<T>(this T target, float value) where T : VisualElement =>
            target.SetMinSize(value, value);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetMinSize<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.SetMinWidth(styleKeyword).SetMinHeight(styleKeyword);
            return target;
        }

        /// <summary>
        /// Set the width and height of an element to auto
        /// </summary>
        public static T ResetMinSize<T>(this T target) where T : VisualElement =>
            target.SetMinSize(StyleKeyword.Auto);

        #endregion
        #region ▶ Element Style - Max Size

        /// <summary>
        /// Set the fixed values for the width and height of an element for the layout
        /// </summary>
        public static T SetMaxSize<T>(this T target, float width, float height) where T : VisualElement =>
            target.SetMaxWidth(width).SetMaxHeight(height);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetMaxSize<T>(this T target, float value) where T : VisualElement =>
            target.SetMaxSize(value, value);

        /// <summary>
        /// Set the same fixed value for the width and height of an element for the layout
        /// </summary>
        public static T SetMaxSize<T>(this T target, StyleKeyword styleKeyword) where T : VisualElement
        {
            target.SetMaxWidth(styleKeyword).SetMaxHeight(styleKeyword);
            return target;
        }

        /// <summary>
        /// Set the width and height of an element to auto
        /// </summary>
        public static T ResetMaxSize<T>(this T target) where T : VisualElement =>
            target.SetMaxSize(StyleKeyword.Auto);

        #endregion

        #endregion

        #endregion
        #region ✦ Style - Spacing

        #region ▶ Element Style - Margin

        public static T SetMarginLeft<T>(this T element, float value) where T : VisualElement
        {
            element.style.marginLeft = value;
            return element;
        }
        public static float GetMarginLeft<T>(this T element) where T : VisualElement
            => element.resolvedStyle.marginLeft;
        public static T SetMarginTop<T>(this T element, float value) where T : VisualElement
        {
            element.style.marginTop = value;
            return element;
        }
        public static float GetMarginTop<T>(this T element) where T : VisualElement
            => element.resolvedStyle.marginTop;
        public static T SetMarginRight<T>(this T element, float value) where T : VisualElement
        {
            element.style.marginRight = value;
            return element;
        }
        public static float GetMarginRight<T>(this T element) where T : VisualElement
            => element.resolvedStyle.marginRight;
        public static T SetMarginBottom<T>(this T element, float value) where T : VisualElement
        {
            element.style.marginBottom = value;
            return element;
        }
        public static float GetMarginBottom<T>(this T element) where T : VisualElement
            => element.resolvedStyle.marginBottom;

        /// <summary> 
        /// Set all margins to 0 (zero) 
        /// </summary>
        public static T ClearMargin<T>(this T element) where T : VisualElement
            => element.SetMargin(0);

        /// <summary>
        /// Set the space reserved for the all the edges margins during the layout phase (Left, Top, Right, Bottom)
        /// </summary>
        public static T SetMargin<T>(this T element, float left, float top, float right, float bottom) where T : VisualElement
        {
            element.SetMarginLeft(left).SetMarginTop(top).SetMarginRight(right).SetMarginBottom(bottom);
            return element;
        }

        /// <summary>
        /// Set the same space reserved for the all the edges margins during the layout phase (Left, Top, Right, Bottom).
        /// </summary>
        public static T SetMargin<T>(this T element, float value) where T : VisualElement =>
            element.SetMargin(value, value, value, value);

        #endregion
        #region ▶ Element Style - Padding
        public static T SetPaddingLeft<T>(this T element, float value) where T : VisualElement
        {
            element.style.paddingLeft = value;
            return element;
        }
        public static float GetPaddingLeft<T>(this T element) where T : VisualElement
            => element.resolvedStyle.paddingLeft;
        public static T SetPaddingTop<T>(this T element, float value) where T : VisualElement
        {
            element.style.paddingTop = value;
            return element;
        }
        public static float GetPaddingTop<T>(this T element) where T : VisualElement
            => element.resolvedStyle.paddingTop;
        public static T SetPaddingRight<T>(this T element, float value) where T : VisualElement
        {
            element.style.paddingRight = value;
            return element;
        }
        public static float GetPaddingRight<T>(this T element) where T : VisualElement
            => element.resolvedStyle.paddingRight;
        public static T SetPaddingBottom<T>(this T element, float value) where T : VisualElement
        {
            element.style.paddingBottom = value;
            return element;
        }
        public static float GetPaddingBottom<T>(this T element) where T : VisualElement
            => element.resolvedStyle.paddingBottom;

        /// <summary> 
        /// Set all Paddings to 0 (zero) 
        /// </summary>
        public static T ClearPadding<T>(this T element) where T : VisualElement
            => element.SetPadding(0);

        /// <summary>
        /// Set the space reserved for the all the edges Paddings during the layout phase (Left, Top, Right, Bottom)
        /// </summary>
        public static T SetPadding<T>(this T element, float top, float right, float bottom, float left) where T : VisualElement
        {
            element.SetPaddingLeft(left).SetPaddingTop(top).SetPaddingRight(right).SetPaddingBottom(bottom);
            return element;
        }

        /// <summary>
        /// Set the same space reserved for the all the edges Paddings during the layout phase (Left, Top, Right, Bottom).
        /// </summary>
        public static T SetPadding<T>(this T element, float value) where T : VisualElement =>
            element.SetPadding(value, value, value, value);
        #endregion

        #endregion
        #region ✦ Style - Text

        #region ▶ Element Style - Font
        public static T SetFont<T>(this T target, FontAsset value) where T : VisualElement
        {
            target.style.unityFontDefinition = new FontDefinition { fontAsset = value };
            return target;
        }

        public static FontDefinition GetFont<T>(this T target) where T : VisualElement =>
            target.style.unityFontDefinition.value;

        #endregion
        #region ▶ Element Style - Font Style
        public static T SetFontStyle<T>(this T target, FontStyle style) where T : VisualElement
        {
            target.style.unityFontStyleAndWeight = style;
            return target;
        }

        public static FontStyle GetFontStyle<T>(this T target) where T : VisualElement =>
            target.style.unityFontStyleAndWeight.value;

        #endregion
        #region ▶ Element Style - Font Size
        public static T SetFontSize<T>(this T target, float value) where T : VisualElement
        {
            target.style.fontSize = value;
            return target;
        }

        public static float GetFontSize<T>(this T target) where T : VisualElement =>
            target.style.fontSize.value.value;
        #endregion
        #region ▶ Element Style - Color

        /// <summary>
        /// Set the color to use when drawing the text of an element
        /// </summary>
        public static T SetColor<T>(this T element, Color value) where T : VisualElement
        {
            element.style.color = value;
            return element;
        }
        public static T SetColor<T>(this T element, string hex) where T : VisualElement
            => element.SetColor(hex.ToColor());

        /// <summary>
        /// Get the color used when drawing the text of an element
        /// </summary>
        public static Color GetColor<T>(this T element) where T : VisualElement
            => element.resolvedStyle.color;

        #endregion
        #region ▶ Element Style - Text Align

        /// <summary>
        /// Set the horizontal and vertical text alignment in the element's box
        /// </summary>
        public static T SetTextAlign<T>(this T target, TextAnchor value) where T : VisualElement
        {
            target.style.unityTextAlign = value;
            return target;
        }

        /// <summary>
        /// Get the horizontal and vertical text alignment in the element's box
        /// </summary>
        public static TextAnchor GetTextAlign<T>(this T target) where T : VisualElement =>
            target.style.unityTextAlign.value;

        #endregion
        #region ▶ Element Style - Wrap (White Space)
        public static T SetWhiteSpace<T>(this T element, WhiteSpace value) where T : VisualElement
        {
            element.style.whiteSpace = value;
            return element;
        }

        public static WhiteSpace GetWhiteSpace<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.whiteSpace;
        }
        #endregion
        #region ▶ Element Style - Text Overflow
        public static T SetTextOverflow<T>(this T element, TextOverflow value) where T : VisualElement
        {
            element.style.textOverflow = value;
            return element;
        }

        public static TextOverflow GetTextOverflow<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.textOverflow;
        }
        #endregion
        #region ▶ Element Style - Outline Width
        public static T SetOutlineWidth<T>(this T element, float value) where T : VisualElement
        {
            element.style.unityTextOutlineWidth = value;
            return element;
        }

        public static float GetOutlineWidth<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unityTextOutlineWidth;
        }
        #endregion
        #region ▶ Element Style - Outline Color
        public static T SetOutlineColor<T>(this T element, Color value) where T : VisualElement
        {
            element.style.unityTextOutlineColor = value;
            return element;
        }

        public static Color GetOutlineColor<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unityTextOutlineColor;
        }
        #endregion
        #region ▶ Element Style - Shadow
        public static T SetTextShadow<T>(this T element, Color color, Vector2 offset, float blurRadius = 0) where T : VisualElement
        {
            TextShadow shadow = new();
            shadow.color = color;
            shadow.offset = offset;
            shadow.blurRadius = blurRadius;

            element.style.textShadow = shadow;
            return element;
        }
        #endregion
        #region ▶ Element Style - Letter Spacing
        public static T SetLetterSpacing<T>(this T element, float value) where T : VisualElement
        {
            element.style.letterSpacing = value;
            return element;
        }

        public static float GetLetterSpacing<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.letterSpacing;
        }
        #endregion
        #region ▶ Element Style - Word Spacing
        public static T SetWordSpacing<T>(this T element, float value) where T : VisualElement
        {
            element.style.wordSpacing = value;
            return element;
        }

        public static float GetWordSpacing<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.wordSpacing;
        }
        #endregion
        #region ▶ Element Style - Paragraph Spacing
        public static T SetParagraphSpacing<T>(this T element, float value) where T : VisualElement
        {
            element.style.unityParagraphSpacing = value;
            return element;
        }

        public static float GetParagraphSpacing<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unityParagraphSpacing;
        }
        #endregion

        #endregion
        #region ✦ Style - Background

        #region ▶ Element Style - Background Color
        public static T SetBackgroundColor<T>(this T element, string hex) where T : VisualElement
            => element.SetBackgroundColor(hex.ToColor());

        public static T SetBackgroundColor<T>(this T element, Color color) where T : VisualElement
        {
            element.style.backgroundColor = color;
            return element;
        }

        public static Color GetBackgroundColor<T>(this T element) where T : VisualElement
            => element.resolvedStyle.backgroundColor;
        #endregion
        #region ▶ Element Style - Background Image

        /// <summary> 
        /// Set the background image to paint in the element's box 
        /// </summary>
        public static T SetBackgroundImage<T>(this T target, Texture2D value) where T : VisualElement
        {
            target.style.backgroundImage = new StyleBackground(value);
            return target;
        }

        public static T SetBackgroundImage<T>(this T target, string path) where T : VisualElement
        {
            target.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>(path));
            return target;
        }

        /// <summary> 
        /// Get the background image Texture2D used to paint in the element's box 
        /// </summary>
        public static Texture2D GetBackgroundImage<T>(this T target) where T : VisualElement
            => target.style.backgroundImage.value.texture;

        #endregion
        #region ▶ Element Style - Background Image Tint Color
        public static T SetBackgroundImageTintColor<T>(this T element, Color color) where T : VisualElement
        {
            element.style.unityBackgroundImageTintColor = color;
            return element;
        }

        public static T SetBackgroundImageTintColor<T>(this T element, string color) where T : VisualElement
            => element.SetBackgroundImageTintColor(color.ToColor());

        public static Color GetBackgroundImageTintColor<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unityBackgroundImageTintColor;
        }
        #endregion
        #region ▶ Element Style - Unity Background Size
        public static T SetBackgroundRepeat<T>(this T element, Repeat repeatX = Repeat.NoRepeat, Repeat repeatY = Repeat.NoRepeat) where T : VisualElement
        {
            element.style.backgroundRepeat = new BackgroundRepeat(repeatX, repeatY);
            return element;
        }

        public static T SetBackgroundSize<T>(this T element, BackgroundSizeType style) where T : VisualElement
        {
            element.style.backgroundSize = new BackgroundSize(style);
            return element;
        }
        public static T SetBackgroundSize<T>(this T element, float width = 100, float height = 100, bool isPixel = false) where T : VisualElement
        {
            if (!isPixel) element.style.backgroundSize = new BackgroundSize(Length.Percent(width), Length.Percent(height));
            else element.style.backgroundSize = new BackgroundSize(width, height);
            return element;
        }

        public static T SetBackgroundPosition<T>(this T element, BackgroundPositionKeyword positionX = BackgroundPositionKeyword.Left, BackgroundPositionKeyword positionY = BackgroundPositionKeyword.Top) where T : VisualElement
        {
            element.style.backgroundPositionX = new BackgroundPosition(positionX);
            element.style.backgroundPositionY = new BackgroundPosition(positionY);
            return element;
        }
        #endregion
        #region ▶ Element Style - Slice
        public static T SetSliceLeft<T>(this T element, int value) where T : VisualElement
        {
            element.style.unitySliceLeft = value;
            return element;
        }
        public static T SetSliceTop<T>(this T element, int value) where T : VisualElement
        {
            element.style.unitySliceTop = value;
            return element;
        }
        public static T SetSliceRight<T>(this T element, int value) where T : VisualElement
        {
            element.style.unitySliceRight = value;
            return element;
        }
        public static T SetSliceBottom<T>(this T element, int value) where T : VisualElement
        {
            element.style.unitySliceBottom = value;
            return element;
        }

        public static T SetSliceScale<T>(this T element, float value) where T : VisualElement
        {
            element.style.unitySliceScale = value;
            return element;
        }

        public static int GetSliceLeft<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unitySliceLeft;
        }
        public static int GetSliceTop<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unitySliceTop;
        }
        public static int GetSliceRight<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unitySliceRight;
        }
        public static int GetSliceBottom<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.unitySliceBottom;
        }

        public static float GetSliceScale<T>(this T element) where T : VisualElement
            => element.resolvedStyle.unitySliceScale;
        #endregion

        #endregion
        #region ✦ Style - Border

        #region ▶ Element Style - Border Color
        public static T SetBorderColor<T>(this T element, string hex) where T : VisualElement
            => element.SetBorderColor(hex.ToColor());

        public static T SetBorderColor<T>(this T element, Color color) where T : VisualElement
        {
            element.style.borderTopColor = color;
            element.style.borderBottomColor = color;
            element.style.borderLeftColor = color;
            element.style.borderRightColor = color;
            return element;
        }
        #endregion
        #region ▶ Element Style - Border Width
        public static T SetBorderWidth<T>(this T element, float value) where T : VisualElement
        {
            element.style.borderTopWidth = value;
            element.style.borderBottomWidth = value;
            element.style.borderLeftWidth = value;
            element.style.borderRightWidth = value;
            return element;
        }

        public static float GetBorderWidth<T>(this T element) where T : VisualElement
        {
            return element.style.borderTopWidth.value; // Assuming all sides are the same
        }
        #endregion
        #region ▶ Element Style - Border Radius
        public static T SetBorderRadius<T>(this T element, float topLeft, float topRight, float bottomRight, float bottomLeft) where T : VisualElement
        {
            element.style.borderTopLeftRadius = topLeft;
            element.style.borderTopRightRadius = topRight;
            element.style.borderBottomRightRadius = bottomRight;
            element.style.borderBottomLeftRadius = bottomLeft;
            return element;
        }
        public static T SetBorderRadius<T>(this T target, float value) where T : VisualElement =>
            target.SetBorderRadius(value, value, value, value);

        public static T SetBorderTopLeftRadius<T>(this T target, float value) where T : VisualElement
        {
            target.style.borderTopLeftRadius = value;
            return target;
        }
        public static float GetBorderTopLeftRadius<T>(this T target) where T : VisualElement =>
            target.style.borderTopLeftRadius.value.value;

        public static T SetBorderTopRightRadius<T>(this T target, float value) where T : VisualElement
        {
            target.style.borderTopRightRadius = value;
            return target;
        }
        public static float GetBorderTopRightRadius<T>(this T target) where T : VisualElement =>
            target.style.borderTopRightRadius.value.value;

        public static T SetBorderBottomRightRadius<T>(this T target, float value) where T : VisualElement
        {
            target.style.borderBottomRightRadius = value;
            return target;
        }
        public static float GetBorderBottomRightRadius<T>(this T target) where T : VisualElement =>
            target.style.borderBottomRightRadius.value.value;

        public static T SetBorderBottomLeftRadius<T>(this T target, float value) where T : VisualElement
        {
            target.style.borderBottomLeftRadius = value;
            return target;
        }
        public static float GetBorderBottomLeftRadius<T>(this T target) where T : VisualElement =>
            target.style.borderBottomLeftRadius.value.value;

        #endregion

        #endregion
        #region ✦ Style - Transform

        #region ▶ Element Style - Transform Origin
        public static T SetOrigin<T>(this T element, float x, float y, bool isPercent = true) where T : VisualElement
        {
            TransformOrigin origin = new();
            origin.x = isPercent ? Length.Percent(x) : x;
            origin.y = isPercent ? Length.Percent(y) : y;

            element.style.transformOrigin = origin;
            return element;
        }
        #endregion
        #region ▶ Element Style - Translate
        public static T SetTranslate<T>(this T element, float x, float y, bool isPercent = true) where T : VisualElement
        {
            Translate translate = new();
            translate.x = isPercent ? Length.Percent(x) : x;
            translate.y = isPercent ? Length.Percent(y) : y;

            element.style.translate = translate;
            return element;
        }
        #endregion
        #region ▶ Element Style - Scale
        public static T SetScale<T>(this T element, float x, float y, bool isPercent = true) where T : VisualElement
        {
            Scale scale = new(new Vector2(x, y));

            element.style.scale = scale;
            return element;
        }
        public static Vector3 GetScale<T>(this T element) where T : VisualElement
        {
            return element.resolvedStyle.scale.value;
        }
        #endregion
        #region ▶ Element Style - Rotate
        public static T SetRotate<T>(this T element, float value, AngleUnit unit) where T : VisualElement
        {
            Angle angle = new(value, unit);
            Rotate rotate = new(angle);

            element.style.rotate = rotate;
            return element;
        }
        #endregion

        #endregion
        #region ✦ Style - Cursor

        #region ▶ Element Style - Cursor Image
        public static T SetCursorImage<T>(this T element, Texture2D texture) where T : VisualElement
        {
            UnityEngine.UIElements.Cursor cursor = new() { texture = texture };
            element.style.cursor = new StyleCursor(cursor);
            return element;
        }
        #endregion

        #endregion
        #region ✦ Style - Transition

        #region ▶ Element Style - Property & Duration

        public static T SetTransitionProperty<T>(this T element, params string[] properties) where T : VisualElement
        {
            List<StylePropertyName> propertyNames = new();
            foreach (var property in properties) propertyNames.Add(property);
            element.style.transitionProperty = propertyNames;
            return element;
        }

        public static T SetTransitionDuration<T>(this T element, params float[] values) where T : VisualElement
        {
            List<TimeValue> timeValues = new();
            foreach (var value in values) timeValues.Add(new TimeValue(value, TimeUnit.Second));
            element.style.transitionDuration = timeValues;
            return element;
        }

        #endregion

        #endregion
    }

    public static class EProperty
    {
        public static T SetAsReadOnly<T>(this T element, bool isReadOnly = true) where T : VisualElement
        {
            element.SetEnabled(!isReadOnly);
            return element;
        }

        public static T SetText<T>(this T element, string text) where T : TextField
        {
            element.value = text;
            return element;
        }

        public static T SetHyperlink<T>(this T element, string link) where T : Button
        {
            element.clicked += () => Application.OpenURL(link);
            return element;
        }

        public static T AddAction<T>(this T element, Action action) where T : Button
        {
            element.clicked += action;
            return element;
        }
    }

    public static class EElement
    {
        public static Label AddLabel(this VisualElement container, string text, string style = null)
        {
            Label label = new Label(text).AddClass(style);
            container.Add(label);
            return label;
        }
    }
}
