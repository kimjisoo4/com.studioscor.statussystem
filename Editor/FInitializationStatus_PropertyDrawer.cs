using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace StudioScor.StatusSystem.Editor
{

    [CustomPropertyDrawer(typeof(FInitializationStatus))]
    public class FInitializationStatus_PropertyDrawer : PropertyDrawer
    {
        public VisualTreeAsset _inspectorXML;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create a new VisualElement to be the root the property UI.
            var container = new VisualElement();

            _inspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{StatusSystemPathUtility.InspectorPath}statussystem-finitialization-status-custom-inspector.uxml");

            if (_inspectorXML)
            {
                var inspectorXML = _inspectorXML.Instantiate();

                var foldout = inspectorXML.Q<Foldout>("Foldout");
                var statusTagField = inspectorXML.Q<ObjectField>("ObjectField_StatusTag");
                var maxValueField = inspectorXML.Q<FloatField>("FloatField_MaxValue");
                var useRatio = inspectorXML.Q<Toggle>("Toggle_UseRatio");
                var currentSlider = inspectorXML.Q<Slider>("Slider_CurrentValue");

                UpdateValue(foldout, statusTagField, useRatio, maxValueField, currentSlider);

                statusTagField.RegisterValueChangedCallback((_) =>
                {
                    UpdateStatusTagField(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                    UpdateFoldoutText(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                });

                maxValueField.RegisterValueChangedCallback((maxValue) =>
                {
                    UpdateMaxValueField(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                    UpdateFoldoutText(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                });

                useRatio.RegisterValueChangedCallback((ratioEvn) =>
                {
                    UpdateValue(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                    UpdateFoldoutText(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                });

                currentSlider.RegisterValueChangedCallback((eventValue) =>
                {
                    UpdateFoldoutText(foldout, statusTagField, useRatio, maxValueField, currentSlider);
                });

                container.Add(inspectorXML);
            }

            return container;
        }

        private void UpdateStatusTagField(Foldout foldout, ObjectField statusTagField, Toggle useRatio, FloatField maxValueField, Slider currentSlider)
        {

        }
        private void UpdateMaxValueField(Foldout foldout, ObjectField statusTagField, Toggle useRatio, FloatField maxValueField, Slider currentSlider)
        {
            if (maxValueField.value < 0f)
                maxValueField.value = 0f;

            if (useRatio.value)
            {

            }
            else
            {
                currentSlider.highValue = maxValueField.value;
            }
        }
        private void UpdateValue(Foldout foldout, ObjectField statusTagField, Toggle useRatio, FloatField maxValueField, Slider currentSlider)
        {
            if (useRatio.value)
            {
                currentSlider.value = currentSlider.value / maxValueField.value * 100f;
                currentSlider.highValue = 100f;
            }
            else
            {
                currentSlider.highValue = maxValueField.value;
                currentSlider.value = currentSlider.value * maxValueField.value * 0.01f;
            }
        }

        private void UpdateFoldoutText(Foldout foldout, ObjectField statusTagField, Toggle useRatio, FloatField maxValueField, Slider currentSlider)
        {
            string tagName = statusTagField.value is StatusTag tag ? tag.name : "EMPTY";
            string values = $"{(useRatio.value ? $"{currentSlider.value * maxValueField.value * 0.01f} / {maxValueField.value}" : $"{currentSlider.value} / {currentSlider.highValue}")}";
            string percent = $"{(currentSlider.value / currentSlider.highValue) * 100:N2}%";

            foldout.text = $"{tagName} [ {values} ( {percent} ) ]";
        }
    }
}
