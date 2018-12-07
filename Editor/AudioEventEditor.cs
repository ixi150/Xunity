using UnityEditor;
using UnityEngine;
using Xunity.Audio;

namespace Xunity.Editor
{
	[CustomEditor(typeof(AudioEvent), true)]
	[CanEditMultipleObjects]
	public class AudioEventEditor : UnityEditor.Editor
	{
		[SerializeField] AudioSource _previewer;

		public void OnEnable()
		{
			_previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
		}

		public void OnDisable()
		{
			DestroyImmediate(_previewer.gameObject);
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
			if (GUILayout.Button("Preview"))
			{
				((AudioEvent) target).Play(_previewer);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}
