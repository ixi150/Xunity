using UnityEngine;
using Xunity.Behaviours;

namespace Xunity.LerpEffects
{
	public abstract class LerpEffect : GameBehaviour 
	{
		[SerializeField, Range(0, 1)] float lerp;

		public float Lerp
		{
			get { return lerp; }
			set
			{
				lerp = Mathf.Clamp01(value);
				Refresh();
			}
		}

		public abstract void Refresh();

		protected override void Awake()
		{
			base.Awake();
			Refresh();
		}

		void OnValidate()
		{
			Refresh();
		}
	}
}
