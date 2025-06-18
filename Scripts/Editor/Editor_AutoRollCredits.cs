using UnityEditor;
using PUCPR.AutoRollCredits;
using static PUCPR.AutoRollCredits.Editor.Editor_Statics;

namespace PUCPR.AutoRollCredits.Editor
{
    [CustomEditor(typeof(Credits))]
    public class Editor_AutoRollCredits : UnityEditor.Editor
    {
        Credits script;

        const string _refString = "References";

        bool _referencesBool;
        const string _referencesLabel = _refString;
        bool _contentBool;
        const string _contentLabel = "Conteudo";
        bool _maskBool;
        const string _maksLabel = "Esconder Mascara";

        public override void OnInspectorGUI()
        {
            script = (Credits)target;

            DrawReferences();
            DrawContent();

            serializedObject.ApplyModifiedProperties();
        }

        #region Draw Rules
        private void DrawReferences()
        {
            FoldoutContext(ref _referencesBool, _referencesLabel,
                () =>
                {
                    DrawGroup("Rects", () =>
                    {
                        DrawProperty(serializedObject, nameof(script._rectMask));
                        DrawProperty(serializedObject, nameof(script._content));
                    });

                    DrawGroup($"Image {_refString}", () =>
                    {
                        DrawProperty(serializedObject, nameof(script._headerImage));
                        DrawProperty(serializedObject, nameof(script._footerImage));
                    });

                    DrawGroup($"TMP {_refString}", () =>
                    {
                        DrawProperty(serializedObject, nameof(script._TMP_developers));
                        DrawProperty(serializedObject, nameof(script._TMP_special));
                        DrawProperty(serializedObject, nameof(script._TMP_finalTxt));
                    });
                });
        }

        private void DrawContent()
        {
            if (!CheckConditional(serializedObject, "_rectMask"))
            {
                WarningRef(nameof(script._rectMask));
                return;
            }

            DrawProperty(serializedObject, nameof(script._showMaskedContent));
            DrawProperty(serializedObject, nameof(script._speed));
            DrawProperty(serializedObject, nameof(script._waitForCallback));
            EditorGUILayout.Space(10);

            DrawGroup("Images", () =>
            {
                DrawProperty(serializedObject, nameof(script._spriteHeader), nameof(script._headerImage));
                DrawProperty(serializedObject, nameof(script._spriteHeaderPrimaryColor));
                if (!script._spriteHeaderPrimaryColor)
                {
                    DrawProperty(serializedObject, nameof(script._spriteHeaderColor));
                    Line();
                }
                DrawProperty(serializedObject, nameof(script._spriteFooter), nameof(script._footerImage));
            });

            DrawGroup($"Color {_refString}", () =>
            {
                DrawProperty(serializedObject, nameof(script._primaryColor));
                DrawProperty(serializedObject, nameof(script._secondaryColor));
                DrawProperty(serializedObject, nameof(script._defaultColor));
            });

            DrawGroup("Font Sizes", () =>
            {
                DrawProperty(serializedObject, nameof(script._titleSize));
                DrawProperty(serializedObject, nameof(script._subTitleSize));
                DrawProperty(serializedObject, nameof(script._defaultSize));
                DrawProperty(serializedObject, nameof(script._finalSize));
            });

            DrawGroup("Content", () =>
            {
                DrawProperty(serializedObject, nameof(script._Developers));
                DrawProperty(serializedObject, nameof(script._Special));
                DrawProperty(serializedObject, nameof(script._Final));
            });
        }

        #endregion
    }
}
