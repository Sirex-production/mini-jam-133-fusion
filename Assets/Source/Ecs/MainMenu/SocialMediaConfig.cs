using UnityEngine;

namespace Ingame
{
	[CreateAssetMenu(fileName = "SocialMediaConfig", menuName = "Ingame/SocialMediaConfig")]
	public sealed class SocialMediaConfig : ScriptableObject
	{
		[SerializeField] private string discordLink;
		[SerializeField] private string twitterLink;
		
		public string DiscordLink => discordLink;
		public string TwitterLink => twitterLink;
	}
}