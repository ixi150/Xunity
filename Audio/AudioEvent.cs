using UnityEngine;

namespace Xunity.Audio
{
	public abstract class AudioEvent : ScriptableObject
	{
		public abstract void Play(AudioSource source);
	}
}