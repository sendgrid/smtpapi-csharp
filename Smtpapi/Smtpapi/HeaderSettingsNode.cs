using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SendGrid.SmtpApi
{
	/// <summary>
	/// </summary>
	internal class HeaderSettingsNode
	{
		#region Private Members

		private readonly Dictionary<string, HeaderSettingsNode> _branches;
		private IEnumerable<string> _array;
		private String _leaf;

		private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
		{
			StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
		};

		#endregion

		public HeaderSettingsNode()
		{
			_branches = new Dictionary<string, HeaderSettingsNode>();
		}

		public void AddArray(List<String> keys, IEnumerable<String> value)
		{
			if (keys.Count == 0)
			{
				_array = value;
			}
			else
			{
				if (_leaf != null || _array != null)
					throw new ArgumentException("Attempt to overwrite setting");

				var key = keys.First();
				if (!_branches.ContainsKey(key))
					_branches[key] = new HeaderSettingsNode();

				var remainingKeys = keys.Skip(1).ToList();
				_branches[key].AddArray(remainingKeys, value);
			}
		}

		public void AddSetting(List<String> keys, String value)
		{
			if (keys.Count == 0)
			{
				_leaf = value;
			}
			else
			{
				if (_leaf != null || _array != null)
					throw new ArgumentException("Attempt to overwrite setting");

				var key = keys.First();
				if (!_branches.ContainsKey(key))
					_branches[key] = new HeaderSettingsNode();

				var remainingKeys = keys.Skip(1).ToList();
				_branches[key].AddSetting(remainingKeys, value);
			}
		}

		public String GetSetting(params String[] keys)
		{
			return GetSetting(keys.ToList());
		}

		public String GetSetting(List<String> keys)
		{
			if (keys.Count == 0)
				return _leaf;
			var key = keys.First();
			if (!_branches.ContainsKey(key))
				throw new ArgumentException("Bad key path!");
			var remainingKeys = keys.Skip(1).ToList();
			return _branches[key].GetSetting(remainingKeys);
		}

		public IEnumerable<String> GetArray(params String[] keys)
		{
			return GetArray(keys.ToList());
		}

		public IEnumerable<String> GetArray(List<String> keys)
		{
			if (keys.Count == 0)
				return _array;
			var key = keys.First();
			if (!_branches.ContainsKey(key))
				throw new ArgumentException("Bad key path!");
			var remainingKeys = keys.Skip(1).ToList();
			return _branches[key].GetArray(remainingKeys);
		}

		public String GetLeaf()
		{
			return _leaf;
		}

		public String ToJson()
		{
			if (_branches.Count > 0)
				return JsonConvert.SerializeObject(_branches, _jsonSerializerSettings);

			if (_leaf != null)
				return JsonConvert.SerializeObject(_leaf, _jsonSerializerSettings);

			return _array != null
				? JsonConvert.SerializeObject(_array, _jsonSerializerSettings)
				: JsonConvert.SerializeObject(new object(), _jsonSerializerSettings);
		}

		public bool IsEmpty()
		{
			if (_leaf != null) return false;
			return _branches == null || _branches.Keys.Count == 0;
		}
	}
}