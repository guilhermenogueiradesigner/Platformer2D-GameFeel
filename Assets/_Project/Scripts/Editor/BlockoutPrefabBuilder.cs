using System.IO;
using JackODrain.Level;
using JackODrain.Player;
using JackODrain.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace JackODrain.EditorTools
{
    public static class BlockoutPrefabBuilder
    {
        private const string PrefabFolder = "Assets/_Project/Prefabs/Blockout";
        private const string SquareSpritePath = PrefabFolder + "/BlockoutSquare.png";
        private const int SquareSize = 16;
        private const int MeterIconCount = 5;
        private const string HazardLayerName = "Hazard";

        private static readonly Color LampColor = new(0.55f, 0.45f, 0.3f);
        private static readonly Color FlameColor = new(1f, 0.7f, 0.35f);
        private static readonly Color ScrollColor = new(0.85f, 0.8f, 0.6f);
        private static readonly Color AltarLightColor = new(0.75f, 0.85f, 1f);
        private static readonly Color SpikeColor = new(0.8f, 0.2f, 0.2f);
        private static readonly Color ToxicWaterColor = new(0.35f, 0.8f, 0.25f, 0.85f);
        private static readonly Color CandleColor = new(1f, 0.9f, 0.5f);
        private static readonly Color HeartColor = new(0.9f, 0.25f, 0.3f);

        [MenuItem("Tools/Jack O'Drain/Create Blockout Prefabs")]
        public static void CreateAll()
        {
            EnsureFolder(PrefabFolder);
            Sprite square = LoadOrCreateSquareSprite();

            SaveIfMissing(BuildLamp(square), "Lamp");
            SaveIfMissing(BuildDoubleJumpScroll(square), "DoubleJumpScroll");
            SaveIfMissing(BuildSpikes(square), "Spikes");
            SaveIfMissing(BuildToxicWater(square), "ToxicWater");
            SaveIfMissing(BuildCandle(square), "Candle");
            SaveIfMissing(BuildHud(square), "Hud");

            AssetDatabase.SaveAssets();
        }

        private static GameObject BuildLamp(Sprite square)
        {
            GameObject lamp = CreateBlock("Lamp", square, LampColor, new Vector2(0.5f, 1.25f));
            AddTrigger(lamp, new Vector2(1f, 2f));
            lamp.AddComponent<Checkpoint>();

            Light2D flame = AddPointLight(lamp, "Flame", FlameColor, 1.2f, 4f, 1.5f);
            CheckpointLamp lampComponent = lamp.AddComponent<CheckpointLamp>();
            SetReference(lampComponent, "_flame", flame);

            return lamp;
        }

        private static GameObject BuildDoubleJumpScroll(Sprite square)
        {
            GameObject scroll = CreateBlock("DoubleJumpScroll", square, ScrollColor, new Vector2(0.75f, 0.5f));
            AddTrigger(scroll, new Vector2(1f, 1f));

            AbilityScroll scrollComponent = scroll.AddComponent<AbilityScroll>();
            SetEnum(scrollComponent, "_ability", (int)Ability.DoubleJump);

            AddPointLight(scroll, "AltarLight", AltarLightColor, 1f, 3f, 0.5f);
            return scroll;
        }

        private static GameObject BuildSpikes(Sprite square)
        {
            GameObject spikes = CreateBlock("Spikes", square, SpikeColor, new Vector2(1f, 0.5f));
            AddTrigger(spikes, new Vector2(0.8f, 0.35f));
            spikes.AddComponent<Hazard>();
            AssignHazardLayer(spikes);
            return spikes;
        }

        private static GameObject BuildToxicWater(Sprite square)
        {
            GameObject water = CreateBlock("ToxicWater", square, ToxicWaterColor, new Vector2(4f, 1f));
            AddTrigger(water, new Vector2(4f, 0.8f));
            water.AddComponent<Hazard>();
            AssignHazardLayer(water);
            AddPointLight(water, "Glow", ToxicWaterColor, 0.5f, 3f, 0.5f);
            return water;
        }

        private static GameObject BuildCandle(Sprite square)
        {
            GameObject candle = CreateBlock("Candle", square, CandleColor, new Vector2(0.375f, 0.625f));
            AddTrigger(candle, new Vector2(0.8f, 0.8f));
            candle.AddComponent<Candle>();
            AddPointLight(candle, "CandleLight", CandleColor, 1f, 2.5f, 0.75f);
            return candle;
        }

        private static GameObject BuildHud(Sprite square)
        {
            var hud = new GameObject("Hud", typeof(RectTransform));

            Canvas canvas = hud.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = hud.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(480f, 270f);
            scaler.matchWidthOrHeight = 0.5f;

            GameObject hearts = CreateMeterRow(hud.transform, "Hearts", square, HeartColor, new Vector2(8f, -8f));
            hearts.AddComponent<HealthMeter>();

            GameObject candles = CreateMeterRow(hud.transform, "Candles", square, CandleColor, new Vector2(8f, -22f));
            candles.AddComponent<CandleMeter>();

            return hud;
        }

        private static GameObject CreateMeterRow(Transform parent, string name, Sprite square, Color color, Vector2 topLeftOffset)
        {
            var row = new GameObject(name, typeof(RectTransform));
            var rowTransform = row.GetComponent<RectTransform>();
            rowTransform.SetParent(parent, false);
            rowTransform.anchorMin = new Vector2(0f, 1f);
            rowTransform.anchorMax = new Vector2(0f, 1f);
            rowTransform.pivot = new Vector2(0f, 1f);
            rowTransform.anchoredPosition = topLeftOffset;
            rowTransform.sizeDelta = new Vector2(80f, 10f);

            HorizontalLayoutGroup layout = row.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 3f;
            layout.childControlWidth = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            var icons = new Image[MeterIconCount];

            for (int index = 0; index < MeterIconCount; index++)
                icons[index] = CreateIcon(rowTransform, square, index);

            IconMeter meter = row.AddComponent<IconMeter>();
            SetColor(meter, "_filledColor", color);
            SetReferenceArray(meter, "_icons", icons);

            return row;
        }

        private static Image CreateIcon(Transform parent, Sprite square, int index)
        {
            var icon = new GameObject($"Icon{index + 1}", typeof(RectTransform));
            var iconTransform = icon.GetComponent<RectTransform>();
            iconTransform.SetParent(parent, false);
            iconTransform.sizeDelta = new Vector2(10f, 10f);

            Image image = icon.AddComponent<Image>();
            image.sprite = square;
            return image;
        }

        private static GameObject CreateBlock(string name, Sprite square, Color color, Vector2 size)
        {
            var root = new GameObject(name);

            var visual = new GameObject("Visual");
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, size.y / 2f, 0f);
            visual.transform.localScale = new Vector3(size.x, size.y, 1f);

            SpriteRenderer renderer = visual.AddComponent<SpriteRenderer>();
            renderer.sprite = square;
            renderer.color = color;

            return root;
        }

        private static void AssignHazardLayer(GameObject hazard)
        {
            int layer = LayerMask.NameToLayer(HazardLayerName);

            if (layer < 0)
            {
                Debug.LogWarning($"Create a '{HazardLayerName}' layer so hazards can be told apart from safe ground.");
                return;
            }

            hazard.layer = layer;
        }

        private static void AddTrigger(GameObject target, Vector2 size)
        {
            BoxCollider2D trigger = target.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
            trigger.size = size;
            trigger.offset = new Vector2(0f, size.y / 2f);
        }

        private static Light2D AddPointLight(GameObject parent, string name, Color color, float intensity, float outerRadius, float height)
        {
            var lightObject = new GameObject(name);
            lightObject.transform.SetParent(parent.transform, false);
            lightObject.transform.localPosition = new Vector3(0f, height, 0f);

            Light2D light = lightObject.AddComponent<Light2D>();
            light.lightType = Light2D.LightType.Point;
            light.color = color;
            light.intensity = intensity;
            light.pointLightInnerRadius = 0f;
            light.pointLightOuterRadius = outerRadius;

            return light;
        }

        private static void SetReference(Object target, string propertyName, Object value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(propertyName).objectReferenceValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetReferenceArray(Object target, string propertyName, Object[] values)
        {
            var serialized = new SerializedObject(target);
            SerializedProperty array = serialized.FindProperty(propertyName);
            array.arraySize = values.Length;

            for (int index = 0; index < values.Length; index++)
                array.GetArrayElementAtIndex(index).objectReferenceValue = values[index];

            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetEnum(Object target, string propertyName, int value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(propertyName).enumValueIndex = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetColor(Object target, string propertyName, Color value)
        {
            var serialized = new SerializedObject(target);
            serialized.FindProperty(propertyName).colorValue = value;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SaveIfMissing(GameObject instance, string prefabName)
        {
            string path = $"{PrefabFolder}/{prefabName}.prefab";

            if (AssetDatabase.LoadAssetAtPath<GameObject>(path) == null)
                PrefabUtility.SaveAsPrefabAsset(instance, path);
            else
                Debug.Log($"Skipped {path}: it already exists.");

            Object.DestroyImmediate(instance);
        }

        private static Sprite LoadOrCreateSquareSprite()
        {
            if (!File.Exists(FullPath(SquareSpritePath)))
            {
                var texture = new Texture2D(SquareSize, SquareSize);
                var pixels = new Color32[SquareSize * SquareSize];

                for (int index = 0; index < pixels.Length; index++)
                    pixels[index] = Color.white;

                texture.SetPixels32(pixels);
                File.WriteAllBytes(FullPath(SquareSpritePath), texture.EncodeToPNG());
                Object.DestroyImmediate(texture);

                AssetDatabase.ImportAsset(SquareSpritePath);
                ConfigureAsPixelSprite(SquareSpritePath);
            }

            return AssetDatabase.LoadAssetAtPath<Sprite>(SquareSpritePath);
        }

        private static void ConfigureAsPixelSprite(string path)
        {
            var importer = (TextureImporter)AssetImporter.GetAtPath(path);
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = SquareSize;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SaveAndReimport();
        }

        private static void EnsureFolder(string folder)
        {
            if (AssetDatabase.IsValidFolder(folder))
                return;

            string parent = Path.GetDirectoryName(folder)?.Replace('\\', '/');
            EnsureFolder(parent);
            AssetDatabase.CreateFolder(parent, Path.GetFileName(folder));
        }

        private static string FullPath(string assetPath)
        {
            return Path.Combine(Directory.GetParent(Application.dataPath).FullName, assetPath);
        }
    }
}
