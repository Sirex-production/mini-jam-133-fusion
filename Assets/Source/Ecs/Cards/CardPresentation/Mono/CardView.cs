using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
	public sealed class CardView : MonoBehaviour
	{
		[BoxGroup("References (Interaction)")]
		[SerializeField, Required] private Rigidbody attachedRigidbody;
		[BoxGroup("References (Interaction)")]
		[SerializeField, Required] private Collider attachedCollider;
		
		[BoxGroup("References (Icon)")]
		[SerializeField, Required] private Image cardIconImage;
		[BoxGroup("References (Icon)")]
		[SerializeField, Required] private Image cardBackgroundImage;
		
		[BoxGroup("References (Description)")]
		[SerializeField, Required] private Image cardDescriptionBackgroundImage;
		[BoxGroup("References (Description)")]
		[SerializeField, Required] private TMP_Text cardDescriptionText;

		public Rigidbody AttachedRigidbody => attachedRigidbody;
	}
}