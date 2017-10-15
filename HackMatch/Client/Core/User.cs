using System.Collections.Generic;
using Android.Graphics;
using System.Runtime.Serialization;
using System.IO;
using System;

namespace HackMatch
{
	[Serializable]
    public class User : IConvertible
    {
		///	<summary>
		///	Property to get a unique username
		/// </summary>
		public string Username { get; set; }

        /// <summary>
        /// Property to get and set first names of user
        /// </summary>
        public string FirstNames { get; set; }

        /// <summary>
        /// Property to get and set last names of user
        /// </summary>
        public string LastNames { get; set; }

        /// <summary>
        /// Property to get and set the user biography
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Property to get and set technologies (including programming languages) and their corresponding experience level
        /// </summary>
        public Dictionary<string, ExperienceLevel> Technologies { get; set; }

        /// <summary>
        /// Property to get and set spoken languages
        /// </summary>
        public List<string> SpokenLanguages { get; set; }

        /// <summary>
        /// Property that stores bitmap stream of ProfilePicture
        /// </summary>
        public byte[] ProfilePictureData { get; set; }

        /// <summary>
        /// Get the currently set profile picture of the user
        /// <para>TODO: Find out the proper type of this property</para>
        /// </summary>
        public Bitmap GetProfilePicture() { return (Bitmap)BitmapFactory.FromArray<byte>(ProfilePictureData); }

		/// <summary>
		/// Set the profile picture of the user
		/// </summary>
		public void SetProfilePicture(Bitmap picture)
		{
			using (var stream = new MemoryStream())
			{
				picture.Compress(Bitmap.CompressFormat.Png, 0, stream);
				ProfilePictureData = stream.ToArray();
			}
		}

		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public string ToString(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}
    }
}