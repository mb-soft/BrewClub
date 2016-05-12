using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace mbsoft.BrewClub.Localization.Xml
{
	[XmlRoot(ElementName = "language")]
	public class Language
	{

		private Dictionary<string, string> words;

		public Language()
		{
			words = new Dictionary<string, string>();
		}


		public string Code { get; set; }

		[XmlElement("word")]
		public Word[] Words
		{
			get
			{
				var output = new List<Word>();

				foreach (var kvp in words)
				{
					output.Add(new Word { Key = kvp.Key, Value = kvp.Value });
				}

				return output.ToArray();
			}
			set
			{
				words.Clear();
				foreach (var word in value)
				{
					words.Add(word.Key, word.Value);
				}
			}
		}

		public string GetWord(string key)
		{
			return words[key];
		}

		public bool ContainsKey(string key)
		{
			return words.ContainsKey(key);
		}

	}
}
